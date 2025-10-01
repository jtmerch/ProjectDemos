using CSharpDataStructAlgorithmApi.DSApiClasses;
using System;
using System.Collections.Generic;

namespace CSharpDataStructAlgorithmApi.Interfaces
{
    public interface IAPIContract
    {
        //These are interface signature lines (this is just the important bits).  Whoever implements will have have these properties
       List<APIArray> Apiarrays { get; }

        void BuildReturnJSON(APIArray modelApiArray);
    }
}
