using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PUC.LDSI.Application.Interfaces;
using PUC.LDSI.Domain.Interfaces.Repository;
using PUC.LDSI.Identity.Entities;
using PUC.LDSI.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PUC.LDSI.MVC.Controllers
{
    [Authorize(Policy = "Professor")]
    public class QuestaoAvaliacaoController : BaseController
    {
        private readonly IAvaliacaoAppService _avaliacaoAppService;
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IQuestaoAvaliacaoRepository _questaoAvaliacaoRepository;

        public QuestaoAvaliacaoController(UserManager<Usuario> user,
                                          IAvaliacaoAppService avaliacaoAppService,
                                          IAvaliacaoRepository avaliacaoRepository,
                                          IQuestaoAvaliacaoRepository questaoAvaliacaoRepository) : base(user)
        {
            _avaliacaoAppService = avaliacaoAppService;
            _avaliacaoRepository = avaliacaoRepository;
            _questaoAvaliacaoRepository = questaoAvaliacaoRepository;
        }

        public async Task<IActionResult> Index(int? avaliacaoId)
        {
            if (avaliacaoId == null) { return NotFound(); }

            var result = await _avaliacaoRepository.ObterAsync(avaliacaoId.Value);

            var avaliacao = Mapper.Map<AvaliacaoViewModel>(result);

            return View(avaliacao);
        }

        public async Task<IActionResult> Create(int? avaliacaoId)
        {
            if (avaliacaoId == null) { return NotFound(); }

            var result = await _avaliacaoRepository.ObterAsync(avaliacaoId.Value);

            var avaliacao = Mapper.Map<AvaliacaoViewModel>(result);

            var questao = new QuestaoAvaliacaoViewModel()
            {
                Avaliacao = avaliacao,
                AvaliacaoId = avaliacao.Id
            };

            ViewData["OpcoesTipo"] = ObterOpcoesTipo();

            return View(questao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvaliacaoId,Tipo,Enunciado")] QuestaoAvaliacaoViewModel questao)
        {
            if (ModelState.IsValid)
            {
                var result = await _avaliacaoAppService.AdicionarQuestaoAvaliacaoAsync(questao.AvaliacaoId, questao.Tipo, questao.Enunciado);

                if (result.Success)
                    return RedirectToAction(nameof(Index), new { avaliacaoId = questao.AvaliacaoId });
                else
                    throw result.Exception;
            }

            return View(questao);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            var result = await _questaoAvaliacaoRepository.ObterAsync(id.Value);

            if (result == null) { return NotFound(); }

            var questao = Mapper.Map<QuestaoAvaliacaoViewModel>(result);

            ViewData["OpcoesTipo"] = ObterOpcoesTipo(questao.Tipo);

            return View(questao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AvaliacaoId,Tipo,Enunciado")] QuestaoAvaliacaoViewModel questao)
        {
            if (id != questao.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                var result = await _avaliacaoAppService.AlterarQuestaoAvaliacaoAsync(questao.Id, questao.Tipo, questao.Enunciado);

                if (result.Success)
                    return RedirectToAction(nameof(Index), new { questao.AvaliacaoId });
                else
                    throw result.Exception;
            }

            return View(questao);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            var result = await _questaoAvaliacaoRepository.ObterAsync(id.Value);

            if (result == null) { return NotFound(); }

            var questao = Mapper.Map<QuestaoAvaliacaoViewModel>(result);

            return View(questao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _avaliacaoAppService.ExcluirQuestaoAvaliacaoAsync(id);

            if (result.Success)
                return RedirectToAction(nameof(Index), new { avaliacaoId = result.Data });
            else
                throw result.Exception;
        }

        private List<SelectListItem> ObterOpcoesTipo(int tipoId = 0)
        {
            return new List<SelectListItem>() {
                new SelectListItem{ Text="Múltipla Escolha", Value = "1", Selected = tipoId == 1 },
                new SelectListItem{ Text="Verdadeiro ou Falso", Value = "2", Selected = tipoId == 2 }
            };
        }
    }
}
