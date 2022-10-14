﻿using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Brand>(a =>
            {
                a.ToTable("Brands").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(p=>p.Models);
            });

            modelBuilder.Entity<Model>(a =>
            {
                a.ToTable("Models").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.BrandId).HasColumnName("BrandId");
                a.Property(p => p.Name).HasColumnName("Name");
                a.Property(p => p.DailyPrice).HasColumnName("DailyPrice");
                a.Property(p => p.ImageUrl).HasColumnName("ImageUrl");
                a.HasOne(p => p.Brand);
            });

            #region User Model Creation
            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("Users").HasKey(u => u.Id);
                u.Property(u => u.Id).HasColumnName("Id");
                u.Property(u => u.FirstName).HasColumnName("FirstName");
                u.Property(u => u.LastName).HasColumnName("LastName");
                u.Property(u => u.Email).HasColumnName("Email");
                u.Property(u => u.Status).HasColumnName("Status");
                u.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
                u.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt");
                u.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType");
                u.HasMany(u => u.UserOperationClaims);
                u.HasMany(u => u.RefreshTokens);
            });
            #endregion

            #region OperationClaim Model Creation
            modelBuilder.Entity<OperationClaim>(u =>
            {
                u.ToTable("OperationClaims").HasKey(u => u.Id);
                u.Property(u => u.Id).HasColumnName("Id");
                u.Property(u => u.Name).HasColumnName("Name");
            });

            OperationClaim[] operationClaimEntitySeeds = { new(1, "Admin"), new(2, "User"), new(3, "Visitor") };
            modelBuilder.Entity<OperationClaim>().HasData(operationClaimEntitySeeds);
            #endregion

            #region UserOperationClaim Model Creation
            modelBuilder.Entity<UserOperationClaim>(u =>
            {
                u.ToTable("UserOperationClaims").HasKey(u => u.Id);
                u.Property(u => u.Id).HasColumnName("Id");
                u.Property(u => u.UserId).HasColumnName("UserId");
                u.Property(u => u.OperationClaimId).HasColumnName("OperationClaimId");
                u.HasOne(u => u.User);
                u.HasOne(u => u.OperationClaim);
            });
            #endregion

            #region RefreshTokens Model Creation
            modelBuilder.Entity<RefreshToken>(r =>
            {
                r.ToTable("RefreshTokens").HasKey(r => r.Id);
                r.Property(u => u.Id).HasColumnName("Id");
                r.Property(u => u.UserId).HasColumnName("UserId");
                r.Property(u => u.Token).HasColumnName("Token");
                r.Property(u => u.Expires).HasColumnName("Expires");
                r.Property(u => u.Created).HasColumnName("Created");
                r.Property(u => u.CreatedByIp).HasColumnName("CreatedByIp");
                r.Property(u => u.Revoked).HasColumnName("Revoked");
                r.Property(u => u.RevokedByIp).HasColumnName("RevokedByIp");
                r.Property(u => u.ReplacedByToken).HasColumnName("ReplacedByToken");
                r.Property(u => u.ReasonRevoked).HasColumnName("ReasonRevoked");
                r.HasOne(u => u.User);
            });
            #endregion

            //Test için oluşturduğumuz veriler
            Brand[] brandEntitySeeds = { new(1, "BMW"), new(2, "Mercedes"), new(3, "Audi"), new(4, "Bugatti"), new(5, "Cadillac") };
            modelBuilder.Entity<Brand>().HasData(brandEntitySeeds);

            Model[] modelEntitySeeds = { new(1, 1, "Series 4", 1500, ""), new(2, 1, "Series 3", 1200, ""), new(3, 2, "A 180", 1000, "") };
           modelBuilder.Entity<Model>().HasData(modelEntitySeeds);
        }
    }
}
