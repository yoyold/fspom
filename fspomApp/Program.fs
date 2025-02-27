open System
open System.Diagnostics

module FspomApp =
    let updateProgressBar (elapsedRatio : float) =
        let width = 50
        let filledCount = int (elapsedRatio * float width)
        let progressBar = String.replicate filledCount "█" + String.replicate (width - filledCount) "-"
        printf "\r\027[36m[%s] %.2f%%\027[0m" progressBar (elapsedRatio * 100.0) // \027[36m is the escape sequence for cyan color
        
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

// Main entry point
[<EntryPoint>]
let main argv =
    printfn "Enter the duration in seconds:"
    match Console.ReadLine() with
    | null -> 
        printfn "No input provided."
        1
    | durationString ->
        match Double.TryParse(durationString) with
        | true, duration ->
            FspomApp.startStopwatchWithProgressBar duration
            0
        | _ ->
            printfn "Invalid input. Please enter a valid duration in seconds."
            1