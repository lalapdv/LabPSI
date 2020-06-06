using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class AvaliacaoController : BaseController
    {
        private readonly IAvaliacaoAppService _avaliacaoAppService;
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public AvaliacaoController(UserManager<Usuario> user,
                                   IAvaliacaoAppService avaliacaoAppService,
                                   IAvaliacaoRepository avaliacaoRepository) : base(user)
        {
            _avaliacaoAppService = avaliacaoAppService;

            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _avaliacaoRepository.ListarAvaliacoesDoProfessorAsync(IntegrationUserId);

            var avaliacoes = Mapper.Map<List<AvaliacaoViewModel>>(result.ToList());

            return View(avaliacoes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Disciplina,Materia,Descricao")] AvaliacaoViewModel avaliacao)
        {
            if (ModelState.IsValid)
            {
                var result = await _avaliacaoAppService.AdicionarAvaliacaoAsync(IntegrationUserId, avaliacao.Disciplina, avaliacao.Materia, avaliacao.Descricao);

                if (result.Success)
                    return RedirectToAction(nameof(Index));
                else
                    throw result.Exception;
            }

            return View(avaliacao);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            var result = await _avaliacaoRepository.ObterAsync(id.Value);

            if (result == null) { return NotFound(); }

            var avaliacao = Mapper.Map<AvaliacaoViewModel>(result);

            return View(avaliacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Disciplina,Materia,Descricao")] AvaliacaoViewModel avaliacao)
        {
            if (id != avaliacao.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                var result = await _avaliacaoAppService.AlterarAvaliacaoAsync(avaliacao.Id, avaliacao.Disciplina, avaliacao.Materia, avaliacao.Descricao);

                if (result.Success)
                    return RedirectToAction(nameof(Index));
                else
                    throw result.Exception;
            }

            return View(avaliacao);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            var result = await _avaliacaoRepository.ObterAsync(id.Value);

            if (result == null) { return NotFound(); }

            var avaliacao = Mapper.Map<AvaliacaoViewModel>(result);

            return View(avaliacao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _avaliacaoAppService.ExcluirAvaliacaoAsync(id);

            if (result.Success)
                return RedirectToAction(nameof(Index));
            else
                throw result.Exception;
        }
    }
}
