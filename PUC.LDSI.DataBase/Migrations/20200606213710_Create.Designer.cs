﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PUC.LDSI.DataBase;

namespace PUC.LDSI.DataBase.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200606213710_Create")]
    partial class Create
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.Aluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("TurmaId");

                    b.HasKey("Id");

                    b.HasIndex("TurmaId");

                    b.ToTable("Aluno");
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.Avaliacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Disciplina")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Materia")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("ProfessorId");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Avaliacao");
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.OpcaoAvaliacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("QuestaoId");

                    b.Property<bool>("Verdadeira")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("QuestaoId");

                    b.ToTable("OpcaoAvaliacao");
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.OpcaoProva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCriacao");

                    b.Property<int>("OpcaoAvaliacaoId");

                    b.Property<int>("QuestaoProvaId");

                    b.Property<bool>("Resposta")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("OpcaoAvaliacaoId");

                    b.HasIndex("QuestaoProvaId");

                    b.ToTable("OpcaoProva");
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.Professor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Professor");
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.Prova", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AlunoId");

                    b.Property<int>("AvaliacaoId");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<DateTime?>("DataProva")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("AvaliacaoId");

                    b.ToTable("Prova");
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.Publicacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvaliacaoId");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("date");

                    b.Property<int>("TurmaId");

                    b.Property<int>("ValorProva");

                    b.HasKey("Id");

                    b.HasIndex("AvaliacaoId");

                    b.HasIndex("TurmaId");

                    b.ToTable("Publicacao");
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.QuestaoAvaliacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvaliacaoId");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Enunciado")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Tipo");

                    b.HasKey("Id");

                    b.HasIndex("AvaliacaoId");

                    b.ToTable("QuestaoAvaliacao");
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.QuestaoProva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCriacao");

                    b.Property<decimal>("Nota")
                        .HasColumnType("decimal(10,4)");

                    b.Property<int>("ProvaId");

                    b.Property<int>("QuestaoId");

                    b.HasKey("Id");

                    b.HasIndex("ProvaId");

                    b.HasIndex("QuestaoId");

                    b.ToTable("QuestaoProva");
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.Turma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Turma");
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.Aluno", b =>
                {
                    b.HasOne("PUC.LDSI.Domain.Entities.Turma", "Turma")
                        .WithMany("Alunos")
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.Avaliacao", b =>
                {
                    b.HasOne("PUC.LDSI.Domain.Entities.Professor", "Professor")
                        .WithMany("Avaliacoes")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.OpcaoAvaliacao", b =>
                {
                    b.HasOne("PUC.LDSI.Domain.Entities.QuestaoAvaliacao", "Questao")
                        .WithMany("Opcoes")
                        .HasForeignKey("QuestaoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.OpcaoProva", b =>
                {
                    b.HasOne("PUC.LDSI.Domain.Entities.OpcaoAvaliacao", "OpcaoAvaliacao")
                        .WithMany("OpcoesProva")
                        .HasForeignKey("OpcaoAvaliacaoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PUC.LDSI.Domain.Entities.QuestaoProva", "QuestaoProva")
                        .WithMany("OpcoesProva")
                        .HasForeignKey("QuestaoProvaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.Prova", b =>
                {
                    b.HasOne("PUC.LDSI.Domain.Entities.Aluno", "Aluno")
                        .WithMany("Provas")
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PUC.LDSI.Domain.Entities.Avaliacao", "Avaliacao")
                        .WithMany("Provas")
                        .HasForeignKey("AvaliacaoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.Publicacao", b =>
                {
                    b.HasOne("PUC.LDSI.Domain.Entities.Avaliacao", "Avaliacao")
                        .WithMany("Publicacoes")
                        .HasForeignKey("AvaliacaoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PUC.LDSI.Domain.Entities.Turma", "Turma")
                        .WithMany("Publicacoes")
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.QuestaoAvaliacao", b =>
                {
                    b.HasOne("PUC.LDSI.Domain.Entities.Avaliacao", "Avaliacao")
                        .WithMany("Questoes")
                        .HasForeignKey("AvaliacaoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("PUC.LDSI.Domain.Entities.QuestaoProva", b =>
                {
                    b.HasOne("PUC.LDSI.Domain.Entities.Prova", "Prova")
                        .WithMany("QuestoesProva")
                        .HasForeignKey("ProvaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PUC.LDSI.Domain.Entities.QuestaoAvaliacao", "QuestaoAvaliacao")
                        .WithMany("QuestoesProva")
                        .HasForeignKey("QuestaoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
