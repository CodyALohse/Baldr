using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Data.EntityFramework;
using Baldr.Models.Enums;

namespace Baldr.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Baldr.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("AccountNumber");

                    b.Property<string>("Comment");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<decimal>("CurrentBalance");

                    b.Property<int?>("InstitutionId");

                    b.Property<decimal>("InterestRate");

                    b.Property<DateTimeOffset>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<decimal>("StartingBalance");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("InstitutionId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Baldr.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<string>("Address3");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<DateTimeOffset>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("State");

                    b.HasKey("Id");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("Baldr.Models.Institution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int?>("ContactInfoId");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<bool>("IsActive");

                    b.Property<DateTimeOffset>("ModifiedOn");

                    b.HasKey("Id");

                    b.HasIndex("ContactInfoId");

                    b.ToTable("Institutions");
                });

            modelBuilder.Entity("Baldr.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int?>("AccountId");

                    b.Property<decimal>("AlternateAmount");

                    b.Property<decimal>("Amount");

                    b.Property<string>("Comment");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<DateTimeOffset>("Date");

                    b.Property<bool>("IsComplete");

                    b.Property<DateTimeOffset>("ModifiedOn");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Baldr.Models.Account", b =>
                {
                    b.HasOne("Baldr.Models.Institution")
                        .WithMany("Accounts")
                        .HasForeignKey("InstitutionId");
                });

            modelBuilder.Entity("Baldr.Models.Institution", b =>
                {
                    b.HasOne("Baldr.Models.Contact", "ContactInfo")
                        .WithMany()
                        .HasForeignKey("ContactInfoId");
                });

            modelBuilder.Entity("Baldr.Models.Transaction", b =>
                {
                    b.HasOne("Baldr.Models.Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId");
                });
        }
    }
}
