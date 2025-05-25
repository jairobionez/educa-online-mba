using EducaOnline.Aluno.Data;
using EducaOnline.Api.Data;
using EducaOnline.Api.Models;
using EducaOnline.Conteudo.Data;
using EducaOnline.Conteudo.Domain;
using EducaOnline.Conteudo.Domain.ValueObjects;
using EducaOnline.Core.Enums;
using EducaOnline.Financeiro.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace EducaOnline.Api.Configurations
{
    public static class SeedConfig
    {
        public static void AddSeedConfig(this IServiceCollection services, IConfiguration configuration)
        {
            IServiceProvider provider = services.BuildServiceProvider();

            using (var serviceScope = provider?.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var passwordHash = serviceScope.ServiceProvider.GetRequiredService<IPasswordHasher<IdentityUser>>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


                var appContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                appContext.Database.Migrate();

                var alunoContext = serviceScope.ServiceProvider.GetRequiredService<AlunoDbContext>();
                alunoContext.Database.Migrate();

                var conteudoContext = serviceScope.ServiceProvider.GetRequiredService<ConteudoDbContext>();
                conteudoContext.Database.Migrate();

                var pagamentoContext = serviceScope.ServiceProvider.GetRequiredService<PagamentoDbContext>();
                pagamentoContext.Database.Migrate();

                // AppDbContext

                #region adm
                var usuarioDb = userManager.FindByEmailAsync("admin@educaonline.com.br").Result;
                if (usuarioDb == null)
                {
                    var identityUser = new IdentityUser("admin@educaonline.com.br");
                    identityUser.Email = "admin@educaonline.com.br";

                    var result = userManager.CreateAsync(identityUser).Result;

                    if (result.Succeeded)
                    {
                        var admininstrador = new Administrador(Guid.Parse(identityUser.Id), "Educa online ADM", "admin@educaonline.com.br");
                        appContext.Set<Administrador>().Add(admininstrador);
                        appContext.SaveChanges();

                        if (admininstrador != null)
                        {
                            CreateRoles(roleManager).Wait();

                            userManager.AddToRoleAsync(identityUser, PerfilUsuarioEnum.ADM.ToString()).Wait();
                        }


                        var hash = passwordHash.HashPassword(identityUser, "Teste@123");
                        identityUser.SecurityStamp = Guid.NewGuid().ToString();
                        identityUser.PasswordHash = hash;
                        userManager.UpdateAsync(identityUser).Wait();
                    }
                }
                #endregion


                #region Conteúdo

                var curso = conteudoContext.Cursos.FirstOrDefault(p => p.Id == Guid.Parse("04effc8b-fa4a-415c-90eb-95cdfdaba1b2"));

                if(curso == null)
                {
                    var aulas = new List<Aula>()
                    {
                        new Aula("Aula 01", "Descrição 01", 10),
                        new Aula("Aula 02", "Descrição 02", 10)
                    };

                    var conteudoProgramatico = new ConteudoProgramatico("Conteúdo inicial", "Conteúdo inicial alimento pelo seed", 20, "Lorem Ipsum is simply dummy text of the printing and typesetting industry.");
                    curso = new Conteudo.Domain.Curso(Guid.Parse("04effc8b-fa4a-415c-90eb-95cdfdaba1b2"), "Curso Inicial", conteudoProgramatico, true);
                    
                    aulas.ForEach(aula => curso.AdicionarAula(aula));

                    conteudoContext.Cursos.Add(curso);
                    conteudoContext.SaveChanges();
                }

                #endregion

                #region aluno
                var usuarioDbAluno = userManager.FindByEmailAsync("aluno@educaonline.com.br").Result;

                if (usuarioDbAluno == null)
                {
                    var identityUser = new IdentityUser("aluno@educaonline.com.br");
                    identityUser.Email = "aluno@educaonline.com.br";

                    var result = userManager.CreateAsync(identityUser).Result;

                    if (result.Succeeded)
                    {
                        var aluno = new Aluno.Domain.Aluno(Guid.Parse(identityUser.Id), "Jairo Bionez", "aluno@educaonline.com.br");
                        aluno.CriarHistorico(0);

                        alunoContext.Set<Aluno.Domain.Aluno>().Add(aluno);
                        alunoContext.SaveChanges();

                        if (aluno != null)
                        {
                            CreateRoles(roleManager).Wait();

                            userManager.AddToRoleAsync(identityUser, PerfilUsuarioEnum.ALUNO.ToString()).Wait();
                        }


                        var hash = passwordHash.HashPassword(identityUser, "Teste@123");
                        identityUser.SecurityStamp = Guid.NewGuid().ToString();
                        identityUser.PasswordHash = hash;
                        userManager.UpdateAsync(identityUser).Wait();
                    }
                }
                #endregion
            }
        }


        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] rolesNames = {
                PerfilUsuarioEnum.ADM.ToString(),
                PerfilUsuarioEnum.ALUNO.ToString(),
            };

            foreach (var namesRole in rolesNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(namesRole);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(namesRole));
                }
            }
        }
    }
}
