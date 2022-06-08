using System.Text;
using System.Text.Json;
using Exam01.Application.Models;
using Exam01.Application.Service;
using Exam01.Hubs;
using Exam01.ViewModel;
using Microsoft.AspNetCore.SignalR;

namespace Exam01.BackgroundTasks;

public class AutoUpdateStockBackgroundTask : BackgroundService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly HttpClient _httpClient;
    private readonly IHubContext<StockHub> _stockHubContext;
    private readonly IHubContext<AdminHub> _adminHubContext;

    public AutoUpdateStockBackgroundTask(IHttpClientFactory httpClientFactory, HttpClient httpClient, IHubContext<StockHub> stockHubContext,IHubContext<AdminHub> adminHubContext)
    {
        _httpClientFactory = httpClientFactory;
        _httpClient = httpClient;
        _stockHubContext = stockHubContext;
        _adminHubContext = adminHubContext;
    }
    private Double CustomeRandomDouble(){
        Random random = new Random();
        string doubleStringRandom = random.Next(15,50) +"."+random.Next(0,9); 
        double doubleRandom = Convert.ToDouble(doubleStringRandom);
        return doubleRandom;
    } 

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        List<Stock> listStock = new List<Stock>();
        using (var httpResponseMessage =
        await _httpClient.GetAsync($"http://localhost:5220/api/Stock/listStock"))
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                listStock = await httpResponseMessage.Content.ReadFromJsonAsync<List<Stock>>();
            }
        }
        Random random = new Random();

        while (!stoppingToken.IsCancellationRequested)
        {
            int i = random.Next(0, listStock.Count);
            var properties = listStock[i].GetType().GetProperties();

            int randomNumberProperty = random.Next(1, 5);
            int number = 1;
            Stock stock = new Stock();
            stock = listStock.ToList().ElementAt(i);
            StockChangedViewModel stockChangedViewModel = new StockChangedViewModel();
            stockChangedViewModel.CellValues = new List<CellValueViewModel>();
            while (randomNumberProperty > number)
            {

                int j = random.Next(1, properties.Count());
                var propertyTypeName = properties[j].Name;
                var propertyType = properties[j].PropertyType;
                double doubleRandom = CustomeRandomDouble();
                int intRandom = random.Next(50, 100000);
                var cellValue = new CellValueViewModel();
                cellValue.CellName = propertyTypeName;
                // stock.GetType().GetProperty("TC").SetValue(stock, 26);
                var isDouble = propertyType.Equals(typeof(double));
                var isInt = propertyType.Equals(typeof(int));
                if(isDouble){
                    stock.GetType().GetProperty(propertyTypeName).SetValue(stock, doubleRandom);
                    cellValue.CellValue = stock.GetType().GetProperty(propertyTypeName).GetValue(stock);
                }
                if(isInt)
                {
                    stock.GetType().GetProperty(propertyTypeName).SetValue(stock, intRandom);
                    cellValue.CellValue = stock.GetType().GetProperty(propertyTypeName).GetValue(stock);
                }
                // switch (propertyTypeName)
                // {
                //     case "TC":
                //         stock.TC = doubleRandom;
                //         cellValue.CellValue = stock.TC;
                //         break;
                //     case "Tran":
                //         stock.Tran = doubleRandom;
                //         cellValue.CellValue = stock.Tran;
                //         break;
                //     case "San":
                //         stock.San = doubleRandom;
                //         cellValue.CellValue = stock.San;
                //         break;
                //     case "MuaG3":
                //         stock.MuaG3 = doubleRandom;
                //         cellValue.CellValue = stock.MuaG3;
                //         break;
                //     case "MuaKL3":
                //         stock.MuaKL3 = intRandom;
                //         cellValue.CellValue = stock.MuaKL3;
                //         break;
                //     case "MuaG2":
                //         stock.MuaG2 = doubleRandom;
                //         cellValue.CellValue = stock.MuaG2;
                //         break;
                //     case "MuaKL2":
                //         stock.MuaKL2 = intRandom;
                //         cellValue.CellValue = stock.MuaKL2;
                //         break;
                //     case "MuaG1":
                //         stock.MuaG1 = doubleRandom;
                //         cellValue.CellValue = stock.MuaG1;
                //         break;
                //     case "MuaKL1":
                //         stock.MuaKL1 = intRandom;
                //         cellValue.CellValue = stock.MuaKL1;
                //         break;
                //     case "Gia":
                //         stock.Gia = doubleRandom;
                //         cellValue.CellValue = stock.Gia;
                //         break;
                //     case "KL":
                //         stock.KL = intRandom;
                //         cellValue.CellValue = stock.KL;
                //         break;
                //     case "Percent":
                //         stock.Percent = doubleRandom;
                //         cellValue.CellValue = stock.Percent;
                //         break;
                //     case "BanG3":
                //         stock.BanG3 = doubleRandom;
                //         cellValue.CellValue = stock.TC;
                //         break;
                //     case "BanKL3":
                //         stock.BanKL3 = intRandom;
                //         cellValue.CellValue = stock.BanKL3;
                //         break;
                //     case "BanG2":
                //         stock.BanG2 = doubleRandom;
                //         cellValue.CellValue = stock.BanG2;
                //         break;
                //     case "BanKL2":
                //         stock.BanKL2 = intRandom;
                //         cellValue.CellValue = stock.BanKL2;
                //         break;
                //     case "BanG1":
                //         stock.BanG1 = doubleRandom;
                //         cellValue.CellValue = stock.BanG1;
                //         break;
                //     case "BanKL1":
                //         stock.BanKL1 = intRandom;
                //         cellValue.CellValue = stock.BanKL1;
                //         break;
                //     case "TongKL":
                //         stock.TongKL = intRandom;
                //         cellValue.CellValue = stock.TongKL;
                //         break;
                //     case "MoCua":
                //         stock.MoCua = doubleRandom;
                //         cellValue.CellValue = stock.MoCua;
                //         break;
                //     case "CaoNhat":
                //         stock.CaoNhat = doubleRandom;
                //         cellValue.CellValue = stock.CaoNhat;
                //         break;
                //     case "ThapNhat":
                //         stock.ThapNhat = doubleRandom;
                //         cellValue.CellValue = stock.ThapNhat;
                //         break;
                //     case "NNMua":
                //         stock.NNMua = intRandom;
                //         cellValue.CellValue = stock.NNMua;
                //         break;
                //     case "NNBan":
                //         stock.NNBan = intRandom;
                //         cellValue.CellValue = stock.NNBan;
                //         break;
                //     case "RoomConLai":
                //         stock.RoomConLai = intRandom;
                //         cellValue.CellValue = stock.RoomConLai;
                //         break;
                // }
                stockChangedViewModel.CellValues.Add(cellValue);
                number++;
            }
            var stockJson = new StringContent(
            JsonSerializer.Serialize(stock),
            Encoding.UTF8,
            System.Net.Mime.MediaTypeNames.Application.Json);
            using (var httpResponseMessage =
            await _httpClient.PutAsync($"http://localhost:5220/api/Stock/updatestock/{stock.Id}", stockJson))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var stockResp = await httpResponseMessage.Content.ReadFromJsonAsync<Stock>();
                    stockChangedViewModel.Id = stockResp.Id;
                    await _stockHubContext.Clients.Group(stock.Id).SendAsync("AutoUpdate", stockChangedViewModel);
                    await _adminHubContext.Clients.All.SendAsync("AutoUpdateStock",stock);
                }
            }
            await Task.Delay(random.Next(1000,3000));
        }
    }
}




