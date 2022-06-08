const DEFAULT_CONFIG = {
    label: "Label", // {String} tiêu đề cho th
    rowSpan: 1, // {Number}
    colSpan: 1, // {Number}
    width: "auto", //{String} chiều rộng của một cột (%, px, rem, em)
    classList: null, //{Array<String>} mảng các class sẽ được thêm vào từng td
    isRowID: false, // thêm thuộc tính id cho thẻ tr có giá trị là giá trị của cell
    style: null, // {String} thêm style cho td
    isHide: false, // {Boolean} nếu là true cột sẽ bị ẩn đi
    formatType: "text", // {String} number hoặc text mặc định là text
    customValue: null, // {String} 
    shortName: null, // {String} shortName của cột sẽ được thêm vào từng td(data-shortName) 
    fullName: null,// {String} fullName của cột sẽ được thêm vào từng td(data-fullName) 
    dataset: null, // {Array<String>} Mảng các dataset sẽ được thêm vào td
}

const PREV_SVG = `<svg class="mn-table-icprev" focusable="false" viewBox="0 0 24 24" aria-hidden="true"><path d="M15.41 16.09l-4.58-4.59 4.58-4.59L14 5.5l-6 6 6 6z"></path></svg>`;
const NEXT_SVG = `<svg class="mn-table-icnext" focusable="false" viewBox="0 0 24 24" aria-hidden="true"><path d="M8.59 16.34l4.58-4.59-4.58-4.59L10 5.75l6 6-6 6z"></path></svg>`;
const LAST_SVG = `<svg class="mn-table-iclast" focusable="false" viewBox="0 0 24 24" aria-hidden="true"><path d="M5.59 7.41L10.18 12l-4.59 4.59L7 18l6-6-6-6zM16 6h2v12h-2z"></path></svg>`;
const FIRST_SVG = `<svg class="mn-table-icfirst" focusable="false" viewBox="0 0 24 24" aria-hidden="true"><path d="M18.41 16.59L13.82 12l4.59-4.59L17 6l-6 6 6 6zM6 6h2v12H6z"></path></svg>`;

const TABLE_CORE_WRAPPER_CLASS_NAME = "mn-table";
const TABLE_CORE_HEAD_CLASS_NAME = "mn-table-head";
const TABLE_CORE_BODY_CLASS_NAME = "mn-table-body";
const TABLE_CORE_HIDE_CLASS_NAME = "hide";
const TABLE_CORE_MAIN_TBODY_ID = "main-tbody";
const TABLE_CORE_PAGINATION_WRAPPER_CLASS_NAME = "mn-table-pagination-wrapper";
const TABLE_CORE_PAGINATION_CLASS_NAME = "mn-table-pagination";
const TABLE_CORE_PAGINATION_CAPTION_CLASS_NAME = "mn-table-pagination-caption";
const TABLE_CORE_PAGINATION_ACTION_CLASS_NAME = "mn-table-pagination-actions";
const CHANGE_PAGE_EVENT_NAME = "changePage";

function formatNumber(num, regex = /(\d)(?=(\d\d\d)+(?!\d))/g, replaceValue = "$1,") {
    if (typeof (num) === "number") {
        let arr = num.toString().split(".");
        arr[0] = arr[0].replace(regex, replaceValue);
        return arr.join(".");
    }
    return num;
}

function getAttributeArrConfigs(finalConfigs) {
    let attributeArrConfig = [];
    const row1Config = finalConfigs[0];
    const row2Config = finalConfigs[1];
    let m = 0;
    row1Config.forEach((cfg, ix) => {
        if (cfg.colSpan === 1) {
            attributeArrConfig.push(cfg);
            finalConfigs[0][ix].dataIndex = attributeArrConfig.length - 1;
        } else {
            attributeArrConfig = attributeArrConfig.concat(row2Config.slice(m, m + cfg.colSpan));
            let j = 0
            for (let i = m; i < m + cfg.colSpan; i++) {
                finalConfigs[1][i].dataIndex = attributeArrConfig.length - cfg.colSpan + j;
                j++;
            }
            m += cfg.colSpan;
        }
    });
    return attributeArrConfig;
}

