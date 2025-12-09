
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourSite.APIs.Errors;
using TourSite.APIs.Helper;
using TourSite.APIs.MidleWare;
using TourSite.Core;
using TourSite.Core.Mapping;
using TourSite.Core.Servicies.Contract;
using TourSite.Repository.Data;
using TourSite.Repository.Data.Contexts;
using TourSite.Repository.Repositories;
using TourSite.Service.Services.CatTours;
using TourSite.Service.Services.Destnations;
using TourSite.Service.Services.Emails;

using TourSite.Service.Services.TourImgs;
using TourSite.Service.Services.Tours;
using TourSite.Service.Services.Trasnfers;
using TourSite.Service.Services.Users;

namespace TourSite.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });

            builder.Services.AddDependency( builder.Configuration);


            var app = builder.Build();


            await app.UseConfigurationMiddleWare();


            app.Run();
        }
    }
}
