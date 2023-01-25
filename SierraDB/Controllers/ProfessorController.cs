using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace SierraDB.Controllers;

using Model;
using Crypto;

[ApiController]
[Route("professor")]
public class ProfessorController : ControllerBase
{
    [HttpPost]
    public ActionResult CadastrarProfessor([FromBody] Professor professor)
    {
        using KinnectContext context = new KinnectContext();

        if (professor == null)
            return BadRequest("Usuário inválido");

        if (professor.Nome.Length < 5)
            return BadRequest("Nome do professor é muito curto");

        if (context.Professor.Count(a => a.Nome == professor.Nome) > 0)
            return BadRequest("Já existe um professor com esse nome");


        professor.Senha = Crypto.Password(professor.Senha);
        context.Professor.Add(professor);
        context.SaveChanges();

        return Ok("Usuário cadastrado com sucesso!");
    }

    [HttpPost("login")]
    public ActionResult LoginProfessor([FromBody] Professor professor)
    {
        using KinnectContext context = new KinnectContext();

        var queryProfessor = context.Professor.FirstOrDefault(a => a.Nome == professor.Nome);
        if (queryProfessor == null)
            return BadRequest("Usuário inválido");

        var cryptoSenha = Crypto.Password(professor.Senha);

        if (queryProfessor.Senha != cryptoSenha)
            return BadRequest("Senha inválida");

        return Ok("Login válido");

    }
    [HttpGet("{professor}")]
    public ActionResult GetModulos(string professor)
    {
        using KinnectContext context = new KinnectContext();

        var queryProfessor = context.Professor.FirstOrDefault(p => p.Nome == professor);
        if (queryProfessor == null)
            return BadRequest("Usuário inválido");

        // var query = context.Professor
        //             .Where(p => p.Nome == professor)
        //             .Join(context.Modulos,
        //                 p => p.Id,
        //                 m => m.Idprofessor,
        //                 (p, m) => new
        //                 {
        //                     modulo = m.Nome,
        //                     professor = p.Nome
        //                 })
        //             .GroupBy(p => p.modulo)
        //             .Select(m => new {
        //                 modulo = m.Key,
        //                 professor = professor
        //             })
        //         .ToArray();
        var query =
        context.Professor
            .Where(p => p.Nome == professor)
            .Join(context.Modulos,
                p => p.Id,
                m => m.Idprofessor,
                (p, m) => m.Nome)
            .ToList();

        if(query.Count() < 1)
            return NotFound();

        return Ok(new
        { 
            professor = professor,
            modulos = query,
        });

    }
}