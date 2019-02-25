console.log('[INIT] utils');

function transformFilterString(filters) {
    //in = 'oname : "912%", oname:"911a%",   description  :  "test%"';
    //out = [['oname','912%'], ['oname', '911%], ['description', 'test%']]
    var regex = /\s*(.+?)\s*:\s*"(.+?)"\W*/g;
    var f = new Array();
    while (found = regex.exec(filters)) {
        f.push([found[1], found[2]]);
    }
    return f;
}
exports.transformFilterString = transformFilterString;
