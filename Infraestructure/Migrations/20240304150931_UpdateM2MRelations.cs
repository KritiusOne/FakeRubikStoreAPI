using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateM2MRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Reviews",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Direccion_IdDireccion",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Direccion_UserDirectionNavId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_UserDirectionNavId",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsProviders",
                table: "ProductsProviders");

            migrationBuilder.DropIndex(
                name: "IX_Products_IdReview",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorias_Productos",
                table: "Categorias_Productos");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_Productos_IdProducto",
                table: "Categorias_Productos");

            migrationBuilder.DropColumn(
                name: "UserDirectionNavId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "IdReview",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "Reviews",
                newName: "rate");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Reviews",
                newName: "Descripcion");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProductsProviders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categorias_Productos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsProviders",
                table: "ProductsProviders",
                columns: new[] { "IdProduct", "IdProvider" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorias_Productos",
                table: "Categorias_Productos",
                columns: new[] { "IdProducto", "IdCategoria" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Productos",
                table: "Reviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Direccion",
                table: "Usuarios",
                column: "IdDireccion",
                principalTable: "Direccion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Productos",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Direccion",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsProviders",
                table: "ProductsProviders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorias_Productos",
                table: "Categorias_Productos");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "rate",
                table: "Reviews",
                newName: "Rate");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Reviews",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "UserDirectionNavId",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProductsProviders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "IdReview",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Categorias_Productos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsProviders",
                table: "ProductsProviders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorias_Productos",
                table: "Categorias_Productos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_UserDirectionNavId",
                table: "Usuarios",
                column: "UserDirectionNavId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IdReview",
                table: "Products",
                column: "IdReview");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Productos_IdProducto",
                table: "Categorias_Productos",
                column: "IdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Reviews",
                table: "Products",
                column: "IdReview",
                principalTable: "Reviews",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Direccion_IdDireccion",
                table: "Usuarios",
                column: "IdDireccion",
                principalTable: "Direccion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Direccion_UserDirectionNavId",
                table: "Usuarios",
                column: "UserDirectionNavId",
                principalTable: "Direccion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
