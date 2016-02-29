Imports System.Threading


Public Class Main

    Dim listenThread As New Thread(New ThreadStart(AddressOf AsynchronousSocketListener.Main))
    'Private Declare Function LockWorkStation Lib "user32.dll" () As Integer 'Load windows API

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.

        'System.Diagnostics.Debugger.Launch()

        listenThread.Start()
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.

    End Sub

End Class 'StateObject
