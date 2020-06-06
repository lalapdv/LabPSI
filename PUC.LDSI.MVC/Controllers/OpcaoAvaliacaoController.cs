using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PUC.LDSI.Application.Interfaces;
using PUC.LDSI.Domain.Entities;
using PUC.LDSI.Domain.Interfaces.Repository;
using PUC.LDSI.Identity.Entities;
using PUC.LDSI.MVC.Models;
using System.Threading.Tasks;

namespace PUC.LDSI.MVC.Controllers
{
    [Authorize(Policy = "Professor")]
    public class OpcaoAvaliacaoController : BaseController
    {
        private readonly IAvaliacaoAppService _avaliacaoAppService;
        private readonly IQuestaoAvaliacaoRepository _questaoAvaliacaoRepository;
        private readonly IOpcaoAvaliacaoRepository _opcaoAvaliacaoRepository;

        public OpcaoAvaliacaoController(UserManager<Usuario> user,
                                        IAvaliacaoAppService avaliacaoAppService,
                                        IQuestaoAvaliacaoRepository questaoAvaliacaoRepository,
                                        IOpcaoAvaliacaoRepository opcaoAvaliacaoRepository) : base(user)
        {
            _avaliacaoAppService = avaliacaoAppService;
            _questaoAvaliacaoRepository = questaoAvaliacaoRepository;
            _opcaoAvaliacaoRepository = opcaoAvaliacaoRepository;
        }

        public async Task<IActionResult> Index(int? questaoId)
        {
            if (questaoId == null) { return NotFound(); }

            var result = await _questaoAvaliacaoRepository.ObterAsync(questaoId.Value);

            var questao = Mapper.Map<QuestaoAvaliacaoViewModel>(result);

            return View(questao);
        }

        public async Task<IActionResult> Create(int? questaoId)
        {
            if (questaoId == null) { return NotFound(); }

            var result = await _questaoAvaliacaoRepository.ObterAsync(questaoId.Value);

            var questao = Mapper.Map<QuestaoAvaliacaoViewModel>(result);

            var opcao = new OpcaoAvaliacaoViewModel()
            {
                Questao = questao,
                QuestaoId = questao.Id
            };

            return View(opcao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestaoId,Descricao,Verdadeira")] OpcaoAvaliacaoViewModel opcaoAvaliacao)
        {
            if (ModelState.IsValid)
            {
                var result = await _avaliacaoAppService.AdicionarOpcaoAvaliacaoAsync(opcaoAvaliacao.QuestaoId, opcaoAvaliacao.Descricao, opcaoAvaliacao.Verdadeira);

                if (result.Success)
                    return RedirectToAction(nameof(Index), new { questaoId = opcaoAvaliacao.QuestaoId });
                else
                    throw result.Exception;
            }

            ViewData["QuestaoId"] = opcaoAvaliacao.QuestaoId;

            return View(opcaoAvaliacao);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            var result = await _opcaoAvaliacaoRepository.ObterAsync(id.Value);

            if (result == null) { return NotFound(); }

            var opcao = Mapper.Map<OpcaoAvaliacaoViewModel>(result);

            return View(opcao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuestaoId,Descricao,Verdadeira")] OpcaoAvaliacao opcaoAvaliacao)
        {
            if (id != opcaoAvaliacao.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                var result = await _avaliacaoAppService.AlterarOpcaoAvaliacaoAsync(opcaoAvaliacao.Id, opcaoAvaliacao.Descricao, opcaoAvaliacao.Verdadeira);

                if (result.Success)
                    return RedirectToAction(nameof(Index), new { questaoId = opcaoAvaliacao.QuestaoId });
                else
                    throw result.Exception;
            }

            return View(opcaoAvaliacao);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            var result = await _opcaoAvaliacaoRepository.ObterAsync(id.Value);

            if (result == null) { return NotFound(); }

            var opcao = Mapper.Map<OpcaoAvaliacaoViewModel>(result);

            return View(opcao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _avaliacaoAppService.ExcluirOpcaoAvaliacaoAsync(id);

            if (result.Success)
                return RedirectToAction(nameof(Index), new { questaoId = result.Data });
            else
                throw result.Exception;
        }
    }
}
