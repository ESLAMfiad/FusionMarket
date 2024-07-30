using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json.Serialization;
using Order_Management.Repository;
using Order_Management.Repository.Data;
using Order_Management.Repository.Repo_Implementation;
using Order_Management.Core.Entities;
using Order_Management.Core.Repositories;
using Order_Management.Service.implementation;
using Order_Managment.Service.Interfaces;
using Order_Management.Mapping;
using Order_Management.Errors;
using Order_Managment.Repository.Data;
using Order_Managment.Repository;
using Orders_Managment.Core;
using Order_Managment.Service.implementation;

namespace Order_Management
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Configure services and dependencies

			// Add services to the container.
			builder.Services.AddControllers();

			// Configure Swagger/OpenAPI
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order Management API", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Id = "Bearer",
								Type = ReferenceType.SecurityScheme
							}
						},
						new List<string>()
					}
				});
			});

			// Configure Entity Framework and Database
			builder.Services.AddDbContext<OrderManagementDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			// Configure Redis
			builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
			{
				var connection = builder.Configuration.GetConnectionString("RedisConnection");
				return ConnectionMultiplexer.Connect(connection);
			});

			// Configure Email Settings
			builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

			#region Repositories

			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IBasketRepository, BasketRepository>();

			#endregion

			// Configure AutoMapper
			builder.Services.AddAutoMapper(typeof(MappingProfile));

			#region Services

			builder.Services.AddScoped<ICustomerService, CustomerService>();
			builder.Services.AddScoped<IOrderService, OrderService>();
			builder.Services.AddScoped<IProductService, ProductService>();
			builder.Services.AddScoped<IInvoicesService, InvoiceService>();
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddTransient<IEmailService, EmailService>();
			builder.Services.AddScoped<ITokenService, TokenService>();
			builder.Services.AddScoped<IBasketService, BasketService>();
			builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

			#endregion

			// Configure JSON options
			builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
			});

			// Configure validation error handling
			builder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					//modelstate= dict [keyvalue pair]
					//key = name of param
					//value= error
					var errors = actionContext.ModelState
						.Where(p => p.Value.Errors.Count > 0)
						.SelectMany(p => p.Value.Errors)
						.Select(e => e.ErrorMessage);

					var validationErrorResponse = new ApiValidationErrors
					{
						Errors = errors
					};

					return new BadRequestObjectResult(validationErrorResponse);
				};
			});

			#endregion

			#region Authentication and Authorization

			// Configure Identity
			builder.Services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<OrderManagementDbContext>()
				.AddDefaultTokenProviders();

			// Configure JWT Authentication
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
				};
			});

			// Configure Authorization policies
			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
			});

			// Configure CORS
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("MyPolicy", corsBuilder =>
				{
					corsBuilder.AllowAnyHeader();
					corsBuilder.AllowAnyMethod();
					corsBuilder.WithOrigins(builder.Configuration["FrontBaseUrl"]);
				});
			});

			#endregion

			var app = builder.Build();

			#region Database Migration and Seeding

			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				var dbContext = services.GetRequiredService<OrderManagementDbContext>();
				await dbContext.Database.MigrateAsync();

				var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
				var userManager = services.GetRequiredService<UserManager<User>>();

				// Seed roles
				string[] roles = { "Admin", "Customer" };
				foreach (var role in roles)
				{
					if (!await roleManager.RoleExistsAsync(role))
					{
						await roleManager.CreateAsync(new IdentityRole(role));
					}
				}

				// Seed database
				await OrderContextSeed.SeedAsync(dbContext);
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "An error occurred during migration or seeding.");
			}

			#endregion

			#region Configure the HTTP request pipeline

			if (app.Environment.IsDevelopment())
			{
				app.UseMiddleware<ExceptionMiddleware>();
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Management API V1");
				});
			}

			app.UseStatusCodePagesWithReExecute("error/{0}");
			app.UseHttpsRedirection();
			app.UseCors("MyPolicy");
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			#endregion

			await app.RunAsync();
		}
	}
}
