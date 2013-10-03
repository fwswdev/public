#-------------------------------------------------------------------------------
# Name: hierarchDes.py
# Purpose:
# Demonstation of hierarchical design in myHDL

# Author: fwswdev @ github
#
# Copyright: fwswdev @ github
#-------------------------------------------------------------------------------

import myhdl

def comblogic(a, b, c, x, y):
  @myhdl.always_comb
  def andlogic():
    temp=(a and (b or c)) # just a random logic
    x.next = temp
    y.next = not temp
  return andlogic


# This code is from myHDL Manual
def Mux(z, a, b, sel):

    """ Multiplexer.
    z -- mux output
    a, b -- data inputs
    sel -- control input: select a if asserted, otherwise b
    """

    @myhdl.always_comb
    def muxLogic():
        if sel == 1:
            z.next = a
        else:
            z.next = b
    return muxLogic


def top(a,b,c,x,y,z):
    inst1=comblogic(a,b,c,x,y)
    inst2=Mux(z,a,b,c)
    return inst1,inst2

# inputs
a = myhdl.Signal(bool())
b = myhdl.Signal(bool())
c = myhdl.Signal(bool())

# output
x = myhdl.Signal(bool())
y = myhdl.Signal(bool())
z = myhdl.Signal(bool())


myhdl.toVHDL.name="Main"
myhdl.toVHDL(top,a,b,c,x,y,z)

