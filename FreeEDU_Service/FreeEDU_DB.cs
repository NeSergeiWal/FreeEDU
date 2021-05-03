using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FreeEDU_Service
{
	public partial class FreeEDU_DB : DbContext
	{
		public FreeEDU_DB()
			: base("name=FreeEDU_DB")
		{
		}

		public virtual DbSet<ACCOUNTS> ACCOUNTS { get; set; }
		public virtual DbSet<COURSES> COURSES { get; set; }
		public virtual DbSet<REQUESTS> REQUESTS { get; set; }
		public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
		public virtual DbSet<USERS> USERS { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ACCOUNTS>()
				.Property(e => e.Role)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<ACCOUNTS>()
				.HasMany(e => e.COURSES)
				.WithRequired(e => e.ACCOUNTS)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ACCOUNTS>()
				.HasOptional(e => e.REQUESTS)
				.WithRequired(e => e.ACCOUNTS);

			modelBuilder.Entity<ACCOUNTS>()
				.HasOptional(e => e.USERS)
				.WithRequired(e => e.ACCOUNTS);
		}
	}
}
