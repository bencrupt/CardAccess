#!/usr/bin/env python

import sys
import RPi.GPIO as GPIO
from mfrc522 import SimpleMFRC522

text = sys.argv[1]
reader = SimpleMFRC522()

try:
        reader.write(text)
finally:
        GPIO.cleanup()