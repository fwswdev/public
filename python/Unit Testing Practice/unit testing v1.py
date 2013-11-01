''' ======================================================================
A practice session of unit testing in python.
from http://www.openp2p.com/pub/a/python/2004/12/02/tdd_pyunit.html

Lesson learned:
    assertTrue is for you if you are used in True/False style evaluations
    in standard C programming language.
======================================================================= '''


import unittest,sys

# Here's our "unit".
def IsOdd(n):
    return n % 2 == 1

def IsEqual(a,b):
    return a==b


''' ========================================
IsOddTests is our test fixture.

testOne and testTwo are our test cases.

failUnless and failIf are both deprecated
    functions.
========================================= '''
# Here's our "unit tests".
class IsOddTests(unittest.TestCase):

    def testOne(self):
        self.failUnless(IsOdd(1))

    def testTwo(self):
        self.failIf(IsOdd(2))



# Another Test Fixture
class IsEqualTests(unittest.TestCase):

    def setUp(self):
        # here I am trying to create an alias (some sort of function pointer)
        # to which assert function I will be using
        # in case the assertTrue will be deprecated in the future
        # I will just change this. :)
        self.__MYASSERTFUNCIFFALSE=self.assertTrue
        pass

    def testOne(self):
        # assertTrue throws an exception if there parameter is False
        self.__MYASSERTFUNCIFFALSE(IsEqual(2,2))
        self.__MYASSERTFUNCIFFALSE(IsEqual(1,1))
        self.__MYASSERTFUNCIFFALSE(IsEqual(1,1))

    def testFailIntentionally(self):
        # assertTrue throws an exception if there parameter is False
        self.assertFalse(IsEqual(1,2))
        self.assertFalse(IsEqual(3,1))
##        self.assertFalse(IsEqual(3,3)) # this will throw exception

    def testTwo(self):
        # assertFalse throws an exception if there parameter is True
        self.assertFalse(IsEqual(2,1))
        self.assertFalse(IsEqual(2,0))
##        self.assertFalse(IsEqual(2,2)) # this will throw exception


def main():
    unittest.main()


if __name__ == '__main__':
    main()


