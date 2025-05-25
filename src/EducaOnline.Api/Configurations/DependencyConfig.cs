using EducaOnline.Aluno.AntiCorruption;
using EducaOnline.Aluno.Application.Commands;
using EducaOnline.Aluno.Application.Events;
using EducaOnline.Aluno.Application.Queries;
using EducaOnline.Aluno.Data.Repository;
using EducaOnline.Aluno.Domain;
using EducaOnline.Conteudo.Application.Services;
using EducaOnline.Conteudo.Data.Repository;
using EducaOnline.Conteudo.Domain;
using EducaOnline.Conteudo.Domain.Services;
using EducaOnline.Core.Communication;
using EducaOnline.Core.Messages.CommonMessages.IntegrationEvents;
using EducaOnline.Core.Messages.CommonMessages.Notifications;
using EducaOnline.Financeiro.Application.Commands;
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
            services.AddScoped<IAulaService, AulaService>();
            services.AddScoped<IAlunoQueries, AlunoQueries>();

            services.AddScoped<IRequestHandler<AdicionarAlunoCommand, bool>, AlunoCommandHandler>();
            services.AddScoped<IRequestHandler<ConcluirAulaCommand, bool>, AlunoCommandHandler>();
            services.AddScoped<IRequestHandler<EmitirCertificadoCommand, bool>, AlunoCommandHandler>();
            services.AddScoped<IRequestHandler<RealizarMatriculaCommand, bool>, AlunoCommandHandler>();
            services.AddScoped<INotificationHandler<PagamentoMatriculaEvent>, AlunoIntegrationEventHandler>();


            // Conteudo
            services.AddScoped<IConteudoAppService, ConteudoAppService>();
            services.AddScoped<IConteudoService, ConteudoService>();
            services.AddScoped<IConteudoRepository, ConteudoRepository>();

            // Financeiro
            services.AddScoped<IFinanceiroRepository, FinanceiroRepository>();
            services.AddScoped<IRequestHandler<RealizarPagamentoMatriculaCommand, bool>, FinanceiroCommandHandler>();

            // Notificacoes
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();


            return services;
        }
    }
}
