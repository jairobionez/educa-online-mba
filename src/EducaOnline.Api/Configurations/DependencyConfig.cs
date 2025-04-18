using EducaOnline.Aluno.Application.Services;
using EducaOnline.Aluno.Data.Repository;
using EducaOnline.Aluno.Domain;
using EducaOnline.Core.Communication;
using EducaOnline.Core.Data.EventSourcing;
using EventSourcing;

namespace EducaOnline.Api.Configurations
{
    public static class DependencyConfig
    {
        public static IServiceCollection AddDependencyConfig(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Event Sourcing
            services.AddSingleton<IEventStoreService, EventStoreService>();
            services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();

            //Aluno
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<IAlunoAppService, AlunoAppService>();


            return services;
        }
    }
}
