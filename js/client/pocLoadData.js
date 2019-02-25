const { ws } = require("./uiassets");
const { observations, database } = require("./main");

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
