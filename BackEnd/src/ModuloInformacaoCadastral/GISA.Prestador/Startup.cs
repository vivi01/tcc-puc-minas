using GISA.Prestador.Configuration;
using GISA.Prestador.Context;
using GISA.Prestador.Repositories;
using GISA.Prestador.Repositories.Interfaces;
using GISA.Prestador.Services;
using GISA.Prestador.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GISA.Prestador
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GISA.Prestador", Version = "v1" });
            });

            //Registro Services
            services.AddSingleton<IEnderecoService, EnderecoService>();
            services.AddSingleton<IPlanoService, PlanoService>();
            services.AddSingleton<IPrestadorService, PrestadorService>();

            //Registro Repositories
            services.AddSingleton<IEnderecoRepository, EnderecoRepository>();
            services.AddSingleton<IPlanoRepository, PlanoRepository>();
            services.AddSingleton<IPrestadorRepository, PrestadorRepository>();

            //ConnectionStrings
            services.AddSingleton<PrestadorContext>();

            services.AddDbContext<PrestadorContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")), optionsLifetime: ServiceLifetime.Singleton);

            #region RabbitMQ Dependencies

            services.AddMessageBusConfiguration(Configuration);

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GISA.Prestador v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
