
using CSharpDataStructAlgorithmApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpDataStructAlgorithmApi.DSApiClasses
{
    public class APIArrays : IAPIContract
    {
        public List<APIArray> Apiarrays { get; private set; }

        public APIArrays() //constructor
        {

            Apiarrays = new List<APIArray>();

            //Measure algorithms by Time Complexity (time) and Space Complexity (how much space).
            //Time complexity is measured in: 
            //1. Experimental Analysis: The run time is measured on various inputs.  Lapse time is from start to end of algorithm.
            //Disadvantages are Limited Input, Hardware dependent.  It is also software dependent and OS dependent.
            //2. Theoritical Analysis (Mathmatical Model): This is performed directly on description of Algorithm.  Advantage is that it is
            //Independent of hardware, sofwtware, OS, network, etc.  It also takes into account ALL possible inputs.  Primitives operations are:
            //Declarations, Assignment, Arithmetic Operations, Comparison Statements, Accessing elements, Calling functions, Returning Functions




        }

        public void BuildReturnJSON(APIArray modelApiArray)
        {
            var DataAlgType = modelApiArray.arrayType;  //"testalbType";
            var DataAlgReturned = modelApiArray.apiResult; //"cant touch this 1,2,3";


            Apiarrays.Add(new APIArray()
            {
                arrayType = DataAlgType,
                apiResult = DataAlgReturned
            });


        }
       
    }
}
