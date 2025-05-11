using Microsoft.EntityFrameworkCore;

namespace BiblioExe.Models
{
    public class ContextoBD : DbContext
    {
        public ContextoBD(DbContextOptions<ContextoBD> opt) : base(opt) { }
        public DbSet<Administrador> Administrador { get; set; }
        public DbSet<Alumno> Alumno { get; set; }
        public DbSet<Carrera> Carrera { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<DatosAlumno> DatosAlumno { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Libro> Libro { get; set; }
        public DbSet<LibroExistencia> LibroExistencia { get; set; }
        public DbSet<Materias> Materias { get; set; }
        public DbSet<Prestamo> Prestamo { get; set; }
        public DbSet<Profesores> Profesores { get; set; }
        public DbSet<SolicitudPrestamo> SolicitudPrestamo { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }

    }
}
