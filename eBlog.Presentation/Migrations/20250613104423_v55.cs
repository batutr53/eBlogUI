using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eBlog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v55 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostModule_Posts_PostId",
                table: "PostModule");

            migrationBuilder.DropForeignKey(
                name: "FK_PostModuleTags_PostModule_PostModulesId",
                table: "PostModuleTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostModule",
                table: "PostModule");

            migrationBuilder.RenameTable(
                name: "PostModule",
                newName: "PostModules");

            migrationBuilder.RenameIndex(
                name: "IX_PostModule_PostId",
                table: "PostModules",
                newName: "IX_PostModules_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostModules",
                table: "PostModules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostModules_Posts_PostId",
                table: "PostModules",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostModuleTags_PostModules_PostModulesId",
                table: "PostModuleTags",
                column: "PostModulesId",
                principalTable: "PostModules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostModules_Posts_PostId",
                table: "PostModules");

            migrationBuilder.DropForeignKey(
                name: "FK_PostModuleTags_PostModules_PostModulesId",
                table: "PostModuleTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostModules",
                table: "PostModules");

            migrationBuilder.RenameTable(
                name: "PostModules",
                newName: "PostModule");

            migrationBuilder.RenameIndex(
                name: "IX_PostModules_PostId",
                table: "PostModule",
                newName: "IX_PostModule_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostModule",
                table: "PostModule",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostModule_Posts_PostId",
                table: "PostModule",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostModuleTags_PostModule_PostModulesId",
                table: "PostModuleTags",
                column: "PostModulesId",
                principalTable: "PostModule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
