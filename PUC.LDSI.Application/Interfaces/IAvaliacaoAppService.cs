using System;
using System.Threading.Tasks;

namespace PUC.LDSI.Application.Interfaces
{
    public interface IAvaliacaoAppService
    {
        Task<DataResult<int>> AdicionarAvaliacaoAsync(int professorId, string disciplina, string materia, string descricao);
        Task<DataResult<int>> AdicionarQuestaoAvaliacaoAsync(int avaliacaoId, int tipo, string enunciado);
        Task<DataResult<int>> AdicionarOpcaoAvaliacaoAsync(int questaoId, string descricao, bool verdadeira);

        Task<DataResult<int>> AlterarAvaliacaoAsync(int id, string disciplina, string materia, string descricao);
        Task<DataResult<int>> AlterarQuestaoAvaliacaoAsync(int id, int tipo, string enunciado);
        Task<DataResult<int>> AlterarOpcaoAvaliacaoAsync(int id, string descricao, bool verdadeira);

        Task<DataResult<int>> ExcluirAvaliacaoAsync(int id);
        Task<DataResult<int>> ExcluirQuestaoAvaliacaoAsync(int id);
        Task<DataResult<int>> ExcluirOpcaoAvaliacaoAsync(int id);

        Task<DataResult<int>> AdicionarPublicacaoAsync(int professorId, int avaliacaoId, int turmaId, DateTime dataInicio, DateTime dataFim, int valorProva);
        Task<DataResult<int>> AlterarPublicacaoAsync(int professorId, int id, DateTime dataInicio, DateTime dataFim, int valorProva);
        Task<DataResult<int>> ExcluirPublicacaoAsync(int professorId, int id);
    }
}
