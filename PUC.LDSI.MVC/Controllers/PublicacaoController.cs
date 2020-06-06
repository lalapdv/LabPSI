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
using System.Linq;
using System.Threading.Tasks;

namespace PUC.LDSI.MVC.Controllers
{
    [Authorize(Policy = "Professor")]
    public class PublicacaoController : BaseController
    {
        private readonly IPublicacaoRepository _publicacaoRepository;
        private readonly IAvaliacaoAppService _avaliacaoAppService;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public PublicacaoController(UserManager<Usuario> user,
                                    IPublicacaoRepository publicacaoRepository,
                                    IAvaliacaoAppService avaliacaoAppService,
                                    ITurmaRepository turmaRepository,
                                    IAvaliacaoRepository avaliacaoRepository) : base(user)
        {
            _publicacaoRepository = publicacaoRepository;
            _avaliacaoAppService = avaliacaoAppService;
            _turmaRepository = turmaRepository;
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _publicacaoRepository.ListarPublicacoesDoProfessorAsync(IntegrationUserId);

            var publicacoes = Mapper.Map<List<PublicacaoViewModel>>(result.ToList());

            return View(publicacoes);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["AvaliacaoId"] = await ObterAvaliacoesDoProfessorParaSelecao();
            ViewData["TurmaId"] = new SelectList(_turmaRepository.ObterTodos(), "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvaliacaoId,TurmaId,DataInicio,DataFim,ValorProva")] PublicacaoViewModel publicacao)
        {
            if (ModelState.IsValid)
            {
                var result = await _avaliacaoAppService.AdicionarPublicacaoAsync(
                    IntegrationUserId,
                    publicacao.AvaliacaoId,
                    publicacao.TurmaId,
                    publicacao.DataInicio,
                    publicacao.DataFim,
                    publicacao.ValorProva);

                if (result.Success)
                    return RedirectToAction(nameof(Index));
                else
                    throw result.Exception;
            }

            return View(publicacao);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            var result = await _publicacaoRepository.ObterAsync(id.Value);

            if (result == null) { return NotFound(); }

            var publicacao = Mapper.Map<PublicacaoViewModel>(result);

            return View(publicacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AvaliacaoId,TurmaId,DataInicio,DataFim,ValorProva,Id")] PublicacaoViewModel publicacao)
        {
            if (id != publicacao.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                var result = await _avaliacaoAppService.AlterarPublicacaoAsync(
                    IntegrationUserId,
                    publicacao.Id,
                    publicacao.DataInicio,
                    publicacao.DataFim,
                    publicacao.ValorProva);

                if (result.Success)
                    return RedirectToAction(nameof(Index));
                else
                    throw result.Exception;
            }

            return View(publicacao);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            var result = await _publicacaoRepository.ObterAsync(id.Value);

            if (result == null) { return NotFound(); }

            var publicacao = Mapper.Map<PublicacaoViewModel>(result);

            return View(publicacao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = IntegrationUserId;

            var result = await _avaliacaoAppService.ExcluirPublicacaoAsync(userId, id);

            if (result.Success)
                return RedirectToAction(nameof(Index));
            else
                throw result.Exception;
        }

        private async Task<List<SelectListItem>> ObterAvaliacoesDoProfessorParaSelecao(int avaliacaoSelecionadaId = 0)
        {
            var lista = new List<SelectListItem>();

            var avaliacoes = await _avaliacaoRepository.ListarAvaliacoesDoProfessorAsync(IntegrationUserId);

            foreach (var avaliacao in avaliacoes)
            {
                lista.Add(new SelectListItem()
                {
                    Text = string.Format("{0}/{1}/{2}", avaliacao.Disciplina, avaliacao.Materia, avaliacao.Descricao),
                    Value = avaliacao.Id.ToString(),
                    Selected = avaliacao.Id == avaliacaoSelecionadaId
                });
            }

            return lista;
        }

    }
}
