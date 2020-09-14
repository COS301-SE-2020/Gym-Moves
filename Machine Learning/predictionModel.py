import sklearn;
from sklearn.neural_network import MLPRegressor
from sklearn.model_selection import train_test_split
import requests
import json
import datetime
import time

class predictionModel:

    def predict(gym,x):
        xValues = list()
        yValues = list()
        getData(gym, xValues, yValues)
        mlp = MLPRegressor(solver= 'lbfgs', learning_rate='adaptive', shuffle = False)
        mlp.fit(xValues,yValues)
        mlp.predict(x)

    def getData(gym, xValues, yValues):
        geturl = "https://gymmoveswebapi.azurewebsites.net/api/gymattendance/get"
        getparams = {'gymId' : gym}
        res = requests.get(url = geturl, params= getparams)
        data = json.loads(res.text)
        classes = getClasses(gym)
    
        for d in data:
            dt = datetime.datetime(d['year'], d['month'], d['day'])
            dayOfWeek = dt.isoweekday()
            lis = list()
            lis.append(dayOfWeek)
            (h,m) = d['time'].split(':')
            intTime = int(h) * 3600 + int(m) * 60
            lis.append(intTime)
            xValues.append(lis)
            yValues.append(d['count'])

    def getClasses(gym):
        classURL = "https://gymmoveswebapi.azurewebsites.net/api/classes/gymlist"
        parameters = {'gymId' : gym}
        res = requests.get(url = classURL, params= parameters)
        data = json.loads(res.text)
        week = [0,0,0,0,0,0,0]

        for c in data:
            ind = time.strptime(c['day'], "%A").tm_wday
            week[ind] += 1
        return week

        

