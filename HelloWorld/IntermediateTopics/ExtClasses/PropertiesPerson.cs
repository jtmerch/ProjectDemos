using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
    public class PropertiesPerson
    {

        //NOTE: typing "prop" and TAB generates the shorter auto implemented property
        public DateTime BirthDate { get; set; }

        /*public DateTime BirthDate { get; private set; } //if you want a private set method, in that case you would set the birthdate in a constructor
        Public Person(DateTime birthdate)
        {
         Birthdate = birthdate
         }*/

        public int Age
        {
            get
            {
                TimeSpan getTimeSpan = DateTime.Today - BirthDate;
                var getYears = getTimeSpan.Days / 365;

                return getYears;
            }
        }



        //NOTE Typing "propfull" and TAB generates the entire properyty OLD School

        //private DateTime _birthdate;

        //public DateTime BirthDate
        //{
        //    get
        //    {
        //        return _birthdate;
        //    }
        //    set
        //    {
        //        _birthdate = value;
        //    }
        //}


    }
}
