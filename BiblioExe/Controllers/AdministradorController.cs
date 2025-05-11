using BiblioExe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BiblioExe.Utilerias;
using System.Text;
using System.Net.Mime;
using System.Net.Mail;

namespace BiblioExe.Controllers
{
    [Authorize(Roles = "3")]
    public class AdministradorController : Controller
    {
        private readonly ContextoBD _context;
        private readonly IWebHostEnvironment _environment;

        public AdministradorController(ContextoBD context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Estadisticas()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NuevoAdmin()
        {
            return View();
        }
        public IActionResult Reportes()
        {
            var Prestamos = _context.Prestamo.ToList();
            ViewBag.Libro = _context.Libro.ToList();
            return View(Prestamos);
        }
        public IActionResult Alumnos(string? Boleta)
        {
            var Alumnos = _context.Alumno.ToList();
            ViewBag.DatosAlumno = _context.DatosAlumno.ToList();
            return View(Alumnos);
        }
        public IActionResult CatalogoAdmin()
        {
            var Libros = _context.Libro.ToList();
            return View(Libros);
        }
        public IActionResult Editar(int id)
        {
            var Libro = _context.Libro.FirstOrDefault(x => x.IDLibro == id);
            var LibroCant = _context.LibroExistencia.FirstOrDefault(x => x.IDLibro == id);
            if (Libro == null)
                return NotFound();
            ViewBag.Nombre = Libro.Nombre.ToString();
            ViewBag.Autor = Libro.Autor.ToString();
            ViewBag.Editorial = Libro.Editorial.ToString();
            ViewBag.Imagen = Libro.Imagen.ToString();
            ViewBag.Existencia = LibroCant.Existencia.ToString();
            return View();
        }
        [HttpPost]
        public IActionResult EditarLibro(int id, string Nombre, string Autor, string Editorial, int Existencia, IFormFile Imagen)
        {
            var Libro = _context.Libro.FirstOrDefault(x => x.IDLibro == id);
            var LibroExistencia = _context.LibroExistencia.FirstOrDefault(x => x.IDLibro == id);

            if (LibroExistencia == null)
            return NotFound();
            Libro.Nombre = Nombre.ToString();
            Libro.Autor = Autor.ToString();
            Libro.Editorial = Editorial.ToString();
            LibroExistencia.Existencia = Existencia;
            _context.Libro.Update(Libro);
            _context.LibroExistencia.Update(LibroExistencia);

            _context.SaveChanges();

            return Redirect("/Administrador/Editar?id="+id);
        }
        public IActionResult AñadirUsuario(string Nombre, string ApellidoP, string ApellidoM, string Correo, string Contraseña, Administrador admin)
        {
            admin.Nombre = Nombre.ToString();
            admin.Ap = ApellidoP.ToString();
            admin.Am = ApellidoM.ToString();
            admin.Correo = Correo.ToString();
            admin.Contraseña = Utilerias.Encriptar.HashString(Contraseña);
            admin.IDTipoUsuario = "3";

            _context.Administrador.Add(admin);
            _context.SaveChanges();

            return RedirectToAction("NuevoAdmin");
        }
        public IActionResult NuevoLibro()
        {
            var Categoria = _context.Categoria.ToList();
            return View(Categoria);
        }
        public IActionResult AñadirLibro(string Nombre, string Autor, string Editorial, string Sinopsis, int Cantidad, int Categoria, IFormFile Imagen, Libro Libro, LibroExistencia libroExistencia)
        {
            if (Imagen == null || Imagen.Length == 0)
                return RedirectToAction("CatalogoAdmin", new { errorImagen = true });

            var extension = Imagen.FileName.Split('.');
            var nombre = Guid.NewGuid().ToString() + "." + extension[extension.Length - 1];
            var path = Path.Combine(_environment.WebRootPath, "Images/Catalogo", nombre);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                Imagen.CopyToAsync(stream);

                Libro.Nombre = Nombre.ToString();
                Libro.Autor = Autor.ToString();
                Libro.Editorial = Editorial.ToString();
                Libro.Sinopsis = Sinopsis.ToString();
                libroExistencia.Existencia = Cantidad;
                Libro.IDCategoria = Categoria;
                Libro.Imagen = nombre;

                _context.Add(Libro);
                _context.Add(libroExistencia);
                _context.SaveChangesAsync();
            }

            return RedirectToAction("CatalogoAdmin");
        }
        public IActionResult BorrarLibro(int LibroSeleccionado)
        {
            var libro = _context.Libro.Find(LibroSeleccionado);
            var libroexistencia = _context.LibroExistencia.Find(LibroSeleccionado);
            var busquedaSolicitud = _context.SolicitudPrestamo.FirstOrDefault(x => x.IDLibro == LibroSeleccionado);
            var busquedaPrestamo = _context.Prestamo.FirstOrDefault(x => x.IDLibro == LibroSeleccionado);
            if (busquedaSolicitud != null)
                _context.SolicitudPrestamo.Remove(busquedaSolicitud);
            if(busquedaPrestamo != null)
                _context.Prestamo.Remove(busquedaPrestamo);

            _context.Libro.Remove(libro);
            _context.LibroExistencia.Remove(libroexistencia);
            _context.SaveChanges();
            return RedirectToAction("CatalogoAdmin");
        }
        public IActionResult Solicitudes()
        {
            var Solicitudes = _context.SolicitudPrestamo.ToList();
            ViewBag.Libro = _context.Libro.ToList();
            return View(Solicitudes);
        }
        public IActionResult Prestamos()
        {
            var Prestamos = _context.Prestamo.ToList();
            ViewBag.Libro = _context.Libro.ToList();
            return View(Prestamos);
        }
        public IActionResult AceptarSolicitud(int IDLibro, Prestamo prestamo)
        {
            string Folio = GenerarCadenaAleatoria(10);
            var busqueda = _context.SolicitudPrestamo.FirstOrDefault(x => x.IDLibro == IDLibro);
            var busquedaDatos = _context.DatosAlumno.Find(busqueda.Boleta);
            var busquedaAlumno = _context.Alumno.Find(busqueda.Boleta);
            var busquedaLibro = _context.Libro.Find(busqueda.IDLibro);
            var busquedaMateria = _context.Materias.Find(busqueda.IDLibro);
            var busquedaProfesor = _context.Profesores.Find(busqueda.IDLibro);
            var correo = busquedaDatos.Email.ToString();
            var NombreAlumno = busquedaAlumno.Nombre + " " + busquedaAlumno.AP + " " + busquedaAlumno.AM;
            var nombreLibro = busquedaLibro.Nombre;
            var nombreMateria = busquedaMateria.Nombre;
            var nombreProfesor = busquedaProfesor.Nombre + " " + busquedaProfesor.ApellidoM + " " + busquedaProfesor.ApellidoM;
            prestamo.Folio = Folio;
            prestamo.IDLibro = IDLibro;
            prestamo.Boleta = busqueda.Boleta;
            prestamo.Grupo = busqueda.Grupo;
            prestamo.Materia = busqueda.Materia;
            prestamo.Maestro = busqueda.Maestro;
            prestamo.Fecha_inicio = busqueda.Fecha_inicio;
            prestamo.Fecha_final = busqueda.Fecha_final;
            prestamo.Cantidad = busqueda.Cantidad;
            prestamo.IDEstado = 1;

            MailMessage mail = new MailMessage();

            string html = "<!DOCTYPE > <html> <head> </head> <body> <div style=\"background-color: #EC6A6A; text-align: center;\"> <img src=\"https://i.pinimg.com/736x/fa/79/68/fa7968c5963c6e861a2fef4470e766d8.jpg\" style=\"border-style: solid; border-color: black;\" width=\"150\" height=\"150\"> <h2 style=\"color:black\">EL REGISTRO DE TU SOLICITUD DE PRESTAMO FUE EXITOSO</h2> <h4 style=\"color:black; font-size: 20;\">EL FOLIO DE TU SOLICTUD ES:<br> <strong style=\"color:white; font-size: 20;\">"+Folio+"</strong></h4> <h4 style=\"color:black; font-size: 20;\">LA FECHA DE TU ENTREGA ES: <br> <strong style=\"color:white; font-size: 20;\">"+busqueda.Fecha_inicio+"</strong></h4> <h4 style=\"color:black; font-size: 20;\">NOMBRE DEL ALUMNO: <br> <strong style=\"color:white; font-size: 20;\">"+NombreAlumno+"</strong></h4> <h4 style=\"color:black; font-size: 20;\">GRUPO: <br> <strong style=\"color:white; font-size: 20;\">"+busquedaAlumno.Grupo+"</strong></h4> <h4 style=\"color:black; font-size: 20;\">NOMBRE DEL LIBRO: <br> <strong style=\"color:white; font-size: 20;\">"+nombreLibro+"</strong></h4> <h4 style=\"color:black; font-size: 20;\">AUTOR:  <br> <strong style=\"color:white; font-size: 20;\">"+busquedaLibro.Autor+"</strong></h4> <h4 style=\"color:black; font-size: 20;\">EDITORIAL: <br> <strong style=\"color:white; font-size: 20;\">"+busquedaLibro.Editorial+"</strong></h4>  </div> </body> </html>";

            Utilerias.Correo.EnviarCorreo(correo,
                "Comprobante de Prestamo",
               html);


            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
            mail.AlternateViews.Add(htmlView);

            _context.Prestamo.Add(prestamo);
            _context.SolicitudPrestamo.Remove(busqueda);
            _context.SaveChanges();
            return RedirectToAction("Prestamos");
        }
        public IActionResult FinalizarPrestamo(int IDPrestamo, DateTime FechaInicio, string[] Folio, int IDLibro, string FolioSeleccionado)
        {
            var busquedaPrestamo = _context.Prestamo.FirstOrDefault(x => x.Folio == FolioSeleccionado);
            var busqedaExistencia = _context.LibroExistencia.Find(IDLibro);
            busquedaPrestamo.IDEstado = 2;
            busquedaPrestamo.FechaEntrega = DateTime.Now;
            busqedaExistencia.Existencia += busquedaPrestamo.Cantidad;

            _context.Prestamo.Update(busquedaPrestamo);
            _context.LibroExistencia.Update(busqedaExistencia);
            _context.SaveChanges();
            return RedirectToAction("Prestamos");
        }
        public IActionResult ValidarRolAlumno(string BoletaSeleccionada, int IDTipoUsuario, DatosAlumno datosAlumno)
        {
            var busqueda = _context.DatosAlumno.Find(BoletaSeleccionada);
            busqueda.IDTipoUsuario = IDTipoUsuario.ToString();
            _context.Update(busqueda);
            _context.SaveChanges();
            return RedirectToAction("Alumnos");
        }
        private string GenerarCadenaAleatoria(int longitud)
        {
            Random random = new Random();
            char[] digitos = new char[longitud];
            for (int i = 0; i < longitud; i++)
            {
                digitos[i] = random.Next(0, 10).ToString()[0];
            }
            return new string(digitos);
        }
    }
}
