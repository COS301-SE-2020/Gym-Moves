import tensorflow as tf
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
            yValues.append(d['count'])
        else:
            lis.append(5)
            yValues.append(10)
        xValues.append(lis)

        #again+++++++++++++++++++
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
            lis.append(int(prev/10))
            yValues.append(int(d['count']/10))
        else:
            lis.append(10)
            yValues.append(12)
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
xValues = list()
yValues = list()
getData(3, xValues, yValues)

#X_train, X_test, y_train, y_test = train_test_split(xValues, yValues, test_size=0.30, random_state=40)
sc_X = StandardScaler()
X_scaled = sc_X.fit_transform(xValues)
print("XVALUES=========================================")
print(X_scaled)
print("YVALUES=========================================")
print(yValues)

learning_rate = 0.001 
training_epochs = 20 
batch_size = 100 
display_step = 1 

x = tf.placeholder(tf.float32, [None, 3])   # 3 features
y = tf.placeholder(tf.float32, [None, 1])   # 1 outputs

n_hidden_1 = 256 
n_hidden_2 = 256

h = tf.Variable(tf.random_normal([3, n_hidden_1])) # bias layer 1 
bias_layer_1 = tf.Variable(tf.random_normal([n_hidden_1])) 


w = tf.Variable(tf.random_normal([n_hidden_1, n_hidden_2]))
bias_layer_2 = tf.Variable(tf.random_normal([n_hidden_2])) 

layer_1 = tf.nn.sigmoid(tf.add(tf.matmul(x, h), bias_layer_1))
layer_2 = tf.nn.sigmoid(tf.add(tf.matmul(layer_1, w), bias_layer_2)) 

output = tf.Variable(tf.random_normal([n_hidden_2, n_classes])) 

bias_output = tf.Variable(tf.random_normal([n_classes])) # output layer 
output_layer = tf.matmul(layer_2, output) + bias_output

cost = tf.reduce_mean(tf.nn.sigmoid_cross_entropy_with_logits(
   logits = output_layer, labels = y)) 

optimizer = tf.train.AdamOptimizer(learning_rate = learning_rate).minimize(cost) 


init = tf.global_variables_initializer() 

with tf.Session() as sess: 
   sess.run(init) 
   
   # Training cycle
   for epoch in range(training_epochs): 
      avg_cost = 0. 
      total_batch = int(len(yValues) / batch_size) 
      
      # Loop over all batches 
      for i in range(total_batch): 
         batch_xs, batch_ys = mnist.train.next_batch(batch_size) 
         # Fit training using batch data 
         sess.run(optimizer, feed_dict = { x: xValues, y: yValues}) 
         # Compute average loss 
##         avg_cost += sess.run(cost, feed_dict = {x: batch_xs, y: batch_ys}) / total_batch
     

#mlp = MLPRegressor(hidden_layer_sizes=(10,), solver= 'lbfgs', activation= 'relu', shuffle = False, 
#learning_rate_init = 0.001, max_iter = 400, tol=0.001, warm_start=True, validation_fraction= 0.1)
#mlp.fit(xValues,yValues)

#print(mlp.predict([xValues[0]]))
#print(mlp.predict([xValues[1]]))
#print(mlp.predict(xValues))
#print(mlp.predict([xValues[0]]))
#print(mlp.predict([xValues[1]]))

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