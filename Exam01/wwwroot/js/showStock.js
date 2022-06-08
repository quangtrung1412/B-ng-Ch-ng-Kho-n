// var connection = new signalR.HubConnectionBuilder().withUrl("/stockHub").build();
// connection.start();
// async function httpClient(endpoints , method = "GET", data = null) {
//     var resp = await fetch(endpoints , {
//         method,
//         headers: {
//             "Content-Type": "application/json"
//         },
//         body: data ? JSON.stringify(data) : data,

//     });
//     return await resp.json();
// }

// const btnsubmit = document.querySelector("#btnSubmit");
// btnsubmit.addEventListener("click", handleClickSubmitForm);
// async function handleClickSubmitForm() {
//     var id = $("#Id").val();
//     var data =
//     {
//         Id: id,
//         TC: Number($("#TC").val()),
//         Tran:Number($("#Tran").val()),
//         San:Number($("#San").val()),
//         MuaG3:Number($("#MuaG3").val()),
//         MuaKL3:Number($("#MuaKL3").val()),
//         MuaG2:Number($("#MuaG2").val()),
//         MuaKL2:Number($("#MuaKL2").val()),
//         MuaG1:Number($("#MuaG1").val()),
//         MuaKL1:Number($("#MuaKL1").val()),
//         Gia:Number($("#Gia").val()),
//         KL:Number($("#KL").val()),
//         Percent:Number($("#Percent").val()),
//         BanG1:Number($("#BanG1").val()),
//         BanKL1:Number($("#BanKL1").val()),
//         BanG2:Number($("#BanG2").val()),
//         BanKL2:Number($("#BanKL2").val()),
//         BanG3:Number($("#BanG3").val()),
//         BanKL3:Number($("#BanKL3").val()),
//         TongKL:Number($("#TongKL").val()),
//         MoCua:Number($("#MoCua").val()),
//         CaoNhat:Number($("#CaoNhat").val()),
//         ThapNhat:Number($("#ThapNhat").val()),
//         NNMua:Number($("#NNMua").val()),
//         NNBan:Number($("#NNBan").val()),
//         RoomConLai:Number($("#RoomConLai").val()),
//     }
//     var resp = await updateStock(id,data);
//     connection.invoke("SendUpdateMessage", resp)

// }

// async function updateStock(id,data) {
//     return await httpClient("/api/Stock/updatestock/"+id,"PUT",data);
// }