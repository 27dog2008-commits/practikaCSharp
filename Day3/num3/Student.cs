public sealed class Student : Person
{
    public double AverageGrade { get; set; }

    public Student(string fullName, int age, double averageGrade) : base(fullName, age)
    {
        AverageGrade = averageGrade;
    }

}
