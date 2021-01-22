using AutoMapper;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.MVC.Models;
using System;
using System.Linq;

namespace PUC.LDSI.MVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Turma, TurmaViewModel>().ReverseMap();

            CreateMap<Avaliacao, AvaliacaoViewModel>()
                .ForMember(destino => destino.Professor, opt => opt.MapFrom(avaliacao => avaliacao.Professor.Nome))
                .ReverseMap();

            CreateMap<QuestaoAvaliacao, QuestaoAvaliacaoViewModel>().ReverseMap();

            CreateMap<OpcaoAvaliacao, OpcaoAvaliacaoViewModel>().ReverseMap();

            CreateMap<Publicacao, PublicacaoViewModel>().ReverseMap();

            CreateMap<Publicacao, ProvaPublicadaViewModel>()
                .ForMember(d => d.Materia, opt => opt.MapFrom(a => a.Avaliacao.Materia))
                .ForMember(d => d.Disciplina, opt => opt.MapFrom(a => a.Avaliacao.Disciplina))
                .ForMember(d => d.Descricao, opt => opt.MapFrom(a => a.Avaliacao.Descricao))
                .ForMember(d => d.Status, opt => opt.ResolveUsing(src => StatusProvaResolve(src)));

            CreateMap<Publicacao, ProvaViewModel>().ReverseMap();

            CreateMap<QuestaoProva, QuestaoProvaViewModel>()
                .ForMember(x => x.DescricaoAvaliacao, opt => opt.MapFrom(a => 
                    a.QuestaoAvaliacao.Avaliacao.Disciplina.Concat(" / " + a.QuestaoAvaliacao.Avaliacao.Materia.Concat(" / " + a.QuestaoAvaliacao.Avaliacao.Descricao))))
                .ForMember(x => x.EhValida, opt => opt.ResolveUsing(src => StatusQuestao(src)))
                .ReverseMap();

            CreateMap<OpcaoProva, OpcaoProvaViewModel>().ReverseMap();
        }

        private bool StatusQuestao(QuestaoProva questao)
        {
            if (questao.QuestaoAvaliacao.Tipo == 1)
            {
                return true;
            } else if (questao.QuestaoAvaliacao.Tipo == 2)
            {
                return true;
            } else
            {
                return false;
            }
        }

        private string StatusProvaResolve(Publicacao publicacao)
        {
            if (publicacao.DataInicio > DateTime.Today)
                return "Agendada";
            else if (publicacao.Avaliacao.Provas.Any() && publicacao.Avaliacao.DataCriacao != null)
                return "Realizada";
            else if (publicacao.DataFim >= DateTime.Today)
                return "Disponível";
            else
                return "Perdida";
        }
    }
}
