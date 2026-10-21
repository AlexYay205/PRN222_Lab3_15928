using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vegan.Services.DuyLD;

namespace Vegan.BlazorWebApp.DuyLD.Pages.Account;

public class LoginModel : PageModel
{
    private readonly ISystemUserAccountService _userAccountService;

    public LoginModel(ISystemUserAccountService userAccountService)
    {
        _userAccountService = userAccountService;
    }

    [BindProperty]
    public string UserName { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userAccountService.GetUserAccount(UserName, Password);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Login failure");
            return Page();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.FullName ?? user.UserName),
            new(ClaimTypes.Role, user.RoleId.ToString())
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        if (!string.IsNullOrWhiteSpace(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
        {
            return LocalRedirect(ReturnUrl);
        }

        return Redirect("/veganplaceduylds");
    }
}