function getParentColum(cfg, mark, rowIx, parentColumn = []) {
    if (mark.length == 0 || parentColumn.length == 2) {
        return parentColumn;
    }
    if (cfg.colSpan <= mark[0][0]) {
        mark[0][0] -= cfg.colSpan;
        parentColumn.push(mark[0][2]);
        parentColumn.push(mark[0][3]);
        if (mark[0][0] <= 0) {
            mark.shift();
        }
        return getParentColum(cfg, mark, rowIx, parentColumn);
    }
}

function getFinalConfigs(configs) {
    let finalConfigs = [];
    let mark = [];
    configs.forEach((value, ix1) => {
        let rowConfig = []
        value.forEach((cfg, ix2) => {
            let config = { ...DEFAULT_CONFIG, ...cfg };
            if (config.colSpan > 1) {
                mark.push([config.colSpan, config.rowSpan, ix2, ix1]);
            }
            if (ix1 !== 0) {
                let parentColumn = getParentColum(config, mark, ix1);
                if (parentColumn.length > 0) {
                    config.parentColumn = parentColumn;
                }
            }
            rowConfig.push(config);
        });
        finalConfigs.push(rowConfig);
    });
    return finalConfigs;
}


function createColGroupElement(attributeArrConfig) {
    const colGroupElement = document.createElement("colgroup");
    attributeArrConfig.forEach(cfg => {
        const colElement = document.createElement("col");
        colElement.style.width = cfg.width;
        if (cfg.isHide) {
            colElement.classList.add(TABLE_CORE_HIDE_CLASS_NAME);
        }
        colGroupElement.appendChild(colElement);
    });
    return colGroupElement;
}

function createTableHead(finalConfigs, colGroupElm) {
    const table_head = document.createElement("table");
    const thead = document.createElement("thead");
    table_head.appendChild(colGroupElm);
    finalConfigs.forEach(val => {
        const tr = document.createElement("tr");
        val.forEach(cfg => {
            const th = document.createElement("th");
            th.innerHTML = cfg.label;
            if (cfg.colSpan > 1) {
                th.colSpan = cfg.colSpan;
            }
            if (cfg.rowSpan > 1) {
                th.rowSpan = cfg.rowSpan;
            }
            if (cfg.isHide) {
                th.classList.add(TABLE_CORE_HIDE_CLASS_NAME);
                const parentThOffset = cfg.parentColumn;
                if (parentThOffset) {
                    const parentTh = thead.children[parentThOffset[1]].children[parentThOffset[0]];
                    if (parentTh.colSpan <= cfg.colSpan) {
                        parentTh.classList.add(TABLE_CORE_HIDE_CLASS_NAME);
                    }
                    parentTh.colSpan -= cfg.colSpan;
                }
            }
            if (cfg.dataIndex) {
                th.setAttribute("data-index", cfg.dataIndex);
            }
            if (cfg.dataset) {
                for (let key in cfg.dataset) {
                    if (key !== "index" && key !== "shortName" && key !== "fullName") {
                        th.setAttribute(`data-${key}`, cfg.dataset[key]);
                    }
                }
            }
            tr.appendChild(th);
        });
        thead.appendChild(tr);
    });
    table_head.classList.add(TABLE_CORE_HEAD_CLASS_NAME);
    table_head.appendChild(thead);

    return table_head;
}

function createTableBody(colGroupElm) {
    const table_body = document.createElement("table");
    const tbody = document.createElement("tbody");
    tbody.id = TABLE_CORE_MAIN_TBODY_ID;
    table_body.classList.add(TABLE_CORE_BODY_CLASS_NAME);
    table_body.append(colGroupElm, tbody);

    return table_body;
}

