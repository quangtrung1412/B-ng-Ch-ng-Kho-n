const Id = "Id"
const TC = "TC"
const Tran = "Tran"
const San = "San"
const MuaG3 = "MuaG3"
const MuaKL3 = "MuaKL3"
const MuaG2 = "MuaG2"
const MuaKL2 = "MuaKL2"
const MuaG1 = "MuaG1"
const MuaKL1 = "MuaKL1"
const Gia = "Gia"
const KL = "KL"
const Percent = "Percent"
const BanG3 = "BanG3"
const BanKL3 = "BanKL3"
const BanG2 = "BanG2"
const BanKL2 = "BanKL2"
const BanG1 = "BanG1"
const BanKL1 = "BanKL1"
const TongKL = "TongKL"
const MoCua = "MoCua"
const CaoNhat = "CaoNhat"
const ThapNhat = "ThapNhat"
const NNMua = "NNMua"
const NNBan = "NNBan"
const RoomConLai = "RoomConLai"


//css ClassName
const YellowColor = "yellow-color";
const PurpleColor = "purple-color";
const BlueColor = "blue-color";
const GreenColor = "green-color";
const RedColor = "red-color";
const CellActive = "cell-active"

//apiEnpoint
const GetAllStockApiEndpoint = "api/Stock/listStock";
const TableFieldConfigs = [
    [
        {
            label: "Mã",
            rowSpan: 2,
            width: "5.5%",
            style: "text-align: left",
            customValue: "<span><input type='checkbox' /><span>${value}</span></span>",
            isRowID: true
        },
        {
            label: "TC",
            rowSpan: 2
        },
        {
            label: "Trần",
            rowSpan: 2
        },
        {
            label: "Sàn",
            rowSpan: 2
        },
        {
            label: "Mua",
            colSpan: 6
        },
        {
            label: "Khớp lệnh",
            colSpan: 3
        },
        {
            label: "Bán",
            colSpan: 6
        },
        {
            label: "Tổng",
            rowSpan: 2,
            width: "5.5%",
            formatType: "number"
        },
        {
            label: "Mở<br/>cửa",
            rowSpan: 2,
            formatType: "number"
        },
        {
            label: "Cao<br/>nhất",
            rowSpan: 2,
            formatType: "number"
        },
        {
            label: "Thấp<br/>nhất",
            rowSpan: 2,
            formatType: "number"

        },
        {
            label: "NN<br/>mua",
            rowSpan: 2,
            width: "5%",
            formatType: "number"
        },
        {
            label: "NN<br/>bán",
            rowSpan: 2,
            width: "5%",
            formatType: "number"
        },
        {
            label: "Room<br/>còn lại",
            rowSpan: 2,
            width: "5.6%",
            formatType: "number"
        }
    ],
    [
        {
            label: "G3",
            formatType: "number"
        },
        {
            label: "KL3",
            formatType: "number"
        },
        {
            label: "G2",
            formatType: "number"
        },
        {
            label: "KL2",
            formatType: "number"
        },
        {
            label: "G1",
            formatType: "number"
        },
        {
            label: "KL1",
            formatType: "number"
        },
        {
            label: "Gia",
            formatType: "number"
        },
        {
            label: "KL",
            formatType: "number"
        },
        {
            label: "&#8882;+/-&#8883;",
            formatType: "number"
        },
        {
            label: "G1",
            formatType: "number"
        },
        {
            label: "KL1",
            formatType: "number"
        },
        {
            label: "G2",
            formatType: "number"
        },
        {
            label: "KL2",
            formatType: "number"
        },
        {
            label: "G3",
            formatType: "number"
        },
        {
            label: "KL3",
            formatType: "number"
        }
    ]
]
const ColumnNameAggregate = [
    [MuaG3, MuaKL3],
    [MuaG2, MuaKL2],
    [MuaG1, MuaKL1],
    [Gia, KL, Percent, Id],
    [BanG1, BanKL1],
    [BanG2, BanKL2],
    [BanG3, BanKL3],
    [MoCua],
    [CaoNhat],
    [ThapNhat]
];




