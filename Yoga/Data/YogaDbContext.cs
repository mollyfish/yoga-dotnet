using Yoga.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yoga.Data
{
	public class YogaDbContext : DbContext
	{
		public YogaDbContext(DbContextOptions<YogaDbContext> options) : base(options)
		{
		}

		/// <summary>
		/// makes EF create tables with non-pluralized names
		/// </summary>
		/// <param name="modelBuilder"></param>

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{




			modelBuilder.Entity<Class>().ToTable("Class");
			modelBuilder.Entity<Donation>().ToTable("Donation");
			modelBuilder.Entity<Event>().ToTable("Event");
			modelBuilder.Entity<MailingList>().ToTable("MailingList");
			modelBuilder.Entity<PhoneNumber>().ToTable("PhoneNumber");
			modelBuilder.Entity<PhysicalAddress>().ToTable("PhysicalAddress");
			modelBuilder.Entity<Table>().ToTable("Table");
			modelBuilder.Entity<VIP>().ToTable("VIP");

			modelBuilder.Entity<EventMailingList>().HasKey(eml => new
			{ eml.EventId, eml.MailingListId });

			modelBuilder.Entity<UserMailingList>().HasKey(uml => new
			{ uml.UserId, uml.MailingListId });

			modelBuilder.Entity<AttendeeTable>().HasKey(at => new
			{ at.UserId, at.TableId, at.EventId });

			modelBuilder.Entity<PhysicalAddress>().HasData(
				new PhysicalAddress
				{
					Id = 6,
					City = "Seattle",
					State = "WA",
					StreetAddress = "123 Main St",
					StreetAddressCont = "STE 100",
					ZipCode = 12345,
				}
				);

		}





		public DbSet<Class> Classes { get; set; }
		public DbSet<Donation> Donations { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<MailingList> MailingLists { get; set; }
		public DbSet<PhoneNumber> PhoneNumbers { get; set; }
		public DbSet<PhysicalAddress> PhysicalAddresses { get; set; }
		public DbSet<Table> Tables { get; set; }
		public DbSet<EventMailingList> EventMailingLists { get; set; }
		public DbSet<UserMailingList> UserMailingLists { get; set; }
		public DbSet<AttendeeTable> AttendeeTables { get; set; }
		public DbSet<VIP> VIPs { get; set; }



	}
}
