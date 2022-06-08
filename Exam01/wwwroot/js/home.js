
function getBrowserName() {
    let userAgent = navigator.userAgent;
    let browserName;

    if (userAgent.match(/edg/i)) {
        browserName = "Edge";
    } else if (userAgent.match(/chrome|chromium|crios/i)) {
        browserName = "Chrome";
    } else if (userAgent.match(/firefox|fxios/i)) {
        browserName = "Firefox";
    } else if (userAgent.match(/safari/i)) {
        browserName = "Safari";
    } else if (userAgent.match(/opr\//i)) {
        browserName = "Opera";
    } else {
        browserName = "No browser detection";
    }
    return browserName;
}

document.cookie = "browserName=" + getBrowserName();
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/stockHub")
    .withAutomaticReconnect()
    .build();
connection.start().then(() => {
    loadData();
});
async function httpClient(enpoints, method = "GET", data = null) {
    var resp = await fetch(enpoints,
        {
            method,
            headers: {
                'Content-Type': 'application/json'
            },
            body: data ? JSON.stringify(data) : data,
        })
    return await resp.json();
}



/**
 * 
 * @param {object} rowObj 
 */
function createRowData(rowObj) {
    const Obj = {};
    const RowData = [];
    ColumnNameAggregate.forEach(subColumnAggregate => {
        var firstValue = rowObj[subColumnAggregate[0]];
        var className = getClassName(firstValue, rowObj[TC], rowObj[Tran], rowObj[San]);
        subColumnAggregate.forEach(columnName => {
            Obj[columnName] = [className];
        });
    })

    for (let key in rowObj) {
        if (!Obj.hasOwnProperty(key)) {
            Obj[key] = [];
        }
        switch (key) {
            case TC:
                Obj[key] = [YellowColor];
                break;
            case Tran:
                Obj[key] = [PurpleColor];
                break;
            case San:
                Obj[key] = [BlueColor];
                break;
        }
        var CellData = createCellData(rowObj[key], Obj[key], key);
        RowData.push(CellData);
    }
    return RowData;
}
function createCellData(cellData, cellClasslist, key) {
    var CellData = { shortName: key };
    if (cellData == 0) {
        CellData.data = null;
    } else {
        if (key == Percent) {
            CellData.data = cellData;
            CellData.customValue = "${value}%";
        } else {
            CellData.data = cellData;
        }

    }
    CellData.classList = cellClasslist;
    return CellData;
}




const Data = [];
async function loadData() {
    var resp = await httpClient(GetAllStockApiEndpoint, "Get");
    resp.forEach(obj => {
        const SubData = createRowData(obj);
        Data.push(SubData);
    });
    const TableObj = new TableCore(TableFieldConfigs, "#root");
    TableObj.setData(Data);

    const AllCheckBoxs = TableObj.initData.tableBody.querySelectorAll(`td[data-shortname="${Id}"] input`);
    AllCheckBoxs.forEach(checkbox => {
        var tr = checkbox.closest("tr");
        var id = tr.id;
        if (localStorage.getItem(id) === "true") {
            checkbox.checked = true;
            connection.invoke("JoinGroup", id);
        } else {
            checkbox.checked = false;
        }
        checkbox.addEventListener("change", handleCheckbox);
    })



    connection.on("AutoUpdate", function (rep) {
        var rowId = rep.id;
        var rowData = TableObj.getRowData(rowId);
        rep.cellValues.forEach(cell => {
            var td = document.querySelector('#' + rowId + ' td[data-shortName="' + cell.cellName + '"]');

            var cellIndex = td.dataset["index"];
            var cellData = rowData[cellIndex];
            cellData.data = cell.cellValue;

            TableObj.setCell(rowId, cellIndex, cellData);
            td.classList.add(CellActive);
            setTimeout(() => {
                td.classList.remove(CellActive);
            }, 500);
        });
        const newRowChanged = TableObj.getRowData(rowId);
        for (let i = 0; i < newRowChanged.length; i++) {
            ColumnNameAggregate.forEach(subColumnAggregate => {
                if (newRowChanged[i].shortName === subColumnAggregate[0]) {
                    var className = getClassName(newRowChanged[i].data, newRowChanged[1].data, newRowChanged[2].data, newRowChanged[3].data);
                    subColumnAggregate.forEach(sub => {
                        var td = document.querySelector('#' + rowId + ' td[data-shortName="' + sub + '"]');

                        var cellIndex;
                        cellIndex = td.dataset["index"];
                        var cellData;
                        if (sub == Id) {
                            var span = td.querySelector("span span");
                            var dataId = span.textContent;
                            cellData = { shortName: sub, data: dataId, classList: [className] }
                            cellIndex = 0;
                        } else {
                            cellData = newRowChanged[cellIndex];
                        }
                        var hasActiveClass = td.classList.contains(CellActive);
                        if (hasActiveClass) {
                            cellData.classList = [className, CellActive];
                        } else {
                            cellData.classList = [className];
                        }
                        TableObj.setCell(rowId, cellIndex, cellData);
                    });

                }
            })
        }
        const checkbox = TableObj.initData.tableBody.querySelector(`#${rowId} td[data-shortname="${Id}"] input`);
        checkbox.checked = true;
        checkbox.addEventListener("change", handleCheckbox)
    });
    connection.on("UpdateMessage", function (rep) {
        var rowId = rep.id;
        var rowData = TableObj.getRowData(rowId);
        rep.cellValues.forEach(cell => {
            var td = document.querySelector('#' + rowId + ' td[data-shortName="' + cell.cellName + '"]');

            var cellIndex = td.dataset["index"];
            var cellData = rowData[cellIndex];
            cellData.data = cell.cellValue;

            TableObj.setCell(rowId, cellIndex, cellData);
            td.classList.add(CellActive);
            setTimeout(() => {
                td.classList.remove(CellActive);
            }, 500);
        });
        const newRowChanged = TableObj.getRowData(rowId);
        for (let i = 0; i < newRowChanged.length; i++) {
            ColumnNameAggregate.forEach(subColumnAggregate => {
                if (newRowChanged[i].shortName === subColumnAggregate[0]) {
                    var className = getClassName(newRowChanged[i].data, newRowChanged[1].data, newRowChanged[2].data, newRowChanged[3].data);
                    subColumnAggregate.forEach(sub => {
                        var td = document.querySelector('#' + rowId + ' td[data-shortName="' + sub + '"]');

                        var cellIndex;
                        cellIndex = td.dataset["index"];
                        var cellData;
                        if (sub == Id) {
                            var span = td.querySelector("span span");
                            var dataId = span.textContent;
                            cellData = { shortName: sub, data: dataId, classList: [className] }
                            cellIndex = 0;
                        } else {
                            cellData = newRowChanged[cellIndex];
                        }
                        var hasActiveClass = td.classList.contains(CellActive);
                        if (hasActiveClass) {
                            cellData.classList = [className, CellActive];
                        } else {
                            cellData.classList = [className];
                        }
                        TableObj.setCell(rowId, cellIndex, cellData);
                    });

                }
            })
        }
        const checkbox = TableObj.initData.tableBody.querySelector(`#${rowId} td[data-shortname="${Id}"] input`);
        checkbox.checked = true;
        checkbox.addEventListener("change", handleCheckbox)
    });
    connection.on("RemoveCheckedBox", (resp) => {
        var checkbox = TableObj.initData.tableBody.querySelector(`#${resp} td[data-shortname="${Id}"] input`);
        checkbox.checked = false;
    });
    connection.on("CheckedBox", (resp) => {
        var checkbox = TableObj.initData.tableBody.querySelector(`#${resp} td[data-shortname="${Id}"] input`);
        checkbox.checked = true;
    });


}

/**
 * 
 * @param {Event} evt 
 */
async function handleCheckbox(evt) {
    var target = evt.target;
    var isChecked = target.checked;
    var trParent = target.closest("tr");
    var id = trParent.id;
    if (isChecked) {
        await connection.invoke("JoinGroup", id);
        localStorage.setItem(id, true);
    } else {
        await connection.invoke("RemoveGroup", id);
        localStorage.setItem(id, false);
    }
}



/**
 * 
 * @param {number} keyValue 
 * @param {number} tcValue 
 * @param {number} tranValue 
 * @param {number} sanValue 
 * @returns 
 */
function getClassName(keyValue, tcValue, tranValue, sanValue) {
    var classColor = "";
    if (keyValue == tranValue) {
        classColor = PurpleColor;
    }
    if (keyValue == sanValue) {
        classColor = BlueColor;
    }
    if (keyValue == tcValue) {
        classColor = YellowColor;
    }
    if (keyValue != tcValue && keyValue != sanValue && keyValue != tranValue) {
        if (keyValue > tcValue) {
            classColor = GreenColor;
        } else {
            classColor = RedColor;
        }
    }
    return classColor;
}





