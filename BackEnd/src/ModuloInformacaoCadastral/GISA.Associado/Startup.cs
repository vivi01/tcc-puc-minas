using GISA.Associado.Configuration;
using GISA.Associado.Context;
using GISA.Associado.Repositories;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services;
using GISA.Associado.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GISA.Associado
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
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GISA.Associado", Version = "v1" });
            });

            //Registro Services
            services.AddSingleton<IAssociadoService, AssociadoService>();
            services.AddSingleton<IEnderecoService, EnderecoService>();
            services.AddSingleton<IMarcacaoExameService, MarcacaoExameService>();
            services.AddSingleton<IPlanoService, PlanoService>();

            //Registro Repositories
            services.AddSingleton<IAssociadoRepository, AssociadoRepository>();
            services.AddSingleton<IEnderecoRepository, EnderecoRepository>();
            services.AddSingleton<IMarcacaoExameRepository, MarcacaoExameRepository>();
            services.AddSingleton<IPlanoRepository, PlanoRepository>();

            //ConnectionStrings

            services.AddSingleton<AssociadoContext>();

            services.AddDbContext<AssociadoContext>(options => options.UseSqlServer(
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GISA.Associado v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("EnableCORS");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });            
        }
    }
}
