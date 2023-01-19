using Microsoft.AspNetCore.Mvc;

namespace SierraDB.Controllers;

[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
    [HttpGet]
    public ActionResult Test()
        => Ok("Sierra BD is Running...");

    [HttpPost]
    public ActionResult TestPost(string parameter)
        => Ok(parameter);
}