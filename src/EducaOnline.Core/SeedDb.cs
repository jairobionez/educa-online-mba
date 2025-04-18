using EducaOnline.Core.Data;
using EducaOnline.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EducaOnline.Core
{
    public static class SeedDb
    {
        public static void DbInitializer(this IServiceCollection services)
        {
            IServiceProvider provider = services.BuildServiceProvider();

            using (var serviceScope = provider?.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                context.Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var passwordHash = serviceScope.ServiceProvider.GetRequiredService<IPasswordHasher<IdentityUser>>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                //var usuarioRepository = serviceScope.ServiceProvider.GetRequiredService<IUsuarioRepository>();
                //var publicacaoRepository = serviceScope.ServiceProvider.GetRequiredService<IPublicacaoRepository>();

                var alunoDb = userManager.FindByEmailAsync("jairo@educa.com.br").Result;

                if (alunoDb is null)
                {
                    var identityUser = new IdentityUser("jairo@educa.com.br");
                    identityUser.PasswordHash = passwordHash.HashPassword(identityUser, "Teste@123");
                    identityUser.Email = "jairo@educa.com.br";

                    var identityResult = userManager.CreateAsync(identityUser).Result;

                    if (identityResult.Succeeded)
                    {
                        CreateRoles(roleManager).Wait();
                        userManager.AddToRoleAsync(identityUser, nameof(PerfilUsuarioEnum.ALUNO)).Wait();

                        //var usuario = new Usuario(Guid.Parse(identityUser.Id), "Jairo", "Bionez", "jairo@devgram.com.br");
                        //usuarioRepository.CreateAsync(usuario).Wait();

                        //var hash = passwordHash.HashPassword(identityUser, "Teste@123");
                        //identityUser.SecurityStamp = Guid.NewGuid().ToString();
                        //identityUser.PasswordHash = hash;
                        //userManager.UpdateAsync(identityUser).Wait();
                    }
                }

                var adminDb = userManager.FindByEmailAsync("admin@educa.com.br").Result;

                if (adminDb is null)
                {
                    var identityUser = new IdentityUser("admin@educa.com.br");
                    identityUser.PasswordHash = passwordHash.HashPassword(identityUser, "Teste@123");
                    identityUser.Email = "admin@educa.com.br";

                    var identityResult = userManager.CreateAsync(identityUser).Result;

                    if (identityResult.Succeeded)
                    {
                        CreateRoles(roleManager).Wait();
                        userManager.AddToRoleAsync(identityUser, nameof(PerfilUsuarioEnum.ADM)).Wait();

                        //var usuario = new Usuario(Guid.Parse(identityUser.Id), "Admin", "Geral", "admin@devgram.com.br");
                        //usuarioRepository.CreateAsync(usuario).Wait();

                        //var hash = passwordHash.HashPassword(identityUser, "Teste@123");
                        //identityUser.SecurityStamp = Guid.NewGuid().ToString();
                        //identityUser.PasswordHash = hash;
                        //userManager.UpdateAsync(identityUser).Wait();
                    }
                }
            }
        }

        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] rolesNames =
            {
                nameof(PerfilUsuarioEnum.ADM),
                nameof(PerfilUsuarioEnum.ALUNO),
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
