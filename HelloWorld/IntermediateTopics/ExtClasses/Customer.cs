using System;
using System.Collections.Generic;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
   public class Customer
    {
        public int Id;
        public string Name;
        public List<Order> Orders; //list is a generic class that takes a parameter (in this case the return type parameter is from the "Order" class)

        public Customer()
        {
            Orders = new List<Order>(); //do this in constructor so that you don't have to initialize the empty list each time you create a cust object.
        }
        public Customer(int id)  //only use constructors if you want to initialize an object
            : this() //calls the constructor without parameters
        {
            this.Id = id;
        }
        public Customer(int ID, string name)  //only use constructors if you want to initialize an object
            : this() //calls the constructor without parameters
        {
            this.Id = ID; //I am able to get rid of this line IF I am referencing the above constructor that already accepts "int id"
            this.Name = name;
        }


    }
}
