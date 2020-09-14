const admin = require("firebase-admin");
const gapi = require("googleapis");
const functions = require("firebase-functions");

admin.initializeApp(functions.config());
var m = gapi.google.ml('v1');

exports.predictSPAM = functions.https.onRequest( async(request, response)=> {
    const time = request.body.decimaltime;
    const dow = request.body.dayoftheweek;
    var instance = [[dow, time]];
    const model = "GymPredictions";
    //const​ { ​credential​ } ​=​​ await gapi.​google​.​auth​.​getApplicationDefault​();
    const {credential} = await gapi.google.auth.getApplicationDefault();
    const modelName = 'projects/fbprojid/models/' + model;
    //var ​​modelName​​ =​​ 'projects/fbprojid/models/​'+ ​model​;
    const preds = await m.projects.predict({
      auth: credential,
      name: modelName,
      requestBody: {
        instance
      }
    /*const​​ preds ​​= ​​await ​​m.​projects​.​predict​({
		auth​:​​ credential, 
		name​:​​ modelName,  
		requestBody​:​​ {
			​instance
		​}​*/
    });
  response.send(preds.data['predictions']);
	//response​.​send​(​preds​.​data​[​'predictions'​][​0​]);
  });

