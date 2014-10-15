# Bootleg mic DIY Computations Script

import math

# Constants
Freq=96.7117988395


# Variables
Rds=2200  * 1.0
Rdrain=470  * 1.0
Cap= 22e-6



Rds=2200  * 1.0
##Rds=1500  * 1.0
Rdrain=470 * 0.5  * 1.0
Cap= 47e-6


# Computation
Gain=Rdrain /Rds
print "Gain", Gain


F = 1/(Rdrain * Cap)
print "Computed F",F


RComputed=1/(Cap*Freq)
print "RComputed", RComputed