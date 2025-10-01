using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.IntermediateTopics.ExtClasses
{
    class FieldsCustomer
    {
        public int ID;
        public string Name;
        public readonly List<FieldsOrder> Orders = new List<FieldsOrder>(); //always initialize the list to an empty list.  You can initialize up here or in default constructor

        public FieldsCustomer(int Id)
        {
            this.ID = Id;
        }
        public FieldsCustomer(int Id, string name)
            : this(Id)
        {
            this.Name = name;
        }

        public void Promote()
        {
            //this line throws error without readonly
          //  Orders = new List<FieldsOrder>();
            //....
        }

    }

    class FieldsOrder //not using this class, only using for List
    {

    }


}
