console.log('[INIT] GetSeriesKeys');
const { ws } = require("./uiassets");
const { GetSeriesKeysRequest } = require('./aconcagua_pb.js');
const { client } = require("./client");
const { transformFilterString } = require("./utils");

function GetSeriesKeys() {
    this.Run = function (sourcenames, filters) {
        return this.CreateRequest(sourcenames, filters).
            then(this.RunQuery).
            then(this.ShowResponse);
    };
    this.CreateRequest = function (sourcenames, filters) {
        return new Promise(function (resolve, reject) {
            try {
                var r = new GetSeriesKeysRequest();
                r.getRequestmetadataMap().set('version', '0.9');
                r.setSourcenamesList(sourcenames);
                transformFilterString(filters).forEach(f => {
                    r.getFiltersMap().set(f[0], f[1]);
                });
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
                client.getSeriesKeys(request, {}, function (err, response) {
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
            catch (err) {
                reject(err);
            }
        });
    };
}
exports.GetSeriesKeys = GetSeriesKeys;
