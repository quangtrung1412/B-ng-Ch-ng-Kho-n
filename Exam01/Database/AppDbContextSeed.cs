using Exam01.Application.Models;
using Exam01.Models;

namespace Exam01.Database;



public class AppDbContextSeed
{
    public async Task SeedAsync(AppDbContext context, ILogger<AppDbContextSeed> logger)
    {
        if (!context.Users.Any())
        {
            logger.LogInformation("Generate default data for User table");
            context.Users.AddRange(GetPredefinedUser());
            await context.SaveChangesAsync();
        }
        if(!context.Stocks.Any())
        {
            logger.LogInformation("Generate default data for Stock table");
            context.Stocks.AddRange(GetPredefinedStock());
            await context.SaveChangesAsync();
        }
    }
    private IEnumerable<Stock> GetPredefinedStock()
    {
        return new List<Stock>(){
            new Stock{Id="AAM",TC=11.2,Tran=11.9,San=15.5,MuaG3=17.7,MuaKL3=1000,MuaG2=9.6,MuaKL2=1500,MuaG1=18.2,MuaKL1=6900,
            Gia=18.2,KL=1000,Percent=0,BanG3=18.5,BanKL3=100,BanG2=18.4,BanKL2=400,BanG1=18.3,BanKL1=700,TongKL=1000,MoCua=18.2,
            CaoNhat=18.2,ThapNhat=18.2,NNMua=1564654,NNBan=121212313,RoomConLai=4718200},
            new Stock{Id="ACB",TC=11.2,Tran=11.9,San=15.5,MuaG3=10.1,MuaKL3=1000,MuaG2=9.6,MuaKL2=1500,MuaG1=18.2,MuaKL1=6900,
            Gia=18.2,KL=1000,Percent=1.4,BanG3=18.5,BanKL3=100,BanG2=18.4,BanKL2=400,BanG1=18.3,BanKL1=700,TongKL=1000,MoCua=18.2,
            CaoNhat=18.2,ThapNhat=18.2,NNMua=0,NNBan=0,RoomConLai=4718200},
            new Stock{Id="ACE",TC=11.2,Tran=11.9,San=15.5,MuaG3=11.9,MuaKL3=1000,MuaG2=9.6,MuaKL2=1500,MuaG1=18.2,MuaKL1=6900,
            Gia=18.2,KL=1000,Percent=1.5,BanG3=18.5,BanKL3=100,BanG2=18.4,BanKL2=400,BanG1=18.3,BanKL1=700,TongKL=1000,MoCua=18.2,
            CaoNhat=18.2,ThapNhat=18.2,NNMua=571000,NNBan=5657412,RoomConLai=4718200},
            new Stock{Id="ACM",TC=11.2,Tran=11.9,San=15.5,MuaG3=15.5,MuaKL3=1000,MuaG2=9.6,MuaKL2=1500,MuaG1=18.2,MuaKL1=6900,
            Gia=18.2,KL=1000,Percent=1.8,BanG3=18.5,BanKL3=100,BanG2=18.4,BanKL2=400,BanG1=18.3,BanKL1=700,TongKL=1000,MoCua=18.2,
            CaoNhat=18.2,ThapNhat=18.2,NNMua=10000,NNBan=100000,RoomConLai=4718200},
        };
    }

    private IEnumerable<User> GetPredefinedUser()
    {
        return new List<User>() {
            new User{Name="User1",Email="User1@gmail.com",Password="User1@123",TemPass="User1@123",
            Avatar="https://www.google.com/url?sa=i&url=https%3A%2F%2Faui.atlassian.com%2Faui%2F8.8%2Fdocs%2Favatars.html&psig=AOvVaw2BMzZfE2clElFiXDQKts6O&ust=1652857870856000&source=images&cd=vfe&ved=0CAwQjRxqFwoTCLDBqsT95fcCFQAAAAAdAAAAABAD"},
            new User{Name="User2",Email="User2@gmail.com",Password="User2@123",TemPass="User2@123",
            Avatar="https://www.google.com/url?sa=i&url=https%3A%2F%2Faui.atlassian.com%2Faui%2F8.8%2Fdocs%2Favatars.html&psig=AOvVaw2BMzZfE2clElFiXDQKts6O&ust=1652857870856000&source=images&cd=vfe&ved=0CAwQjRxqFwoTCLDBqsT95fcCFQAAAAAdAAAAABAD"},
            new User{Name="User3",Email="User3@gmail.com",Password="User3@123",TemPass="User3@123",
            Avatar="https://www.google.com/url?sa=i&url=https%3A%2F%2Faui.atlassian.com%2Faui%2F8.8%2Fdocs%2Favatars.html&psig=AOvVaw2BMzZfE2clElFiXDQKts6O&ust=1652857870856000&source=images&cd=vfe&ved=0CAwQjRxqFwoTCLDBqsT95fcCFQAAAAAdAAAAABAD"}
        };
    }
}