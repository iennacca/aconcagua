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
        var query = new GetVersion();
        query.Run((err, response) => {
            query.ShowResponse(err, response);
            $('#getversion_status').text((err ? err.message : 'OK'));
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
        var query = new GetSeriesKeys();
        query.Run(
            $('#getserieskeys_sources').val().split(','), 
            $('#getserieskeys_filters').val().split(','), 
            (err, response) => {
                query.ShowResponse(err, response);
                $('#getserieskeys_status').text((err ? err.message : 'OK'));
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
        var query = new GetMetadata();
        query.Run(
            $('#getdata_sources').val().split(','), 
            $('#getdata_seriescodes').val().split(','),
            $('#getdata_metadata').val().split(','), 
            (err, response) => {
                query.ShowResponse(err, response);
                $('#getdata_status').text((err ? err.message : 'OK'));
            });
    }
    catch(err) {
        console.log(err.message);
        $('#getdata_status').text(err.message);
    }
});

$('#getsheetdata').on("click", function () {
    try {
        $('#getsheetdata_status').text('');
        var keysquery = new GetSeriesKeys();
        keysquery.Run(
            [$('#getsheetdata_source').val()], 
            $('#getsheetdata_filters').val().split(','), 
            (err, response) => {
                keysquery.ShowResponse(err, response);

                var dataquery = new GetMetadata();
                dataquery.Run(
                    [$('#getsheetdata_source').val()], 
                    response.getKeysList().map(k => k.getSeriesname()),
                    $('#getsheetdata_metadata').val().split(','), 
                    (err, response) => {
                        dataquery.ShowResponse(err, response);
                        $('#getsheetdata_status').text((err ? err.message : 'OK'));
                    });
                $('#getsheetdata_status').text((err ? err.message : 'OK'));                
        });
    }
    catch(err) {
        console.log(err.message);
        $('#getsheetdata_status').text(err.message);
    }
});


function GetVersion() {
    this.Run = function(callback) {
        var request = this.CreateRequest();
        client.getVersion(request, {}, callback);
    }

    this.CreateRequest = function() {
        return new proto.google.protobuf.Empty();
    }

    this.ShowResponse = function(err, response) {
        if (!err) {
            var versionText = response.getVersion();
            $('#getversion_version').val(versionText);
        }
    }
}

function GetSeriesKeys() {
    this.Run = function (sourcenames, filters, callback) {
        var request = this.CreateRequest(sourcenames, filters);
        client.getSeriesKeys(request, {}, callback);
    }

    this.CreateRequest = function(sourcenames, filters) {
        r = new GetSeriesKeysRequest();
        r.getRequestmetadataMap().set('version','0.9');
        r.setSourcenamesList(sourcenames);

        filters.forEach(f => {
            var s = f.split(':');
            r.getFiltersMap().set(s[0],s[1]);
        });
        return r;
    }

    this.ShowResponse = function(err, response) {
        if (!err) {
            // add headers
            let firstRow = ws.rows(0);
            let headers = ['source', 'series'];

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
    }
}

function GetMetadata() {
    this.Run = function(sourceList, seriesCodeList, metadataHeadersList, callback) {
        var request = this.CreateRequest(sourceList, seriesCodeList, metadataHeadersList);
        client.getMetadata(request, {}, callback);
    }

    this.CreateRequest = function(sourceList, seriesCodeList, metadataHeadersList) {
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
    }

    this.ShowResponse = function(err, response) {
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