function createPagination(showFirstButton, showLastButton) {
    const tablepPaginationWrapper = document.createElement("div");
    const tablePagination = document.createElement("div");
    const tablePaginationCaption = document.createElement("p");
    const tablePaginationActionWrapper = document.createElement("div");
    const tablePaginationPrev = document.createElement("button");
    const tablePaginationNext = document.createElement("button");
    const tablePaginationFirst = document.createElement("button");
    const tablePaginationLast = document.createElement("button");

    tablePaginationFirst.innerHTML = FIRST_SVG;
    tablePaginationFirst.setAttribute("data-btn", "first");

    tablePaginationLast.innerHTML = LAST_SVG;
    tablePaginationLast.setAttribute("data-btn", "last");

    tablepPaginationWrapper.classList.add(TABLE_CORE_PAGINATION_WRAPPER_CLASS_NAME);
    tablePagination.classList.add(TABLE_CORE_PAGINATION_CLASS_NAME);
    tablePaginationCaption.classList.add(TABLE_CORE_PAGINATION_CAPTION_CLASS_NAME);
    tablePaginationActionWrapper.classList.add(TABLE_CORE_PAGINATION_ACTION_CLASS_NAME);

    tablePaginationPrev.innerHTML = PREV_SVG;
    tablePaginationNext.innerHTML = NEXT_SVG;
    tablePaginationPrev.setAttribute("data-btn", "prev");
    tablePaginationNext.setAttribute("data-btn", "next");
    tablePagination.append(tablePaginationCaption, tablePaginationActionWrapper);
    tablepPaginationWrapper.appendChild(tablePagination);

    if (showFirstButton) {
        tablePaginationActionWrapper.appendChild(tablePaginationFirst);
    }
    tablePaginationActionWrapper.appendChild(tablePaginationPrev);
    tablePaginationActionWrapper.appendChild(tablePaginationNext);
    if (showLastButton) {
        tablePaginationActionWrapper.appendChild(tablePaginationLast);
    }

    return tablepPaginationWrapper;
}

function init(configs, cssSelector) {
    const finalConfigs = getFinalConfigs(configs),
        attributeArrConfig = getAttributeArrConfigs(finalConfigs),
        colGroupElmForHead = createColGroupElement(attributeArrConfig),
        colGroupElmForBody = createColGroupElement(attributeArrConfig),
        tableHead = createTableHead(finalConfigs, colGroupElmForHead),
        tableBody = createTableBody(colGroupElmForBody);
    const wrapper = document.querySelector(cssSelector);
    const tableWrapper = document.createElement("div");
    tableWrapper.classList.add(TABLE_CORE_WRAPPER_CLASS_NAME);
    wrapper.appendChild(tableWrapper);
    tableWrapper.append(tableHead, tableBody);
    return {
        tableWrapper,
        tableHead,
        tableBody,
        attributeArrConfig,
        finalConfigs,
        colGroupElmForHead,
        colGroupElmForBody
    }
}

