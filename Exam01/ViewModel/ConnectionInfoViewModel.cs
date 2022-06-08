namespace Exam01.ViewModel;

public class ConnectionInfoViewModel : EventArgs
{

    public string ConnectionId { get; set; }
    public string IPAddress { get; set; }
    public string BrowserName { get; set; }

    public override bool Equals(object obj)
    {
        if (obj.GetType() != typeof(ConnectionInfoViewModel))
        {
            return false;
        }
        ConnectionInfoViewModel other = (ConnectionInfoViewModel)obj;
        if ((this.ConnectionId == null && other.ConnectionId != null) ||
        (this.ConnectionId != null && !this.ConnectionId.Equals(other.ConnectionId)))
        {
            return false;
        }
        return true;
    }
    
    public override int GetHashCode()
    {
        int hash = 0;
        hash += (ConnectionId != null ? ConnectionId.GetHashCode() : 0);
        return hash;
    }
}