using System;
using System.Linq;

int[] numbers = { 8, 3, 12, 5, 2, 9, 4, 7 };
Student[] students = {
    new Student(1, "Ana", 17, "Burgas"),
    new Student(2, "Boris", 18, "Sofia"),
    new Student(3, "Clara", 20, "Burgas"),
    new Student(4, "Deyan", 19, "Varna"),
    new Student(5, "Elena", 18, "Sofia"),
    new Student(6, "Filip", 21, "Burgas")
};
Course[] courses = {
    new Course(101, "C# Basics"),
    new Course(102, "Web Development"),
    new Course(103, "Databases"),
    new Course(104, "Algorithms")
};
Enrollment[] enrollments = {
    new Enrollment(1, 1, 101, 5),
    new Enrollment(2, 2, 101, 6),
    new Enrollment(3, 3, 102, 6),
    new Enrollment(4, 4, 102, 4),
    new Enrollment(5, 5, 103, 5),
    new Enrollment(6, 6, 103, 6),
    new Enrollment(7, 2, 103, 4),
    new Enrollment(8, 3, 101, 3),
    new Enrollment(9, 6, 102, 5)
};

var q1 = numbers.Select(n => n * n);
var q2 = students.Select(s => new { s.Name, s.City });
var q3 = numbers.Where(n => n % 2 == 0);
var q4 = students.Where(s => s.City == "Burgas" && s.Age >= 18);
var q5 = students.Where(s => s.Age >= 18).Select(s => s.Name);
var q6 = numbers.OrderBy(n => n);
var q7 = students.Where(s => s.Age >= 18).OrderBy(s => s.Age).Select(s => s.Name);
var q8 = numbers.OrderBy(n => -n);
var q9 = students.First(s => s.City == "Sofia");
var q11 = students.Single(s => s.Id == 4);
var q14 = students.GroupBy(s => s.City);
var q15 = students.GroupBy(s => s.Age >= 18);
var q16 = enrollments.Where(e => e.Grade >= 5).GroupBy(e => e.CourseId);
var q17 = enrollments.Join(students, e => e.StudentId, s => s.Id, (e, s) => new { e.Id, s.Name, e.Grade });
var q18 = courses.Join(enrollments.Where(e => e.Grade >= 5), c => c.Id, e => e.CourseId, (c, e) => new { c.Title, e.StudentId, e.Grade });
var q19 = enrollments
    .Where(e => e.Grade >= 5)
    .Join(students, e => e.StudentId, s => s.Id, (e, s) => new { e, s })
    .Join(courses, es => es.e.CourseId, c => c.Id, (es, c) => new { es.e.Id, es.s.Name, c.Title, es.e.Grade })
    .OrderBy(x => x.Id)
    .Select(x => new { x.Name, x.Title, x.Grade });
var q20 = enrollments
    .Join(students, e => e.StudentId, s => s.Id, (e, s) => new { e, s })
    .Join(courses, es => es.e.CourseId, c => c.Id, (es, c) => new { CourseId = c.Id, c.Title, es.s.Name, es.e.Grade, EnrollmentId = es.e.Id })
    .GroupBy(x => new { x.CourseId, x.Title })
    .OrderBy(g => g.Key.CourseId)
    .Select(g => g.OrderByDescending(x => x.Grade).ThenBy(x => x.EnrollmentId).First())
    .Select(x => new { x.Title, x.Name, x.Grade });

public record Student(int Id, string Name, int Age, string City);
public record Course(int Id, string Title);
public record Enrollment(int Id, int StudentId, int CourseId, int Grade);