function setDataToTd(data, td, tdCf) {
    let innerHTML = null;
    if (typeof (data) == "object") {
        innerHTML = data.data;
        tdCf = { ...tdCf, ...data }
    } else {
        innerHTML = data;
    }
    if (tdCf.style) {
        td.style = tdCf.style;
    }
    if (tdCf.isHide) {
        td.classList.add(TABLE_CORE_HIDE_CLASS_NAME);
    }
    if (tdCf.formatType === "number") {
        innerHTML = formatNumber(innerHTML);
    }
    if (tdCf.customValue) {
        innerHTML = tdCf.customValue.replace(/\${value}/g, innerHTML);
    }
    if (tdCf.shortName) {
        if (typeof (data) == "object") {
            td.setAttribute("data-shortName", tdCf.shortName.replace(/\${value}/g, data.data));
        } else {
            td.setAttribute("data-shortName", tdCf.shortName.replace(/\${value}/g, data));
        }
    }
    if (tdCf.fullName) {
        if (typeof (data) == "object") {
            td.setAttribute("data-fullName", tdCf.fullName.replace(/\${value}/g, data.data));
        } else {
            td.setAttribute("data-fullName", tdCf.fullName.replace(/\${value}/g, data));
        }
    }
    if (tdCf.dataIndex) {
        td.setAttribute("data-index", tdCf.dataIndex);
    }
    if (tdCf.classList) {
        tdCf.classList.forEach(className => {
            td.classList.add(className);
        })
    }
    if (tdCf.dataset) {
        for (let key in tdCf.dataset) {
            if (key !== "index" && key !== "shortName" && key !== "fullName") {
                td.setAttribute(`data-${key}`, tdCf.dataset[key]);
            }
        }
    }
    td.innerHTML = innerHTML;
}

function getTotalPages(rowCount, pageSize) {
    if (rowCount % pageSize === 0) {
        return Math.round(rowCount / pageSize);
    } else {
        return (Math.floor(rowCount / pageSize) + 1);
    }
}

function dispatchChangePage(target, currentPage) {
    const e = new CustomEvent(CHANGE_PAGE_EVENT_NAME, { detail: { currentPage } });
    target.dispatchEvent(e);
}

function setCaption(captionElm, currentPage, pageSize, rowCount) {
    const startItem = (currentPage - 1) * pageSize + 1;
    let endItem = startItem + pageSize - 1;
    endItem = endItem <= rowCount ? endItem : rowCount;
    captionElm.innerText = `${startItem}-${endItem} / ${rowCount}`;
}

function editClassListForPaginationBtn(currentPage, totalPages, paginationAction) {
    const btnPrev = paginationAction.querySelector("button[data-btn='prev']");
    const btnNext = paginationAction.querySelector("button[data-btn='next']");
    const btnFirst = paginationAction.querySelector("button[data-btn='first']");
    const btnLast = paginationAction.querySelector("button[data-btn='last']");

    btnPrev.classList.remove("disabled");
    btnNext.classList.remove("disabled");
    if (btnFirst) {
        btnFirst.classList.remove("disabled");
    }
    if (btnLast) {
        btnLast.classList.remove("disabled");
    }
    if (currentPage == 1) {
        btnPrev.classList.add("disabled");
        if (btnFirst) {
            btnFirst.classList.add("disabled");
        }
    } else if (currentPage === totalPages) {
        btnNext.classList.add("disabled");
        if (btnLast) {
            btnLast.classList.add("disabled");
        }
    }
}

/**
 * 
 * @param {number} tbodyIndex 
 * @param {HTMLTableElement} tableBody 
 * @returns 
 */
function getTableBody(tbodyIndex, tableBody) {
    return tbodyIndex != undefined
        ? tableBody.tBodies[tbodyIndex]
        : tableBody.querySelector(`tbody#${TABLE_CORE_MAIN_TBODY_ID}`);
}


class TableCore {
    /**
    * 
    * @param {Array<Array<Object>>} configs 
    * @param {String} cssSelector 
    */
    constructor(configs, cssSelector) {
        this.initData = init(configs, cssSelector);
        this.data = [];
    }

    /**
     * 
     * @param {Number} tbodyIndex 
     */
    clearData(tbodyIndex) {
        const tbodyElement = getTableBody(tbodyIndex, this.initData.tableBody);
        if (tbodyElement) {
            this.data = [];
            tbodyElement.innerHTML = '';
        }
    }

    /**
      * Thiết lập dữ liệu cho bảng
      * @param {Array<Array<Object>>} data 
      * @param {Number} tbodyIndex
      */
    setData(data, tbodyIndex) {
        this.clearData(tbodyIndex);
        data.forEach(dtRow => {
            this.addRow(dtRow, tbodyIndex);
        });
    }

