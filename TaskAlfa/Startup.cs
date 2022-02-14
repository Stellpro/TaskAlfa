using AlfaControlingDb;
using MatBlazor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskAlfa.Data.Services;
using TaskAlfa.Data.SharedServices;
using TaskAlfa.SharedServices;
using TaskDb;

namespace TaskAlfa
{
        
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _hostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<TaskService>();
            services.AddScoped<SystemPageService>();
            services.AddScoped<TaskStatusService>();
            services.AddMatBlazor();
            services.AddSingleton<ILogger, Logger>();
            services.AddSingleton<ILoggerProvider, LoggerProvider>();
            services.AddSingleton<IPushNotificationsQueue, PushNotificationsQueue>();
            services.AddHostedService<PushNotificationsDequeuer>();
            services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
            services.AddHttpContextAccessor();

            services.AddHttpClient();
            services.AddDbContext<TaskDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AlfaControllingContext")));//Controlling

            services.AddDbContext<ControllingDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("AlfaContext")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
