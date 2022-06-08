namespace Exam01.ViewModel;



public class UserConnectionViewModel :EventArgs
{
    public string UserIdentifier {get;set;}
    public string UserName {get;set;}
    public List<ConnectionInfoViewModel> ConnectionInfos {get;set;}

}