using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using TourSite.APIs.Errors;
using TourSite.Core;
using TourSite.Core.Entities;
using TourSite.Core.Mapping;
using TourSite.Core.Servicies.Contract;
using TourSite.Repository.Data.Contexts;
using TourSite.Repository.Repositories;
using TourSite.Service.Services.Auth;
using TourSite.Service.Services.Cache;
using TourSite.Service.Services.CatTours;
using TourSite.Service.Services.Destnations;
using TourSite.Service.Services.Emails;

using TourSite.Service.Services.Token;
using TourSite.Service.Services.TourImgs;
using TourSite.Service.Services.Tours;
using TourSite.Service.Services.Trasnfers;
using TourSite.Service.Services.Users;


namespace TourSite.APIs.Helper
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services,IConfiguration configuration)
        {
            services.IConnectionMultiplexer();
            services.AddBulitinService();
            services.AddSwaggerService();
            services.AddDbContextSerevice(configuration);
            services.AddUserDefinedSerevice();
            services.AddMappingSerevice(configuration);
            services.AddValidationErrorService(configuration);
            services.AddingIDentityService();
            services.AddingAuthenticationService(configuration);

            return services;
        }
        private static IServiceCollection AddBulitinService(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }
        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(); 
            return services;
        }
        private static IServiceCollection AddDbContextSerevice(this IServiceCollection services,IConfiguration configuration)
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddDbContext<TourDbContext>(options =>
                         options.UseSqlServer(configuration.GetConnectionString("Default")));

            return services;
        }
        private static IServiceCollection AddUserDefinedSerevice(this IServiceCollection services)
        {

            services.AddScoped<IToursService, TourService>();
            services.AddScoped<ICategoryTourService, CategoryTourService>();
            services.AddScoped<ITourImgService, TourImgService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDestinationService, DestnationService>();
            services.AddScoped<ITransferService, TransferService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();


            


            return services;
        }
        private static IServiceCollection AddMappingSerevice(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(m => m.AddProfile(new UserProfile()));

            services.AddAutoMapper(m => m.AddProfile(new TourProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new CategoryTourProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new TourImgProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new EmailsProfile()));
            services.AddAutoMapper(m => m.AddProfile(new DestnationProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new TransferProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new EmailsProfile()));
            services.AddAutoMapper(m => m.AddProfile(new DestnationALlProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new TourAllProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new CategoryTourAllProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new TransferAllProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new UserProfile()));



            return services;
        }
        private static IServiceCollection AddValidationErrorService(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<ApiBehaviorOptions>(options =>

                 options.InvalidModelStateResponseFactory = (actionContext) => {

                     var errors = actionContext.ModelState
                   .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                           .Select(x => x.ErrorMessage)
                             .ToArray();

                     var response = new ValidationErrorResponse()
                     {
                         errors = errors
                     };

                     return new BadRequestObjectResult(response);
                 }
                         );


            return services;
        }
        private static IServiceCollection IConnectionMultiplexer(this IServiceCollection services)
        {

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse("localhost:6379", true);
                configuration.AbortOnConnectFail = false; // لتجنب الانهيار لو السيرفر لسه بيبدأ
                return ConnectionMultiplexer.Connect(configuration);
            });
            return services;
        }
        private static IServiceCollection AddingIDentityService(this IServiceCollection services)
        {

            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<TourDbContext>();
            return services;
        }
         private static IServiceCollection AddingAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"])
                    ),
                };

                // ======== أهم تعديل: نقرأ التوكن من الكوكي ========
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Cookies["jwt"];

                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            // ❗ لازم ترجّع الـ services
            return services;
        }


    }
}
