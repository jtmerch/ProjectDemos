class Dog : Animal
{
    // This isn't actually overriding Animal.speak(int) because the parameter is short instead of int.
    // Since the signatures don't match, C# creates a new method.
    // When the code runs, it falls back to Animal.speak() because no valid override exists.
    public string speak(short x) { return "bow-wow"; }

    // Corrected:
    // public override string speak(int x) { return "bow-wow"; }
}