    deleteRow(rowId, tbodyIndex) {
        const tbodyElement = getTableBody(tbodyIndex, this.initData.tableBody);
        if (tbodyElement) {
            const tr = this.getRow(rowId, tbodyIndex);
            if (tr) {
                const attributeArrConfig = this.initData.attributeArrConfig;
                const cfIndex = attributeArrConfig.findIndex(cfg => cfg.isRowID);
                const dtIndex = this.data.findIndex(dt => {
                    if (typeof (dt) == "object") {
                        return dt[cfIndex].data == rowId
                    } else {
                        return dt[cfIndex] == rowId;
                    }
                });
                this.data.splice(dtIndex, 1);
                tbodyElement.removeChild(tr);
                return tr;
            }
        }
        return null;
    }

    /**
     * 
     * @param {any} rowId 
     * @param {number} tbodyIndex 
     * @returns {HTMLTableRowElement}
     */
    getRow(rowId, tbodyIndex) {
        const tbodyElement = getTableBody(tbodyIndex, this.initData.tableBody);
        if (tbodyElement) {
            return tbodyElement.querySelector(`#${rowId}`);
        }
        return null;
    }

    /**
     * 
     * @param {*} rowId 
     * @param {number} tbodyIndex 
     * @returns {Array}
     */
    getRowData(rowId, tbodyIndex) {
        const tr = this.getRow(rowId, tbodyIndex);
        if (tr) {
            return this.data[tr.rowIndex];
        }
        return null;
    }

    setCell(rowId, cellIndex, data, tbodyIndex) {
        const tbodyElement = getTableBody(tbodyIndex, this.initData.tableBody);
        if (tbodyElement) {
            var tr = this.getRow(rowId, tbodyIndex);
            if (tr) {
                var td = tr.cells[cellIndex];
                this.data[tr.rowIndex][cellIndex] = data;
                const attributeArrConfig = this.initData.attributeArrConfig;
                const cf = attributeArrConfig[cellIndex];
                if (cf.isRowID) {
                    if (typeof (data) == "object") {
                        tr.id = data.data;
                    } else {
                        tr.id = data;
                    }
                }
                td.classList = [];
                setDataToTd(data, td, cf);
                return td;
            }
        }
        return null;
    }

    setRow(rowId, data, tbodyIndex) {
        const tbodyElement = getTableBody(tbodyIndex, this.initData.tableBody);
        if (tbodyElement) {
            var tr = this.getRow(rowId, tbodyIndex);
            this.data[tr.rowIndex] = data;
            if (tr) {
                tr.innerHTML = '';
                const attributeArrConfig = this.initData.attributeArrConfig;
                for (let i = 0; i < attributeArrConfig.length; i++) {
                    const cf = attributeArrConfig[i];
                    const td = document.createElement("td");
                    if (cf.isRowID) {
                        if (typeof (data[i]) == "object") {
                            tr.id = data[i].data;
                        } else {
                            tr.id = data[i];
                        }
                    }
                    setDataToTd(data[i], td, cf);
                    tr.appendChild(td);
                }
                return tr;
            }
        }
        return null;
    }

