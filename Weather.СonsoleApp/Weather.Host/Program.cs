using AppConfiguration.AppConfig;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Weather.BL.Mapping;
using Weather.DataAccess.Configuration;
using Weather.Host.Extension;
using AppConfiguration.Constants;
using Hangfire;
using Weather.BL.Configuration;

public class Program
{
    public static void Main(string[] args)
        => CreateHostBuilder(args).Build().Run();

    // EF Core uses this method at design time to access the DbContext
    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>());
}

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = Configuration.GetConnectionString(Connection.ConnectionString);
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);
        services.Configure<BackgroundJobConfiguration>(Configuration.GetSection(nameof(BackgroundJobConfiguration)));
        services.Configure<RouteOptions>(o => o.LowercaseUrls = true);

        services.AddStartupFilter();
        services.AddRepositories();
        services.AddServices();
        services.AddAutoMapper();
        services.AddLogging(x => x.AddSerilog());
        services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
        services.AddHangfireServer();
        services.AddLogging(opt => opt.AddSimpleConsole());

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "WeatherApi", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherApi v1"));
           app.UseHangfireDashboard("/dashboard");
        }

        app.UseRouting();

        app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}