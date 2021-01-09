﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace cnpj_data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Company", b =>
                {
                    b.Property<string>("CNPJ")
                        .HasMaxLength(14)
                        .HasColumnType("TEXT");

                    b.Property<string>("BusinessName")
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("Cep")
                        .HasMaxLength(8)
                        .HasColumnType("TEXT");

                    b.Property<string>("FantasyName")
                        .HasMaxLength(55)
                        .HasColumnType("TEXT");

                    b.Property<int>("RegistrationSituation")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RegistrationSituationDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("SocialCapital")
                        .HasColumnType("REAL");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("CNPJ");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Member", b =>
                {
                    b.Property<string>("CNPJCPF")
                        .HasMaxLength(14)
                        .HasColumnType("TEXT");

                    b.Property<int>("Identifier")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.HasKey("CNPJCPF");

                    b.ToTable("Members");
                });
#pragma warning restore 612, 618
        }
    }
}