    /**
     * Thêm một hàng mới vào bảng
     * @param {Array<Object>} data 
     * @param {Number} tbodyIndex
     * @returns Thẻ tr vừa được thêm
     */
    addRow(data, tbodyIndex) {
        const tbodyElement = getTableBody(tbodyIndex, this.initData.tableBody);
        if (tbodyElement) {
            this.data.push(data);
            const tr = document.createElement("tr");
            const attributeArrConfig = this.initData.attributeArrConfig;
            for (let i = 0; i < attributeArrConfig.length; i++) {
                const cf = attributeArrConfig[i];
                const td = document.createElement("td");
                if (cf.isRowID) {
                    if (typeof (data[i]) == "object") {
                        tr.id = data[i].data;
                    } else {
                        tr.id = data[i];
                    }
                }
                setDataToTd(data[i], td, cf);
                tr.appendChild(td);
            }
            tbodyElement.appendChild(tr);
            return tr;
        }
        return null;
    }
    /**
     * Hiển thị một cột đang bị ẩn 
     * @param {String | Number} identify - index của column hoặc shortName hoặc fullName
     */
    showColumn(identify) {
        const tableHead = this.initData.tableHead;
        const tableBody = this.initData.tableBody;
        const tbodys = this.initData.tableBody.tBodies;
        const colGroupForBody = this.initData.colGroupElmForBody;
        const colGroupForHead = this.initData.colGroupElmForHead;
        const attributeArrConfig = this.initData.attributeArrConfig;
        let firstTd;
        if (typeof (identify) == "string") {
            firstTd = tableBody.querySelector(`td[data-shortName='${identify}']`);
            if (!firstTd) {
                firstTd = tableBody.querySelector(`td[data-fullName='${identify}']`);
            }
        } else {
            firstTd = tableBody.querySelector(`td[data-index='${identify}']`);
        }
        const columnIndex = firstTd.dataset.index;
        const th = tableHead.querySelector(`th[data-index='${columnIndex}']`);
        if (th && th.classList.contains(HIDE_CLASS_NAME)) {
            this.initData.attributeArrConfig[columnIndex].isHide = false;
            th.classList.remove(HIDE_CLASS_NAME);
            const parentThOffset = attributeArrConfig[columnIndex].parentColumn;
            if (parentThOffset) {
                const parentTh = tableHead.tHead.children[parentThOffset[1]].children[parentThOffset[0]];
                if (parentTh.classList.contains(HIDE_CLASS_NAME)) {
                    parentTh.colSpan = attributeArrConfig[columnIndex].colSpan;
                    parentTh.classList.remove(HIDE_CLASS_NAME);
                } else {
                    parentTh.colSpan += attributeArrConfig[columnIndex].colSpan;
                }
            }
            colGroupForHead.children[columnIndex].classList.remove(HIDE_CLASS_NAME);
            colGroupForBody.children[columnIndex].classList.remove(HIDE_CLASS_NAME);
            for (let ix = 0; ix < tbodys.length; ix++) {
                const rows = tbodys[ix].rows;
                for (let i = 0; i < rows.length; i++) {
                    rows[i].cells[columnIndex].classList.remove(HIDE_CLASS_NAME);
                }
            }
        }
    }

    /**
     * Ẩn một cột
     * @param {String | Number} identify - index của column hoặc shortName hoặc fullName
     */
    hideColumn(identify) {
        const tableHead = this.initData.tableHead;
        const tableBody = this.initData.tableBody;
        const tbodys = this.initData.tableBody.tBodies;
        const colGroupForBody = this.initData.colGroupElmForBody;
        const colGroupForHead = this.initData.colGroupElmForHead;
        const attributeArrConfig = this.initData.attributeArrConfig;
        let firstTd;
        if (typeof (identify) == "string") {
            firstTd = tableBody.querySelector(`td[data-shortName='${identify}']`);
            if (!firstTd) {
                firstTd = tableBody.querySelector(`td[data-fullName='${identify}']`);
            }
        } else {
            firstTd = tableBody.querySelector(`td[data-index='${identify}']`);
        }
        const columnIndex = firstTd.dataset.index;
        const th = tableHead.querySelector(`th[data-index='${columnIndex}']`);
        if (th && !th.classList.contains(HIDE_CLASS_NAME)) {
            this.initData.attributeArrConfig[columnIndex].isHide = true;
            th.classList.add(HIDE_CLASS_NAME);
            const parentThOffset = attributeArrConfig[columnIndex].parentColumn;
            if (parentThOffset) {
                const parentTh = tableHead.tHead.children[parentThOffset[1]].children[parentThOffset[0]];
                if (parentTh.colSpan <= attributeArrConfig[columnIndex].colSpan) {
                    parentTh.classList.add(HIDE_CLASS_NAME);
                }
                parentTh.colSpan -= attributeArrConfig[columnIndex].colSpan;
            }
            colGroupForHead.children[columnIndex].classList.add(HIDE_CLASS_NAME);
            colGroupForBody.children[columnIndex].classList.add(HIDE_CLASS_NAME);
            for (let ix = 0; ix < tbodys.length; ix++) {
                const rows = tbodys[ix].rows;
                for (let i = 0; i < rows.length; i++) {
                    rows[i].cells[columnIndex].classList.add(HIDE_CLASS_NAME);
                }
            }
        }
    }

