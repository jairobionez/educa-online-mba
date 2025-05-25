using EducaOnline.Aluno.Data;
using EducaOnline.Api.Data;
using EducaOnline.Api.Middlewares;
using EducaOnline.Conteudo.Data;
using EducaOnline.Financeiro.Data;
using Microsoft.EntityFrameworkCore;

namespace EducaOnline.Api.Configurations
{
    public static class ApiConfig
    {
        public static void AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AlunoDbContext>(options =>
               options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ConteudoDbContext>(options =>
               options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<PagamentoDbContext>(options =>
              options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers()
                  .AddJsonOptions(options =>
                  {
                      options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                  });

            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                         .WithOrigins("http://localhost:4200")
                         .AllowAnyMethod()
                         .AllowAnyHeader()
                         .AllowCredentials()
                         .WithExposedHeaders("X-Pagination"));
            });

            services.AddSwaggerGen();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerConfig();
            services.AddAutoMapper(typeof(Program));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
        }

        public static void UseApiConfig(this WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerConfig();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware(typeof(ExceptionMiddleware));
            app.UseRouting();

            app.UseCors("Total");

            app.UseIdentityConfig();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
