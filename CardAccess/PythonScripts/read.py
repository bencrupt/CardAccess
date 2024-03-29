#!/usr/bin/env python

import RPi.GPIO as GPIO
from mfrc522 import SimpleMFRC522

reader = SimpleMFRC522()

try:
        id, text = reader.read_no_block()
        print(id)
        print(text)
finally:
        GPIO.cleanup()
