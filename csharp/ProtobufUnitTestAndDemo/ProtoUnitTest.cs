/*
 * Protobuf Unit Test by 7
 * 
 * */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtoBuf;
using System.Collections.Generic;
using System.IO;

namespace ProtbufUnitTest
{
    [TestClass]
    public class ProtoUnitTest
    {



//---#-------------------#######---------------------
//--##----####--#####-------#----######--####--#####-
//-#-#---#--------#---------#----#------#--------#---
//---#----####----#---------#----#####---####----#---
//---#--------#---#---------#----#-----------#---#---
//---#---#----#---#---------#----#------#----#---#---
//-#####--####----#---------#----######--####----#---
//---------------------------------------------------



        [ProtoContract]
        public class Person
        {
            [ProtoMember(1)]
            public string Name { get; set; }
            [ProtoMember(2)]
            public int Age { get; set; }
            [ProtoMember(3)]
            public List<string> Hobbies { get; set; }
        }


        [TestMethod]
        public void ProtoBufTest()
        {
            var newPerson = new Person();
            newPerson.Name = "Harry";
            newPerson.Age = 99;
            newPerson.Hobbies = new List<string>();

            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, newPerson);

                ms.Flush();
                ms.Position = 0;

                var deserializePerson = Serializer.Deserialize<Person>(ms);

                Assert.IsTrue(deserializePerson.Name == "Harry");
                Assert.IsNull(deserializePerson.Hobbies);  // <<<<<<<<<<<< Pay attention to this and compare it with the code below
            }
        }




//--#####---------------------------------------------
//-#-----#-#----#-#####-----#####-######--####--#####-
//-------#-##---#-#----#------#---#------#--------#---
//--#####--#-#--#-#----#------#---#####---####----#---
//-#-------#--#-#-#----#------#---#-----------#---#---
//-#-------#---##-#----#------#---#------#----#---#---
//-#######-#----#-#####-------#---######--####----#---
//----------------------------------------------------




        [ProtoContract]
        public class PersonWithConstructor
        {
            [ProtoMember(1)]
            public string Name { get; set; }
            [ProtoMember(2)]
            public int Age { get; set; }
            [ProtoMember(3)]
            public List<string> Hobbies { get; set; }

            public PersonWithConstructor()
            {
                Hobbies = new List<string>();
            }
        }



        [TestMethod]
        public void ProtoBufTestWithPersonConstructor()
        {
            var newPerson = new PersonWithConstructor();
            newPerson.Name = "Harry";
            newPerson.Age = 99;
            newPerson.Hobbies = new List<string>();

            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, newPerson);

                ms.Flush();
                ms.Position = 0;

                var deserializePerson = Serializer.Deserialize<PersonWithConstructor>(ms);

                Assert.IsTrue(deserializePerson.Name == "Harry");
                Assert.IsNotNull(deserializePerson.Hobbies); // Is not null because we initialized the list on our constructor
            }

        }

    }
}
