using System.ComponentModel.DataAnnotations;

namespace Exam01.ViewModel;



public class StockDisplayViewModel
{   
    public string Id { get; set; }
    public double TC { get; set; }
    public double Tran { get; set; }
    public double San { get; set; }
    public int TongKL { get; set; }
    public int RoomConLai { get; set; }

}