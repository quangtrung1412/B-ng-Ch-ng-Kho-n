using System.Security.Claims;
using Exam01.Application.Models;
using Exam01.Application.Service;
using Exam01.Hubs;
using Exam01.Models;
using Exam01.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Exam01.Controllers;
public class HomeConTroller:Controller
{
    private readonly IStockService _stockService;
    private readonly ILogger<HomeConTroller> _logger;
    private readonly IUserService _userService;
    private readonly IHubContext<StockHub> _hubContext;

    public HomeConTroller (IStockService stockService, ILogger<HomeConTroller> logger,IUserService userService,IHubContext<StockHub> hubContext)
    {
        _stockService= stockService;
        _logger= logger;
        _userService=userService;
        _hubContext = hubContext;
    }
    [Authorize(AuthenticationSchemes="ClientCookie")]
    public async Task<IActionResult> Index()
    {
        if(HttpContext.User.Identities== null || !HttpContext.User.Identity.IsAuthenticated)
        {
            return View("Login");
        }
        var data = await _stockService.ListAsync();
        return View(data);
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if(ModelState.IsValid){
            User user = new User();
            user = await _userService.GetByEmailAsync(loginViewModel.Email);
            if(user!=null){
                if( loginViewModel.Password.Equals(user.Password))
                {
                    var Claim = new Claim[]
                    {
                        new Claim(ClaimTypes.Name , user.Email),
                        new Claim("Id",user.Id.ToString()),
                        new Claim("FullName",user.Name),
                        new Claim(ClaimTypes.NameIdentifier, user.Email)
                    };
                    var ClaimsIdentity = new ClaimsIdentity(Claim,"ClientCookie");
                    var ClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity[]{ClaimsIdentity});
                    await HttpContext.SignInAsync("ClientCookie",ClaimsPrincipal,new AuthenticationProperties{
                        IsPersistent = loginViewModel.IsRememberme
                    });
                    if (!string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                    {
                        return Redirect(loginViewModel.ReturnUrl);
                    }
                    return RedirectToAction("Index","Home");
                }
            }
        }
        ViewData["LoginFail"] = "LoginFail";
        return View();
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("ClientCookie");
        return RedirectToAction("Login","Home");
    }

}