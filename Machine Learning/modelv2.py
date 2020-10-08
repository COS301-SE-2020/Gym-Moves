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
from datetime import timedelta
import neurolab as nl

def getCount7DaysAgo(dt,dates):
    prevweek = dt - timedelta(days = 7)
    for el in dates:
        if el[0] == prevweek:
            return el[1]
    return 0

def getData(gym, xValues, yValues):
    geturl = "https://gymmoveswebapi.azurewebsites.net/api/gymattendance/get"
    getparams = {'gymId' : gym}
    res = requests.get(url = geturl, params= getparams)
    data = json.loads(res.text)
    classes = getClasses(gym)
    dates = list()
    for d in data:
        dt = datetime.datetime(d['year'], d['month'], d['day'])
        temp = list()
        temp.append(dt)
        temp.append(d['count'])
        dates.append(temp)

    
    for d in data:
        dt = datetime.datetime(d['year'], d['month'], d['day'])
        dayOfWeek = dt.isoweekday()
        prev = getCount7DaysAgo(dt,dates)
        #print(dayOfWeek)
        lis = list()
        lis.append(dayOfWeek)
        (h,m) = d['time'].split(':')
        intTime = int(h) - 6
        #intTime = int(h) * 3600 + int(m) * 60
        lis.append(intTime)
        if prev != 0:
            lis.append(prev)
            yValues.append([d['count']])
        else:
            lis.append(5)
            yValues.append([10])
        xValues.append(lis)

        #again+++++++++++++++++++
    for d in data:
        dt = datetime.datetime(d['year'], d['month'], d['day'])
        dayOfWeek = dt.isoweekday()
        prev = getCount7DaysAgo(dt,dates)
        #print(dayOfWeek)
        lis = []
        lis.append(dayOfWeek)
        (h,m) = d['time'].split(':')
        intTime = int(h) - 6
        #intTime = int(h) * 3600 + int(m) * 60
        lis.append(intTime)
        if prev != 0:
            lis.append(int(prev/10))
            yValues.append([int(d['count']/10)])
        else:
            lis.append(10)
            yValues.append([12])
        xValues.append(lis)
            
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
xValues = []
yValues = []
getData(3, xValues, yValues)

#X_train, X_test, y_train, y_test = train_test_split(xValues, yValues, test_size=0.30, random_state=40)
print("XVALUES=========================================")
print(xValues)
print("YVALUES=========================================")
print(yValues)

mlp = nl.net.newff([[1,7],[0,14],[0,1000]], [100,1])
mlp.trainf = nl.train.train_ncg
error = mlp.train(xValues, yValues, epochs=500, show=5)
print("ERROR=======================")
print(error)


#if (not len(firebase_admin._apps)):
#	cred = credentials.Certificate(r'Machine Learning\sdk.json')
#	firebase_admin.initialize_app(cred, {
#        'storageBucket': 'fbprojid.appspot.com'
#    })
#db = firestore.client()
#joblib.dump(mlp, 'model.joblib')
#bucket = storage.bucket()
#b = bucket.blob('model-v/model.joblib')
#b.upload_from_filename('model.joblib')
#print('model uploaded!')

#predict_train = mlp.predict(xValues)
#prediction = mlp.predict([[1,72000]])
#print(prediction.round(0))
#print(predict_train.round(0))