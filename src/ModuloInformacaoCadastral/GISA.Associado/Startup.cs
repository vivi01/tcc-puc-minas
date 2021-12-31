using GISA.Associado.Extensions;
using GISA.Associado.Repositorios;
using GISA.Associado.Repositorios.Interfaces;
using GISA.Associado.Servicos;
using GISA.Associado.Servicos.Interfaces;
using GISA.EventBusRabbitMQ;
using GISA.EventBusRabbitMQ.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GISA.Associado", Version = "v1" });
            });

            services.AddTransient<IAssociadoService, AssociadoService>();
            services.AddTransient<IAssociadoRepository, AssociadoRepository>();

            #region RabbitMQ Dependencies

            var hostName = Configuration["EventBus:HostName"];
            var userName = string.Empty;
            var password = string.Empty;

            if (!string.IsNullOrEmpty(Configuration["EventBus:UserName"]))
            {
                userName = Configuration["EventBus:UserName"];
            }

            if (!string.IsNullOrEmpty(Configuration["EventBus:Password"]))
            {
               password = Configuration["EventBus:Password"];
            }

            services.AddSingleton(sp => RabbitHutch.CreateBus(hostName, userName, password));           

            services.AddSingleton<IBus, RabbitBus>();          

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

          //  app.UseRabbitListener();
        }
    }
}
