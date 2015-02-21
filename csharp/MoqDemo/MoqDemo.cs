using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace MoqTesting
{


//-###-######----------------------------------------
//--#--#-----#-######-#####---####---####--#----#----
//--#--#-----#-#------#----#-#------#----#-##---#----
//--#--######--#####--#----#--####--#----#-#-#--#----
//--#--#-------#------#####-------#-#----#-#--#-#----
//--#--#-------#------#---#--#----#-#----#-#---##----
//-###-#-------######-#----#--####---####--#----#----
//---------------------------------------------------



    public interface IPerson
    {
        string GetName();
    }



//-#-----#------------------------------
//-#-----#-#####-#-#------#-#####-#---#-
//-#-----#---#---#-#------#---#----#-#--
//-#-----#---#---#-#------#---#-----#---
//-#-----#---#---#-#------#---#-----#---
//-#-----#---#---#-#------#---#-----#---
//--#####----#---#-######-#---#-----#---
//--------------------------------------


    public class PersonUtility
    {
        public string SayName(IPerson p, bool display)
        {
            return display ? p.GetName() : String.Empty;
        }
    }




//-#-----#---------------######-----------------------
//-##---##--####---####--#-----#-######-#----#--####--
//-#-#-#-#-#----#-#----#-#-----#-#------##--##-#----#-
//-#--#--#-#----#-#----#-#-----#-#####--#-##-#-#----#-
//-#-----#-#----#-#--#-#-#-----#-#------#----#-#----#-
//-#-----#-#----#-#---#--#-----#-#------#----#-#----#-
//-#-----#--####---###-#-######--######-#----#--####--
//----------------------------------------------------



    [TestClass]
    public class MoqDemo
    {
        [TestMethod]
        public void MoqDemoTest()
        {
            const string STR_FOR_COMPARISON = "Hey";

            // Create the MockPerson and MockRepo
            var mockrepo = new MockRepository(MockBehavior.Strict);
            var mockPerson = mockrepo.Create<IPerson>();

            // If GetName is encountered, we return "Hey"
            mockPerson.Setup(m => m.GetName()).Returns(STR_FOR_COMPARISON);

            // Get the IPerson Object
            var iPerson = mockPerson.Object;

            // Run a simple test
            string personName = iPerson.GetName();
            Assert.IsTrue(String.Equals(STR_FOR_COMPARISON, personName));

            // Test using the Utility class
            var util = new PersonUtility();
            personName = util.SayName(iPerson, true);
            Assert.IsTrue(String.Equals(STR_FOR_COMPARISON, personName));
            personName = util.SayName(iPerson, false);
            Assert.IsTrue(String.Equals(String.Empty, personName));
        }
    }





//----------------------------------------------------------------------
//----------------------------------------------------------------------
//----------------------------------------------------------------------
//----------------------------------------------------------------------
//----------------------------------------------------------------------
//----------------------------------------------------------------------
//----------------------------------------------------------------------
//----------------------------------------------------------------------

    /* ============================================================== 
    The next example is for primitive mocking. 
    Approach is different but achieves the same thing as above.
    ============================================================== */



    public class DummyPerson:IPerson
    {
        public string GetName()
        {
            return "Hey,Dummy Here";
        }
    }



    [TestClass]
    public class PrimitiveMocking
    {
        [TestMethod]
        public void PrimitiveMockingTest()
        {
            const string STR_FOR_COMPARISON = "Hey,Dummy Here";

            // Create the IPerson Object
            IPerson iPerson = new DummyPerson();

            // Run a simple test
            string personName = iPerson.GetName();
            Assert.IsTrue(String.Equals(STR_FOR_COMPARISON, personName));

            // Test using the Utility class
            var util = new PersonUtility();
            personName = util.SayName(iPerson, true);
            Assert.IsTrue(String.Equals(STR_FOR_COMPARISON, personName));
            personName = util.SayName(iPerson, false);
            Assert.IsTrue(String.Equals(String.Empty, personName));
        }
    }


}
