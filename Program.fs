module Program
open Models
open Services

[<EntryPoint>]
let main args =
    let course1 = createCourse (1, "English for Beginners", Beginner, 8, "John Doe")
    let course2 = createCourse (2, "Business English", Intermediate, 10, "Jane Doe")

    let student1 = { Id = 1; Name = "Alice"; Level = Intermediate; EnrolledCourses = [course1; course2] }
    let student2 = { Id = 2; Name = "Bob"; Level = Intermediate; EnrolledCourses = [course1] }
    let students = [student1; student2]

    let lesson1 = { Id = 1; Title = "Greetings and Introductions"; DurationMinutes = 30; Course = course1 }
    let lesson2 = { Id = 2; Title = "Present Simple"; DurationMinutes = 45; Course = course1 }

    let enrollment1 = enrollStudent (student1, course1)
    let completedEntollment = completeCourse enrollment1

    let describeCourse = describeCourse Beginner

    printfn "%s" describeCourse

    let beginnerCourses = filterCoursesByLevel Beginner [course1; course2]

    printfn "BeginnerCourses: %A" beginnerCourses

    match findStudentById 1 students with
    | Some student -> printfn "Found student: %s" student.Name
    | None -> printfn "Student not found"

    let studentCount = countStudentsInCourse course1 [student1; student2]
    printfn "Total students in %s: %d" course1.Title studentCount
    0
