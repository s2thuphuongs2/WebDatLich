using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebDatLich.Models;

public partial class AppointmentschedulerContext : DbContext
{
    public AppointmentschedulerContext()
    {
    }

    public AppointmentschedulerContext(DbContextOptions<AppointmentschedulerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<CorporateCustomer> CorporateCustomers { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Exchange> Exchanges { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<RetailCustomer> RetailCustomers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Work> Works { get; set; }

    public virtual DbSet<WorkingPlan> WorkingPlans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PT;Database=appointmentscheduler;Integrated Security=true;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.ToTable("appointments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CanceledAt)
                .HasColumnType("datetime")
                .HasColumnName("canceled_at");
            entity.Property(e => e.End)
                .HasColumnType("datetime")
                .HasColumnName("end");
            entity.Property(e => e.IdCanceler).HasColumnName("id_canceler");
            entity.Property(e => e.IdCustomer).HasColumnName("id_customer");
            entity.Property(e => e.IdInvoice).HasColumnName("id_invoice");
            entity.Property(e => e.IdProvider).HasColumnName("id_provider");
            entity.Property(e => e.IdWork).HasColumnName("id_work");
            entity.Property(e => e.Start)
                .HasColumnType("datetime")
                .HasColumnName("start");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.IdCancelerNavigation).WithMany(p => p.AppointmentIdCancelerNavigations)
                .HasForeignKey(d => d.IdCanceler)
                .HasConstraintName("FK_appointments_users_canceler");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.AppointmentIdCustomerNavigations)
                .HasForeignKey(d => d.IdCustomer)
                .HasConstraintName("FK_appointments_users_customer");

            entity.HasOne(d => d.IdInvoiceNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdInvoice)
                .HasConstraintName("FK_appointments_invoices");

            entity.HasOne(d => d.IdProviderNavigation).WithMany(p => p.AppointmentIdProviderNavigations)
                .HasForeignKey(d => d.IdProvider)
                .HasConstraintName("FK_appointments_users_provider");

            entity.HasOne(d => d.IdWorkNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdWork)
                .HasConstraintName("FK_appointments_works");
        });

        modelBuilder.Entity<CorporateCustomer>(entity =>
        {
            entity.HasKey(e => e.IdCustomer).HasName("PK__corporat__8CC9BA46842BE846");

            entity.ToTable("corporate_customers");

            entity.Property(e => e.IdCustomer)
                .ValueGeneratedNever()
                .HasColumnName("id_customer");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("company_name");
            entity.Property(e => e.VatNumber)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("vat_number");

            entity.HasOne(d => d.IdCustomerNavigation).WithOne(p => p.CorporateCustomer)
                .HasForeignKey<CorporateCustomer>(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_corporate_customer_user");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.IdCustomer).HasName("PK__customer__8CC9BA4606A71C2E");

            entity.ToTable("customers");

            entity.Property(e => e.IdCustomer)
                .ValueGeneratedNever()
                .HasColumnName("id_customer");

            entity.HasOne(d => d.IdCustomerNavigation).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_customer_user");
        });

        modelBuilder.Entity<Exchange>(entity =>
        {
            entity.ToTable("exchanges");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExchangeStatus)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("exchange_status");
            entity.Property(e => e.IdAppointmentRequested).HasColumnName("id_appointment_requested");
            entity.Property(e => e.IdAppointmentRequestor).HasColumnName("id_appointment_requestor");

            entity.HasOne(d => d.IdAppointmentRequestedNavigation).WithMany(p => p.ExchangeIdAppointmentRequestedNavigations)
                .HasForeignKey(d => d.IdAppointmentRequested)
                .HasConstraintName("FK_exchange_appointment_requested");

            entity.HasOne(d => d.IdAppointmentRequestorNavigation).WithMany(p => p.ExchangeIdAppointmentRequestorNavigations)
                .HasForeignKey(d => d.IdAppointmentRequestor)
                .HasConstraintName("FK_exchange_appointment_requestor");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("invoices");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Issued)
                .HasColumnType("datetime")
                .HasColumnName("issued");
            entity.Property(e => e.Number)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("number");
            entity.Property(e => e.Status)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("messages");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IdAppointment).HasColumnName("id_appointment");
            entity.Property(e => e.IdAuthor).HasColumnName("id_author");
            entity.Property(e => e.Message1)
                .HasColumnType("text")
                .HasColumnName("message");

