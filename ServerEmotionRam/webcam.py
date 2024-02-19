import os
import cv2
import torch
import numpy as np
from flask import Flask, jsonify
import threading
from threading import Thread
from utils import EmotionPerceiver, get_face_locations_mediapipe

app = Flask(__name__)

emotion = None
lock = threading.Lock()

def process_video():
    global emotion
    video_stream = cv2.VideoCapture(0)
    fer_pipeline = EmotionPerceiver(device='cpu')

    while True:
        (grabbed, frame) = video_stream.read()
        if not grabbed:
            print(f'[INFO] Stopping execution due to lack of frames.')
            break

        # Frame é a imagem capturada pela câmera
        frame = cv2.resize(frame, (1280, 720))
        frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        face_locations = get_face_locations_mediapipe(frame_rgb)

        if face_locations is None:
            continue

        for face_location in face_locations:
            (x1, y1), (x2, y2) = face_location  # Cada face detectada
            face = frame_rgb[y1:y2, x1:x2, :]
            #emotion eh a variavel global que sera enviada pelo server flask quando requisitada
            emotion, scores = fer_pipeline.predict_emotions(
                face, logits=True)  # Cada emoção detectada
            cv2.rectangle(frame, (x1, y1), (x2, y2), (0, 255, 0), 3)
            cv2.putText(frame, emotion, (x1, y1-5),
                        cv2.FONT_HERSHEY_PLAIN, 2, (0, 255, 0), 2)
        #print(emotion)
        cv2.imshow('Frame', frame)
        cv2.waitKey(1)

@app.route('/emotion')
def get_emotion():
    global emotion
    with lock:
        return jsonify({'emotion': emotion})

if __name__ == '__main__':
    video_thread = Thread(target=process_video)
    video_thread.daemon = True
    video_thread.start()
    app.run()