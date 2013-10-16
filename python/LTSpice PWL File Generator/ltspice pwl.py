"""  ---------------------------------------------------------------------------
Name:       LTSpice PWL.py
Purpose:    This tool is used generate PWL File for LTSpice.
            Originally, this generator is used to easily create
            a square wave signal.
            
            As of the moment, I am still looking for a good reference guide
            for PWL file so that I can create a flexible tool to generate 
            PWL file.
            
            For now, square wave is only supported

Author:     fwswdev @ github
Platform:
            Python 2.7.5
            No external libraries used
Created:    2013-10-16 14:20
Version:
            00.07.00
Copyright:
Licence:
--------------------------------------------------------------------------- """

####### The variables below this line can be modified by the user ###########

# The user needs to modify this. This will be created or overwritten (in case the file exist)
TARGET_FILE=r'c:\windows\temp\pwl.txt'

# The user needs to modify this to create an array of  voltages
LST_VOLTAGE=[4e3,0,-4e3,0]

# This is used to fine tune the timing of the square wave
STR_BIG_TIME='500m'
STR_SMALL_TIME='.01m'


# uncomment the True to display the File Contents
BOOL_DISPLAY_FILE_CONTENTS=False  # | True









#####################################################
############ DO NOT MODIFY ANYTHING BELOW ###########
#####################################################
import time

starttime=time.time()

tmp='0 0\n' # create temporary storage of string so that we can display the contents later

for x in LST_VOLTAGE:
    tmp+='+%s \t %f\n' % (STR_SMALL_TIME,x)
    tmp+='+%s \t %f\n' % (STR_BIG_TIME,x)

with open(TARGET_FILE,'w') as f:
    f.write(tmp)

if(BOOL_DISPLAY_FILE_CONTENTS):
    print '========= File Contents: ==========\n'
    print tmp
    print '========= EOF ==========\n\n'

print "Process Done!"
print  "File Created: '%s'"  % (TARGET_FILE)

endtime=time.time()

elapsedtime=endtime-starttime

print "Elapsed time in seconds:", elapsedtime

# actually no need to do this. :)
LST_VOLTAGE=None
tmp=None


# EOF

