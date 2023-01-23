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
    public ActionResult GetNota(int aluno)
    {
        using KinnectContext context = new KinnectContext();

        var queryAluno = context.Alunos.FirstOrDefault(a => a.Id == aluno);
        if (queryAluno == null)
            return BadRequest("Usuário inválido");

        var query = context.Alunos
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
                .Where(obj => obj.respostaQ == obj.respostaA)
                .GroupBy(a => a.aluno)
                // new { a.TextData, a.DataBaseName }
                .Select(n => new
                {
                    aluno = n.Key,
                    nota = n.Sum(p => p.peso)
                })
            .ToArray();

        return Ok(query);
    }

    [HttpGet("aluno")]
    public ActionResult GetAlunos()
    {
        using KinnectContext context = new KinnectContext();
         
        var alunos = context.Alunos;

        if (alunos == null)
            return BadRequest("Não há alunos cadastrados");

        return Ok(alunos);
    }

    [HttpGet("modulo")]
    public ActionResult GetModulos()
    {
        using KinnectContext context = new KinnectContext();
         
        var modulos = context.Modulos;

        if (modulos == null)
            return BadRequest("Não há modulos cadastrados");

        return Ok(modulos);
    }

    [HttpGet("professor")]
    public ActionResult GetProfessores()
    {
        using KinnectContext context = new KinnectContext();
         
        var professores = context.Professor;

        if (professores == null)
            return BadRequest("Não há professores cadastrados");

        return Ok(professores);
    }
}
    [HttpGet("questoes")]
    public ActionResult GetQuestoes()
    {
        using KinnectContext context = new KinnectContext();
         
        var questoes = context.Questoes;

        if (questoes == null)
            return BadRequest("Não há questões cadastradas");

        return Ok(questoes);
    }