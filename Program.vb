Imports System
Imports System.Drawing
Imports System.IO
Imports System.Media
Imports System.Threading.Thread
Imports Figgle

#Region "To-Do List"
'Add custom Logging
'Use DraxCodes' way of sending messages using embeds instead of plain text
'Create help/repeat/shuffle command - finished=repeat
'Create clear queue command - Created command but only clears the first song in the queue
'See if you can make the bot work via yt links instead of remember song name
'Figure out how to make multi-colored ascii text for the banner
#End Region

Module Program
    Dim path = AppDomain.CurrentDomain.BaseDirectory
    Dim process As Process = New Process
    Dim lavalink = $"{path}" + "Lavalink.jar"
    Dim app = $"{path}" + "application.yaml"
    Sub Main()
        Call setUp().GetAwaiter.GetResult()
    End Sub

    Private Async Function setUp() As Task
        Console.Title = "Gawr Gura"
        setBanner("/ Gawr Gura \", ConsoleColor.Cyan, ConsoleColor.Green)
        Await (loggingManager.LogSetupAsync("setup", "Looking for Lavalink server..."))
        If Not File.Exists(lavalink) Or Not File.Exists(app) Then
            Await loggingManager.LogCriticalAsync("setup", "After the program closes please add your Lavalink.jar and application.yml file into the bin\netcoreapp3.1 folder.")
            Console.WriteLine()
            Sleep(3000)
            Environment.Exit(0)
        Else
            Await loggingManager.LogSetupAsync("setup", "Lavalink server has been found now starting server...")
            process.EnableRaisingEvents = False
            process.StartInfo.FileName = "javaw.exe"
            process.StartInfo.Arguments = "-jar " + """" + path + "Lavalink.jar"
            process.Start()
            Sleep(5000)
            Await loggingManager.LogSetupAsync("setup", "Sever is setup now starting bot")
            Sleep(1500)
            Call New bot().mainAsync().GetAwaiter().GetResult()

        End If
    End Function

    Private Sub setBanner(text As String, color As ConsoleColor, _color As ConsoleColor)
        Console.ForegroundColor = color
        Console.Write(FiggleFonts.Standard.Render(text), color)
        Console.ForegroundColor = _color
        Console.WriteLine("===================================================================", _color)
    End Sub

    Private Sub consoleTextColor(text As String, color As ConsoleColor)
        Console.ForegroundColor = color
        Console.WriteLine(vbTab + text, color)
    End Sub
End Module
