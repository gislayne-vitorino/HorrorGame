![header](front.gif)

## EmotionRAM - Faces

This repository contains a minimal working example of one of our experiments from EmotionRAM for facial expression recognition. This means that we are focusing entirely on facial expressions in this case, without concern for context or body language.

This demo is based on EfficientNet-B0 architecture and was trained on AfeW. So, for my fellow LatinX colleagues, some problems with fairness are expected, as AfeW is not a culturally representative dataset. We will be publishing our dataset for brazilian emotion recognition soon to solve this limitation.

### Usage
After you clone this repository, please install [PyTorch](https://pytorch.org/) < 2.0 according to your CUDA compatibility and the following packages:

    pip install timm=0.9.7 opencv-python mediapipe Pillow

It is recommended to use an Anaconda environment to configure a virtual environment. Afterwards, just run `webcam.py` from the terminal or other IDE. You may need to configure your webcam id on [line 9](https://gitlab.cin.ufpe.br/wlc2/emotionram-faces/-/blob/main/webcam.py?ref_type=heads#L9) (`0` is for built-in webcam, `1` is for external webcam) and change your device on [line 10](https://gitlab.cin.ufpe.br/wlc2/emotionram-faces/-/blob/main/webcam.py?ref_type=heads#L10) if your computer do not have CUDA support (`'cuda'` is for GPU and `'cpu'` is for CPU).

Similarly, inference.py allows you to predict emotions on a video stored locally. This file looks for an `input.mp4` into the folder and saves the video as `output.mp4`. Please notice that you need to install [ffmpeg](https://ffmpeg.org/download.html) for this to work properly.
