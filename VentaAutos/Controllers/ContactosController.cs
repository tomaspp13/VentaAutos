using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace VentaAutos.Controllers
{
    public class ContactosController : Controller
    {
        public IActionResult Contacto()
        {
            ViewData["ActiveController"] = "Contactos";
            ViewData["ActiveAction"] = "Contacto";
            return View();
        }

        [HttpPost]
        public IActionResult Enviar(string nombre, string email, string mensaje)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(mensaje))
            {
                ViewBag.Mensaje = "Todos los campos son obligatorios.";
                return View("Contacto");
            }

            EnviarCorreo(nombre, email);

            ViewBag.Mensaje = "¡Gracias por tu mensaje, te responderemos pronto!";
            return View("Contacto");
        
        }
        private void EnviarCorreo(string nombre, string email)
        {
            // Configurar el cliente SMTP con Brevo
            var smtpClient = new SmtpClient("smtp-relay.brevo.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("86c3ad002@smtp-brevo.com", "tJSDfKWZBGIVpxc5"), // Tus credenciales de Brevo
                EnableSsl = true,
            };

            // Crear el mensaje de correo
            var mensaje = new MailMessage
            {
                From = new MailAddress("tomassilvanunez@gmail.com"), // Usa un correo verificado en Brevo
                Subject = "Gracias por contactarnos",
                Body = $"Hola {nombre}, hemos recibido tu correo correctamente.",
                IsBodyHtml = false,
            };

            mensaje.To.Add(email);

                smtpClient.Send(mensaje);

                Console.WriteLine("Correo enviado correctamente.");
        }
    }
}


