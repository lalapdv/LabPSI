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

        public ProvaController(UserManager<Usuario> user, IPublicacaoRepository publicacaoRepository) : base(user)
        {
            _publicacaoRepository = publicacaoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _publicacaoRepository.ListarPublicacoesDoAlunoAsync(IntegrationUserId);

            var avaliacoes = Mapper.Map<List<ProvaPublicadaViewModel>>(result.ToList());

            return View(avaliacoes);
        }
    }
}