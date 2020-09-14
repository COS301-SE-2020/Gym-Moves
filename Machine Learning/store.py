import firebase_admin
from firebase_admin import credentials
from firebase_admin import firestore
import joblib
from firebase_admin import storage
import predictionModel

if (not len(firebase_admin._apps)):
	cred = credentials.Certificate(r'Machine Learning\sdk.json')
	firebase_admin.initialize_app(cred, {
        'storageBucket': 'fbprojid.appspot.com'
    })
db = firestore.client()
mlp = predictionModel.predictionModel()
joblib.dump(mlp, 'model.joblib')
bucket = storage.bucket()
b = bucket.blob('model-v/model.joblib')
b.upload_from_filename('model.joblib')
print('model uploaded!')

