using Lugat.Brokers.Storages;
using Lugat.Services.Foundations.Bolimlar;
using Lugat.Services.Foundations.Categories;
using Lugat.Services.Foundations.Words;
using Lugat.Services.Orchestrations.BolimsWords;
using Lugat.Services.Orchestrations.CategorySections;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        BrokersMethod(builder);
        FoundationsMethod(builder);
        OrchestrationsMethod(builder);
        var app = builder.Build();

        app.UseStaticFiles();

        app.MapControllerRoute(name: "def",
            pattern: "{controller=Home}/{action=Index}/{id?}");

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