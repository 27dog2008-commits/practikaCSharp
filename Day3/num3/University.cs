public class University
{
    private Person[] people;

    public University(Person[] people)
    {
        this.people = people;
    }


    public Student GetBestStudent()
    {
        Student best = null;
        foreach (var person in people)
        {
            if (person is Student student)
            {
                if (best == null || student.AverageGrade > best.AverageGrade)
                {
                    best = student;
                }
            }
        }
        return best;
    }
    
    public Teacher[] GetTeachersByAge(int age)
    {
        List<Teacher> result = new List<Teacher>();
        foreach (var person in people)
        {
            if (person is Teacher teacher && teacher.Age > age)
            {
                result.Add(teacher);
            }
        }
        return result.ToArray();
    }
}
