using GISA.EventBusRabbitMQ;
using GISA.EventBusRabbitMQ.Extensions;
using GISA.EventBusRabbitMQ.Interfaces;
using GISA.EventBusRabbitMQ.Settings;
using GISA.Prestador.Context;
using GISA.Prestador.Repositories;
using GISA.Prestador.Repositories.Interfaces;
using GISA.Prestador.Services;
using GISA.Prestador.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;

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
            services.AddMediatR(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GISA.Prestador", Version = "v1" });
            });

            //Registro Services
            services.AddTransient<IEnderecoService, EnderecoService>();
            services.AddTransient<IPlanoService, PlanoService>();
            services.AddTransient<IPrestadorService, PrestadorService>();

            //Registro Repositories
            services.AddTransient<IEnderecoRepository, EnderecoRepository>();
            services.AddTransient<IPlanoRepository, PlanoRepository>();
            services.AddTransient<IPrestadorRepository, PrestadorRepository>();

            //ConnectionStrings
            services.AddDbContext<PrestadorContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("PrestadorConnection")));

            var rabbitConfig = Configuration.GetSection("rabbit");

            services.Configure<RabbitOptionsSettings>(rabbitConfig);

            services.AddSingleton<RabbitOptionsSettings>(sp =>
               sp.GetRequiredService<IOptions<RabbitOptionsSettings>>().Value);

            services.AddRabbit(Configuration);

            #region RabbitMQ Dependencies

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
