module Services

open Models

// Used tuple to create a course
let createCourse (id: int, title: string, level: Level, durationWeeks: int, teacher: string) : Response<Course> = 
    if title = "" || teacher = "" then
        Error "Course title or teacher cannot be empty"
    else
        Success { Id = id; Title = title; Level = level; DurationWeeks = durationWeeks; Teacher = teacher }

let enrollStudent (student: Student) (course: Course) : Response<Enrollment> =
    if student.Level = course.Level then
        Success { Student = student; Course = course; Status = Active }
    else
        Error "Student level does not match course level"

let completeCourse (enrollment: Enrollment) : Response<Enrollment> =
    match enrollment.Status with
    | Completed -> Error "Course already completed"
    | Active -> Success { enrollment with Status = Completed }

// Curried Functions
// Partially Applied Functions
let filterCoursesByLevel (level: Level) (courses: Course list) : Response<Course list> =
    let result = courses |> List.filter (fun c -> c.Level = level)
    if result = [] then Error "No courses found for the specified level" else Success result

let describeCourse level =
    match level with 
    | Beginner -> "Build a strong foundation in English with essential vocabulary, basic grammar, and everyday conversation skills. Perfect for those starting from scratch or with minimal knowledge."
    | Intermediate -> "Improve fluency and confidence by expanding vocabulary, mastering complex grammar, and engaging in discussions on various topics. Ideal for learners who can hold simple conversations but want to enhance their skills."
    | Advanced -> "Achieve full proficiency with advanced grammar, idiomatic expressions, and professional communication skills. Designed for learners who want to refine their accuracy, expand their academic or business English, and speak with confidence."

let tryFindStudentById id (students: Student list) : Response<Student> =
    match students |> List.tryFind (fun s -> s.Id = id) with
    | Some student -> Success student
    | None -> Error $"Student with id %d{id} not found"

let studentsInCourse course students = 
    students |> List.filter (fun student -> List.contains course student.EnrolledCourses)

let countStudentsInCourse (course: Course) (students: Student list) : Response<int> =
    let count = studentsInCourse course students |> List.length
    if count = 0 then Error "No students enrolled in the course" else Success count