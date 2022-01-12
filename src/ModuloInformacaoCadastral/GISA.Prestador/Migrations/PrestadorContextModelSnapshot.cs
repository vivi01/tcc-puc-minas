﻿// <auto-generated />
using System;
using GISA.Prestador.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GISA.Prestador.Migrations
{
    [DbContext(typeof(PrestadorContext))]
    partial class PrestadorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GISA.Prestador.Entities.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CEP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rua")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("GISA.Prestador.Entities.Plano", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClassificacaoPlano")
                        .HasColumnType("int");

                    b.Property<int>("CodigoPlano")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoPlano")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Plano");
                });

            modelBuilder.Entity("GISA.Prestador.Entities.Prestador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Categoria")
                        .HasColumnType("int");

                    b.Property<int>("CodigoPrestador")
                        .HasColumnType("int");

                    b.Property<string>("CpfCnpj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EnderecoId")
                        .HasColumnType("int");

                    b.Property<string>("Formacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlanoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Prestador");
                });

            modelBuilder.Entity("PlanoPrestador", b =>
                {
                    b.Property<int>("PlanosId")
                        .HasColumnType("int");

                    b.Property<int>("PrestadoresId")
                        .HasColumnType("int");

                    b.HasKey("PlanosId", "PrestadoresId");

                    b.HasIndex("PrestadoresId");

                    b.ToTable("PlanoPrestador");
                });

            modelBuilder.Entity("GISA.Prestador.Entities.Prestador", b =>
                {
                    b.HasOne("GISA.Prestador.Entities.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId");

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("PlanoPrestador", b =>
                {
                    b.HasOne("GISA.Prestador.Entities.Plano", null)
                        .WithMany()
                        .HasForeignKey("PlanosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GISA.Prestador.Entities.Prestador", null)
                        .WithMany()
                        .HasForeignKey("PrestadoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
