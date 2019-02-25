console.log('[INIT] main');
const { wb, ws } = require("./uiassets");
const { GetVersion } = require("./GetVersion");
const { GetSeriesKeys } = require("./GetSeriesKeys");
const { GetMetadata } = require("./GetMetadata");


$("#spreadsheet").igSpreadsheet({
    height: "600",
    width: "70%",
    workbook: wb,
    selectionChanged: onSelectionChanged
});

let database, observations;
exports.database = database;
exports.observations = observations;

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