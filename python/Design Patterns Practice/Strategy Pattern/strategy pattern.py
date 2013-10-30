"""
This is an exercise for implementing Strategy Pattern on Python.
There are not interfaces on python, so we use the abstract base class.
"""

import abc



class IStrategy:
    __metaclass__ = abc.ABCMeta

    @abc.abstractmethod
    def execute(self,a,b): pass





class Add(IStrategy):
    # override the execute
    def execute(self,a,b):
        print a+b



class Subtract(IStrategy):
    # override the execute
    def execute(self,a,b):
        print a-b



class Context:
    __strat=None

    def __init__(self,strategy):
        self.__strat=strategy

    def ChangeStrategy(self,strategy):
        self.__strat=strategy

    def ExecuteStrategy(self,a,b):
        self.__strat.execute(a,b)


if(__name__=='__main__'):

    # to test the interface implementation capability
    if 0:
        m=Add()
        m.execute(1,2)

    # strategy pattern example 1
    if 0:
        c=Context(Add())
        c.ExecuteStrategy(4,5)
        c=Context(Subtract())
        c.ExecuteStrategy(4,5)

    # strategy pattern example 2
    if 1:
        c=Context(Add())
        c.ExecuteStrategy(8,6)
        c.ChangeStrategy(Subtract())
        c.ExecuteStrategy(8,6)

