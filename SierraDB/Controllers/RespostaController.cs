using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace SierraDB.Controllers;

using Model;
using Crypto;

[ApiController]
[Route("resposta")]
public class RespostaController : ControllerBase
{
    [HttpPost]
    public ActionResult CadastrarResposta([FromBody] Resposta resposta)
    {
        using KinnectContext context = new KinnectContext();

        if(resposta == null)
            return BadRequest("Inválido");
    
        context.Respostas.Add(resposta);
        context.SaveChanges();

        return Ok("Resposta cadastrada com sucesso!");
    }

    [HttpPost("procurarResposta")]
    public ActionResult ProcurarResposta([FromBody] Resposta resposta)
    {
        using KinnectContext context = new KinnectContext();

        var queryQuestao = context.Questoes.FirstOrDefault(q => q.Id == resposta.Idquestoes);
        
        if (queryQuestao.Resposta != resposta.Resposta1)
            return BadRequest("Resposta incorreta");

        return Ok("Resposta Correta");

    }
}