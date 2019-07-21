#!/usr/bin/env python

import RPi.GPIO as GPIO
import sys

GPIO.setmode(GPIO.BCM) # GPIO Numbers instead of board numbers
Pin = int(sys.argv[1])
#RELAIS_1_GPIO = 17
GPIO.setup(Pin, GPIO.OUT) # GPIO Assign mode
GPIO.output(Pin, GPIO.HIGH) # on
