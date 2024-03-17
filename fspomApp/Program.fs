open System
open System.Diagnostics

let updateProgressBar (elapsedRatio : float) =
    let width = 50
    let filledCount = int (elapsedRatio * float width)
    let progressBar = String.replicate filledCount "█" + String.replicate (width - filledCount) "-"
    printf "\r[%s] %.2f%%" progressBar (elapsedRatio * 100.0)

let startStopwatchWithProgressBar durationInSeconds =
    let stopwatch = Stopwatch()
    printfn "Stopwatch started. Press any key to stop..."
    stopwatch.Start()

    let mutable elapsedRatio = 0.0
    while elapsedRatio < 1.0 do
        let elapsedTime = float stopwatch.ElapsedMilliseconds / 1000.0
        elapsedRatio <- min (elapsedTime / durationInSeconds) 1.0
        updateProgressBar elapsedRatio
        System.Threading.Thread.Sleep(100) // Delay to reduce flickering

    stopwatch.Stop()
    printfn "\nStopwatch stopped. Elapsed time: %.2f seconds" (stopwatch.Elapsed.TotalSeconds)

[<EntryPoint>]
let main argv =
    printfn "Enter the duration in seconds:"
    match Console.ReadLine() with
    | durationString ->
        match Double.TryParse(durationString) with
        | true, duration ->
            startStopwatchWithProgressBar duration
            0
        | _ ->
            printfn "Invalid input. Please enter a valid duration in seconds."
            1
