python -m venv ./venv
call ./venv/Scripts/activate.bat
pip install pipreqs
pipreqs ./ --force
pip3 install torch torchvision torchaudio --index-url https://download.pytorch.org/whl/cu118
pip install opencv-python
pip install accelerate
pip install transformers
pip install -r requirements.txt
py install.py
call run.bat