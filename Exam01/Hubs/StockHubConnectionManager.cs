using Exam01.ViewModel;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;

namespace Exam01.Hubs;

public class StockHubConnectionManager
{
    private  Dictionary<string, UserConnectionViewModel> userInfos = new Dictionary<string, UserConnectionViewModel>();
    private event EventHandler<UserConnectionViewModel> connectEventHandler;
    private event EventHandler<UserConnectionViewModel> disconnectEventHandler;

    public void OnConnect(UserConnectionViewModel userConnectionViewModel)
    {
        var userIdentifer = userConnectionViewModel.UserIdentifier;
        if (userIdentifer != null)
        {
            if (userInfos.ContainsKey(userIdentifer))
            {
                var connectionInfos = userInfos[userIdentifer].ConnectionInfos;
                userInfos[userIdentifer].UserIdentifier = userConnectionViewModel.UserIdentifier;
                userInfos[userIdentifer].UserName = userConnectionViewModel.UserName;
                connectionInfos.AddRange(userConnectionViewModel.ConnectionInfos);
            }
            else
            {
                userInfos.Add(userIdentifer, userConnectionViewModel);
            }
        }
        connectEventHandler?.Invoke(this, userConnectionViewModel);
    }

    public void OnDisconnect(UserConnectionViewModel userConnectionViewModel)
    {
        var userIdentifer = userConnectionViewModel.UserIdentifier;
        var connectionInfo = userConnectionViewModel.ConnectionInfos.FirstOrDefault();
        if(userIdentifer!=null)
        {
           if(userInfos.ContainsKey(userIdentifer))
           {
               userInfos[userIdentifer].ConnectionInfos.Remove(connectionInfo);
               if(userInfos[userIdentifer].ConnectionInfos.Count()==0){
                   userInfos.Remove(userIdentifer);
               }
           }
        }
        disconnectEventHandler?.Invoke(this, userConnectionViewModel);
    }

    public void SubscribeConnect(EventHandler<Exam01.ViewModel.UserConnectionViewModel> handler)
    {
       connectEventHandler += handler;
    }

    public void SubscribeDisconnect(EventHandler<Exam01.ViewModel.UserConnectionViewModel> handler)
    {
       disconnectEventHandler += handler;
    }
    public int GetNumberOfConnections()
    {
        int numberOfConnections = 0;
        foreach (var key in userInfos.Keys)
        {
            numberOfConnections+=userInfos[key].ConnectionInfos.Count();
        }
        return numberOfConnections;
    }
    public int GetNumberOfUsers()
    {
        return userInfos.Count();
    }
    public List<UserConnectionViewModel> GetUserConnectionViewModels()
    {
        List<UserConnectionViewModel> userConnectionViewModels = new List<UserConnectionViewModel>();
        foreach( var key  in userInfos.Keys)
        {
            userConnectionViewModels.Add(userInfos[key]);
        }
        return userConnectionViewModels;
    }
    
}