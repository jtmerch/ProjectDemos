class Cat : Animal
{
    // Missing the override keyword.
    // This hides the base method rather than overriding it.
    // When called through an Animal reference, Animal.speak() gets called instead.
    public string speak(int x) { return "meow"; }

    // Corrected:
    // public override string speak(int x) { return "meow"; }
}