            entity.HasOne(d => d.IdAppointmentNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdAppointment)
                .HasConstraintName("FK_messages_appointment");

            entity.HasOne(d => d.IdAuthorNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdAuthor)
                .HasConstraintName("FK_messages_author");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("notifications");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.Message)
                .HasColumnType("text")
                .HasColumnName("message");
            entity.Property(e => e.Title)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.Url)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("url");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_notifications_user");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.IdProvider).HasName("PK__provider__BCFF0234DA2221EC");

            entity.ToTable("providers");

            entity.Property(e => e.IdProvider)
                .ValueGeneratedNever()
                .HasColumnName("id_provider");

            entity.HasOne(d => d.IdProviderNavigation).WithOne(p => p.Provider)
                .HasForeignKey<Provider>(d => d.IdProvider)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_provider_user");
        });

        modelBuilder.Entity<RetailCustomer>(entity =>
        {
            entity.HasKey(e => e.IdCustomer).HasName("PK__retail_c__8CC9BA46A9FEDEA4");

            entity.ToTable("retail_customers");

            entity.Property(e => e.IdCustomer)
                .ValueGeneratedNever()
                .HasColumnName("id_customer");

            entity.HasOne(d => d.IdCustomerNavigation).WithOne(p => p.RetailCustomer)
                .HasForeignKey<RetailCustomer>(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_retail_customer_user");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mobile");
            entity.Property(e => e.Password)
                .HasMaxLength(80)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("password");
            entity.Property(e => e.Postcode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("postcode");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("street");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasMany(d => d.IdWorks).WithMany(p => p.IdUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "WorksProvider",
                    r => r.HasOne<Work>().WithMany()
                        .HasForeignKey("IdWork")
                        .HasConstraintName("FK_works_providers_works"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("IdUser")
                        .HasConstraintName("FK_works_providers_users_provider"),
                    j =>
                    {
                        j.HasKey("IdUser", "IdWork");
                        j.ToTable("works_providers");
                        j.IndexerProperty<int>("IdUser").HasColumnName("id_user");
                        j.IndexerProperty<int>("IdWork").HasColumnName("id_work");
                    });

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UsersRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_roles_role"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_users_user"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("users_roles");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<Work>(entity =>
        {
            entity.ToTable("works");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Editable).HasColumnName("editable");
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Target)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("target");
        });

        modelBuilder.Entity<WorkingPlan>(entity =>
        {
            entity.HasKey(e => e.IdProvider).HasName("PK__working___BCFF0234B6DDDEE3");

            entity.ToTable("working_plans");

            entity.Property(e => e.IdProvider)
                .ValueGeneratedNever()
                .HasColumnName("id_provider");
            entity.Property(e => e.Friday)
                .HasColumnType("text")
                .HasColumnName("friday");
            entity.Property(e => e.Monday)
                .HasColumnType("text")
                .HasColumnName("monday");
            entity.Property(e => e.Saturday)
                .HasColumnType("text")
                .HasColumnName("saturday");
            entity.Property(e => e.Sunday)
                .HasColumnType("text")
                .HasColumnName("sunday");
            entity.Property(e => e.Thursday)
                .HasColumnType("text")
                .HasColumnName("thursday");
            entity.Property(e => e.Tuesday)
                .HasColumnType("text")
                .HasColumnName("tuesday");
            entity.Property(e => e.Wednesday)
                .HasColumnType("text")
                .HasColumnName("wednesday");

            entity.HasOne(d => d.IdProviderNavigation).WithOne(p => p.WorkingPlan)
                .HasForeignKey<WorkingPlan>(d => d.IdProvider)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_working_plans_provider");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
