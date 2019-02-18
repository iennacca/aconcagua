// aconcagua block start
const {GetSeriesKeysRequest, GetMetadataRequest, SourceSeriesKey} = require('./aconcagua_pb.js');
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
        $('#getversion_status').text('');
        aconcaguaGetVersion((err, response) => {
            $('#getversion_status').text(
                (err ? err : 'OK')
            );
        });
    }
    catch(err) {
        console.log(err.message);
        $('#getversion_status').text(err.message);
    }
});

$('#getserieskeys').on("click", function () {
    try {
        $('#getserieskeys_status').text('');
        aconcaguaGetSeriesKeys(
            $('#getserieskeys_sources').val().split(','), 
            $('#getserieskeys_filters').val().split(','), 
            (err, response) => {
                $('#getserieskeys_status').text(
                    (err ? err : 'OK')
                );
            });
    }
    catch(err) {
        console.log(err.message);
        $('#getserieskeys_status').text(err.message);
    }
});

$('#getdata').on("click", function () {
    try {
        // pocLoadData();
        $('#getdata_status').text('');
        aconcaguaGetMetadata(
            $('#getdata_sources').val().split(','), 
            $('#getdata_seriescodes').val().split(','),
            $('#getdata_metadata').val().split(','), 
            (err, response) => {
                $('#getdata_status').text(' OK');
            });
    }
    catch(err) {
        console.log(err.message);
        $('#getdata_status').text(err.message);
    }
});

function aconcaguaGetVersion(callback) {
    var request = createrequest();
    client.getVersion(request, {}, showresponse);

    function createrequest() {
        return new proto.google.protobuf.Empty();
    }

    function showresponse(err, response) {
        if (!err) {
            var versionText = response.getVersion();
            $('#getversion_version').val(versionText);
        }
        callback(err, response);
    }
}

function aconcaguaGetSeriesKeys(sourcenames, filters, callback) {
    var request = createrequest(sourcenames, filters);
    client.getSeriesKeys(request, {}, showresponse);

    function createrequest(sourcenames, filters) {
        r = new GetSeriesKeysRequest();
        var rm = r.getRequestmetadataMap().set('version','0.9');
        var mh = r.setSourcenamesList(sourcenames);

        var sl = new Array();        
        filters.forEach(f => {
            var s = f.split(':');
            r.getFiltersMap().set(s[0],s[1]);
            console.log('filter: ' + s[0] + ': ' + s[1]);
        });

        console.log(r.getRequestmetadataMap().get('version'));
        console.log(r.getSourcenamesList());
        console.log(r.getFiltersMap());
        return r;
    };

    function showresponse(err, response) {
        if (!err) {
            // add headers
            let firstRow = ws.rows(0);
            let headers = ['source', 'series'];

            console.log(headers);
            headers.forEach((header, colIndex) => {
                firstRow.setCellValue(colIndex, header.trim());
            });

            // add data
             var r = response.getKeysList();
             r.forEach((rowData, rowIndex) => {
                let wsRow = ws.rows(rowIndex + 1);
                let md = [
                    rowData.getSourcename(),
                    rowData.getSeriesname()
                ];

                md.forEach((cellData, cellIndex) => {
                    wsRow.setCellValue(cellIndex, cellData);
                });
            });        
        }
        callback(err, response);
    }
}

function aconcaguaGetMetadata(sourceList, seriesCodeList, metadataHeadersList, callback) {
    var request = createrequest(sourceList, seriesCodeList, metadataHeadersList);
    client.getMetadata(request, {}, showresponse);

    function createrequest(sourceList, seriesCodeList, metadataHeadersList) {
        r = new GetMetadataRequest();
        var rm = r.getRequestmetadataMap().set('version','0.9');
        var mh = r.setMetadataheadersList(metadataHeadersList);

        var sl = new Array(); 
        sourceList.forEach(s => {       
            seriesCodeList.forEach(sc => {
                var ssk = new SourceSeriesKey();
                ssk.setSourcename(s);
                ssk.setSeriesname(sc);
                sl.push(ssk);
                console.log('sourceSeries:' + ssk.getSourcename() + '.' + ssk.getSeriesname());
            })
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
             var r = response.getSeriesdataList();
             r.forEach((rowData, rowIndex) => {
                let wsRow = ws.rows(rowIndex + 1);
                let md = [
                    rowData.getKey().getSourcename(),
                    rowData.getKey().getSeriesname()
                ].concat(rowData.getValuesList());

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