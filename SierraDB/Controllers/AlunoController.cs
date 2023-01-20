using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace SierraDB.Controllers;

using Model;
using Crypto;
using SierraDB.DTO;

[ApiController]
[Route("aluno")]
public class AlunoController : ControllerBase
{
    [HttpPost]
    public ActionResult CadastrarAluno([FromBody] Aluno aluno)
    {
        using KinnectContext context = new KinnectContext();

        if (aluno == null)
            return BadRequest("Usuário inválido");

        if (aluno.Nome.Length < 5)
            return BadRequest("Nome do aluno é muito curto");

        if (context.Alunos.Count(a => a.Nome == aluno.Nome) > 0)
            return BadRequest("Já existe um aluno com esse nome");

        aluno.Senha = Crypto.Password(aluno.Senha);
        context.Alunos.Add(aluno);
        context.SaveChanges();

        return Ok("Usuário cadastrado com sucesso!");
    }

    [HttpPost("login")]
    public ActionResult LoginAluno([FromBody] Aluno aluno)
    {
        using KinnectContext context = new KinnectContext();

        var queryAluno = context.Alunos.FirstOrDefault(a => a.Nome == aluno.Nome);
        if (queryAluno == null)
            return BadRequest("Usuário inválido");

        var cryptoSenha = Crypto.Password(aluno.Senha);

        if (queryAluno.Senha != cryptoSenha)
            return BadRequest("Senha inválida");

        return Ok("Login válido");

    }
    [HttpPost("notaAluno")]
    public ActionResult GetNota([FromBody] AlunoNota aluno)
    {
        using KinnectContext context = new KinnectContext();

        var queryAluno = context.Alunos.FirstOrDefault(a => a.Id == aluno.Aluno);

        var queryModulo = context.Questoes.Join(context.Modulos,
                                            questao => questao.Idmodulo,
                                            modulo => modulo.Id,
                                            (questao, modulo) => new
                                            {
                                                idQ = questao.Id,
                                                pesoQ = questao.Peso,
                                                nomeModulo = modulo.Nome
                                            });

        var queryResposta = queryModulo.Join(context.Respostas,
                                                questao => questao.idQ,
                                                resposta => resposta.Idquestoes,
                                                (questao, resposta) => new
                                                {
                                                    pesoQ = questao.pesoQ,
                                                    resposta = resposta.Resposta1,
                                                    idAluno = resposta.Idaluno,
                                                    nmModulo = questao.nomeModulo
                                                });

        var queryNota = queryResposta.Join(context.Alunos,
                                        alunoR => alunoR.idAluno,
                                        aluno => aluno.Id,
                                        (alunoR, aluno) => new
                                        {
                                            nomeAluno = aluno.Nome,
                                            notaQ = alunoR.pesoQ,
                                            nomeModulo = alunoR.nmModulo
                                        })
                                        .GroupBy(modulo => modulo.nomeModulo)
                                        .Select(m => new
                                        {
                                            nomeModulo = m.Key
                                        });



        return Ok();
    }
}