using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CMon.IoTApp.Models;

namespace CMon.IoTApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20170424205915_Add_Field_Reading_Synchronized")]
    partial class Add_Field_Reading_Synchronized
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("CMon.IoTApp.Models.AppConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Tax");

                    b.Property<int>("Voltage");

                    b.HasKey("Id");

                    b.ToTable("AppConfiguration");
                });

            modelBuilder.Entity("CMon.IoTApp.Models.Reading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<double>("Power");

                    b.Property<bool>("Synchronized");

                    b.Property<double>("Value");

                    b.Property<int>("Voltage");

                    b.HasKey("Id");

                    b.ToTable("Readings");
                });
        }
    }
}
