"""
This is an exercise for implementing Observer Pattern on Python.
There are no interfaces on python, so we use the abstract base class.
"""

import abc


### Interfaces / Abstract Classes ###

class IObservable:
    __metaclass__ = abc.ABCMeta

    @abc.abstractmethod
    def notify(self,data): pass

    @abc.abstractmethod
    def addObserver(self,observer): pass


    @abc.abstractmethod
    def deleteObserver(self,observer): pass


class IObserver:
    __metaclass__ = abc.ABCMeta

    @abc.abstractmethod
    def update(self,data): pass


### Concrete Classes ###

class HumanInput(IObservable):

    __observerlst=[]

    def notify(self,data):
        if(len(self.__observerlst)!=0):
            for x in self.__observerlst:
                x.update(data)
        else: print "list is empty"

    def addObserver(self,observer):
        self.__observerlst.append(observer)


    def deleteObserver(self,observer):
        if(observer in self.__observerlst):
            self.__observerlst.remove(observer)
            print "observer deleted"
        else:
            print "observer not in list"



class LcdDisplay(IObserver):
    __name=None

    def __init__(self,name):
        self.__name=name


    def update(self,data):
        print self.__name,data




### Main ####

if(__name__=='__main__'):
    if 1:
        disp1=LcdDisplay('Hitachi')
        disp2=LcdDisplay('Novatech')

        h=HumanInput()

        # two subscribers
        h.addObserver(disp1)
        h.addObserver(disp2)

        # notify them
        h.notify(1)

        # delete one subscriber
        h.deleteObserver(disp1)

        #notify subscriber
        h.notify(2)

        # delete one subscriber
        h.deleteObserver(disp1)

        # delete one subscriber
        h.deleteObserver(disp2)

        #notify subscriber
        h.notify(3)
