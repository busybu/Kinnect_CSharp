using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace SierraDB.Controllers;

using Model;
using Crypto;

[ApiController]
[Route("aluno")]
public class AlunoController : ControllerBase
{
    [HttpPost]
    public ActionResult CadastrarAluno([FromBody] Aluno aluno)
    {
        using KinnectContext context = new KinnectContext();

        if(aluno == null)
            return BadRequest("Usuário inválido");

        if (aluno.Nome.Length < 5)
            return BadRequest("Nome do aluno é muito curto");

        if (context.Alunos.Count(a => a.Nome == aluno.Nome) > 0)
            return BadRequest("Já existe um aluno com esse nome");

        if (aluno.Senha.Length < 8)
            return BadRequest("Senha muito curta");


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
        if(queryAluno == null)
            return BadRequest("Usuário inválido");

        var cryptoSenha = Crypto.Password(aluno.Senha);

        if (queryAluno.Senha != cryptoSenha)
            return BadRequest("Senha inválida");
    
        return Ok("Login válido");

    }
}