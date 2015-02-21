using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naming_Conventions
{
    // Sevenstring's C# naming conventions


    // Enumeration
    // I always put an E or _E at the end
    public enum MessagesIdE // or MessagesIdE
    {
        Message1,
        Message2,
    }



    // For classes that does some processing,
    // I don't have a standard naming convention but it 
	// should be something descriptive. Sometimes I add
	// "Behavior" or "Process" at the end
    public class Person // or PersonBehavior // or PersonProcess
    {
        public void Walk() 
        {
            Console.WriteLine("Walk");
        }
    }

    // For class that is used for holding data and no processes (I usually do this
    // for clearer separation between behavior and data), I usually put "Data" at the
    // end of the class.
    public class PersonData
    {
        public string Name { get; set; }
    }


    // Struct
    // I always put T at the end
    public struct MessagesT
    {
        public int MessageIntId;
    }


    // For Interfaces, I usually do the normal way, prefixing the name with an "I"
    public interface IPerson
    {
        void Walk();
    }


    // For inheriting classes, I usually put Base at the start. I rarely do this (only when needed)
    public class BaseView
    {
        public void SwitchView() { }
    }


    // For AbstractClasses, I usually put a ABase at the start (to tell that it is an
    // abstract base class.
    public abstract class ABasePerson
    {
        public abstract void Walk();
    }
    
    
    // for Protected, Public, Private, Constants
    public class Car
    {
        // CONSTANTS: I always use All Caps for constants
        const string MY_KEY = "KEY";

        // PRIVATE VARIABLES: I usually prefix it with m (some people use underscore)
        private bool mIsKeyOpened;


        // PRIVATE METHODS: I do two approaches for private methods
        // 1. small case at the start of the method name (I usually prefer this)
        private void getName() { }
        // 2. add a "prv" at the start of method name (I usually do this inside abstract classes. You can see the reason below)
        private void prvGetName() { }


        // PROTECTED METHODS
        // I usually add prot at the beginning of the method name
        private void protGetName() { }

        // PROTECTED VARIABLES
        // I usually add p or prot ath the beginning of the variable name
        private bool pHasKey; // coming from C/C++, this looks like pertaining to a pointer
        private bool protHasKey;


        // PUBLIC METHODS
        // For public methods, first letter must be capitalized
        public void Walk() { }
    }


}
