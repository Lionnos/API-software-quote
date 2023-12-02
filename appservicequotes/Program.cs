using appservicequotes.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

namespace appservicequotes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //  ==================== ADD
            AppSettings.Init();
            //  ====================

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();

            //  ====================
            builder.Services.AddControllers();

            builder.Services.Configure<ApiBehaviorOptions>( options => options.SuppressModelStateInvalidFilter = true );

            IdentityModelEventSource.ShowPII = true;

            builder.Services.AddCors( options =>
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(AppSettings.GetOriginRequest().Split(','))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowedToAllowWildcardSubdomains();
                })
            );

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = "API_Rest",
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.GetJwtSecret()))
                };
            });
            /*  builder.Services.AddMvc()
                    .AddJsonOptions(
                        options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );  */
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            //  ====================

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API de cotización de desarrollo de software",
                    Description = "",
                    //TermsOfService = new Uri("https://github.com/Lionnos/SoftwareProjectQuote.git"),
                    Contact = new OpenApiContact
                    {
                        Name = "Git Hub",
                        Url = new Uri("https://github.com/Lionnos/SoftwareProjectQuote.git")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Licencse",
                        Url = new Uri("https://codideep.com/curso/verporcodigocurso/201905180000001")
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                });
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    //options.RoutePrefix = string.Empty;
                });
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();

            //  ===================
            app.MapControllers();
            app.UseCors("default");
            app.UseAuthentication();
            //  ===================

            app.Run();
        }
    }
}
