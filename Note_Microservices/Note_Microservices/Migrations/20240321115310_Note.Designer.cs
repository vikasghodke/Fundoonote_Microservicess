﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Note_Microservices.Context;

#nullable disable

namespace Note_Microservices.Migrations
{
    [DbContext(typeof(NoteContext))]
    [Migration("20240321115310_Note")]
    partial class Note
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Note_Microservices.NoteEntity.NoteEntity1", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NoteId"));

                    b.Property<bool>("Archived")
                        .HasColumnType("bit");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("NoteDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoteTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NoteId");

                    b.ToTable("NoteTable");
                });
#pragma warning restore 612, 618
        }
    }
}
