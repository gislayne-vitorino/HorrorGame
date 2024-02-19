'''
mediapipe_utils.py
Created on 2023 02 22 12:28:05
Description: File containing utils methods for processing mediapipe faces and processing FER

Author: Will <wlc2@cin.ufpe.br>
'''
from typing import Union, Tuple
import math
import mediapipe as mp
from torchvision import transforms
import torch
import numpy as np
from PIL import Image
mp_face_detection = mp.solutions.face_detection


def _normalized_to_pixel_coordinates(
        normalized_x: float, normalized_y: float, image_width: int,
        image_height: int) -> Union[None, Tuple[int, int]]:
    """Converts normalized value pair to pixel coordinates."""

    # Checks if the float value is between 0 and 1.
    def is_valid_normalized_value(value: float) -> bool:
        return (value > 0 or math.isclose(0, value)) and (value < 1 or
                                                          math.isclose(1, value))

    if not (is_valid_normalized_value(normalized_x) and
            is_valid_normalized_value(normalized_y)):
        # TODO: Draw coordinates even if it's outside of the image bounds.
        return None
    x_px = min(math.floor(normalized_x * image_width), image_width - 1)
    y_px = min(math.floor(normalized_y * image_height), image_height - 1)
    return x_px, y_px


def get_face_locations_mediapipe(image):
    image_rows, image_cols, _ = image.shape
    face_locs = []
    with mp_face_detection.FaceDetection(
            model_selection=1, min_detection_confidence=0.5) as face_detection:
        results = face_detection.process(image)
    if not results.detections:
        return None
    for dets in results.detections:
        relative_bounding_box = dets.location_data.relative_bounding_box
        rect_start_point = _normalized_to_pixel_coordinates(
            relative_bounding_box.xmin, relative_bounding_box.ymin, image_cols,
            image_rows)
        rect_end_point = _normalized_to_pixel_coordinates(
            relative_bounding_box.xmin + relative_bounding_box.width,
            relative_bounding_box.ymin + relative_bounding_box.height, image_cols,
            image_rows)
        face_locs.append([rect_start_point, rect_end_point])

    return face_locs


class EmotionPerceiver:
    def __init__(self, device='cpu'):

        self.device = device
        self.idx_to_class = {
            0: 'Anger', 1: 'Contempt', 2: 'Disgust', 3: 'Fear', 4: 'Happiness', 5: 'Neutral', 6: 'Sadness', 7: 'Surprise'
        }
        self.img_size = 224
        self.test_transforms = transforms.Compose(
            [
                transforms.Resize((self.img_size, self.img_size)),
                transforms.ToTensor(),
                transforms.Normalize(mean=[0.485, 0.456, 0.406],
                                     std=[0.229, 0.224, 0.225])
            ]
        )

        model = torch.load('enet_b0_8_best_afew.pt')

        if isinstance(model.classifier, torch.nn.Sequential):
            self.classifier_weights = model.classifier[0].weight.cpu(
            ).data.numpy()
            self.classifier_bias = model.classifier[0].bias.cpu().data.numpy()
        else:
            self.classifier_weights = model.classifier.weight.cpu().data.numpy()
            self.classifier_bias = model.classifier.bias.cpu().data.numpy()
        # enet_b0_8_best_afew

        model.classifier = torch.nn.Identity()
        model = model.to(self.device)
        self.model = model.eval()

    def get_probab(self, features):
        x = np.dot(features, np.transpose(
            self.classifier_weights))+self.classifier_bias
        return x

    def extract_features(self, face_img):
        img_tensor = self.test_transforms(Image.fromarray(face_img))
        img_tensor.unsqueeze_(0)
        features = self.model(img_tensor.to(self.device))
        features = features.data.cpu().numpy()
        return features

    def predict_emotions(self, face_img, logits=True):
        features = self.extract_features(face_img)
        scores = self.get_probab(features)[0]
        x = scores
        pred = np.argmax(x)

        if not logits:
            e_x = np.exp(x - np.max(x)[np.newaxis])
            e_x = e_x / e_x.sum()[None]
            scores = e_x
        return self.idx_to_class[pred], scores

    def extract_multi_features(self, face_img_list):
        imgs = [self.test_transforms(Image.fromarray(face_img))
                for face_img in face_img_list]
        features = self.model(torch.stack(imgs, dim=0).to(self.device))
        features = features.data.cpu().numpy()
        return features

    def predict_multi_emotions(self, face_img_list, logits=True):
        features = self.extract_multi_features(face_img_list)
        scores = self.get_probab(features)
        preds = np.argmax(scores, axis=1)
        x = scores
        pred = np.argmax(x[0])

        if not logits:
            e_x = np.exp(x - np.max(x, axis=1)[:, np.newaxis])
            e_x = e_x / e_x.sum(axis=1)[:, None]
            if self.is_mtl:
                scores[:, :-2] = e_x
            else:
                scores = e_x

        return [self.idx_to_class[pred] for pred in preds], scores
