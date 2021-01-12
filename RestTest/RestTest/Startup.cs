using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestTest.Filters;
using RestTest.Models;
using Microsoft.EntityFrameworkCore;
using RestTest.Repository;
using RestTest.Models.DataManager;



namespace RestTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
     
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();

     
            //Adds an intercept filter to the Rest Api
            services.AddMvc( options =>
                 {
                     options.Filters.Add<JsonExceptionFilter>();
                 })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //allows lowercase routes
            services.AddRouting(options => options.LowercaseUrls = true);

            //Set up the database connection
            services.AddDbContext<EmployeeContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            //Setup Repository
            services.AddScoped<IDataRepository<Employee>, EmployeeManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
 
            app.UseCors(x => x
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .SetIsOriginAllowed(origin => true) // allow any origin
                       .AllowCredentials()); // allow credentials
            app.UseMvc();
        }
    }
}
