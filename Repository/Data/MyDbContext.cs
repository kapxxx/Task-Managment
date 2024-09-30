using DTOs.ResponseDTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Data
{
	public class MyDbContext :IdentityDbContext<ApplicationUser>
	{
		public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options) { }

        
        public DbSet<Tasks> Tasks { get; set; }
	//	public DbSet<Comments> Comments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserTaskDTO>()
           .HasNoKey()
           .ToView(null);


            modelBuilder.Entity<ApplicationUser>()
		   .HasMany(u => u.Tasks) // A user can have many tasks
		   .WithOne(t => t.CreatedBy) // Each task is created by one user
		   .HasForeignKey(t => t.CreatedByUserId) // Foreign key in Tasks table
		   .OnDelete(DeleteBehavior.Restrict); // 

		}
	}
}
