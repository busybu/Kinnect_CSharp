using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace SierraDB.Controllers;

using Model;
using Crypto;

[ApiController]
[Route("modulo")]
public class ModuloController : ControllerBase
{
    [HttpPost]
    public ActionResult CadastrarModulo([FromBody] Modulo modulo)
    {
        using KinnectContext context = new KinnectContext();

        if (modulo == null)
            return BadRequest("Modulo inválido");

        if (context.Modulos.Count(m => m.Nome == modulo.Nome) > 0)
            return BadRequest("Já existe um modulo com esse nome");

        var prof = context.Professor.FirstOrDefault(p => p.Id == modulo.Idprofessor);

        if (prof == null)
            return BadRequest("Professor inválido");

        context.Modulos.Add(modulo);
        context.SaveChanges();

        return Ok("Modulo cadastrado com sucesso!");
    }

    [HttpPost("buscarModulo")]
    public ActionResult buscarModulo([FromBody] Modulo modulo)
    {
        using KinnectContext context = new KinnectContext();

        var queryModulo = context.Modulos.FirstOrDefault(m => m.Nome == m.Nome);
        if (queryModulo == null)
            return BadRequest("Modulo não encontrado");

        return Ok("Modulo encontrado");

    }

    [HttpGet]
    public ActionResult GetModulos()
    {
        using KinnectContext context = new KinnectContext();

        var modulos = context.Modulos;

        if (modulos == null)
            return BadRequest("Não há modulos cadastrados");

        return Ok(modulos);
    }

    [HttpGet("getAlunos/{modulo}")]
    public ActionResult GetAlunos(string modulo)
    {
        using KinnectContext context = new KinnectContext();

        var queryModulo = context.Modulos.FirstOrDefault(m => m.Nome == modulo);
        if (queryModulo == null)
            return BadRequest("Modulo inválido");


        var query = context.Modulos
                    .Where(m => m.Nome == modulo)
                    .Join(context.Questoes,
                        idModulo => idModulo.Id,
                        q => q.Idmodulo,
                        (idModulo, q) => new
                        {
                            modulo = modulo,
                            idQ = q.Id,
                            // respQ = q.Resposta
                        })
                        .Join(context.Respostas,
                            obj => obj.idQ,
                            resp => resp.Idquestoes,
                            (obj, resp) => new
                            {
                                modulo = modulo,
                                
                            })

        return Ok(modulos);
    }

}