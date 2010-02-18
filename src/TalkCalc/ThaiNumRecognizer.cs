/*
Imports System.Speech.Recognition
Imports System.Threading
Imports System.Linq.Expressions
Imports System.Globalization

Public Class ThaiNumRecognizer
    Inherits Recognizer

    Private _engine As New SpeechRecognitionEngine()

    Public Sub New()
        _engine.LoadGrammar(buildGrammar())
        _engine.SetInputToDefaultAudioDevice()
    End Sub

    Private Function buildGrammar() As Grammar
        Dim builder = New GrammarBuilder()

        Dim numbers = New Choices("Neug", "Saawng", "Saam", "See", "Haa", "Hohk", "Jet", "Paadt", "Gaao")
        Dim ops = New Choices("Buak", "Lob", "Koon", "Haan")

        builder.Append(numbers)

        Return New Grammar(builder)
    End Function


    Protected Overrides Sub StartCore()
        AddHandler _engine.SpeechDetected, AddressOf speechDetected
        AddHandler _engine.SpeechHypothesized, AddressOf speechHypothesized
        AddHandler _engine.SpeechRecognized, AddressOf speechRecognized

        _engine.RecognizeAsync(RecognizeMode.Multiple)
    End Sub

    Protected Overrides Function StopCore() As Expression
        _engine.RecognizeAsyncStop()

        RemoveHandler _engine.SpeechRecognized, AddressOf speechRecognized
        RemoveHandler _engine.SpeechHypothesized, AddressOf speechHypothesized
        RemoveHandler _engine.SpeechDetected, AddressOf speechDetected

        Return Nothing
    End Function


    Private Sub speechDetected(ByVal sender As Object, ByVal e As SpeechDetectedEventArgs)
        Debug.WriteLine("DETECTED: " + e.AudioPosition.ToString())
    End Sub

    Private Sub speechHypothesized(ByVal sender As Object, ByVal e As SpeechHypothesizedEventArgs)
        Debug.WriteLine("HYPOTHESIZED: " + e.Result.Text)
    End Sub

    Private Sub speechRecognized(ByVal sender As Object, ByVal e As SpeechRecognizedEventArgs)
        Debug.WriteLine("RECOGNIZED: " + e.Result.Text)
    End Sub

    Private Sub speechRejected(ByVal sender As Object, ByVal e As SpeechRecognitionRejectedEventArgs)
        Debug.WriteLine("REJECTED: " + e.Result.Text)
    End Sub

End Class
*/