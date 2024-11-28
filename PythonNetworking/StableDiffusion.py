import torch
from PIL import Image
import numpy as np
import cv2
from diffusers import StableDiffusionControlNetPipeline
from diffusers import ControlNetModel
from diffusers import UniPCMultistepScheduler

def stableDiffusion(pathToImage,prompt,steps):
    try:
        # Load the image from the provided path
        image = Image.open(pathToImage)
    except Exception as e:
        print(f"Error loading image: {e}")
        return None

    # Resize image to the desired size
    newsize = (1024, 1024)
    image = image.resize(newsize)
    
    print(f"Image loaded and resized: {image}")

    # Initialize the ControlNet model and pipeline
    controlnet = ControlNetModel.from_pretrained(
        "lllyasviel/sd-controlnet-openpose", torch_dtype=torch.float16
    )
    
    pipe = StableDiffusionControlNetPipeline.from_pretrained(
        "runwayml/stable-diffusion-v1-5", controlnet=controlnet, safety_checker=None, torch_dtype=torch.float16
    )
    
    pipe.scheduler = UniPCMultistepScheduler.from_config(pipe.scheduler.config)
    
    # Remove if you do not have xformers installed
    # pipe.enable_xformers_memory_efficient_attention()
    
    pipe.enable_model_cpu_offload()
    
    print(prompt)

    # Perform the image generation
    image_out = pipe(prompt, image, num_inference_steps=steps).images[0]
    
    # Convert the generated image to an array
    image_arr = np.array(image_out)

    #image_out.save(f"outs/{prompt}.png")

    output_image_path = "generated_image.png"
    image_out.save(output_image_path)
    
    return output_image_path
