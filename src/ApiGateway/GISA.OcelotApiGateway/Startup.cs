using GISA.OcelotApiGateway.Data;
using GISA.OcelotApiGateway.Data.Interfaces;
using GISA.OcelotApiGateway.Repositories;
using GISA.OcelotApiGateway.Repositories.Interfaces;
using GISA.OcelotApiGateway.Services;
using GISA.OcelotApiGateway.Services.Interfaces;
using GISA.OcelotApiGateway.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Text;

namespace GISA.OcelotApiGateway
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDataProtection();            

            services.Configure<AuthDatabaseSettings>(Configuration.GetSection(nameof(AuthDatabaseSettings)));

            services.AddSingleton<IAuthDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<AuthDatabaseSettings>>().Value);

            //repositories
            services.AddTransient<IAuthContext, AuthContext>();
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            //services
            services.AddTransient<ITokenService, TokenService>();            

            services.AddCors();

            services.AddAuthentication()
                .AddJwtBearer("associados_auth_scheme", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthSettings.AssociadoSecret)),
                        ValidAudience = "associadosAudience",
                        ValidIssuer = "associadosIssuer",
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                }).AddJwtBearer("prestador_auth_scheme", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthSettings.PrestadorSecret)),
                        ValidAudience = "prestadorAudience",
                        ValidIssuer = "prestadorIssuer",
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                })
                .AddJwtBearer("conveniado_auth_scheme", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthSettings.ConveniadoSecret)),
                        ValidAudience = "conveniadoAudience",
                        ValidIssuer = "conveniadoIssuer",
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                })
                .AddJwtBearer("comunicacaoLegado_auth_scheme", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthSettings.AcessoLegadoSecret)),
                        ValidAudience = "comunicacaoLegadoAudience",
                        ValidIssuer = "comunicacaoLegadoIssuer",
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddOcelot();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GISA.OcelotApiGateway", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GISA.OcelotApiGateway v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //ocelot
            await app.UseOcelot();
        }
    }
}
