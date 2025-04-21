using EducaOnline.Aluno.Data.Repository;
using EducaOnline.Aluno.Domain;
using EducaOnline.Core.Communication;
using EducaOnline.Core.Data.EventSourcing;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using EventSourcing;
using MediatR;

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
            services.AddScoped<IAlunoService, AlunoService>();
            services.AddScoped<IAlunoRepository, AlunoRepository>();

            // Notificacoes
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();


            return services;
        }
    }
}
