using Microsoft.AspNetCore.Mvc;
using BiblioExe.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;

namespace BiblioExe.Controllers
{
    [Authorize]
    public class AlumnoController : Controller
    {
        private readonly ContextoBD _context;

        public AlumnoController(ContextoBD context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "1,2")]
        public IActionResult Panel()
        {
            string boleta = User.Claims.FirstOrDefault(x => x.Type == "Boleta").Value;
            var busqueda = _context.Alumno.Find(boleta);
            var busquedaCarrera = _context.Carrera.Find(busqueda.IDCarrera);
            ViewBag.Boleta = boleta;
            ViewBag.Nombre = busqueda.Nombre + " " + busqueda.AP + " " + busqueda.AM;
            ViewBag.Email = User.Claims.FirstOrDefault(x => x.Type == "Email").Value;
            ViewBag.Contraseña = User.Claims.FirstOrDefault(x => x.Type == "Contraseña").Value.ToString();
            ViewBag.Semestre = busqueda.Semestre;
            ViewBag.Carrera = busquedaCarrera.DescCarrera;
            ViewBag.Grupo = busqueda.Grupo;
            return View();
        }
        [Authorize(Roles = "2")]
        public IActionResult Prestamo(int id)
        {
            var busqueda = _context.Libro.Find(id);
            var existencia = _context.LibroExistencia.FirstOrDefault(x => x.IDLibro == id);
            var Semestre = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Semestre").Value);
            var Carrera = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Carrera").Value);
            var Materias = _context.Materias.Where(x => (x.Semestre == Semestre && x.Carrera == Carrera) || (x.Semestre == Semestre && x.Carrera == 7)).ToList();
            var Profesores = _context.Profesores.Where(x => (x.Semestre == Semestre && x.Carrera == Carrera) || (x.Semestre == Semestre && x.Carrera == 7)).ToList();
            ViewBag.Materias = Materias;
            ViewBag.Profesores = Profesores;
            ViewBag.Nombre = busqueda.Nombre;
            ViewBag.Autor = busqueda.Autor;
            ViewBag.Editorial = busqueda.Editorial;
            ViewBag.existencia = existencia.Existencia;
            

            return View();
        }
        public IActionResult SolicitarPrestamo(int IDLibro, int Materia, int Profesor, DateTime FechaInicio, DateTime FechaFinal, int Cantidad, SolicitudPrestamo Solicitud)
        {
            var NombreMateria = _context.Materias.Find(Materia);
            var NombreProfesor = _context.Profesores.Find(Profesor);
            var Existencia = _context.LibroExistencia.Find(IDLibro);

            if (Materia == 0 || Profesor == 0)
                return RedirectToAction("Catalogo", "Usuarios", new { error = true });
            
            Solicitud.IDLibro = IDLibro;
            Solicitud.Boleta = User.Claims.FirstOrDefault(x => x.Type == "Boleta").Value;
            Solicitud.Grupo = User.Claims.FirstOrDefault(x => x.Type == "Grupo").Value;
            Solicitud.Materia = NombreMateria.Nombre.ToString();
            Solicitud.Maestro = NombreProfesor.ApellidoP.ToString() + " " + NombreProfesor.ApellidoM.ToString() + " " + NombreProfesor.Nombre.ToString();
            Solicitud.Fecha_inicio = FechaInicio;
            Solicitud.Fecha_final = FechaFinal;
            Solicitud.Cantidad = Cantidad;
            Existencia.Existencia -= Cantidad;

            _context.SolicitudPrestamo.Update(Solicitud);
            _context.LibroExistencia.Update(Existencia);
            _context.SaveChanges();

            return RedirectToAction("Solicitudes");
        }
        [Authorize(Roles = "2")]
        public IActionResult Solicitudes()
        {
            var boleta = User.Claims.FirstOrDefault(x => x.Type == "Boleta").Value;
            var DatosPrestamo = _context.SolicitudPrestamo.Where(x => x.Boleta == boleta).ToList();
            ViewBag.Prestamos = _context.Prestamo.Where(x => x.Boleta == boleta).ToList();
            ViewBag.Libro = _context.Libro.ToList(); 
            return View(DatosPrestamo);
        }
        [Authorize(Roles = "1")]
        public IActionResult SolicitudesAlumno()
        {
            var Grupo = User.Claims.FirstOrDefault(x => x.Type == "Grupo").Value;
            var DatosPrestamo = _context.SolicitudPrestamo.Where(x => x.Grupo == Grupo).ToList();
            ViewBag.Prestamos = _context.Prestamo.Where(x => x.Grupo == Grupo).ToList();
            ViewBag.Libro = _context.Libro.ToList();
            return View(DatosPrestamo);
        }
    }
}
