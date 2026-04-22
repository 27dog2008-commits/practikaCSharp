using System;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static void Main()
    {
        Person[] people = new Person[]
        {
            new Student("Иван Иванов", 20, 4.8),
            new Student("Мария Петрова", 21, 4.9),
            new Teacher("Сергей Смирнов", 45),
            new Teacher("Елена Кузнецова", 52)
        };

        University university = new University(people);

        Student best = university.GetBestStudent();
        Console.WriteLine($"Лучший студент: {best.FullName}, балл: {best.AverageGrade}");

        Teacher[] teachers = university.GetTeachersByAge(50);
        Console.WriteLine("Преподаватели старше 50:");
        foreach (var t in teachers)
        {
            Console.WriteLine(t.FullName);
        }
    }
}