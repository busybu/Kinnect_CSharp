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

    [HttpGet("buscarModulo")]
    public ActionResult buscarModulo()
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
                        idM => idM.Id,
                        q => q.Idmodulo,
                        (idM, q) => new
                        {
                            modulo = modulo,
                            idQ = q.Id
                        })
                    .Join(context.Respostas,
                        obj => obj.idQ,
                        r => r.Idquestoes,
                        (obj, r) => new
                        {
                            modulo = modulo,
                            idQ = r.Idquestoes,
                            idA = r.Idaluno
                        })
                    .Join(context.Alunos,
                        obj => obj.idA,
                        a => a.Id,
                        (obj, a) => new
                        {
                            modulo = modulo,
                            nomeAluno = a.Nome
                        })
                        .GroupBy(a => a.nomeAluno)
                        .Select( a => new {
                            nome = a.Key,
                            modulo = modulo
                        })
                    .ToArray();

        return Ok(query);
    }

}