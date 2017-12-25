using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CSCI3110Project2.Models.DbContexts;

namespace CSCI3110Project2.Migrations
{
    [DbContext(typeof(PersonProjectRoleDbContext))]
    [Migration("20171106142959_Dakota")]
    partial class Dakota
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CSCI3110Project2.Models.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("CSCI3110Project2.Models.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("CSCI3110Project2.Models.Entities.ProjectRole", b =>
                {
                    b.Property<int>("PersonId");

                    b.Property<int>("ProjectId");

                    b.HasKey("PersonId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectRoles");
                });

            modelBuilder.Entity("CSCI3110Project2.Models.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(450);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int?>("ProjectRolePersonId");

                    b.Property<int?>("ProjectRoleProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectRolePersonId", "ProjectRoleProjectId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CSCI3110Project2.Models.Entities.ProjectRole", b =>
                {
                    b.HasOne("CSCI3110Project2.Models.Entities.Person", "Person")
                        .WithMany("Roles")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CSCI3110Project2.Models.Entities.Project", "Project")
                        .WithMany("Roles")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CSCI3110Project2.Models.Entities.Role", b =>
                {
                    b.HasOne("CSCI3110Project2.Models.Entities.ProjectRole")
                        .WithMany("Roles")
                        .HasForeignKey("ProjectRolePersonId", "ProjectRoleProjectId");
                });
        }
    }
}
