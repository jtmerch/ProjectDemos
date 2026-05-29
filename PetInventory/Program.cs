// Problem 1
Animal d = new Dog();
Console.Write(d.speak(0)); //not emitting bow-wow

// Problem 2
// int main() isn't a valid C# entry point. It should be Main with a capital M, and usually static void Main() or static int Main(string[] args).
int main()
{
    B b = new B();

    // The printf style formatting isn't valid in C#. You can use string interpolation or another string manipulation method.
    Console.WriteLine("%d %d\n", b.a.a, b.a.b);
    return 0;
}

// Corrected:
// static void Main()
// {
//     B b = new B();
//     Console.WriteLine($"{b.a.a} {b.a.b}");
// }
