console.log('[INIT] GetObservations');
const { ws } = require("./uiassets");
const { GetObservationsRequest, SourceSeriesKey } = require('./aconcagua_pb.js');
const { client } = require("./client");

function GetObservations() {
    this.Run = function (sourceList, seriesCodeList, frequencies) {
        return this.CreateRequest(sourceList, seriesCodeList, frequencies).
            then(this.RunQuery).
            then(this.ShowResponse);
    };
    this.CreateRequest = function (sourceList, seriesCodeList, frequencies) {
        return new Promise(function (resolve, reject) {
            try {
                var r = new GetObservationsRequest();
                r.getRequestmetadataMap().set('version', '0.9');
                r.setFrequencies(frequencies);
                var sl = new Array();
                sourceList.forEach(s => {
                    seriesCodeList.forEach(sc => {
                        var ssk = new SourceSeriesKey();
                        ssk.setSourcename(s);
                        ssk.setSeriesname(sc);
                        sl.push(ssk);
                    });
                });
                r.setKeysList(sl);
                resolve(r);
            }
            catch (err) {
                reject(err);
            }
        });
    };
    this.RunQuery = function (request) {
        return new Promise(function (resolve, reject) {
            try {
                client.getObservations(request, {}, function (err, response) {
                    if (err)
                        reject(err);
                    else
                        resolve(response);
                });
            }
            catch (err) {
                reject(err);
            }
        });
    };

    this.ShowResponse = function (response) {
        return new Promise(function (resolve, reject) {
            try {
                // add headers
                let firstRow = ws.rows(0);
                let headers = ['source', 'series', 'status'];
                
                console.log('Hello');
                headers.forEach((header, colIndex) => {
                    firstRow.setCellValue(colIndex, header);
                });

                var r = response.getSeriesdataList();
                r.forEach((rowData, rowIndex) => {
                    let wsRow = ws.rows(rowIndex + 1);

                    var message = rowData.getMessagestatus().getMessage();
                    
                    var entries = [...rowData.getValuesMap().entries()].
                        map(kv => kv[0] + ": " + kv[1]);

                    let md = [
                        rowData.getKey().getSourcename(),
                        rowData.getKey().getSeriesname(),
                        rowData.getMessagestatus().getMessage()
                    ].concat(entries);

                    md.forEach((cellData, cellIndex) => {
                        wsRow.setCellValue(cellIndex, cellData);
                    });
                });
                resolve(response);
            }
            catch (err) {
                reject(err);
            }
        });
    };
}
exports.GetObservations = GetObservations;
