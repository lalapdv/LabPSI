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
        }

        private string StatusProvaResolve(Publicacao publicacao)
        {
            if (publicacao.DataInicio > DateTime.Today)
                return "Agendada";
            else if (publicacao.Avaliacao.Provas.Any())
                return "Realizada";
            else if (publicacao.DataFim >= DateTime.Today)
                return "Disponível";
            else
                return "Perdida";
        }
    }
}
