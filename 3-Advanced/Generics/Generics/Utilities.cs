using System;

namespace Generics
{
    //Where T : IComparable  (constraint toan interface)
    //Where T : Product (constraint to a class)
    //Where T : struct (constraint to a value type)
    //Where T : class (constraint to a reference type)
    //Where T : structs (constraint to a object that has a default constructor)

    public class Utilities<T> where T : IComparable, new() //constraint to a default constructor
    {
        public int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        public void DoSomething(T value)
        {
            var obj = new T();
        }

        public T Max(T a, T b)
        {
            return a.CompareTo(b) > 0 ? a : b;
        }
    }
}