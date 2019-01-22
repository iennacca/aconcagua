let wb = new $.ig.excel.Workbook($.ig.excel.WorkbookFormat.excel2007);
let ws = wb.worksheets().add("Sheet1");

$("#spreadsheet").igSpreadsheet({
    height: "600",
    width: "70%",
    workbook: wb,
    selectionChanged: onSelectionChanged
});

let database, observations;

$('button2').on("click", function () {
    console.log('into button2');
    const {GetVersionRequest, GetVersionReply} = require('./aconcagua_pb.js');
    const {AconcaguaClient} = require('./aconcagua_grpc_web_pb.js');
    
    var client = new AconcaguaClient('http://localhost:50051');
    
    var request = new GetVersionRequest();
    
    client.getVersion(request, {}, (err, response) => {
      console.log(response.getVersion());
    });
});


$('button1').on("click", function () {
    let url = new URL('https://localhost:44397/api/testdata');
    observations = document.querySelector('#observations').value;
    database = document.querySelector('#database').value;
    let searchParams = new URLSearchParams({
        "database": database,
        "observations": observations
    });
    url.search = searchParams;

    fetch(url)
        .then(function (response) {
            return response.json();
        })
        .then(loadData);
});

function loadData(data) {
    // add headers
    let firstRow = ws.rows(0);
    let headers = observations.split(',');

    headers.forEach((header, colIndex) => {
        firstRow.setCellValue(0, 'Database');
        firstRow.setCellValue(colIndex + 1, header.trim());
    });

    // add data
    data.forEach((rowData, rowIndex) => {
        let wsRow = ws.rows(rowIndex + 1);
        wsRow.setCellValue(0, database);
        rowData.forEach((cellData, cellIndex) => {
            wsRow.setCellValue(cellIndex + 1, cellData);
        });
    });
}

function onSelectionChanged(evt, ui) {
    var activeCellRangeIdx = ui.owner.getActiveSelection().activeCellRangeIndex();
    var rng = ui.owner.getActiveSelection().cellRanges().item(activeCellRangeIdx);

    if (rng.firstRow() !== rng.lastRow()){
        return;
    }

    let firstRow = ws.rows(0);
    let firstCol = rng.firstColumn();
    let selectedRow = ws.rows(rng.firstRow());
    chartData = [];

    for (let index = firstCol; index < rng.lastColumn() + 1; index++){
        chartData.push({
            observation: firstRow.getCellValue(index),
            data: selectedRow.getCellValue(index)
        });
    }

    console.log(chartData);
    $('#chart').igDataChart("option", "dataSource", chartData);

    evt.stopPropagation();
    return true;
    //chartData = [{ observation: "OValue17", data: -0.217 }, ...];
}

var chartData;

$('#chart').igDataChart({
    height: "250px",
    width: "25%",
    dataSource: chartData,
    axes: [
        {
            name: "xAxis",
            type: "categoryX",
            label: "observation",
            labelTextStyle: "8pt Verdana"
        },
        {
            name: "yAxis",
            type: "numericY"
        }
    ],
    series: [
        {
            name: "test",
            type: "line",
            xAxis: "xAxis",
            yAxis: "yAxis",
            valueMemberPath: "data"
        }
    ]
});