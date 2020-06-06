using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Exception;
using PUC.LDSI.Domain.Interfaces.Repository;
using PUC.LDSI.Domain.Interfaces.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.Domain.Services
{
    public class AvaliacaoService : IAvaliacaoService
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IPublicacaoRepository _publicacaoRepository;
        private readonly IOpcaoAvaliacaoRepository _opcaoAvaliacaoRepository;
        private readonly IQuestaoAvaliacaoRepository _questaoAvaliacaoRepository;

        public AvaliacaoService(IAvaliacaoRepository avaliacaoRepository,
                                IPublicacaoRepository publicacaoRepository,
                                IOpcaoAvaliacaoRepository opcaoAvaliacaoRepository,
                                IQuestaoAvaliacaoRepository questaoAvaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
            _publicacaoRepository = publicacaoRepository;
            _opcaoAvaliacaoRepository = opcaoAvaliacaoRepository;
            _questaoAvaliacaoRepository = questaoAvaliacaoRepository;
        }

        public async Task<int> AdicionarAvaliacaoAsync(int professorId, string disciplina, string materia, string descricao)
        {
            var avaliacao = new Avaliacao() { ProfessorId = professorId, Disciplina = disciplina, Materia = materia, Descricao = descricao };

            var erros = avaliacao.Validate();

            if (erros.Length == 0)
            {
                await _avaliacaoRepository.AdicionarAsync(avaliacao);

                _avaliacaoRepository.SaveChanges();

                return avaliacao.Id;
            }
            else throw new DomainException(erros);
        }

        public async Task<int> AdicionarOpcaoAvaliacaoAsync(int questaoId, string descricao, bool verdadeira)
        {
            ValidarOpcaoAvaliacao(questaoId, verdadeira);

            var opcaoAvaliacao = new OpcaoAvaliacao() { QuestaoId = questaoId, Descricao = descricao, Verdadeira = verdadeira };

            var erros = opcaoAvaliacao.Validate();

            if (erros.Length == 0)
            {
                await _opcaoAvaliacaoRepository.AdicionarAsync(opcaoAvaliacao);

                _opcaoAvaliacaoRepository.SaveChanges();

                return opcaoAvaliacao.Id;
            }
            else throw new DomainException(erros);
        }

        public async Task<int> AdicionarQuestaoAvaliacaoAsync(int avaliacaoId, int tipo, string enunciado)
        {
            var questaoAvaliacao = new QuestaoAvaliacao() { AvaliacaoId = avaliacaoId, Tipo = tipo, Enunciado = enunciado };

            var erros = questaoAvaliacao.Validate();

            if (erros.Length == 0)
            {
                await _questaoAvaliacaoRepository.AdicionarAsync(questaoAvaliacao);

                _questaoAvaliacaoRepository.SaveChanges();

                return questaoAvaliacao.Id;
            }
            else throw new DomainException(erros);
        }

        public async Task<int> AlterarAvaliacaoAsync(int id, string disciplina, string materia, string descricao)
        {
            var avaliacao = await _avaliacaoRepository.ObterAsync(id);

            avaliacao.Descricao = descricao;
            avaliacao.Disciplina = disciplina;
            avaliacao.Materia = materia;

            var erros = avaliacao.Validate();

            if (erros.Length == 0)
            {
                _avaliacaoRepository.Modificar(avaliacao);

                return _avaliacaoRepository.SaveChanges();
            }
            else throw new DomainException(erros);
        }

        public async Task<int> AlterarOpcaoAvaliacaoAsync(int id, string descricao, bool verdadeira)
        {
            var opcaoAvaliacao = await _opcaoAvaliacaoRepository.ObterAsync(id);

            ValidarOpcaoAvaliacao(opcaoAvaliacao.QuestaoId, verdadeira);

            opcaoAvaliacao.Descricao = descricao;
            opcaoAvaliacao.Verdadeira = verdadeira;

            var erros = opcaoAvaliacao.Validate();

            if (erros.Length == 0)
            {
                _opcaoAvaliacaoRepository.Modificar(opcaoAvaliacao);

                return _opcaoAvaliacaoRepository.SaveChanges();
            }
            else throw new DomainException(erros);
        }

        public async Task<int> AlterarQuestaoAvaliacaoAsync(int id, int tipo, string enunciado)
        {
            var questaoAvaliacao = await _questaoAvaliacaoRepository.ObterAsync(id);

            questaoAvaliacao.Tipo = tipo;
            questaoAvaliacao.Enunciado = enunciado;

            var erros = questaoAvaliacao.Validate();

            if (erros.Length == 0)
            {
                _questaoAvaliacaoRepository.Modificar(questaoAvaliacao);

                return _questaoAvaliacaoRepository.SaveChanges();
            }
            else throw new DomainException(erros);
        }

        public async Task ExcluirAvaliacaoAsync(int id)
        {
            var avaliacao = await _avaliacaoRepository.ObterAsync(id);

            if (avaliacao.Publicacoes?.Count > 0)
                throw new DomainException("Não é possível excluir uma avaliação que já foi publicada ou realizada!");

            if (avaliacao.Questoes?.Count > 0) 
            {
                foreach (var questao in avaliacao.Questoes) 
                {
                    if (questao.Opcoes.Count > 0) 
                    {
                        foreach (var opcao in questao.Opcoes)
                            _opcaoAvaliacaoRepository.Excluir(opcao.Id);

                        _opcaoAvaliacaoRepository.SaveChanges();
                    }

                    _questaoAvaliacaoRepository.Excluir(questao.Id);
                }

                _questaoAvaliacaoRepository.SaveChanges();
            }

            _avaliacaoRepository.Excluir(id);

            _avaliacaoRepository.SaveChanges();
        }

        public async Task<int> ExcluirOpcaoAvaliacaoAsync(int id)
        {
            var opcaoAvaliacao = await _opcaoAvaliacaoRepository.ObterAsync(id);

            if (opcaoAvaliacao.OpcoesProva?.Count > 0)
                throw new DomainException("Não é possível excluir a opção de uma avaliação que já foi realizada!");

            _opcaoAvaliacaoRepository.Excluir(id);

            _opcaoAvaliacaoRepository.SaveChanges();

            return opcaoAvaliacao.QuestaoId;
        }

        public async Task<int> ExcluirQuestaoAvaliacaoAsync(int id)
        {
            var questaoAvaliacao = await _questaoAvaliacaoRepository.ObterAsync(id);

            if (questaoAvaliacao.QuestoesProva?.Count > 0)
                throw new DomainException("Não é possível excluir a questão de uma avaliação que já foi realizada!");

            if (questaoAvaliacao.Opcoes?.Count > 0)
            {
                foreach (var opcao in questaoAvaliacao.Opcoes)
                {
                    _opcaoAvaliacaoRepository.Excluir(opcao.Id);
                }

                _opcaoAvaliacaoRepository.SaveChanges();
            }

            _questaoAvaliacaoRepository.Excluir(id);

            _questaoAvaliacaoRepository.SaveChanges();

            return questaoAvaliacao.AvaliacaoId;
        }

        private void ValidarOpcaoAvaliacao(int questaoId, bool verdadeira)
        {
            if (verdadeira) {
                var questaoGravada = _questaoAvaliacaoRepository.ObterAsync(questaoId).Result;

                if (questaoGravada.Tipo == 1 && questaoGravada.Opcoes.Where(x => x.Verdadeira).Any())
                    throw new DomainException("Já existe uma opção marcada como verdadeira para essa questão.");
            }
        }

        public async Task<int> AdicionarPublicacaoAsync(int professorId, int avaliacaoId, int turmaId, DateTime dataInicio, DateTime dataFim, int valorProva)
        {
            var publicacao = new Publicacao();

            publicacao.AvaliacaoId = avaliacaoId;
            publicacao.TurmaId = turmaId;
            publicacao.DataInicio = dataInicio;
            publicacao.DataFim = dataFim;
            publicacao.ValorProva = valorProva;

            var erros = publicacao.Validate();

            if (erros.Length == 0)
            {
                var avaliacao = await _avaliacaoRepository.ObterAsync(avaliacaoId);

                if (avaliacao == null)
                    throw new DomainException("A avaliação informada não foi encontrada!");

                if (avaliacao.ProfessorId != professorId)
                    throw new DomainException("A avaliação informada não pertence ao professor logado!");

                if (avaliacao.Publicacoes.Where(x => x.TurmaId == turmaId).Any())
                    throw new DomainException("Essa avaliação já foi publicada para esta turma!");

                if (!avaliacao.Questoes.Any() || avaliacao.Questoes.Where(x => x.Opcoes.Count < 4).Any())
                    throw new DomainException("Essa avaliação não está completa! É necessário que todas as questões tenham ao menos 4 opções!");

                await _publicacaoRepository.AdicionarAsync(publicacao);

                _publicacaoRepository.SaveChanges();

                return publicacao.Id;
            }
            else throw new DomainException(erros);
        }

        public async Task<int> AlterarPublicacaoAsync(int professorId, int id, DateTime dataInicio, DateTime dataFim, int valorProva)
        {
            var publicacao = await _publicacaoRepository.ObterAsync(id);

            if (publicacao == null)
                throw new DomainException("A publicação não foi encontrada!");

            publicacao.DataInicio = dataInicio;
            publicacao.DataFim = dataFim;
            publicacao.ValorProva = valorProva;

            var erros = publicacao.Validate();

            if (erros.Length == 0)
            {
                if (publicacao.Avaliacao.ProfessorId != professorId)
                    throw new DomainException("A avaliação informada não pertence ao professor logado!");

                _publicacaoRepository.Modificar(publicacao);

                _publicacaoRepository.SaveChanges();

                return publicacao.Id;
            }
            else throw new DomainException(erros);
        }

        public async Task<int> ExcluirPublicacaoAsync(int professorId, int id)
        {
            var publicacao = await _publicacaoRepository.ObterAsync(id);

            if (publicacao == null)
                throw new DomainException("A publicação não foi encontrada!");

            if (publicacao.Avaliacao.ProfessorId != professorId)
                throw new DomainException("A avaliação informada não pertence ao professor logado!");

            if (publicacao.Avaliacao.Provas?.Count > 0)
                throw new DomainException("Não é permitido excluir uma publicação quando a prova já foi feita por algum aluno!");

            _publicacaoRepository.Excluir(id);

            _publicacaoRepository.SaveChanges();

            return publicacao.Id;
        }
    }
}
