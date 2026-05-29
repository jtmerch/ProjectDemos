class B
{
    // You can't make an object const in C#. Const only works with simple values like numbers and strings.
    // Even if this did compile, const values are accessed through the class, not through an object instance like b.a.a.
    // A const has to be given a value when it's declared. You can't assign it later in the constructor.
    public const A a;

    // Corrected: use readonly instead of const, which allows a reference type
    // public readonly A a;

    public B()
    {
        // You're never actually creating an instance of A. Since there's no new A(), the object is null, and trying to set a value on it would throw an error.
        a.a = 10;

        // Corrected: create an instance of A first, then set the value
        // a = new A();
        // a.a = 10;
    }
}
