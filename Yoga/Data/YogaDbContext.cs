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
			modelBuilder.Entity<Person>().ToTable("Person");
			modelBuilder.Entity<Class>().ToTable("Class");
			modelBuilder.Entity<Donation>().ToTable("Donation");
			modelBuilder.Entity<Event>().ToTable("Event");
			modelBuilder.Entity<MailingList>().ToTable("MailingList");
			modelBuilder.Entity<PhoneNumber>().ToTable("PhoneNumber");
			modelBuilder.Entity<PhysicalAddress>().ToTable("PhysicalAddress");
			modelBuilder.Entity<EmailAddress>().ToTable("EmailAddress");
			modelBuilder.Entity<Table>().ToTable("Table");
			modelBuilder.Entity<VIP>().ToTable("VIP");

			modelBuilder.Entity<EventMailingList>().HasKey(eml => new
			{ eml.EventId, eml.MailingListId });

			modelBuilder.Entity<UserMailingList>().HasKey(uml => new
			{ uml.UserId, uml.MailingListId });

			modelBuilder.Entity<AttendeeTable>().HasKey(at => new
			{ at.UserId, at.TableId, at.EventId });

			modelBuilder.Entity<Person>().HasData(
				new Person
				{
					Id = 1,
					FirstName = "Peggy",
					LastName = "Kent",
					FullName = "Peggy Kent",
					DateAdded = DateTime.Parse("3/24/12")
				},
				new Person
				{
					Id = 2,
					FirstName = "Dan",
					LastName = "Kent",
					FullName = "Dan Kent",
					DateAdded = DateTime.Parse("5/22/18")
				},
				new Person
				{
					Id = 3,
					FirstName = "Fred",
					LastName = "Harder",
					FullName = "Fred Harder",
					DateAdded = DateTime.Parse("9/1/92")
				}
				);

			modelBuilder.Entity<PhoneNumber>().HasData(
				new PhoneNumber
				{
					Id = 1,
					PersonId = 1,
					Phone = "1234567890",
					DateAdded = DateTime.Parse("7/12/13"),
					IsPrimary = true
				},
				new PhoneNumber
				{
					Id = 2,
					PersonId = 2,
					Phone = "1231231231",
					DateAdded = DateTime.Parse("6/30/16"),
					IsPrimary = true
				},
				new PhoneNumber
				{
					Id = 3,
					PersonId = 3,
					Phone = "1201201200",
					DateAdded = DateTime.Parse("3/17/00"),
					IsPrimary = true
				}
				);

			modelBuilder.Entity<MailingList>().HasData(
				new MailingList
				{
					Id = 1,
					Title = "Master List"
				},
				new MailingList
				{
					Id = 2,
					Title = "2016 Breakfast Attendees"
				},
				new MailingList
				{
					Id = 3,
					Title = "2016 Breakfast Invitees"
				},
				new MailingList
				{
					Id = 4,
					Title = "Current Instructors"
				}
				);
			modelBuilder.Entity<PhysicalAddress>().HasData(
				new PhysicalAddress
				{
					Id = 1,
					Title = "MI Community Center",
					City = "Mercer Island",
					State = "WA",
					StreetAddress = "123 West Mercer Way",
					ZipCode = "13345",
					DateAdded = DateTime.Parse("4/13/12")
				},
				new PhysicalAddress
				{
					Id = 2,
					PersonId = 2,
					City = "Denver",
					State = "CO",
					StreetAddress = "123 Water Avenue",
					StreetAddressCont = "#430",
					ZipCode = "12321",
					DateAdded = DateTime.Parse("1/23/13"),
					IsPrimary = true
				},
				new PhysicalAddress
				{
					Id = 3,
					Title = "Denver VA",
					City = "Lakewood",
					State = "CO",
					StreetAddress = "123 Market St",
					ZipCode = "12459",
					DateAdded = DateTime.Parse("8/15/17"),
					IsPrimary = true
				},
				new PhysicalAddress
				{
					Id = 4,
					PersonId = 3,
					City = "Bellevue",
					State = "WA",
					StreetAddress = "12345 134th Ave SE",
					ZipCode = "12045",
					DateAdded = DateTime.Parse("9/2/16")
				},
				new PhysicalAddress
				{
					Id = 5,
					PersonId = 1,
					City = "Port Townsend",
					State = "WA",
					StreetAddress = "1231 Pierce St",
					StreetAddressCont = "Unit A",
					ZipCode = "13335",
					DateAdded = DateTime.Parse("11/11/11"),
					IsPrimary	= true
				},
				new PhysicalAddress
				{
					Id = 6,
					City = "Seattle",
					State = "WA",
					StreetAddress = "123 Main St",
					StreetAddressCont = "STE 100",
					ZipCode = "12345",
					DateAdded = DateTime.Parse("4/10/99")
				}
				);
			modelBuilder.Entity<Event>().HasData(
				new Event
				{
					Id = 1,
					LocationId = 1,
					Title = "2016 Breakfast",
					Date = DateTime.Parse("11/4/16")
				},
				new Event
				{
					Id = 2,
					LocationId = 1,
					Title = "2017 Breakfast",
					Date = DateTime.Parse("11/8/17")
				}
				);
			modelBuilder.Entity<Table>().HasData(
				new Table
				{
					Id = 1,
					CaptainId = 3,
					Capacity = 12,
					EventId = 1
				},
				new Table
				{
					Id = 2,
					CaptainId = 2,
					Capacity = 18,
					EventId = 1
				},
				new Table
				{
					Id = 3,
					CaptainId = 2,
					Capacity = 12,
					EventId = 1
				}
				);

			modelBuilder.Entity<Class>().HasData(
				new Class
				{
					Id = 1,
					InstructorId = 3,
					Title = "Basic Yoga",
					Date = DateTime.Parse("12/23/13"),
					LocationId = 3
				}
				);

			modelBuilder.Entity<Donation>().HasData(
				new Donation
				{
					Id = 1,
					DonorId = 2,
					DonorDisplayName = "Brother Daniel",
					DisplayAsAnonymous = false,
					Amount = 400.50m,
					Date = DateTime.Parse("1/1/17"),
					Honoree = "Ann Kent",
					TaxReceiptSent = true,
					TaxReceiptSentDate = DateTime.Parse("1/10/17"),
					ThankYouSent = false,
					DonationType = DonationType.card
				},
				new Donation
				{
					Id = 2,
					DonorId = 2,
					DisplayAsAnonymous = true,
					Amount = 4.50m,
					Date = DateTime.Parse("1/3/12"),
					TaxReceiptSent = true,
					TaxReceiptSentDate = DateTime.Parse("1/10/12"),
					ThankYouSent = true,
					ThankYouSentDate = DateTime.Parse("1/11/12"),
					DonationType = DonationType.cash
				},
				new Donation
				{
					Id = 3,
					DonorId = 1,
					DisplayAsAnonymous = true,
					Amount = 4000.00m,
					Date = DateTime.Parse("10/16/15"),
					TaxReceiptSent = true,
					TaxReceiptSentDate = DateTime.Parse("10/17/15"),
					ThankYouSent = true,
					ThankYouSentDate = DateTime.Parse("11/1/15"),
					DonationType = DonationType.cash
				}
				);
			modelBuilder.Entity<EmailAddress>().HasData(
				new EmailAddress
				{
					Id = 1,
					PersonId = 2,
					Email = "dan@kentfamily.com",
					DateAdded = DateTime.Parse("8/5/19"),
					IsPrimary = true
				},
				new EmailAddress
				{
					Id = 2,
					PersonId = 1,
					Email = "peggy@kentfamily.com",
					DateAdded = DateTime.Parse("5/15/12"),
					IsPrimary = true
				}
				);

		}




		public DbSet<Person> People { get; set; }
		public DbSet<Class> Classes { get; set; }
		public DbSet<Donation> Donations { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<MailingList> MailingLists { get; set; }
		public DbSet<PhoneNumber> PhoneNumbers { get; set; }
		public DbSet<PhysicalAddress> PhysicalAddresses { get; set; }
		public DbSet<EmailAddress> EmailAddresses { get; set; }
		public DbSet<Table> Tables { get; set; }
		public DbSet<EventMailingList> EventMailingLists { get; set; }
		public DbSet<UserMailingList> UserMailingLists { get; set; }
		public DbSet<AttendeeTable> AttendeeTables { get; set; }
		public DbSet<VIP> VIPs { get; set; }



	}
}
