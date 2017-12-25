using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSCI3110Project2.Migrations
{
    public partial class Dakota2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_ProjectRoles_ProjectRolePersonId_ProjectRoleProjectId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_ProjectRolePersonId_ProjectRoleProjectId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ProjectRolePersonId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ProjectRoleProjectId",
                table: "Roles");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "ProjectRoles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoles_RoleId",
                table: "ProjectRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoles_Roles_RoleId",
                table: "ProjectRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoles_Roles_RoleId",
                table: "ProjectRoles");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRoles_RoleId",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "ProjectRoles");

            migrationBuilder.AddColumn<int>(
                name: "ProjectRolePersonId",
                table: "Roles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectRoleProjectId",
                table: "Roles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ProjectRolePersonId_ProjectRoleProjectId",
                table: "Roles",
                columns: new[] { "ProjectRolePersonId", "ProjectRoleProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_ProjectRoles_ProjectRolePersonId_ProjectRoleProjectId",
                table: "Roles",
                columns: new[] { "ProjectRolePersonId", "ProjectRoleProjectId" },
                principalTable: "ProjectRoles",
                principalColumns: new[] { "PersonId", "ProjectId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