    addPagination(configs) {
        let { defaultPage, pageSize, rowCount, showFirstButton, showLastButton } = configs;
        const totalRows = this.initData.tableBody.rows.length;
        let currentPage = 1;
        const oldPagination = this.initData.tableWrapper.querySelector('.' + TABLE_CORE_PAGINATION_WRAPPER_CLASS_NAME);
        if (oldPagination) {
            oldPagination.remove();
        }
        if (!pageSize) {
            pageSize = totalRows;
        }
        if (!rowCount) {
            rowCount = totalRows;
        }
        const totalPages = getTotalPages(rowCount, pageSize);
        const paginationWrapper = createPagination(showFirstButton, showLastButton);
        if (pageSize < rowCount && rowCount >= totalRows) {
            const paginationAction = paginationWrapper.querySelector('.' + TABLE_CORE_PAGINATION_ACTION_CLASS_NAME);
            if (defaultPage > 1 && defaultPage <= totalPages) {
                currentPage = defaultPage;
            }
            editClassListForPaginationBtn(currentPage, totalPages, paginationAction);
            const caption = paginationWrapper.querySelector('.' + TABLE_CORE_PAGINATION_CAPTION_CLASS_NAME);
            setCaption(caption, currentPage, pageSize, rowCount);
            const actionBtns = paginationWrapper.querySelectorAll('.' + TABLE_CORE_PAGINATION_ACTION_CLASS_NAME + ' button');
            actionBtns.forEach(btn => {
                const dataBtn = btn.dataset.btn;
                btn.addEventListener("click", evt => {
                    switch (dataBtn) {
                        case "prev":
                            if (currentPage > 1) {
                                currentPage--;
                                setCaption(caption, currentPage, pageSize, rowCount);
                                dispatchChangePage(paginationWrapper, currentPage);
                                editClassListForPaginationBtn(currentPage, totalPages, paginationAction);
                            }
                            break;
                        case "next":
                            if (currentPage < totalPages) {
                                currentPage++;
                                setCaption(caption, currentPage, pageSize, rowCount);
                                dispatchChangePage(paginationWrapper, currentPage);
                                editClassListForPaginationBtn(currentPage, totalPages, paginationAction);
                            }
                            break;
                        case "last":
                            if (currentPage < totalPages) {
                                currentPage = totalPages;
                                setCaption(caption, currentPage, pageSize, rowCount);
                                dispatchChangePage(paginationWrapper, currentPage);
                                editClassListForPaginationBtn(currentPage, totalPages, paginationAction);
                            }
                            break;
                        case "first":
                            if (currentPage > 1) {
                                currentPage = 1;
                                setCaption(caption, currentPage, pageSize, rowCount);
                                dispatchChangePage(paginationWrapper, currentPage);
                                editClassListForPaginationBtn(currentPage, totalPages, paginationAction);
                            }
                            break;
                    }
                });
            })

            this.initData.tableWrapper.appendChild(paginationWrapper);
        }
        return {
            element: paginationWrapper,
            onchangePage: callBack => {
                paginationWrapper.addEventListener("changePage", evt => {
                    callBack(evt.detail.currentPage);
                })
            }
        }
    }
}