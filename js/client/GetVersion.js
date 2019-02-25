console.log('[INIT] GetVersion');
const { client } = require("./client");

function GetVersion() {
    this.Run = function () {
        return this.CreateRequest().
            then(this.RunQuery).
            then(this.ShowResponse);
    };
    this.CreateRequest = function () {
        return new Promise(function (resolve, reject) {
            try {
                resolve(new proto.google.protobuf.Empty());
            }
            catch (err) {
                reject(err);
            }
        });
    };
    this.RunQuery = function (request) {
        return new Promise(function (resolve, reject) {
            try {
                client.getVersion(request, {}, function (err, response) {
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
                var versionText = response.getVersion();
                $('#getversion_version').val(versionText);
                resolve(response);
            }
            catch (err) {
                reject(err);
            }
        });
    };
}
exports.GetVersion = GetVersion;
