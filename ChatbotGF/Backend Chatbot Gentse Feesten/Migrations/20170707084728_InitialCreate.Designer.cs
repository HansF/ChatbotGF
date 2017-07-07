﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Chatbot_GF.SQLite;

namespace Chatbot_GF.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20170707084728_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Chatbot_GF.Model.User", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("date");

                    b.HasKey("id");

                    b.ToTable("Users");
                });
        }
    }
}