module ProgressBarTests =
    open System
    open System.IO
    open Xunit
    open System.Threading
    open System.Diagnostics
    
    // Import the main module
    open FspomApp
    
    // Capture the console output to test the formatted string of the progress bar
    let captureConsoleOutput action =
        let originalOut = Console.Out
        use sw = new StringWriter()
        Console.SetOut(sw)
        action()
        Console.SetOut(originalOut)
        sw.ToString()
    
    [<Fact>]
    let ``updateProgressBar displays the correct format at 0%`` () =
        let output = captureConsoleOutput (fun () -> updateProgressBar 0.0)
        // Replace with proper assertion library
        Assert.StartsWith("\r\027[36m[--------------------------------------------------] 0.00%\027[0m", output)
    
    [<Fact>]
    let ``updateProgressBar displays the correct format at 50%`` () =
        let output = captureConsoleOutput (fun () -> updateProgressBar 0.5)
        Assert.StartsWith("\r\027[36m[█████████████████████████-------------------------] 50.00%\027[0m", output)
    
    [<Fact>]
    let ``updateProgressBar displays the correct format at 100%`` () =
        let output = captureConsoleOutput (fun () -> updateProgressBar 1.0)
        Assert.StartsWith("\r\027[36m[██████████████████████████████████████████████████] 100.00%\027[0m", output)
    
    [<Fact>]
    let ``startStopwatchWithProgressBar runs for a given duration`` () =
        let durationInSeconds = 0.5
        let sw = Stopwatch()
        
        let output = captureConsoleOutput (fun () ->
            sw.Start()
            startStopwatchWithProgressBar durationInSeconds
            sw.Stop()
        )
        // Verify elapsed time is close to the expected duration
        Assert.InRange(sw.Elapsed.TotalSeconds, durationInSeconds - 0.1, durationInSeconds + 0.1)
        // Check for the final output string indicating 100% progress
        Assert.Contains("Stopwatch stopped. Elapsed time: 0.5", output)