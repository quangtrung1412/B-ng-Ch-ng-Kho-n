using System.Security.Claims;
using Exam01.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;

namespace Exam01.Hubs;
[Authorize(AuthenticationSchemes = "ClientCookie")]
public class StockHub : Hub
{
    private static Dictionary<string, List<string>> userInfos = new Dictionary<string, List<string>>();
    private readonly StockHubConnectionManager _connectionManager;
    public StockHub(StockHubConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }
    public async Task JoinGroup(string groupName)
    {
        var userIdentifier = Context.UserIdentifier;
        foreach (var userInfo in userInfos[userIdentifier])
        {
        await Groups.AddToGroupAsync(userInfo, groupName);
        }
        await Clients.User(userIdentifier).SendAsync("CheckedBox",groupName);
    }
    public async Task RemoveGroup(string groupName)
    {
        
        var userIdentifier = Context.UserIdentifier;
        foreach (var userInfo in userInfos[userIdentifier])
        {
            await Groups.RemoveFromGroupAsync(userInfo, groupName);
        }
        await Clients.User(userIdentifier).SendAsync("RemoveCheckedBox",groupName);
    }

    public override Task OnConnectedAsync()
    {
        var userIdentifier = Context.UserIdentifier;
        var connectionId = Context.ConnectionId;
        if (connectionId != null)
        {
            if (userInfos.ContainsKey(userIdentifier))
            {
                userInfos[userIdentifier].Add(connectionId);
            }
            else
            {
                List<string> connectionIds = new List<string>();
                connectionIds.Add(connectionId);
                userInfos.Add(userIdentifier, connectionIds);
            }
            var connectionInfo = GetConnectionInfo(Context);
            _connectionManager.OnConnect(connectionInfo);
        }
        return base.OnConnectedAsync();
    }
    public override Task OnDisconnectedAsync(Exception exception)
    {
        var userIdentifier = Context.UserIdentifier;
        var connectionId = Context.ConnectionId;
        if (userIdentifier != null)
        {
            if (userInfos.ContainsKey(userIdentifier))
            {
                userInfos[userIdentifier].Remove(connectionId);
                if (userInfos[userIdentifier].Count() == 0)
                {
                    userInfos.Remove(userIdentifier);
                }
            }
        }
        var connectionInfo = GetConnectionInfo(Context);
        _connectionManager.OnDisconnect(connectionInfo);
        return base.OnDisconnectedAsync(exception);
    }

    private UserConnectionViewModel GetConnectionInfo(HubCallerContext context)
    {
        var UserName = context.User.Claims.ElementAt(2).Value;
        var remoteAddress = GetRemoteIPAddress(context);
        var browserName = Context.GetHttpContext().Request.Cookies["browserName"];
        ConnectionInfoViewModel connectionInfo = new ConnectionInfoViewModel()
        {
            ConnectionId = context.ConnectionId,
            BrowserName = browserName,
            IPAddress = remoteAddress
        };
        List<ConnectionInfoViewModel> connectionInfos = new List<ConnectionInfoViewModel>();
        connectionInfos.Add(connectionInfo);
        return new UserConnectionViewModel
        {
            UserIdentifier = context.UserIdentifier,
            UserName = UserName,
            ConnectionInfos = connectionInfos
        };
    }

    private string GetRemoteIPAddress(HubCallerContext context)
    {
        var feature = context.Features.Get<IHttpConnectionFeature>();
        var remoteAddress = feature.RemoteIpAddress.ToString();
        if (remoteAddress.Equals("::1"))
        {
            remoteAddress = "127.0.0.1";
        }
        return remoteAddress;
    }
}