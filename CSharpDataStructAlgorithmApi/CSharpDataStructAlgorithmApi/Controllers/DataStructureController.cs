using CSharpDataStructAlgorithmApi.DSApiClasses;
using CSharpDataStructAlgorithmApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSharpDataStructAlgorithmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataStructureController : ControllerBase
    {
        // GET: api/<DataStructureController>
        [HttpGet]
        public IEnumerable<APIArray> Get()
        {

            APIArray myAPIInput = new APIArray();
          

            myAPIInput.arrayType = "Single Dimensional Arrays";


            var UserInput = "";
            var SystemOutput = "day after april fools";

            myAPIInput.apiResult = $"Input: {UserInput} Output: {SystemOutput}";


            IAPIContract apiArrayJSON = new APIArrays();
            apiArrayJSON.BuildReturnJSON(myAPIInput);


            return apiArrayJSON.Apiarrays; //getting the values from the class property and returning JSON


            // return new string[] { "value1", "value2" };

        }

        // GET api/<DataStructureController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DataStructureController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DataStructureController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DataStructureController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
