using System;
using System.Collections.Generic;


namespace HelloWorld.IntermediateTopics.ExtClasses
{
    class HttpCookie
    {
        private Dictionary<string, string> _dictionary; //Dictionary uses HashTable
        public DateTime ExpDate { get; set; }


        public HttpCookie()
        {
            //constructor initializes
            _dictionary = new Dictionary<string, string>();
            //_ExpDate = ....
        }

        public string this[string key] //use "this" because indexers do not have a name
        {
           get 
            {
                return _dictionary[key];
            }
            set 
            {
                _dictionary[key] = value; //"value is a keyword
            }
        }
    }
}
