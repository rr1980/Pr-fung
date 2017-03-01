using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Prüfung.Entitys;

namespace Prüfung.DB
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleToUser> RoleToUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<RoleToUser>().HasKey(t => new { t.UserId, t.RoleId });
            //modelBuilder.Entity<RoleToUser>().HasOne(rtu => rtu.Role).WithMany(r => r.RoleToUsers).HasForeignKey(rtu => rtu.RoleId);
            //modelBuilder.Entity<RoleToUser>().HasOne(rtu => rtu.User).WithMany(r => r.RoleToUsers).HasForeignKey(rtu => rtu.UserId);

            modelBuilder
               .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
               .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Prüfung.Entitys.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd();

                    //b.Property<int>("UserRoleType");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Prüfung.Entitys.RoleToUser", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleToUsers");
                });

            modelBuilder.Entity("Prüfung.Entitys.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.Property<string>("Vorname");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Prüfung.Entitys.RoleToUser", b =>
                {
                    b.HasOne("Prüfung.Entitys.Role", "Role")
                        .WithMany("RoleToUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Prüfung.Entitys.User", "User")
                        .WithMany("RoleToUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
