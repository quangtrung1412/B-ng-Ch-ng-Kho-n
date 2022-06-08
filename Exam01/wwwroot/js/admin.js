var connection = new signalR.HubConnectionBuilder()
    .withUrl("/adminHub")
    .withAutomaticReconnect()
    .build();
connection.start();
connection.on("OnConnection", function (numberConnect, numberUser, userConnectionViewModels) {
    createHtml(numberConnect, numberUser, userConnectionViewModels);
})

connection.on("Connection", function (numberConnect, numberUser, userConnectionViewModels) {

    createHtml(numberConnect, numberUser, userConnectionViewModels);

});

connection.on("DisConnect", function (numberDisConnect, numberUser, userConnectionViewModels) {
    createHtml(numberDisConnect, numberUser, userConnectionViewModels);
});
function createHtml(numberConnect, numberUser, userConnectionViewModels) {
    var connection = document.querySelector(".connection span");
    var userConnect = document.querySelector(".userConnection span");
    connection.textContent = numberConnect;
    userConnect.textContent = numberUser;
    var tBody = $("#tbody");
    var stringHtml = "";
    userConnectionViewModels.forEach((userConnectionViewModel) => {
        var userName = userConnectionViewModel.userName;
        var Email = userConnectionViewModel.userIdentifier;
        var NumberConnection = userConnectionViewModel.connectionInfos.length;
        var ConnectionInf = JSON.stringify(userConnectionViewModel.connectionInfos);
        stringHtml += `<tr id="${Email}">`,
            stringHtml += `<th scope="row">${Email}</th>`,
            stringHtml += `<td>${userName}</td>`,
            stringHtml += `<td>${NumberConnection}</td>`,
            stringHtml += `<td><p class="btn btn-info " data-bs-toggle="modal"  data-bs-target="#exampleModal">Xem</p></td>`,
            stringHtml += `</tr>`
    });
    tBody.html(stringHtml);
    var btnViews = document.querySelectorAll("#tbody td p");
    btnViews.forEach(btnView => {
        btnView.addEventListener("click", (evt) => {
            var target = evt.target;
            var trParent = target.closest("tr");
            var email = trParent.id;
            var Html = "";
            userConnectionViewModels.forEach((userConnectionViewModel) => {
                if(userConnectionViewModel.userIdentifier === email){
                    var tBodyModal = $("#tbodyModal");
                    document.querySelector("#exampleModalLabel").textContent = userConnectionViewModel.userIdentifier;
                    userConnectionViewModel.connectionInfos.forEach(connectionInfor=>{
                        var connectionId = connectionInfor.connectionId;
                        var browserName = connectionInfor.browserName;
                        var ipAddress = connectionInfor.ipAddress;
                        Html += `<tr id="${connectionId}">`,
                        Html += `<td>${connectionId}</td>`,
                        Html += `<td>${browserName}</td>`,
                        Html += `<td>${ipAddress}</td>`,
                        Html += `</tr>`
                    })
                    tBodyModal.html(Html);

                }
            })
        });
    });
}


connection.on("AutoUpdateStock",(resp)=>{
    var tc  = document.querySelector(`#${resp.id} .${TC}`);
    var tran  = document.querySelector(`#${resp.id} .${Tran}`);
    var san  = document.querySelector(`#${resp.id} .${San}`);
    var tongKL  = document.querySelector(`#${resp.id} .${TongKL}`);
    var roomConLai  = document.querySelector(`#${resp.id} .${RoomConLai}`);
    tc.textContent=resp.tc;
    tran.textContent=resp.tran;
    san.textContent=resp.san;
    tongKL.textContent =resp.tongKL;
    roomConLai.textContent= resp.roomConLai;
})

