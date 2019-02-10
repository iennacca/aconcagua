// aconcagua block start
const {GetMetadataRequest, SourceSeriesKey} = require('./aconcagua_pb.js');
const {TimeseriesDataServiceClient} = require('./aconcagua_grpc_web_pb.js');

var client = new TimeseriesDataServiceClient('http://localhost:50050', null, null);
// aconcagua block end

let wb = new $.ig.excel.Workbook($.ig.excel.WorkbookFormat.excel2007);
let ws = wb.worksheets().add("Sheet1");


$("#spreadsheet").igSpreadsheet({
    height: "600",
    width: "70%",
    workbook: wb,
    selectionChanged: onSelectionChanged
});

let database, observations;

$('#getversion').on("click", function () {
    try {
        var request = new proto.google.protobuf.Empty();
        $('#getversionstatus').text('');
        aconcaguaGetVersion(request, (err, response) => {
            var versionText = response.getVersion();
            $('#version').val(versionText);
            $('#getversionstatus').text('Test ok!');
        });
    }
    catch(err) {
        msg = 'Error:' + err.message;
        console.log(msg);
        $('#getversionstatus').text(msg);
    }
});

$('#getdata').on("click", function () {
    try {
        // pocLoadData();
        aconcaguaGetData(
            [
                [$('#database').val(), $('#seriescode').val()]
            ], 
            $('#metadata').val().split(','), 
            (err, response) => {
                $('#getversionstatus').text('Ok');
            });
    }
    catch(err) {
        msg = 'Error:' + err.message;
        console.log(msg);
        $('#getdatastatus').text(msg);
    }
});

function aconcaguaGetVersion(request, callback) {
    client.getVersion(request, {}, callback);
}

function aconcaguaGetData(searchSeriesList, metadataHeadersList, callback) {
    var request = createrequest(searchSeriesList, metadataHeadersList);
    client.getMetadata(request, {}, showresponse);

    function createrequest(sourceSeriesList, metadataHeadersList) {
        r = new GetMetadataRequest();
        var rm = r.getRequestmetadataMap().set('version','0.9');
        var mh = r.setMetadataheadersList(metadataHeadersList);

        var sl = new Array();        
        sourceSeriesList.forEach(s => {
            var ssk = new SourceSeriesKey();
            ssk.setSourcename(s[0]);
            ssk.setSeriesname(s[1]);
            sl.push(ssk);
            console.log('sourceSeries:' + ssk.getSourcename() + '.' + ssk.getSeriesname());
        });
        r.setKeysList(sl);

        console.log(r.getRequestmetadataMap().get('version'));
        console.log(r.getMetadataheadersList()[1]);
        console.log(r.getKeysList()[0]);
        return r;
    };

    function showresponse(err, response) {
        if (!err) {
            // add headers
            let firstRow = ws.rows(0);
            let headers = ['source', 'series'].concat(response.getMetadataheadersList());

            console.log(headers);
            headers.forEach((header, colIndex) => {
                firstRow.setCellValue(colIndex, header.trim());
            });

            // add data
             var r = response.getDatalistList();
             r.forEach((rowData, rowIndex) => {
                let wsRow = ws.rows(rowIndex + 1);
                let md = [
                    rowData.getKey().getSourcename(),
                    rowData.getKey().getSeriesname()
                ].concat(rowData.getDataList());

                md.forEach((cellData, cellIndex) => {
                    wsRow.setCellValue(cellIndex, cellData);
                });
            });        
        }
        callback(err, response);
    }
}

function pocLoadData() {
    // original click handler from Infragistics POC
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
}

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