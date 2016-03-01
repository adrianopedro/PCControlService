Imports System.Text
Imports System.Web.Script.Serialization
Imports System.Runtime.InteropServices
Imports PCControlSocketService.WindowsApi

Class Executer
    Dim cmd As New StringBuilder
    Dim content As String
    Dim act As String
    Dim jsondata As Object
    Dim jsonsubdata As Object
    Dim decoder As New JavaScriptSerializer()
    Dim resp As UInteger


    Public Sub Builder(ByVal content As String)
        'Debug.Print(content)
        jsondata = decoder.DeserializeObject(content)
    End Sub

    Public Sub Run()
        Debug.Print(jsondata("act"))
        Select Case jsondata("act")
            Case "setpwd"
                cmd.Append("NET USER """ + jsondata("data")("u") + """ " + jsondata("data")("p") + "")
                Debug.Print(ShellAsUser(cmd, jsondata("admin")("u"), jsondata("admin")("p")))
            Case "lock"
                cmd.Append("rundll32.exe user32.dll,LockWorkStation")
                Debug.Print(cmd.ToString)
                Debug.Print(ShellAsUser(cmd, jsondata("admin")("u"), jsondata("admin")("p")))

            Case "msg"
                Dim WTS_CURRENT_SERVER_HANDLE As IntPtr = IntPtr.Zero
                Dim WTS_CURRENT_SESSION As Integer = WTSGetActiveConsoleSessionId()
                Dim title As String = "NOTIFICATION"
                Dim tlen As Integer = title.Length
                Dim msg As String = jsondata("data")("msg")
                Dim mlen As Integer = msg.Length
                Dim response As Integer = 0
                Dim style = WindowsApi.MBStyleFlags.Exclamation Or WindowsApi.MBStyleFlags.SystemModal
                WindowsApi.WTSSendMessage(WTS_CURRENT_SERVER_HANDLE, WTS_CURRENT_SESSION, title, tlen, msg, mlen, style, 3000, Nothing, False)
            Case "raw"
                cmd.Append(jsondata("data")("raw"))
                Debug.Print(ShellAsUser(cmd, jsondata("admin")("u"), jsondata("admin")("p")))
        End Select
    End Sub

    Private Function ShellAsUser(ByVal cmd As StringBuilder, Optional ByVal username As String = Nothing, Optional ByVal password As String = Nothing) As String
        Dim UserTokenHandle As IntPtr = IntPtr.Zero
        Dim EnvironmentBlock As IntPtr = IntPtr.Zero

        If (username IsNot Nothing And password IsNot Nothing) Then
            WindowsApi.LogonUser(".\" + username, Nothing, password, WindowsApi.LogonType.LOGON32_LOGON_INTERACTIVE, WindowsApi.LogonProvider.LOGON32_PROVIDER_DEFAULT, UserTokenHandle)
        Else
            WindowsApi.WTSQueryUserToken(WindowsApi.WTSGetActiveConsoleSessionId(), UserTokenHandle)
        End If
        'CreateEnvironmentBlock(EnvironmentBlock, UserTokenHandle, False)

        Dim ProcInfo As New WindowsApi.PROCESS_INFORMATION
        Dim StartInfo As New WindowsApi.STARTUPINFOW
        StartInfo.cb = CUInt(Marshal.SizeOf(StartInfo))
        Try
            Return WindowsApi.CreateProcessAsUser(UserTokenHandle, Nothing, cmd, IntPtr.Zero, IntPtr.Zero, False, WindowsApi.CreateProcessFlags.CREATE_NEW_CONSOLE, EnvironmentBlock, Nothing, StartInfo, ProcInfo)
        Catch ex As Exception
            Return ex.Message
        End Try

        If Not UserTokenHandle = IntPtr.Zero Then
            WindowsApi.CloseHandle(UserTokenHandle)
        End If

        'Clear cmd
        cmd = Nothing
    End Function

End Class

