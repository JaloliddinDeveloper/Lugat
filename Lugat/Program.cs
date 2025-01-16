//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Lugat.Brokers.Storages;
using Lugat.Services.Foundations.Bolimlar;
using Lugat.Services.Foundations.Categories;
using Lugat.Services.Foundations.Words;
using Lugat.Services.Orchestrations.BolimsWords;
using Lugat.Services.Orchestrations.CategorySections;
using System.Net;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Listen(IPAddress.Any, 5001);  
        });

        builder.Services.AddControllersWithViews();
        BrokersMethod(builder);
        FoundationsMethod(builder);
        OrchestrationsMethod(builder);
        var app = builder.Build();

        app.UsePathBase("/lugat");

        app.UseStaticFiles();

        app.MapControllerRoute(name: "def",
            pattern: "{controller=Admin}/{action=Index}/{id?}");

        app.Run();
    }

    private static void BrokersMethod(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IStorageBroker, StorageBroker>();
    }

    private static void OrchestrationsMethod(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IRetrieveBolimWithWords, RetrieveBolimWithWords>();
        builder.Services.AddTransient<ICategorysWithSections, CategorysWithSections>();
    }

    private static void FoundationsMethod(WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ICategoryService, CategoryService>();
        builder.Services.AddTransient<IBolimService, BolimService>();
        builder.Services.AddTransient<IWordService, WordService>();
    }
}