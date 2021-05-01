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

		public virtual DbSet<Accounts> Accounts { get; set; }
		public virtual DbSet<Courses> Courses { get; set; }
		public virtual DbSet<USERS> USERS { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Accounts>()
				.HasMany(e => e.Courses)
				.WithRequired(e => e.Accounts)
				.HasForeignKey(e => e.Account_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Accounts>()
				.HasOptional(e => e.USERS)
				.WithRequired(e => e.Accounts);
		}
	}
}
