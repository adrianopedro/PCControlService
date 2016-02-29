Imports System.Runtime.InteropServices
Imports System.Text

Public Class WindowsApi
    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function LockWorkStation() As Boolean
    End Function

    <DllImport("Advapi32.dll", SetLastError:=True)>
    Public Shared Function LogonUser(ByVal lpszUsername As String, ByVal lpszDomain As String, ByVal lpszPassword As String, ByVal dwLogonType As LogonType, ByVal dwLogonProvider As LogonProvider, ByRef phToken As IntPtr) As Integer
    End Function

    <DllImport("kernel32.dll", EntryPoint:="WTSGetActiveConsoleSessionId", SetLastError:=True)>
    Public Shared Function WTSGetActiveConsoleSessionId() As UInteger
    End Function

    <DllImport("Wtsapi32.dll", EntryPoint:="WTSQueryUserToken", SetLastError:=True)>
    Public Shared Function WTSQueryUserToken(ByVal SessionId As UInteger, ByRef phToken As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("Advapi32.dll", EntryPoint:="DuplicateTokenEx", SetLastError:=True)>
    Public Shared Function DuplicateTokenEx(
                        ByVal ExistingTokenHandle As IntPtr,
                        ByVal dwDesiredAccess As UInt32,
                        ByRef lpThreadAttributes As SECURITY_ATTRIBUTES,
                        ByVal ImpersonationLevel As Integer,
                        ByVal TokenType As Integer,
                        ByRef DuplicateTokenHandle As System.IntPtr) As Boolean
    End Function


    <DllImport("kernel32.dll", EntryPoint:="CloseHandle", SetLastError:=True)>
    Public Shared Function CloseHandle(<InAttribute()> ByVal hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("Advapi32.dll", EntryPoint:="CreateProcessAsUser", ExactSpelling:=False, SetLastError:=True, CharSet:=CharSet.Unicode)>
    Public Shared Function CreateProcessAsUser(
                           ByVal hToken As IntPtr,
                           ByVal lpApplicationName As String,
                           <[In](), Out(), [Optional]()> ByVal lpCommandLine As StringBuilder,
                           ByVal lpProcessAttributes As IntPtr,
                           ByVal lpThreadAttributes As IntPtr,
                           <MarshalAs(UnmanagedType.Bool)> ByVal bInheritHandles As Boolean,
                           ByVal dwCreationFlags As Integer,
                           ByVal lpEnvironment As IntPtr,
                           ByVal lpCurrentDirectory As String,
                           <[In]()> ByRef lpStartupInfo As STARTUPINFOW,
                           <Out()> ByRef lpProcessInformation As PROCESS_INFORMATION) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("wtsapi32.dll", SetLastError:=True)>
    Public Shared Function WTSSendMessage(ByVal hServer As IntPtr, ByVal SessionId As Int32, ByVal title As String, ByVal titleLength As UInt32, ByVal message As String, ByVal messageLength As UInt32, ByVal style As UInt32, ByVal timeout As UInt32, ByRef pResponse As UInt32, ByVal bWait As Boolean) As Boolean
    End Function

    <DllImport("Advapi32.dll", SetLastError:=True, EntryPoint:="CreateProcessWithLogonW")>
    Public Shared Function CreateProcessWithLogonW(ByVal lpUsername As String, ByVal lpDomain As String, ByVal lpPassword As String, ByVal dwLogonFlags As Int32, ByVal lpApplicationName As String, ByVal lpCommandLine As StringBuilder, ByVal dwCreationFlags As Int32, ByVal lpEnvironment As IntPtr, ByVal lpCurrentDirectory As String, ByRef si As STARTUPINFOW, ByRef pi As PROCESS_INFORMATION) As Integer
    End Function

    <DllImport("Userenv.dll", EntryPoint:="CreateEnvironmentBlock", SetLastError:=True)>
    Public Shared Function CreateEnvironmentBlock(ByRef lpEnvironment As IntPtr, ByVal hToken As IntPtr, ByVal bInherit As Boolean) As Boolean
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Public Structure SECURITY_ATTRIBUTES
        Public nLength As UInteger
        Public lpSecurityDescriptor As IntPtr
        <MarshalAs(UnmanagedType.Bool)>
        Public bInheritHandle As Boolean
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure STARTUPINFOW
        Public cb As UInteger
        <MarshalAs(UnmanagedType.LPWStr)>
        Public lpReserved As String
        <MarshalAs(UnmanagedType.LPWStr)>
        Public lpDesktop As String
        <MarshalAs(UnmanagedType.LPWStr)>
        Public lpTitle As String
        Public dwX As UInteger
        Public dwY As UInteger
        Public dwXSize As UInteger
        Public dwYSize As UInteger
        Public dwXCountChars As UInteger
        Public dwYCountChars As UInteger
        Public dwFillAttribute As UInteger
        Public dwFlags As UInteger
        Public wShowWindow As UShort
        Public cbReserved2 As UShort
        Public lpReserved2 As IntPtr
        Public hStdInput As IntPtr
        Public hStdOutput As IntPtr
        Public hStdError As IntPtr
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure STARTUPINFO
        Public cb As Integer
        Public lpReserved As String
        Public lpDesktop As String
        Public lpTitle As String
        Public dwX As Integer
        Public dwY As Integer
        Public dwXSize As Integer
        Public dwYSize As Integer
        Public dwXCountChars As Integer
        Public dwYCountChars As Integer
        Public dwFillAttribute As Integer
        Public dwFlags As Integer
        Public wShowWindow As Short
        Public cbReserved2 As Short
        Public lpReserved2 As Integer
        Public hStdInput As Integer
        Public hStdOutput As Integer
        Public hStdError As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure PROCESS_INFORMATION
        Public hProcess As IntPtr
        Public hThread As IntPtr
        Public dwProcessId As UInteger
        Public dwThreadId As UInteger
    End Structure

    <Flags()>
    Enum MBStyleFlags
        SystemModal = 4096
        Critical = 16
        Exclamation = 48
    End Enum

    Enum LogonType As Integer
        'This logon type is intended for users who will be interactively using the computer, such as a user being logged on 
        'by a terminal server, remote shell, or similar process.
        'This logon type has the additional expense of caching logon information for disconnected operations; 
        'therefore, it is inappropriate for some client/server applications,
        'such as a mail server.
        LOGON32_LOGON_INTERACTIVE = 2

        'This logon type is intended for high performance servers to authenticate plaintext passwords.
        'The LogonUser function does not cache credentials for this logon type.
        LOGON32_LOGON_NETWORK = 3

        'This logon type is intended for batch servers, where processes may be executing on behalf of a user without 
        'their direct intervention. This type is also for higher performance servers that process many plaintext
        'authentication attempts at a time, such as mail or Web servers. 
        'The LogonUser function does not cache credentials for this logon type.
        LOGON32_LOGON_BATCH = 4

        'Indicates a service-type logon. The account provided must have the service privilege enabled. 
        LOGON32_LOGON_SERVICE = 5

        'This logon type is for GINA DLLs that log on users who will be interactively using the computer. 
        'This logon type can generate a unique audit record that shows when the workstation was unlocked. 
        LOGON32_LOGON_UNLOCK = 7

        'This logon type preserves the name and password in the authentication package, which allows the server to make 
        'connections to other network servers while impersonating the client. A server can accept plaintext credentials 
        'from a client, call LogonUser, verify that the user can access the system across the network, and still 
        'communicate with other servers.
        'NOTE: Windows NT:  This value is not supported. 
        LOGON32_LOGON_NETWORK_CLEARTEXT = 8

        'This logon type allows the caller to clone its current token and specify new credentials for outbound connections.
        'The new logon session has the same local identifier but uses different credentials for other network connections. 
        'NOTE: This logon type is supported only by the LOGON32_PROVIDER_WINNT50 logon provider.
        'NOTE: Windows NT:  This value is not supported. 
        LOGON32_LOGON_NEW_CREDENTIALS = 9
    End Enum

    Enum LogonProvider As Integer
        'Use the standard logon provider for the system. 
        'The default security provider is negotiate, unless you pass NULL for the domain name and the user name 
        'is not in UPN format. In this case, the default provider is NTLM. 
        'NOTE: Windows 2000/NT:   The default security provider is NTLM.
        LOGON32_PROVIDER_DEFAULT = 0
        LOGON32_PROVIDER_WINNT35 = 1
        LOGON32_PROVIDER_WINNT40 = 2
        LOGON32_PROVIDER_WINNT50 = 3
    End Enum

    Enum CreateProcessFlags
        DEBUG_PROCESS = &H1
        DEBUG_ONLY_THIS_PROCESS = &H2
        CREATE_SUSPENDED = &H4
        DETACHED_PROCESS = &H8
        CREATE_NEW_CONSOLE = &H10
        NORMAL_PRIORITY_CLASS = &H20
        IDLE_PRIORITY_CLASS = &H40
        HIGH_PRIORITY_CLASS = &H80
        REALTIME_PRIORITY_CLASS = &H100
        CREATE_NEW_PROCESS_GROUP = &H200
        CREATE_UNICODE_ENVIRONMENT = &H400
        CREATE_SEPARATE_WOW_VDM = &H800
        CREATE_SHARED_WOW_VDM = &H1000
        CREATE_FORCEDOS = &H2000
        BELOW_NORMAL_PRIORITY_CLASS = &H4000
        ABOVE_NORMAL_PRIORITY_CLASS = &H8000
        INHERIT_PARENT_AFFINITY = &H10000
        INHERIT_CALLER_PRIORITY = &H20000
        CREATE_PROTECTED_PROCESS = &H40000
        EXTENDED_STARTUPINFO_PRESENT = &H80000
        PROCESS_MODE_BACKGROUND_BEGIN = &H100000
        PROCESS_MODE_BACKGROUND_END = &H200000
        CREATE_BREAKAWAY_FROM_JOB = &H1000000
        CREATE_PRESERVE_CODE_AUTHZ_LEVEL = &H2000000
        CREATE_DEFAULT_ERROR_MODE = &H4000000
        CREATE_NO_WINDOW = &H8000000
        PROFILE_USER = &H10000000
        PROFILE_KERNEL = &H20000000
        PROFILE_SERVER = &H40000000
        CREATE_IGNORE_SYSTEM_DEFAULT = &H80000000
    End Enum

End Class