import numpy as np
import sklearn;
from sklearn.neural_network import MLPRegressor
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import StandardScaler
import requests
import json
import datetime
import time
#import firebase_admin
#from firebase_admin import credentials
#from firebase_admin import firestore
#import joblib
#from firebase_admin import storage
from datetime import timedelta
from tensorflow.keras.layers import Dense
from tensorflow.keras.models import Sequential
from tensorflow.keras.activations import hard_sigmoid


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
        lis = np.array([dayOfWeek])
        #lis.append(dayOfWeek)
        (h,m) = d['time'].split(':')
        intTime = int(h) - 6
        #intTime = int(h) * 3600 + int(m) * 60
        np.append(lis, intTime)
        if prev != 0: 
            np.append(lis, prev)
            np.append(yValues, d['count'])
        else:
            np.append(lis, 5)
            np.append(yValues,10)
        np.append(xValues, lis)

        #again+++++++++++++++++++
    for d in data:
        dt = datetime.datetime(d['year'], d['month'], d['day'])
        dayOfWeek = dt.isoweekday()
        prev = getCount7DaysAgo(dt,dates)
        #print(dayOfWeek)
        lis = np.array([dayOfWeek])
        #lis.append(dayOfWeek)
        (h,m) = d['time'].split(':')
        intTime = int(h) - 6
        #intTime = int(h) * 3600 + int(m) * 60
        np.append(lis, intTime)
        if prev != 0:
            np.append(lis, int(prev/10))
            np.append(yValues, int(d['count']/10))
        else:
            np.append(lis,10)
            np.append(yValues, 12)
        np.append(xValues, lis)
            
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
xValues = np.empty([350,3])
yValues = np.empty([1])
getData(3, xValues, yValues)

#X_train, X_test, y_train, y_test = train_test_split(xValues, yValues, test_size=0.30, random_state=40)

print("XVALUES=========================================")
print(xValues)
print("YVALUES=========================================")
print(yValues)

model = Sequential()
model.add(Dense(1, input_shape=(8,), activation=hard_sigmoid, kernel_initializer='glorot_uniform'))
model.compile(loss='binary_crossentropy', optimizer='adam', metrics=['accuracy'])

    # Train the perceptron using stochastic gradient descent
    # with a validation split of 20%
model.fit(xValues, yValues, epochs=225, batch_size=25, verbose=1, validation_split=0.1)

    # Evaluate the model accuracy
_, accuracy = model.evaluate(X, y)
print("%0.3f" % accuracy)
     

mlp = MLPRegressor(hidden_layer_sizes=(10,), solver= 'lbfgs', activation= 'relu', shuffle = False, 
learning_rate_init = 0.001, max_iter = 400, tol=0.001, warm_start=True, validation_fraction= 0.1)
mlp.fit(xValues,yValues)

print(mlp.predict([xValues[0]]))
print(mlp.predict([xValues[1]]))
print(mlp.predict(xValues))
print(mlp.predict([xValues[0]]))
print(mlp.predict([xValues[1]]))

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