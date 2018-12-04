using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yoga.Data;
using Yoga.Models;
using Yoga.Services;
using Microsoft.AspNetCore.Http;
using Yoga.Models.Interfaces;
using Yoga.Models.Services;

namespace Yoga
{
	public class Startup
	{
		public IConfiguration Configuration { get; set; }

		public Startup(IConfiguration configuration)
		{
			var builder = new ConfigurationBuilder().AddEnvironmentVariables();
			builder.AddUserSecrets<Startup>();
			Configuration = builder.Build();
		}
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<IdentityOptions>(options =>
			{
				// Default Password settings.
				//options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				//options.Password.RequireNonAlphanumeric = true;
				//options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 6;
				//options.Password.RequiredUniqueChars = 1;


				options.User.AllowedUserNameCharacters =
			"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				options.User.RequireUniqueEmail = true;
			});

			services.AddMvc();

			services.AddIdentity<User, IdentityRole>()
					.AddEntityFrameworkStores<YogaUsersDbContext>()
					.AddDefaultTokenProviders();

			services.AddDbContext<YogaDbContext>(options =>
					options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddDbContext<YogaUsersDbContext>(options =>
					options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

			services.AddAuthorization(options =>
			{
				options.AddPolicy("AdminsOnly", policy => policy.RequireRole(UserRole.Admin));
				options.AddPolicy("Instructors", policy => policy.RequireRole(UserRole.Instructor));
			});

			// Add application services.
			services.AddTransient<IEmailSender, EmailSender>();
			services.AddScoped<IVIP, VIPService>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				//app.UseBrowserLink();
				app.UseDatabaseErrorPage();
			}


			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
									name: "default",
									template: "{controller=Home}/{action=Index}/{id?}");
			});

			app.Run(async (context) =>
			{
				await context.Response.WriteAsync("Well, crap. Something went wrong.");
			});
		}
	}
}
