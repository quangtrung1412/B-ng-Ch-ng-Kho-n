using Exam01.ViewModel;
using Microsoft.AspNetCore.SignalR;

namespace Exam01.Hubs;
public class AdminHub : Hub
{
    private readonly StockHubConnectionManager _stockHubConnectionManager;
    private readonly IHubContext<AdminHub> _hubContext;

    public AdminHub(StockHubConnectionManager stockHubConnectionManager, IHubContext<AdminHub> hubContext)
    {
        _stockHubConnectionManager = stockHubConnectionManager;
        stockHubConnectionManager.SubscribeConnect(HandleConnected);
        stockHubConnectionManager.SubscribeDisconnect(HandleDisConnected);
        _hubContext = hubContext;
    }

    public override Task OnConnectedAsync()
    {
        var userConnectionViewModels = _stockHubConnectionManager.GetUserConnectionViewModels();
        var NumberConnection = _stockHubConnectionManager.GetNumberOfConnections();
        var NumberUser = _stockHubConnectionManager.GetNumberOfUsers();
        Clients.All.SendAsync("OnConnection",NumberConnection,NumberUser,userConnectionViewModels);
        return base.OnConnectedAsync();
    }
    public override Task OnDisconnectedAsync(Exception exception)
    {
        return base.OnDisconnectedAsync(exception);   
    }
    public async void HandleConnected(object sender, UserConnectionViewModel userConnectionViewModel)
    {
        var userConnectionViewModels = _stockHubConnectionManager.GetUserConnectionViewModels();
        var NumberConnection = _stockHubConnectionManager.GetNumberOfConnections();
        var NumberUser = _stockHubConnectionManager.GetNumberOfUsers();
        await _hubContext.Clients.All.SendAsync("Connection",NumberConnection,NumberUser,userConnectionViewModels);
    }
    public async void HandleDisConnected (object sender , UserConnectionViewModel userConnectionViewModel)
    {
          var userConnectionViewModels = _stockHubConnectionManager.GetUserConnectionViewModels();
        var NumberDisConnection = _stockHubConnectionManager.GetNumberOfConnections();
        var NumberUser = _stockHubConnectionManager.GetNumberOfUsers();
        await _hubContext.Clients.All.SendAsync("DisConnect",NumberDisConnection,NumberUser,userConnectionViewModels);

    }
}