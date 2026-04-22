public abstract class Person
{
    public string FullName { get; set; }
    public int Age { get; set; }

    public Person(string fullName, int age)
    {
        FullName = fullName;
        Age = age;
    }
}
