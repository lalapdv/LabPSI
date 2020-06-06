using PUC.LDSI.Application.Interfaces;
using PUC.LDSI.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace PUC.LDSI.Application.AppServices
{
    public class AvaliacaoAppService : IAvaliacaoAppService
    {
        private readonly IAvaliacaoService _avaliacaoService;

        public AvaliacaoAppService(IAvaliacaoService avaliacaoService)
        {
            _avaliacaoService = avaliacaoService;
        }

        public async Task<DataResult<int>> AdicionarAvaliacaoAsync(int professorId, string disciplina, string materia, string descricao)
        {
            try
            {
                var retorno = await _avaliacaoService.AdicionarAvaliacaoAsync(professorId, disciplina, materia, descricao);

                return new DataResult<int>(retorno);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ex);
            }
        }

        public async Task<DataResult<int>> AdicionarOpcaoAvaliacaoAsync(int questaoId, string descricao, bool verdadeira)
        {
            try
            {
                var retorno = await _avaliacaoService.AdicionarOpcaoAvaliacaoAsync(questaoId, descricao, verdadeira);

                return new DataResult<int>(retorno);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ex);
            }
        }

        public async Task<DataResult<int>> AdicionarQuestaoAvaliacaoAsync(int avaliacaoId, int tipo, string enunciado)
        {
            try
            {
                var retorno = await _avaliacaoService.AdicionarQuestaoAvaliacaoAsync(avaliacaoId, tipo, enunciado);

                return new DataResult<int>(retorno);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ex);
            }
        }

        public async Task<DataResult<int>> AlterarAvaliacaoAsync(int id, string disciplina, string materia, string descricao)
        {
            try
            {
                var retorno = await _avaliacaoService.AlterarAvaliacaoAsync(id, disciplina, materia, descricao);

                return new DataResult<int>(retorno);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ex);
            }
        }

        public async Task<DataResult<int>> AlterarOpcaoAvaliacaoAsync(int id, string descricao, bool verdadeira)
        {
            try
            {
                var retorno = await _avaliacaoService.AlterarOpcaoAvaliacaoAsync(id, descricao, verdadeira);

                return new DataResult<int>(retorno);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ex);
            }
        }

        public async Task<DataResult<int>> AlterarQuestaoAvaliacaoAsync(int id, int tipo, string enunciado)
        {
            try
            {
                var retorno = await _avaliacaoService.AlterarQuestaoAvaliacaoAsync(id, tipo, enunciado);

                return new DataResult<int>(retorno);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ex);
            }
        }

        public async Task<DataResult<int>> ExcluirAvaliacaoAsync(int id)
        {
            try
            {
                await _avaliacaoService.ExcluirAvaliacaoAsync(id);

                return new DataResult<int>(1);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ex);
            }
        }

        public async Task<DataResult<int>> ExcluirOpcaoAvaliacaoAsync(int id)
        {
            try
            {
                var retorno = await _avaliacaoService.ExcluirOpcaoAvaliacaoAsync(id);

                return new DataResult<int>(retorno);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ex);
            }
        }

        public async Task<DataResult<int>> ExcluirQuestaoAvaliacaoAsync(int id)
        {
            try
            {
                var retorno = await _avaliacaoService.ExcluirQuestaoAvaliacaoAsync(id);

                return new DataResult<int>(retorno);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ex);
            }
        }

        public async Task<DataResult<int>> AdicionarPublicacaoAsync(int professorId, int avaliacaoId, int turmaId, DateTime dataInicio, DateTime dataFim, int valorProva)
        {
            try
            {
                var retorno = await _avaliacaoService.AdicionarPublicacaoAsync(professorId, avaliacaoId, turmaId, dataInicio, dataFim, valorProva);

                return new DataResult<int>(retorno);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ex);
            }
        }

        public async Task<DataResult<int>> AlterarPublicacaoAsync(int professorId, int id, DateTime dataInicio, DateTime dataFim, int valorProva)
        {
            try
            {
                var retorno = await _avaliacaoService.AlterarPublicacaoAsync(professorId, id, dataInicio, dataFim, valorProva);

                return new DataResult<int>(retorno);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ex);
            }
        }

        public async Task<DataResult<int>> ExcluirPublicacaoAsync(int professorId, int id)
        {
            try
            {
                var retorno = await _avaliacaoService.ExcluirPublicacaoAsync(professorId, id);

                return new DataResult<int>(retorno);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ex);
            }
        }
    }
}
