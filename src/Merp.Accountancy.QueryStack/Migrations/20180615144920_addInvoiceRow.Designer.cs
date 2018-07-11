﻿// <auto-generated />
using System;
using Merp.Accountancy.QueryStack;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Merp.Accountancy.QueryStack.Migrations
{
    [DbContext(typeof(AccountancyDbContext))]
    [Migration("20180615144920_addInvoiceRow")]
    partial class addInvoiceRow
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Merp.Accountancy.QueryStack.Model.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Currency");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime?>("DueDate");

                    b.Property<bool>("IsOverdue");

                    b.Property<bool>("IsPaid");

                    b.Property<Guid?>("JobOrderId");

                    b.Property<string>("Number");

                    b.Property<Guid>("OriginalId");

                    b.Property<DateTime?>("PaymentDate");

                    b.Property<string>("PurchaseOrderNumber");

                    b.Property<decimal>("TaxableAmount");

                    b.Property<decimal>("Taxes");

                    b.Property<decimal>("TotalPrice");

                    b.HasKey("Id");

                    b.HasIndex("Date");

                    b.HasIndex("DueDate");

                    b.HasIndex("IsOverdue");

                    b.HasIndex("IsPaid");

                    b.HasIndex("JobOrderId");

                    b.HasIndex("PurchaseOrderNumber");

                    b.ToTable("Invoice");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Invoice");
                });

            modelBuilder.Entity("Merp.Accountancy.QueryStack.Model.InvoiceRow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("Currency");

                    b.Property<string>("Description");

                    b.Property<Guid>("InvoiceId");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("Taxes");

                    b.Property<decimal>("TotalAmount");

                    b.HasKey("Id");

                    b.ToTable("InvoiceRows");
                });

            modelBuilder.Entity("Merp.Accountancy.QueryStack.Model.JobOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<Guid>("CustomerId");

                    b.Property<string>("CustomerName");

                    b.Property<DateTime?>("DateOfCompletion");

                    b.Property<DateTime>("DateOfRegistration");

                    b.Property<DateTime>("DateOfStart");

                    b.Property<string>("Description");

                    b.Property<DateTime>("DueDate");

                    b.Property<bool>("IsCompleted");

                    b.Property<bool>("IsTimeAndMaterial");

                    b.Property<Guid>("ManagerId");

                    b.Property<string>("ManagerName");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Number")
                        .IsRequired();

                    b.Property<Guid>("OriginalId");

                    b.Property<decimal?>("Price");

                    b.Property<string>("PurchaseOrderNumber");

                    b.HasKey("Id");

                    b.HasIndex("CustomerName");

                    b.HasIndex("IsCompleted");

                    b.HasIndex("Name");

                    b.ToTable("JobOrders");
                });

            modelBuilder.Entity("Merp.Accountancy.QueryStack.Model.IncomingInvoice", b =>
                {
                    b.HasBaseType("Merp.Accountancy.QueryStack.Model.Invoice");


                    b.ToTable("IncomingInvoice");

                    b.HasDiscriminator().HasValue("IncomingInvoice");
                });

            modelBuilder.Entity("Merp.Accountancy.QueryStack.Model.OutgoingInvoice", b =>
                {
                    b.HasBaseType("Merp.Accountancy.QueryStack.Model.Invoice");


                    b.ToTable("OutgoingInvoice");

                    b.HasDiscriminator().HasValue("OutgoingInvoice");
                });

            modelBuilder.Entity("Merp.Accountancy.QueryStack.Model.Invoice", b =>
                {
                    b.OwnsOne("Merp.Accountancy.QueryStack.Model.Invoice+PartyInfo", "Customer", b1 =>
                        {
                            b1.Property<int>("InvoiceId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("City");

                            b1.Property<string>("Country");

                            b1.Property<string>("Name")
                                .HasMaxLength(100);

                            b1.Property<string>("NationalIdentificationNumber");

                            b1.Property<Guid>("OriginalId");

                            b1.Property<string>("PostalCode");

                            b1.Property<string>("StreetName");

                            b1.Property<string>("VatIndex");

                            b1.HasIndex("Name");

                            b1.ToTable("Invoice");

                            b1.HasOne("Merp.Accountancy.QueryStack.Model.Invoice")
                                .WithOne("Customer")
                                .HasForeignKey("Merp.Accountancy.QueryStack.Model.Invoice+PartyInfo", "InvoiceId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("Merp.Accountancy.QueryStack.Model.Invoice+PartyInfo", "Supplier", b1 =>
                        {
                            b1.Property<int>("InvoiceId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("City");

                            b1.Property<string>("Country");

                            b1.Property<string>("Name")
                                .HasMaxLength(100);

                            b1.Property<string>("NationalIdentificationNumber");

                            b1.Property<Guid>("OriginalId");

                            b1.Property<string>("PostalCode");

                            b1.Property<string>("StreetName");

                            b1.Property<string>("VatIndex");

                            b1.HasIndex("Name");

                            b1.ToTable("Invoice");

                            b1.HasOne("Merp.Accountancy.QueryStack.Model.Invoice")
                                .WithOne("Supplier")
                                .HasForeignKey("Merp.Accountancy.QueryStack.Model.Invoice+PartyInfo", "InvoiceId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
