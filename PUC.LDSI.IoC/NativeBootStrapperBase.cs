using Microsoft.Extensions.DependencyInjection;
using PUC.LDSI.Application.AppServices;
using PUC.LDSI.Application.Interfaces;
using PUC.LDSI.DataBase.Repository;
using PUC.LDSI.Domain.Interfaces.Repository;
using PUC.LDSI.Domain.Interfaces.Services;
using PUC.LDSI.Domain.Services;

namespace PUC.LDSI.IoC
{
    public abstract class NativeBootStrapperBase
    {
        public static void RegisterServices(IServiceCollection services) {
            //Application
            services.AddScoped<ITurmaAppService, TurmaAppService>();
            services.AddScoped<IProfessorAppService, ProfessorAppService>();
            services.AddScoped<IAvaliacaoAppService, AvaliacaoAppService>();

            //Domain - Repository
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
            services.AddScoped<IOpcaoAvaliacaoRepository, OpcaoAvaliacaoRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IQuestaoAvaliacaoRepository, QuestaoAvaliacaoRepository>();
            services.AddScoped<ITurmaRepository, TurmaRepository>();
            services.AddScoped<IPublicacaoRepository, PublicacaoRepository>();

            //Domain - Services
            services.AddScoped<ITurmaService, TurmaService>();
            services.AddScoped<IProfessorService, ProfessorService>();
            services.AddScoped<IAvaliacaoService, AvaliacaoService>();
        }
    }
}
