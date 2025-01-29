using Microsoft.AspNetCore.Mvc;
using bulgarita.Services;

namespace bulgarita.Controllers;

[ApiController]
[Route("[controller]")]

public class Roller : ControllerBase
{
    [HttpPost("RolVer/{Kullanıcı_Kimliği}/{Rol}")]
    public IActionResult RolVer(string Kullanıcı_Kimliği, string Rol)
    {
        Models.Roller roller = new Models.Roller(Kullanıcı_Kimliği, Rol);

        if(RollerFonksiyonları.RolVer(roller))
        {
            return new StatusCodeResult(201); //Created
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }

    [HttpDelete("RolAl/{Kullanıcı_Kimliği}/{Rol}")]
    public IActionResult RolAl(string Kullanıcı_Kimliği, string Rol)
    {
        Models.Roller roller = new Models.Roller(Kullanıcı_Kimliği, Rol);

        if(RollerFonksiyonları.RolAl(roller))
        {
            return new StatusCodeResult(200); //OK
        }
        else
        {
            return new StatusCodeResult(422); //Unprocessable Content
        }
    }
}