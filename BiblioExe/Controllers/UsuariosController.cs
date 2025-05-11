using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using BiblioExe.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using BiblioExe.Utilerias;

namespace BiblioExe.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly ContextoBD _context;
        public UsuariosController(ContextoBD context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string? Error)
        {
            ViewBag.Error = Error;
            return View();
        }
        
        [AllowAnonymous]
        public IActionResult Registro(string? SinBoleta, string? Registrado, string? CorreoOcupado)
        {
            ViewBag.SinBoleta = SinBoleta;
            ViewBag.Registrado = Registrado;
            ViewBag.CorreoOcupado = CorreoOcupado;
            return View();
        }

        [AllowAnonymous]
        public IActionResult Catalogo(string? error)
        {
            if (error != null)
                ViewBag.Error = true;
            var Libros = _context.Libro.ToList();
            return View(Libros);
        }

        [Authorize(Roles = "1,2,3")]
        public IActionResult Libro(int id)
        {
            var busqueda = _context.Libro.FirstOrDefault(x => x.IDLibro == id);
            if(busqueda == null)
                return NotFound();
            ViewBag.Imagen = busqueda.Imagen;
            ViewBag.Nombre = busqueda.Nombre;
            ViewBag.Autor = busqueda.Autor;
            ViewBag.Editorial = busqueda.Editorial;
            ViewBag.Sinopsis = busqueda.Sinopsis;
            ViewBag.Id = id;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RegistrarAlumno(string Boleta, string Correo, string Contraseña, int IDUsuario, DatosAlumno DatosAlumno)
        {
            var busquedaRegistro = _context.DatosAlumno.FirstOrDefault(x => x.Boleta == Boleta.ToString());
            var busquedaCorreo = _context.DatosAlumno.FirstOrDefault(x => x.Email == Correo.ToString());
            var busquedaBoleta = _context.Alumno.FirstOrDefault(x => x.Boleta == Boleta.ToString());

            if(busquedaBoleta == null)
                return RedirectToAction("Registro", new { SinBoleta = true });

            if (busquedaRegistro != null)
                return RedirectToAction("Registro", new { Registrado = true });

            if (busquedaCorreo != null)
                return RedirectToAction("Registro", new { CorreoOcupado = true });

            DatosAlumno.Boleta = Boleta.ToString();
            DatosAlumno.Email = Correo.ToString();
            DatosAlumno.Contraseña = Utilerias.Encriptar.HashString(Contraseña);
            DatosAlumno.IDTipoUsuario = "1";

            _context.DatosAlumno.Add(DatosAlumno);
            _context.SaveChanges();
            Utilerias.Correo.EnviarCorreo(Correo.ToString(), "Registro BiblioExe", "Tu registro se realizo con exito ahora gozas de libre acceso a la pagina :)");

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string Usuario, string Contraseña)
        {
            var busquedaAlumno = _context.DatosAlumno.FirstOrDefault(x => x.Email == Usuario);
            var busquedaAdmin = _context.Administrador.FirstOrDefault(x => x.Correo == Usuario);

            if (busquedaAlumno != null)
            {
                bool pass = Encriptar.VerifyHash(Contraseña, busquedaAlumno.Contraseña);
                if (pass == true)
                {
                    var busqueda = _context.Alumno.Find(busquedaAlumno.Boleta);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Role, busquedaAlumno.IDTipoUsuario.ToString()),
                        new Claim("Email", busquedaAlumno.Email.ToString()),
                        new Claim("Boleta", busquedaAlumno.Boleta.ToString()),
                        new Claim("Contraseña", busquedaAlumno.Contraseña.ToString()),
                        new Claim("Grupo", busqueda.Grupo.ToString()),
                        new Claim("Semestre", busqueda.Semestre.ToString()),
                        new Claim("Carrera", busqueda.IDCarrera.ToString())
                    };

                    var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);

                    return RedirectToAction("Panel", "Alumno");
                }
                else
                    return RedirectToAction("Login", new { error = true });
            }
            else if(busquedaAdmin != null)
            {
                bool pass = Encriptar.VerifyHash(Contraseña, busquedaAdmin.Contraseña);
                if (pass == true)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("Email", busquedaAdmin.Correo.ToString()),
                        new Claim(ClaimTypes.Role, busquedaAdmin.IDTipoUsuario),
                        new Claim("Nombre", busquedaAdmin.Nombre.ToString() + " " + busquedaAdmin.Ap.ToString() + " " + busquedaAdmin.Am.ToString()),
                        new Claim("Pass", busquedaAdmin.Contraseña.ToString()),
                    };

                    var userIdentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);

                    return RedirectToAction("CatalogoAdmin", "Administrador");
                }
                else
                    return RedirectToAction("Login", new { error = true });
            }
            else
                return RedirectToAction("Login", new { error = true });
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
