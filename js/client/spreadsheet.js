// aconcagua block start
const {GetSeriesKeysRequest, GetMetadataRequest, SourceSeriesKey} = require('./aconcagua_pb.js');
const {TimeseriesDataServiceClient} = require('./aconcagua_grpc_web_pb.js');

var client = new TimeseriesDataServiceClient('http://localhost:50450', null, null);
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

        query.Run().
            then((response) => {
                $('#getversion_status').text('Ok');
            }).catch((err) => {
                $('#getversion_status').text(err.message);
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
            $('#getserieskeys_filters').val()
        ).then((response) => {
            $('#getserieskeys_status').text('Ok');
        }).catch((err) => {
            $('#getserieskeys_status').text(err.message);
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
            $('#getdata_metadata').val().split(',')
        ).then((response) => { 
            $('#getdata_status').text('Ok');
        }).catch((err) => {
            $('#getdata_status').text(err.message);
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
            $('#getsheetdata_filters').val(),
        ).then((response) => {
            var dataquery = new GetMetadata();
            dataquery.Run(
                [$('#getsheetdata_source').val()], 
                response.getKeysList().map(k => k.getSeriesname()),
                $('#getsheetdata_metadata').val().split(',')
            ).then((response) => {
                $('#getsheetdata_status').text('Ok');
            }).catch((err) => {
                $('#getsheetdata_status').text(err.message);
            });                
        }).catch((err) => {
            console.log(err.message);
            $('#getsheetdata_status').text(err.message);    
        });
    }
    catch(err) {
        console.log(err.message);
        $('#getsheetdata_status').text(err.message);
    }
});


function GetVersion() {
    this.Run = function() {
        return this.CreateRequest().
            then(this.RunQuery).
            then(this.ShowResponse);
    }

    this.CreateRequest = function() {
        return new Promise(function(resolve, reject) {
            try {
                resolve(new proto.google.protobuf.Empty());
            }
            catch(err) {
                reject(err);
            }
        });
    }

    this.RunQuery = function(request) {
        return new Promise(function(resolve, reject) {
            try {
                client.getVersion(request, {}, function(err, response) {
                    if (err) reject(err);
                    else resolve(response);                    
                });
            }
            catch(err) {
                reject(err);
            }
        });
    }

    this.ShowResponse = function(response) {
        return new Promise(function(resolve, reject) {
            try {
                var versionText = response.getVersion();
                $('#getversion_version').val(versionText);
                resolve(response);
            }
            catch(err) {
                reject(err);
            }
        });
    }
}

function GetSeriesKeys() {
    this.Run = function (sourcenames, filters) {
        return this.CreateRequest(sourcenames, filters).
            then(this.RunQuery).
            then(this.ShowResponse);
    }

    this.CreateRequest = function(sourcenames, filters) {
        return new Promise(function(resolve, reject) {
            try{
                r = new GetSeriesKeysRequest();
                r.getRequestmetadataMap().set('version','0.9');
                r.setSourcenamesList(sourcenames);
        
                transformFilterString(filters).forEach(f => {
                    r.getFiltersMap().set(f[0],f[1]);
                });
                resolve(r);        
            }
            catch(err) {
                reject(err);
            }
        });
    }

    this.RunQuery = function(request) {
        return new Promise(function(resolve, reject) {
            try {
                client.getSeriesKeys(request, {}, function(err, response) {
                    if (err) reject(err);
                    else resolve(response);                    
                });
            }
            catch(err) {
                reject(err);
            }
        });
    }

    this.ShowResponse = function(response) {
        return new Promise(function(resolve, reject) {
            try {
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
                resolve(response);        
            }
            catch(err) {
                reject(err);
            }
        });
    }
}

function GetMetadata() {
    this.Run = function(sourceList, seriesCodeList, metadataHeadersList) {
        return this.CreateRequest(sourceList, seriesCodeList, metadataHeadersList).
            then(this.RunQuery).
            then(this.ShowResponse);
    }

    this.CreateRequest = function(sourceList, seriesCodeList, metadataHeadersList) {
        return new Promise(function(resolve, reject) {
            try {
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
                    })
                });
                r.setKeysList(sl);
                resolve(r);
            }
            catch(err) {
                reject(err);
            }
        });
    }

    this.RunQuery = function(request) {
        return new Promise(function(resolve, reject) {
            try {
                client.getMetadata(request, {}, function(err, response) {
                    if (err) reject(err);
                    else resolve(response);                    
                });
            }
            catch(err) {
                reject(err);
            }
        })
    }

    this.ShowResponse = function(response) {
        return new Promise(function(resolve, reject) {
            try {
                // add headers
                let firstRow = ws.rows(0);
                let headers = ['source', 'series'].concat(response.getMetadataheadersList());

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
                resolve(response);        
            }
            catch(err) {
                reject(err);
            }
        });
    }
}

function transformFilterString(filters) {
    //in = 'oname : "912%", oname:"911a%",   description  :  "test%"';
    //out = [['oname','912%'], ['oname', '911%], ['description', 'test%']]
    var regex = /\s*(.+?)\s*:\s*"(.+?)"\W*/g;
    var f = new Array();

    while(found = regex.exec(filters)) {
      f.push([found[1], found[2]]);
    } 
    return f;
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