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

    [HttpGet("notaAluno/{aluno}")]
    public ActionResult GetNota(string aluno)
    {
        using KinnectContext context = new KinnectContext();

        var queryAluno = context.Alunos.FirstOrDefault(a => a.Nome == aluno);
        if (queryAluno == null)
            return BadRequest("Usuário inválido");

        var query = context.Alunos
            .Where(a => a.Nome == aluno)
            .Join(context.Respostas,
                aluno => aluno.Id,
                resp => resp.Idaluno,
                (aluno, resp) => new
                {
                    idQ = resp.Idquestoes,
                    respostaA = resp.Resposta1,
                    nome = aluno.Nome,
                })
            .Join(context.Questoes,
                obj => obj.idQ,
                quest => quest.Id,
                (obj, quest) => new
                {
                    idM = quest.Idmodulo,
                    respostaQ = quest.Resposta,
                    respostaA = obj.respostaA,
                    nome = obj.nome,
                    peso = quest.Peso,
                    idQ = obj.idQ
                })
            .Join(context.Modulos,
                obj => obj.idM,
                modulo => modulo.Id,
                (obj, modulo) => new
                {
                    modulo = modulo.Nome,
                    respostaA = obj.respostaA,
                    respostaQ = obj.respostaQ,
                    aluno = obj.nome,
                    peso = obj.peso,
                    idQ = obj.idQ
                })
            .ToArray()
            .Where(x => x.respostaA.Replace(',', '.') == x.respostaQ.Replace(',', '.'))
            .GroupBy(x => x.modulo)
            .Select(g => new
            {
                modulo = g.Key,
                aluno = g.First().aluno,
                nota = g.Sum(p => p.peso)
            });


        return Ok(query);
    }

}