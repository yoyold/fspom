open System
open System.Diagnostics

let startStopwatch durationInSeconds =
    let stopwatch = Stopwatch()
    printfn "Stopwatch started. Press any key to stop..."
    stopwatch.Start()

    // Wait until the specified duration has elapsed
    let endTime = DateTime.UtcNow.AddSeconds(float durationInSeconds)
    while DateTime.UtcNow < endTime do
        System.Threading.Thread.Sleep(100) // Sleep to reduce CPU usage

    stopwatch.Stop()
    printfn "Stopwatch stopped. Elapsed time: %f seconds" (stopwatch.Elapsed.TotalSeconds)

[<EntryPoint>]
let main argv =
    printfn "Enter the duration in seconds:"
    match Console.ReadLine() with
    | durationString ->
        match Double.TryParse(durationString) with
        | true, duration ->
            startStopwatch duration
            0
        | _ ->
            printfn "Invalid input. Please enter a valid duration in seconds."
            1
