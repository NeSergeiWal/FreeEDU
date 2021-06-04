using System.Data.Entity;

namespace FreeEDU_Service.DataBase
{
	public partial class FreeEDU_DB : DbContext
	{
		public FreeEDU_DB()
			: base("name=FreeEDU_DB")
		{
		}

		public virtual DbSet<ACCOUNTS> ACCOUNTS { get; set; }
		public virtual DbSet<COMMENTS> COMMENTS { get; set; }
		public virtual DbSet<COMPLETED> COMPLETED { get; set; }
		public virtual DbSet<COURSES> COURSES { get; set; }
		public virtual DbSet<REQUESTS> REQUESTS { get; set; }
		public virtual DbSet<USERS> USERS { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ACCOUNTS>()
				.Property(e => e.Role)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<ACCOUNTS>()
				.HasMany(e => e.COMMENTS)
				.WithRequired(e => e.ACCOUNTS)
				.HasForeignKey(e => e.User_Id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ACCOUNTS>()
				.HasMany(e => e.COMPLETED)
				.WithRequired(e => e.ACCOUNTS)
				.HasForeignKey(e => e.User_Id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ACCOUNTS>()
				.HasMany(e => e.COURSES)
				.WithRequired(e => e.ACCOUNTS)
				.HasForeignKey(e => e.Teacher_Id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ACCOUNTS>()
				.HasMany(e => e.REQUESTS)
				.WithRequired(e => e.ACCOUNTS)
				.HasForeignKey(e => e.Account_Id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<COURSES>()
				.HasMany(e => e.COMMENTS)
				.WithRequired(e => e.COURSES)
				.HasForeignKey(e => e.Course_Id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<COURSES>()
				.HasMany(e => e.COMPLETED)
				.WithRequired(e => e.COURSES)
				.HasForeignKey(e => e.Course_Id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<USERS>()
				.HasOptional(e => e.ACCOUNTS)
				.WithRequired(e => e.USERS);

			Database.SetInitializer(new ContextInitializer());
		}
	}
}
