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

        if(professor == null)
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
        if(queryProfessor == null)
            return BadRequest("Usuário inválido");

        var cryptoSenha = Crypto.Password(professor.Senha);

        if (queryProfessor.Senha != cryptoSenha)
            return BadRequest("Senha inválida");
    
        return Ok("Login válido");

    }
}