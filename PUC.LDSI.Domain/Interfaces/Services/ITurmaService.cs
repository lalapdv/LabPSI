using PUC.LDSI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Interfaces.Services
{
    public interface ITurmaService
    {
        Task<int> AdicionarTurmaAsync(string descricao);
        Task<int> AlterarTurmaAsync(int id, string descricao);
        List<Turma> ListarTurmas();
        Task<Turma> ObterAsync(int id);
        Task ExcluirAsync(int id);
        Task<int> IncluirAlunoAsync(int turmaId, string nomeAluno);
    }
}
