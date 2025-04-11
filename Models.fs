module Models

// Records
// Discriminated Unions
type Level = Beginner | Intermediate | Advanced

type Course = {
    Id: int
    Title: string
    Level: Level
    DurationWeeks: int
    Teacher: string
}

type Student = {
    Id: int
    Name: string
    Level: Level
    EnrolledCourses: Course list
}

type Lesson = {
    Id: int
    Title: string
    DurationMinutes: int
    Course: Course
}

// Discriminated Unions
type EnrolledCourses = Active | Completed

type Enrollment = {
    Student: Student
    Course: Course
    Status: EnrolledCourses
}

type Response<'T> = 
    | Success of 'T
    | Error of string