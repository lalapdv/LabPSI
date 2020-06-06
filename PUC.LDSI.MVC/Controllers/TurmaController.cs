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
    public class TurmaController : BaseController
    {
        private readonly ITurmaAppService _turmaAppService;
        private readonly ITurmaRepository _turmaRepository;

        public TurmaController(UserManager<Usuario> user,
                               ITurmaAppService turmaAppService,
                               ITurmaRepository turmaRepository) : base(user)
        {
            _turmaRepository = turmaRepository;
            _turmaAppService = turmaAppService;
        }

        // GET: Turma
        public IActionResult Index()
        {
            var result = _turmaRepository.ObterTodos();

            var turmas = Mapper.Map<List<TurmaViewModel>>(result.ToList());

            return View(turmas);
        }

        // GET: Turma/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Id")] TurmaViewModel turma)
        {
            if (ModelState.IsValid)
            {
                var result = await _turmaAppService.AdicionarTurmaAsync(turma.Nome);

                if (result.Success)
                    return RedirectToAction(nameof(Index));
                else
                    throw result.Exception;
            }
            return View(turma);
        }

        // GET: Turma/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _turmaRepository.ObterAsync(id.Value);

            if (turma == null)
            {
                return NotFound();
            }

            var viewModel = Mapper.Map<TurmaViewModel>(turma);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Id")] TurmaViewModel turma)
        {
            if (id != turma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _turmaAppService.AlterarTurmaAsync(turma.Id, turma.Nome);

                if (result.Success)
                    return RedirectToAction(nameof(Index));
                else
                    throw result.Exception;
            }

            return View(turma);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _turmaRepository.ObterAsync(id.Value);

            if (turma == null)
            {
                return NotFound();
            }

            var viewModel = Mapper.Map<TurmaViewModel>(turma);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _turmaAppService.ExcluirAsync(id);

            if (result.Success)
                return RedirectToAction(nameof(Index));
            else
                throw result.Exception;
        }
    }
}
