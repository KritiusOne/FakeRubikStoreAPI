using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Direccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Direccion = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: true),
                    ciudad = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: true),
                    estado = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: true),
                    pais = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: true),
                    descripcion = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direccion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreProducto = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Precio = table.Column<double>(type: "double precision", nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    Imagen = table.Column<string>(type: "text", unicode: false, nullable: true),
                    Descripcion = table.Column<string>(type: "text", unicode: false, nullable: false),
                    Miniatura = table.Column<string>(type: "text", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Categorias_Productos",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "integer", nullable: false),
                    IdProducto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias_Productos", x => new { x.IdProducto, x.IdCategoria });
                    table.ForeignKey(
                        name: "FK_Categorias_Productos_Categorias",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categorias_Productos_Productos",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsProviders",
                columns: table => new
                {
                    IdProvider = table.Column<int>(type: "integer", nullable: false),
                    IdProduct = table.Column<int>(type: "integer", nullable: false),
                    IdProductosNavigationId = table.Column<int>(type: "integer", nullable: false),
                    IdProveedoresNavigationId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsProviders", x => new { x.IdProduct, x.IdProvider });
                    table.ForeignKey(
                        name: "FK_ProductsProviders_Productos_IdProductosNavigationId",
                        column: x => x.IdProductosNavigationId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsProviders_Providers_IdProveedoresNavigationId",
                        column: x => x.IdProveedoresNavigationId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdRol = table.Column<int>(type: "integer", nullable: false),
                    IdDireccion = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Apellido = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Contrasena = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Direccion",
                        column: x => x.IdDireccion,
                        principalTable: "Direccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_Rol",
                        column: x => x.IdRol,
                        principalTable: "Rol",
                        principalColumn: "IdRol");
                });

            migrationBuilder.CreateTable(
                name: "Envios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEstado = table.Column<int>(type: "integer", nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Envios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Envios_Estado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Envios_Usuarios",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    rate = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => new { x.IdProducto, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_Review_Productos",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Usuarios",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    IdEnvio = table.Column<int>(type: "integer", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp", nullable: false),
                    PrecioFinal = table.Column<double>(type: "double precision", nullable: false),
                    numerotarjeta = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordenes_Envios",
                        column: x => x.IdEnvio,
                        principalTable: "Envios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordenes_Usuarios",
                        column: x => x.IdUser,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Productos_Ordenes",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "integer", nullable: false),
                    IdOrden = table.Column<int>(type: "integer", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    Precio = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos_Ordenes", x => new { x.IdProducto, x.IdOrden });
                    table.ForeignKey(
                        name: "FK_Productos_Ordenes_Ordenes",
                        column: x => x.IdOrden,
                        principalTable: "Ordenes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Productos_Ordenes_Productos",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Productos_IdCategoria",
                table: "Categorias_Productos",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_IdEstado",
                table: "Envios",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_IdUsuario",
                table: "Envios",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_IdEnvio",
                table: "Ordenes",
                column: "IdEnvio");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_IdUser",
                table: "Ordenes",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Ordenes_IdOrden",
                table: "Productos_Ordenes",
                column: "IdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsProviders_IdProductosNavigationId",
                table: "ProductsProviders",
                column: "IdProductosNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsProviders_IdProveedoresNavigationId",
                table: "ProductsProviders",
                column: "IdProveedoresNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UsuarioId",
                table: "Reviews",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdDireccion",
                table: "Usuarios",
                column: "IdDireccion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRol",
                table: "Usuarios",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categorias_Productos");

            migrationBuilder.DropTable(
                name: "Productos_Ordenes");

            migrationBuilder.DropTable(
                name: "ProductsProviders");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Envios");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Direccion");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
