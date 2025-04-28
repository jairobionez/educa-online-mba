using EducaOnline.Aluno.Data.Repository;
using EducaOnline.Aluno.Domain;
using EducaOnline.Conteudo.Application.Services;
using EducaOnline.Conteudo.Data.Repository;
using EducaOnline.Conteudo.Domain;
using EducaOnline.Conteudo.Domain.Services;
using EducaOnline.Core.Communication;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using EducaOnline.Financeiro.Data.Repository;
using EducaOnline.Financeiro.Domain;
using MediatR;

namespace EducaOnline.Api.Configurations
{
    public static class DependencyConfig
    {
        public static IServiceCollection AddDependencyConfig(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Aluno
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<IAlunoService, AlunoService>();

            // Conteudo
            services.AddScoped<IConteudoAppService, ConteudoAppService>();
            services.AddScoped<IConteudoService, ConteudoService>();
            services.AddScoped<IConteudoRepository, ConteudoRepository>();

            // Financeiro
            services.AddScoped<IFinanceiroRepository, FinanceiroRepository>();

            // Notificacoes
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();


            return services;
        }
    }
}
