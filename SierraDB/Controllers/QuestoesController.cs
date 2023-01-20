using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace SierraDB.Controllers;

using Model;
using Crypto;
using Parse;

[ApiController]
[Route("questoes")]
public class QuestoesController : ControllerBase
{
    [HttpPost]
    public ActionResult CadastrarQuestao([FromBody] Questo questao)
    {
        using KinnectContext context = new KinnectContext();

        if(questao == null)
            return BadRequest("Inválido");

        var resposta = Parse.CalculateString(questao.Descricao);
        questao.Resposta = resposta.ToString();

        context.Questoes.Add(questao);
        context.SaveChanges();

        return Ok("Questão cadastrada com sucesso!");
    }

    [HttpPost("procurarQuestao")]
    public ActionResult ProcurarQuestao([FromBody] Questo questao)
    {
        using KinnectContext context = new KinnectContext();

        var queryQuestao = context.Questoes.FirstOrDefault(q => q.Descricao == q.Descricao);
        if(queryQuestao == null)
            return BadRequest("Questão já cadastrada!");

        return Ok("Questão encontrada!");

    }
}