module Services

open Models

// Used tuple to create a course
let createCourse (id: int, title: string, level: Level, durationWeeks: int, teacher: string) = 
    { Id = id; Title = title; Level = level; DurationWeeks = durationWeeks; Teacher = teacher }

let enrollStudent (student, course) = 
    { Student = student; Course = course; Status = Active }

let completeCourse enrollment = 
    { enrollment with Status = Completed }

// Curried Functions
// Partially Applied Functions
let filterCoursesByLevel level courses : Course list = 
    courses |> List.filter (fun course -> course.Level = level)

let describeCourse level =
    match level with 
    | Beginner -> "Build a strong foundation in English with essential vocabulary, basic grammar, and everyday conversation skills. Perfect for those starting from scratch or with minimal knowledge."
    | Intermediate -> "Improve fluency and confidence by expanding vocabulary, mastering complex grammar, and engaging in discussions on various topics. Ideal for learners who can hold simple conversations but want to enhance their skills."
    | Advanced -> "Achieve full proficiency with advanced grammar, idiomatic expressions, and professional communication skills. Designed for learners who want to refine their accuracy, expand their academic or business English, and speak with confidence."

let findStudentById id (students: Student list) = 
    students |> List.tryFind (fun student -> student.Id = id)

let studentsInCourse course students = 
    students |> List.filter (fun student -> List.contains course student.EnrolledCourses)

let countStudentsInCourse course students = 
        studentsInCourse course students |> List.length