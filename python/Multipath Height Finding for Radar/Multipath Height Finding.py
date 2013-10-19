""" ---------------------------------------------------------------------------
Name: Multipath Height Finding.py
Purpose:
    This tool is used to compute for the feasibility of range testing
    of RF devices with concern to multipath issues.

Author: fwswdev @ github
Platform:
    Python 2.7.5
    Sympy library
Created: 2013-10-19 1116
Version:
    00.07.00
Copyright:
Licence:
Notes:
    All units of distance and height must be in meters.
    Please refer to this site before using this Py script:
        http://www.radartutorial.eu/18.explanations/ex51.en.html
--------------------------------------------------------------------------- """

import sympy

#### DO NOT MODIFY THIS PART ###
c=299792458 #m/s
th,ha,ht,R=sympy.symbols('th,ha,ht,R')
MainEq=sympy.Eq( (2.0*ha*ht)/(c*R),th)
#### DO NOT MODIFY THIS PART ###


if 1: # look for th

    _ha,_ht,_R=30.48,3048,92600 # this is the example from the website. the units are converted to meters
    _ha,_ht,_R=1.5,80,600 # the test we did and we wanted this result
    _ha,_ht,_R=10.9544511501033,10.9544511501033,600 # the ideal case to be tested on the field
##    _ha,_ht,_R=1.5,2.5,300 # this is our actual range test

    eq=MainEq.subs( [ (ha,_ha),(ht,_ht),(R,_R)  ]  )
    res=sympy.tsolve(eq,th)
    ans=res[0]
    print "th in seconds", ans
    print "Bandwidth in MHz", 1e-6/ans
    print '-' * 20



if 1: # look for ha,ht, assuming we wanted them in equal height

    _R=600
    _th=1.33425638079261e-9 #

    eq=MainEq.subs( [ (R,_R),(th,_th),(ha,ht)  ]  )
    res=sympy.tsolve(eq,ht)
    print "Height (choose the positive value):\n",res
    print '-' * 20

