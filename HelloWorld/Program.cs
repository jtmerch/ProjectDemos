

using HelloWorld.AdvancedTopics;
using HelloWorld.BeginTopics;
using HelloWorld.IntermediateTopics;
using HelloWorld.Math;
using HelloWorld.TimCoreyString;
using System;
using System.Collections.Generic;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. to create a constructor type "ctor" and tab twice
            //2. to duplicate entry press ctrl + D
            //3. to do Console.writeline type "cw" and tab twice
            //4. going to project "Power Command" -> Open Command Window, cd to "bin/Debug" and typing in "ildasm HelloWorld.exe" allows ou to see the intermediate language disassembly
            //5. Encapsulation hiding data in the class from other class, and allowing those to only be accessed via Getter and Setter methods 
            //6. The object class is the parent of all classes in dotnet
            //7. Compsition is a relationship between two classes that allows one to contain the other.  compisition is typically preferred over Inheritance because Composition is more loosely coupled
            //8. use the ": base" keyword to reuse the code from the constructor of the class you inherit from

            /*9: Boxing is the process of converting a value type instance to an object reference (Has a performance penalty)
           Example:
           int number = 10;
           object obj = number;

           Unboxing is the opposite of Boxing (Has a performance penalty)
           Example:
           object obj = 10;
           int number = int(obj);
           */

            //10. Method overriding is modifying the implementation of an inherited method
            /*11. Abstract Modifier declares that it is missing imiplementation.  For example in the Draw method in class Shape, there is no body for Draw, so call it "abstract"
              You don't have to use abstract all the time.  You use abstract when you want to provide common behaviour while forcing a developer to follow your design.
               A. Abstract member cannot include implementation Ex:
                public abstract void Draw(); //no body
               B. if a member is abstract, the container class must also be abstract.
               C. In a Derived clas you ust implement all abstract members in the base abstract class (you have to override all)
               D. Abstract classes cannot be instantiated.
               */

            //12. Sealed (rarely if ever used) modifiers are the opposite of abstract.  The prevent derivation of classes or orerriding of methods.  They are slightly faster because of run-time opimizations.  
            //13. An Interface is a language construct taht is similar to a class (in terms of syntax) but is fundamentally different. Unlike classes Interfaces do not have implementation (body or code) or Access Modifiers (public, private, protected).
            //    Interfaces are used to beuild loosely-coupled applications (opposite of tightly coupled)

            //14. Generics don't have a performane penalty

            //15. In Asynchronous Program Execution - when a function is called, program execution continues to the next line,
            //without wiating for the funtion to complete.  Used to be multi-threading and callbacks, now use "Async/Await"




            //BeginnerObjects(); //for beginner

            //IntermediateObjects(); //For Intermediate

            // AdvancedObjects(); //For Advanced

          //  TimCoreyString();



        }

        static void BeginnerObjects()
        {


            ////new Object/General info
            //var objObject = new clsObject();
            //objObject.runObject();
            //////------------------------------

            // ------------new ENUMS---------------------
            //var objEnums = new clsEnums();
            //objEnums.runEnums();
            //////------------------------------

            //------------------------------
            //new Reference Types vs, Value Types
            //var objRefTypeVsValueType = new clsRefTypeVsValueType();
            //objRefTypeVsValueType.runRefTypeVsValueType();
            //////------------------------------



            //------------------------------
            //new Conditional Statments
            //var objConditional = new clsConditional();
            //objConditional.runConditional();
            ////------------------------------

            //  ------------------------------
            // new Interation Statments
            //var objIteration = new clsIteration();
            //objIteration.runIteration();
            ////------------------------------


            //------------------------------
            //new Random class
            //var objRandom = new clsRandom();
            //objRandom.runRandom();
            ////------------------------------

            //------------------------------
            ////new Arrays
            //var objArrays = new clsArrays();
            //objArrays.runArrays();
            //////------------------------------


            ////------------------------------
            ////new Lists
            //var objLists = new clsLists();
            //objLists.runLists();
            //////------------------------------

            ////------------------------------
            ////new DateTime
            // var objDateTime = new clsDateTime();
            // objDateTime.runDateTime();
            //////------------------------------


            ////------------------------------
            ////new TimeSpan
            //var objTimeSpan = new clsTimeSpan();
            //objTimeSpan.runTimeSpan();
            //////------------------------------


            ////------------------------------
            ////new String
            //var objString = new clsString();
            //objString.runString();
            //////------------------------------


            ////------------------------------
            ////new Summarize Text
            //var objSummarizeText = new clsSummarizeText();
            //objSummarizeText.runSummarizeText();
            //////------------------------------

            ////------------------------------
            ////new StringBuilder
            //var objStringBuilder = new clsStringBuilder();
            //objStringBuilder.runStringBuilder();
            //////------------------------------

            ////------------------------------
            ////new File and FileInfo
            // var objFileAndFileInfo = new clsFileAndFileInfo();
            //  objFileAndFileInfo.runFileAndFileInfo();
            //////------------------------------

            ////------------------------------
            ////new Directory and DirectoryInfo
            //var objDirectoryAndDirectoryInfo = new clsDirectoryAndDirectoryInfo();
            //objDirectoryAndDirectoryInfo.runDirectoryAndDirectoryInfo();
            //////------------------------------

            ////------------------------------
            ////new Path
            //var objPath = new clsPath();
            //objPath.runPath();
            //////------------------------------

            ////------------------------------
            ////new Path
            //var objPath = new clsPath();
            //objPath.runPath();
            //////------------------------------

            ////------------------------------
            ////new Debugging
            //var objDebugging = new clsDebugging();
            // objDebugging.runDebugging();
            //////------------------------------
        }

        static void IntermediateObjects()
        {
            ////new Classes First example
            //var objClassesFirst = new clsClassesFirst();
            //objClassesFirst.runClassesFirst();
            //////------------------------------

            ////Constructors (methods to initialize always have the same name as the class)
            // var objConstructors = new clsConstructors();
            // objConstructors.runConstructors();
            //////------------------------------

            //////Object Initializers
            //var objObjectInitializers = new clsObjectInitializers();
            //objObjectInitializers.runObjectInitializers();
            ////////------------------------------

            //////Fields
            // var objFields = new clsFields();
            // objFields.runFields();
            ////////------------------------------

            //////Fields
            //  var objAccessModifiers = new clsAccessModifiers();
            //  objAccessModifiers.runAccessModifiers();
            ////////------------------------------

            //////Properties
            // var objProperties = new clsProperties();
            // objProperties.runProperties();
            ////////------------------------------


            //////Indexer
            //  var objIndexer = new clsIndexer();
            //  objIndexer.runIndexer();
            ////////------------------------------


            //////Inheritance
            //var objInheritance = new clsInheritance();
            //objInheritance.runInheritance();
            ////////------------------------------

            //////Composition
            //  var objComposition = new clsComposition();
            //  objComposition.runComposition();
            ////////------------------------------

            //////Access Modifiers
            //var objAccesModifiers = new clsAccessModifiers();
            //objAccesModifiers.runAccessModifiers();
            ////////------------------------------

            //////Constructor Inheritance
            // var objConstructorInheritance = new clsConstructorInheritance();
            // objConstructorInheritance.runConstructorInheritance();
            ////////------------------------------

            //////Upcasting Downcasting
            //  var objUpcastingDowncasting = new clsUpcastingDowncasting();
            //  objUpcastingDowncasting.runUpcastingDowncasting();
            ////////------------------------------

            //////Boxing Unboxing
            // var objBoxingUnboxing = new clsBoxingUnboxing();
            // objBoxingUnboxing.runBoxingUnboxing();
            ////////------------------------------

            ////// Method Overriding
            //  var objMethodOverriding = new clsMethodOverriding();
            //  objMethodOverriding.runMethodOverriding();
            ////////------------------------------
            ///

            ////// Abstract Classes and Members
            // var objclsAbstractClassesMembers = new clsAbstractClassesMembers();
            // objclsAbstractClassesMembers.runAbstractClassesMembers();
            ////////------------------------------
            ///

            ////// Interfaces and Testability
            //var objclsAbstractClassesMembers = new clsAbstractClassesMembers();
            //objclsAbstractClassesMembers.runAbstractClassesMembers();
            ////////------------------------------
            ///

            ////// Interface Extensibility
            var objInterfaceExtensibility = new clsInterfaceExtensibility();
            objInterfaceExtensibility.runInterfaceExtensibility();
            ////////------------------------------
            ///


        }

        static void AdvancedObjects()
        {
            //NOTE: Some advanced source code is in ncluded folder.

            ////Generics
            //  var objGenerics = new clsGenerics();
            //  objGenerics.runGenerics();
            //////------------------------------
            ///

            ////Events And Delegates
            //   var objEventsAndDelegates = new clsEventsAndDelegates();
            //   objEventsAndDelegates.runEventsAndDelegates();
            //////------------------------------


            //Asynchronous Programming: In Asynchronous Program Execution - when a function is called, program execution continues to the next line,
            //without wiating for the funtion to complete.  Used to be multi-threading and callbacks, now use "Async/Await"
            var objAsynchronousProg = new clsAsynchronousProg();
            objAsynchronousProg.runAsynchronousProg();
            //////------------------------------
         



        }

        static void TimCoreyString()
        {
          
            var objStringConversion = new StringConversion();
            objStringConversion.ConvertString();
            //////------------------------------




        }
    }

}
