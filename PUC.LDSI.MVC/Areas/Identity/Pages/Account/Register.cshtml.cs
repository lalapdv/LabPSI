using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using PUC.LDSI.Application;
using PUC.LDSI.Application.Interfaces;
using PUC.LDSI.Domain.Interfaces.Repository;
using PUC.LDSI.Identity.Entities;

namespace PUC.LDSI.MVC.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IProfessorAppService _professorAppService;
        private readonly ITurmaRepository _turmaRepository;
        private readonly ITurmaAppService _turmaAppService;

        public RegisterModel(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IProfessorAppService professorAppService,
            ITurmaRepository turmaRepository,
            ITurmaAppService turmaAppService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _professorAppService = professorAppService;
            _turmaRepository = turmaRepository;
            _turmaAppService = turmaAppService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
            [EmailAddress]
            [Display(Name = "E-mail")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo Nome é obrigatório.")]
            [DataType(DataType.Text)]
            [Display(Name = "Nome")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "O campo Senha é obrigatório.")]
            [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar senha")]
            [Compare("Password", ErrorMessage = "A senha informada e a confirmação não são as mesmas.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Aluno ou Professor?")]
            public int Tipo { get; set; }

            [Display(Name = "Informe sua Turma")]
            public int TurmaId { get; set; }
        }

        public SelectList Turmas()
        {
            var turmas = _turmaRepository.ObterTodos().ToList();

            return new SelectList(turmas, "Id", "Nome");
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result = default(DataResult<int>);

                if (Input.Tipo == 1)
                    result = await _professorAppService.IncluirProfessorAsync(Input.Nome);
                else
                    result = await _turmaAppService.IncluirAlunoAsync(Input.TurmaId, Input.Nome);

                if (result.Success)
                {
                    var user = new Usuario { UserName = Input.Email, Email = Input.Email, IntegrationId = result.Data, UserType = Input.Tipo };

                    var identityResult = await _userManager.CreateAsync(user, Input.Password);

                    if (identityResult.Succeeded)
                    {
                        if (Input.Tipo == 1)
                            await _signInManager.UserManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Professor"));
                        else
                            await _signInManager.UserManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Aluno"));

                        _logger.LogInformation("User created a new account with password.");

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return LocalRedirect(returnUrl);
                    }
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else throw result.Exception;
            }
            return Page();
        }
    }
}
