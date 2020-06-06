using PUC.LDSI.Application.Interfaces;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUC.LDSI.Application.AppServices
{
    public class TurmaAppService : ITurmaAppService
    {
        private readonly ITurmaService _turmaService;

        public TurmaAppService(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        public async Task<DataResult<int>> AdicionarTurmaAsync(string descricao)
        {
            try
            {
                var retorno = await _turmaService.AdicionarTurmaAsync(descricao);

                return new DataResult<int>(retorno);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ex);
            }
        }

        public async Task<DataResult<int>> AlterarTurmaAsync(int id, string descricao)
        {
            try
            {
                var retorno = await _turmaService.AlterarTurmaAsync(id, descricao);

                return new DataResult<int>(retorno);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ex);
            }
        }

        public async Task<DataResult<int>> ExcluirAsync(int id)
        {
            try
            {
                await _turmaService.ExcluirAsync(id);

                return new DataResult<int>(1);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ex);
            }
        }

        public DataResult<List<Turma>> ListarTurmas()
        {
            try
            {
                var retorno = _turmaService.ListarTurmas();

                return new DataResult<List<Turma>>(retorno);
            }
            catch (Exception ex)
            {
                return new DataResult<List<Turma>>(ex);
            }
        }

        public async Task<DataResult<Turma>> ObterAsync(int id)
        {
            try
            {
                var retorno = await _turmaService.ObterAsync(id);

                return new DataResult<Turma>(retorno);
            }
            catch (Exception ex)
            {
                return new DataResult<Turma>(ex);
            }
        }

        public async Task<DataResult<int>> IncluirAlunoAsync(int turmaId, string nomeAluno)
        {
            try
            {
                var retorno = await _turmaService.IncluirAlunoAsync(turmaId, nomeAluno);

                return new DataResult<int>(retorno);
            }
            catch (Exception ex)
            {
                return new DataResult<int>(ex);
            }
        }
    }
}
