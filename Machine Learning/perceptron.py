import sklearn;
from sklearn.neural_network import MLPRegressor
from sklearn.model_selection import train_test_split
import requests
import json
import datetime
import time

import firebase_admin
from firebase_admin import credentials
from firebase_admin import firestore
import joblib
from firebase_admin import storage

def getData(gym, xValues, yValues):
    geturl = "https://gymmoveswebapi.azurewebsites.net/api/gymattendance/get"
    getparams = {'gymId' : gym}
    res = requests.get(url = geturl, params= getparams)
    data = json.loads(res.text)
    classes = getClasses(gym)
    #k = 0
    
    for d in data:
        dt = datetime.datetime(d['year'], d['month'], d['day'])
        dayOfWeek = dt.isoweekday()
        #print(dayOfWeek)
        lis = list()
        lis.append(dayOfWeek)
        (h,m) = d['time'].split(':')
        intTime = int(h) * 3600 + int(m) * 60
        lis.append(intTime)
        #print(lis)
        xValues.append(lis)
        yValues.append(d['count'])
    #print(xValues)
    #print(yValues)

def getClasses(gym):
    classURL = "https://gymmoveswebapi.azurewebsites.net/api/classes/gymlist"
    parameters = {'gymId' : gym}
    res = requests.get(url = classURL, params= parameters)
    data = json.loads(res.text)
    #print(res.text)
    week = [0,0,0,0,0,0,0]

    for c in data:
        ind = time.strptime(c['day'], "%A").tm_wday
        week[ind] += 1
    #print(week)
    return week

def getPrediction(mlp, xValues):
    mlp.predict(xValues)

xValues = list()
yValues = list()
getData(3, xValues, yValues)
mlp = MLPRegressor(solver= 'lbfgs', learning_rate='adaptive', shuffle = False)
mlp.fit(xValues,yValues)

if (not len(firebase_admin._apps)):
	cred = credentials.Certificate(r'Machine Learning\sdk.json')
	firebase_admin.initialize_app(cred, {
        'storageBucket': 'fbprojid.appspot.com'
    })
db = firestore.client()

joblib.dump(mlp, 'model.joblib')
bucket = storage.bucket(name=)
b = bucket.blob('model-v1/model.joblib')
b.upload_from_filename('model.joblib')
print('model uploaded!')

#X_train, X_test, y_train, y_test = train_test_split(xValues, yValues, test_size=0.30, random_state=40)
#print(xValues)
#print(yValues)

    
#print(prediction.round(2))
#print(predict_train.round(2))