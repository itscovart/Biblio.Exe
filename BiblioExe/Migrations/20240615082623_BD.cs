using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioExe.Migrations
{
    public partial class BD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carrera",
                columns: table => new
                {
                    IDCarrera = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescCarrera = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrera", x => x.IDCarrera);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    IDCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescCategoria = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.IDCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    IDEstado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoNombre = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.IDEstado);
                });

            migrationBuilder.CreateTable(
                name: "LibroExistencia",
                columns: table => new
                {
                    IDLibro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Existencia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibroExistencia", x => x.IDLibro);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    IDMateria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Semestre = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    Carrera = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.IDMateria);
                });

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    IDProfesor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ApellidoP = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ApellidoM = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Semestre = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    Carrera = table.Column<int>(type: "int", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.IDProfesor);
                });

            migrationBuilder.CreateTable(
                name: "TipoUsuario",
                columns: table => new
                {
                    IDTipoUsuario = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    DescTipo = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuario", x => x.IDTipoUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Alumno",
                columns: table => new
                {
                    Boleta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Grupo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Semestre = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    IDCarrera = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumno", x => x.Boleta);
                    table.ForeignKey(
                        name: "FK_Alumno_Carrera_IDCarrera",
                        column: x => x.IDCarrera,
                        principalTable: "Carrera",
                        principalColumn: "IDCarrera",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Libro",
                columns: table => new
                {
                    IDLibro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Editorial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sinopsis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDCategoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libro", x => x.IDLibro);
                    table.ForeignKey(
                        name: "FK_Libro_Categoria_IDCategoria",
                        column: x => x.IDCategoria,
                        principalTable: "Categoria",
                        principalColumn: "IDCategoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    IDAdmin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Am = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IDTipoUsuario = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.IDAdmin);
                    table.ForeignKey(
                        name: "FK_Administrador_TipoUsuario_IDTipoUsuario",
                        column: x => x.IDTipoUsuario,
                        principalTable: "TipoUsuario",
                        principalColumn: "IDTipoUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatosAlumno",
                columns: table => new
                {
                    Boleta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IDTipoUsuario = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    TokenRestauracion = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaCaducidadToken = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosAlumno", x => x.Boleta);
                    table.ForeignKey(
                        name: "FK_DatosAlumno_Alumno_Boleta",
                        column: x => x.Boleta,
                        principalTable: "Alumno",
                        principalColumn: "Boleta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatosAlumno_TipoUsuario_IDTipoUsuario",
                        column: x => x.IDTipoUsuario,
                        principalTable: "TipoUsuario",
                        principalColumn: "IDTipoUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prestamo",
                columns: table => new
                {
                    Folio = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDLibro = table.Column<int>(type: "int", nullable: false),
                    Boleta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Grupo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Materia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Maestro = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Fecha_inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_final = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    IDEstado = table.Column<int>(type: "int", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamo", x => x.Folio);
                    table.ForeignKey(
                        name: "FK_Prestamo_Alumno_Boleta",
                        column: x => x.Boleta,
                        principalTable: "Alumno",
                        principalColumn: "Boleta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prestamo_Estado_IDEstado",
                        column: x => x.IDEstado,
                        principalTable: "Estado",
                        principalColumn: "IDEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prestamo_Libro_IDLibro",
                        column: x => x.IDLibro,
                        principalTable: "Libro",
                        principalColumn: "IDLibro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudPrestamo",
                columns: table => new
                {
                    IDSolicitud = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDLibro = table.Column<int>(type: "int", nullable: false),
                    Boleta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Grupo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Materia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Maestro = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Fecha_inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_final = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudPrestamo", x => x.IDSolicitud);
                    table.ForeignKey(
                        name: "FK_SolicitudPrestamo_Alumno_Boleta",
                        column: x => x.Boleta,
                        principalTable: "Alumno",
                        principalColumn: "Boleta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudPrestamo_Libro_IDLibro",
                        column: x => x.IDLibro,
                        principalTable: "Libro",
                        principalColumn: "IDLibro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrador_IDTipoUsuario",
                table: "Administrador",
                column: "IDTipoUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Alumno_IDCarrera",
                table: "Alumno",
                column: "IDCarrera");

            migrationBuilder.CreateIndex(
                name: "IX_DatosAlumno_IDTipoUsuario",
                table: "DatosAlumno",
                column: "IDTipoUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Libro_IDCategoria",
                table: "Libro",
                column: "IDCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_Boleta",
                table: "Prestamo",
                column: "Boleta");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_IDEstado",
                table: "Prestamo",
                column: "IDEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamo_IDLibro",
                table: "Prestamo",
                column: "IDLibro");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudPrestamo_Boleta",
                table: "SolicitudPrestamo",
                column: "Boleta");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudPrestamo_IDLibro",
                table: "SolicitudPrestamo",
                column: "IDLibro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "DatosAlumno");

            migrationBuilder.DropTable(
                name: "LibroExistencia");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Prestamo");

            migrationBuilder.DropTable(
                name: "Profesores");

            migrationBuilder.DropTable(
                name: "SolicitudPrestamo");

            migrationBuilder.DropTable(
                name: "TipoUsuario");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Alumno");

            migrationBuilder.DropTable(
                name: "Libro");

            migrationBuilder.DropTable(
                name: "Carrera");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
