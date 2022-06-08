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

namespace Exam01.Controllers;


public class AdminController : Controller
{
    private readonly IStockService _stockService;
    private readonly IUserService _userService;
    private readonly ILogger<AdminController> _logger;
    private readonly IHubContext<StockHub> _hubContext;

    public AdminController(IUserService userService, ILogger<AdminController> logger, IStockService stockService, IHubContext<StockHub> hubContext)
    {
        _stockService = stockService;
        _userService = userService;
        _logger = logger;
        _hubContext = hubContext;
    }
    [Authorize(AuthenticationSchemes="AdminCookie")]
    public async Task<IActionResult> Index()
    {
        List<StockDisplayViewModel> stockDisplayViewModels = new List<StockDisplayViewModel>();

        var datas = await _stockService.ListAsync();
        foreach (var data in datas)
        {
            StockDisplayViewModel stockDisplayViewModel = new StockDisplayViewModel();
            stockDisplayViewModel.Id = data.Id;
            stockDisplayViewModel.TC = data.TC;
            stockDisplayViewModel.Tran = data.Tran;
            stockDisplayViewModel.San = data.San;
            stockDisplayViewModel.TongKL = data.TongKL;
            stockDisplayViewModel.RoomConLai = data.RoomConLai;
            stockDisplayViewModels.Add(stockDisplayViewModel);
        }
        return View(stockDisplayViewModels);
    }
    public IActionResult Login()
    {
        if (HttpContext.User.Identity == null || !HttpContext.User.Identity.IsAuthenticated)
        {
            return View();
        }
        return RedirectToAction("Index","Home");

    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (ModelState.IsValid)
        {
            User user = new User();
            user = await _userService.GetByEmailAsync(loginViewModel.Email);
            if (user != null)
            {
                if (loginViewModel.Password.Equals(user.Password))
                {
                    var Claim = new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("FullName",user.Name),
                    new Claim(ClaimTypes.NameIdentifier,user.Email)
                    };
                    var claimsIdentity = new ClaimsIdentity(Claim, "AdminCookie");
                    var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity[] { claimsIdentity });
                    await HttpContext.SignInAsync("AdminCookie",claimsPrincipal, new AuthenticationProperties
                    {
                        IsPersistent = loginViewModel.IsRememberme
                    });
                    if (!string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                    {
                        return Redirect(loginViewModel.ReturnUrl);
                    }
                    return RedirectToAction("Index");
                }
            }

        }
        ViewData["LoginFail"] = "LoginFail";
        return View();
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("AdminCookie");
        return RedirectToAction("Login");
    }
    [Authorize(AuthenticationSchemes="AdminCookie")]
    public async Task<IActionResult> ShowStockDetail(string Id)
    {
        Stock stock = new Stock();
        stock = await _stockService.GetByIdAsync(Id);
        if (stock == null)
        {
            return RedirectToAction("Index");
        }
        return View("StockDetail", stock);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateStock(string id, Stock stock)
    {
        if (!id.Equals(stock.Id))
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            var stockDb = await _stockService.GetByIdAsync(id);
            if (stockDb != null)
            {
                var type = typeof(Stock);
                StockChangedViewModel stockChangedViewModel = new StockChangedViewModel();
                stockChangedViewModel.Id = id;
                stockChangedViewModel.CellValues = new List<CellValueViewModel>();
                foreach (var prop in type.GetProperties())
                {
                    var stockDbPropertyValue = type.GetProperty(prop.Name).GetValue(stockDb);
                    var stockModelPropertyValue = type.GetProperty(prop.Name).GetValue(stock);
                    if (!stockDbPropertyValue.Equals(stockModelPropertyValue))
                    {
                        var cellValue = new CellValueViewModel
                        {
                            CellName = prop.Name,
                            CellValue = stockModelPropertyValue
                        };
                        stockChangedViewModel.CellValues.Add(cellValue);
                    }
                }
                await _stockService.UpdateAsync(stock);
                await _hubContext.Clients.Group(id).SendAsync("UpdateMessage",  stockChangedViewModel);
                return RedirectToAction("Index");
            }
        }
        return View(stock);
    }
}
