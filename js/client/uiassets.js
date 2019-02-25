console.log('[INIT] uiassets');
let wb = new $.ig.excel.Workbook($.ig.excel.WorkbookFormat.excel2007);
exports.wb = wb;
let ws = wb.worksheets().add("Sheet1");
exports.ws = ws;
