// <auto-generated />
using System;
using GISA.Associado.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GISA.Associado.Migrations
{
    [DbContext(typeof(AssociadoContext))]
    partial class AssociadoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AssociadoMarcacaoExame", b =>
                {
                    b.Property<int>("AssociadosId")
                        .HasColumnType("int");

                    b.Property<int>("MarcacaoExamesId")
                        .HasColumnType("int");

                    b.HasKey("AssociadosId", "MarcacaoExamesId");

                    b.HasIndex("MarcacaoExamesId");

                    b.ToTable("AssociadoMarcacaoExame");
                });

            modelBuilder.Entity("GISA.Associado.Entities.Associado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoAssociado")
                        .HasColumnType("int");

                    b.Property<string>("CpfCnpj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EnderecoId")
                        .HasColumnType("int");

                    b.Property<string>("Formacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlanoId")
                        .HasColumnType("int");

                    b.Property<bool>("PossuiPlanoOdontologico")
                        .HasColumnType("bit");

                    b.Property<int>("SituacaoAssociado")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ValorPlano")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("PlanoId");

                    b.ToTable("Associados");
                });

            modelBuilder.Entity("GISA.Associado.Entities.Endereco", b =>
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

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("GISA.Associado.Entities.MarcacaoExame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoExame")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataExame")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MarcacaoExames");
                });

            modelBuilder.Entity("GISA.Associado.Entities.Plano", b =>
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

                    b.Property<decimal>("ValorBase")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Planos");
                });

            modelBuilder.Entity("AssociadoMarcacaoExame", b =>
                {
                    b.HasOne("GISA.Associado.Entities.Associado", null)
                        .WithMany()
                        .HasForeignKey("AssociadosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GISA.Associado.Entities.MarcacaoExame", null)
                        .WithMany()
                        .HasForeignKey("MarcacaoExamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GISA.Associado.Entities.Associado", b =>
                {
                    b.HasOne("GISA.Associado.Entities.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GISA.Associado.Entities.Plano", "Plano")
                        .WithMany()
                        .HasForeignKey("PlanoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");

                    b.Navigation("Plano");
                });
#pragma warning restore 612, 618
        }
    }
}
