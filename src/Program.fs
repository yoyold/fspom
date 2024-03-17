open System
open System.Diagnostics

let startStopwatch () =
    let stopwatch = Stopwatch()
    printfn "Stopwatch started. Press any key to stop..."
    stopwatch.Start()

    // Wait for any key press to stop the stopwatch
    Console.ReadKey() |> ignore

    stopwatch.Stop()
    printfn "Stopwatch stopped. Elapsed time: %f seconds" (stopwatch.Elapsed.TotalSeconds)

[<EntryPoint>]
let main argv =
    if argv.Length = 1 && argv.[0] = "start" then
        startStopwatch()
        0
    else
        printfn "Usage: dotnet run start"
        1
