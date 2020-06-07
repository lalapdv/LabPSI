using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PUC.LDSI.Domain.Interfaces.Repository;
using PUC.LDSI.Identity.Entities;
using PUC.LDSI.MVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.MVC.Controllers
{
    [Authorize(Policy = "Aluno")]
    public class ProvaController : BaseController
    {
        private readonly IPublicacaoRepository _publicacaoRepository;
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public ProvaController(UserManager<Usuario> user, IPublicacaoRepository publicacaoRepository, IAvaliacaoRepository avaliacaoRepository) : base(user)
        {
            _publicacaoRepository = publicacaoRepository;
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _publicacaoRepository.ListarPublicacoesDoAlunoAsync(IntegrationUserId);

            var avaliacoes = Mapper.Map<List<ProvaPublicadaViewModel>>(result.ToList());

            return View(avaliacoes);
        }

        public async Task<IActionResult> IniciarProva(int? avaliacaoId)
        {
            var result = IniciarRealizacaoDaProvaAsync(IntegrationUserId);

            var questao = Mapper.Map<QuestaoProvaViewModel>(result);

            return RedirectToAction("QuestaoProva", new { questao });
        }

        public async Task<IActionResult> ConcluirProva(int? provaId)
        {
            ObterAsync(provaId);



            return View(QuestaoProvaViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ConcluirProva(QuestaoProvaViewModel questao)
        {
            ConcluirProvaAsync(model);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> QuestaoProva(int? provaId, int questaoId = 0, int avancar = 1)
        {
            var result = await _publicacaoRepository.ObterAsync(provaId);

            var prova = Mapper.Map<ProvaPublicadaViewModel>(result.ToList());

            if (questaoId == 0)
            {
                var avaliacao = await _avaliacaoRepository.ObterAsync(prova.AvaliacaoId);

            }
            else if (questaoId > 0)
            {
                var avaliacao = await _avaliacaoRepository.ObterAsync(prova.AvaliacaoId);

            }
            else
            {
                var avaliacao = await _avaliacaoRepository.ObterAsync(prova.AvaliacaoId);

            }

            return View(QuestaoProvaViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> QuestaoProva(QuestaoProvaViewModel model)
        {
            GravarQuestaoProvaAsync(model);

            return RedirectToAction("QuestaoProva", new { provaId = model.provaId, questaoId = model.questaoId, avancar = model.avancar });
        }
    }
}