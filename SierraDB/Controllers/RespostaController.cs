using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace SierraDB.Controllers;

using Model;
using SierraDB.DTO;

[ApiController]
[Route("resposta")]
public class RespostaController : ControllerBase
{
    [HttpPost]
    public ActionResult CadastrarResposta([FromBody] Resposta resposta)
    {
        using KinnectContext context = new KinnectContext();

        if (resposta == null)
            return BadRequest("Inválido");

        context.Respostas.Add(resposta);
        context.SaveChanges();

        return Ok("Resposta cadastrada com sucesso!");
    }

    [HttpPost("procurarResposta")]
    public ActionResult ProcurarResposta([FromBody] AlunoQuestao alunoQuestao)
    {
        using KinnectContext context = new KinnectContext();

        var queryAluno = context.Alunos.FirstOrDefault(a => a.Id == alunoQuestao.Aluno);
        var queryQuestao = context.Questoes.FirstOrDefault(q => q.Id == alunoQuestao.Questao);
        
        if(queryAluno == null || queryQuestao == null)
            return BadRequest("Aluno ou questão inexistente");

        var resposta = context.Respostas.FirstOrDefault(r => r.Idaluno == alunoQuestao.Aluno && r.Idquestoes == alunoQuestao.Questao);
        var resposta1 = resposta.Resposta1;

        if (queryQuestao.Resposta != resposta1)
            return Ok("Resposta Incorreta");

        return Ok("Resposta Correta");

    }
}