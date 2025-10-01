using System;

namespace Generics
{
    class Program
    {
        //Where T : IComparable  (constraint toan interface)
        //Where T : Product (constraint to a class)
        //Where T : struct (constraint to a value type)
        //Where T : class (constraint to a reference type)
        //Where T : structs (constraint to a object that has a default constructor)

        static void Main(string[] args)
        {
            var number = new Nullable<int>();
            Console.WriteLine("Has Value ?" + number.HasValue);
            Console.WriteLine("Value: " + number.GetValueOrDefault());
        }
    }
}
