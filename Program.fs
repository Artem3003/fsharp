module Program
open Models
open Services

[<EntryPoint>]
let main args =
    let courses =
        [ createCourse (1, "English for Beginners", Beginner, 8, "John Doe")
          createCourse (2, "Business English", Intermediate, 10, "Jane Doe") ]
        |> List.choose (function Success c -> Some c | _ -> None)

    let course1 = courses |> List.find (fun c -> c.Id = 1)
    let course2 = courses |> List.find (fun c -> c.Id = 2)

    let student1 = { Id = 1; Name = "Alice"; Level = Intermediate; EnrolledCourses = [course1; course2] }
    let student2 = { Id = 2; Name = "Bob"; Level = Intermediate; EnrolledCourses = [course1] }
    let students = [student1; student2]

    let lesson1 = { Id = 1; Title = "Greetings and Introductions"; DurationMinutes = 30; Course = course1 }
    let lesson2 = { Id = 2; Title = "Present Simple"; DurationMinutes = 45; Course = course1 }

    match enrollStudent student1 course1 with
    | Success enrollment ->
        match completeCourse enrollment with
        | Success completed -> printfn "Course completed: %A" completed
        | Error msg -> printfn "Completion error: %s" msg
    | Error msg -> printfn "Enrollment error: %s" msg

    let desc = describeCourse Beginner
    printfn "Beginner Course Description:\n%s" desc

    match filterCoursesByLevel Beginner courses with
    | Success beginnerCourses -> printfn "Beginner Courses: %A" beginnerCourses
    | Error msg -> printfn "%s" msg

    match tryFindStudentById 1 students with
    | Success student -> printfn "Found student: %s" student.Name
    | Error msg -> printfn "%s" msg

    match countStudentsInCourse course1 students with
    | Success count -> printfn "Total students in %s: %d" course1.Title count
    | Error msg -> printfn "Error: %s" msg

    0