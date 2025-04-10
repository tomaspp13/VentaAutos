using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class CuentaController : Controller
{
    public IActionResult LoginDueño()
    {
        return View();
    }

    [HttpPost]
    public IActionResult LoginDueño(string email, string password)
    {
        if (email.Equals("tomas@gmail.com") && password.Equals( "12345"))
        {
            return RedirectToAction("Index", "Autos");
        }

        ViewBag.Error = "Email o contraseña incorrectos.";
        return View();
    }
}


