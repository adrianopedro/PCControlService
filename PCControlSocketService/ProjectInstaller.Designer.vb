<System.ComponentModel.RunInstaller(True)> Partial Class ProjectInstaller
    Inherits System.Configuration.Install.Installer

    'Installer overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PCControlSocketServiceProcessInstaller = New System.ServiceProcess.ServiceProcessInstaller()
        Me.PCControlSocketServiceInstaller = New System.ServiceProcess.ServiceInstaller()
        '
        'PCControlSocketServiceProcessInstaller
        '
        Me.PCControlSocketServiceProcessInstaller.Password = Nothing
        Me.PCControlSocketServiceProcessInstaller.Username = Nothing
        '
        'PCControlSocketServiceInstaller
        '
        Me.PCControlSocketServiceInstaller.ServiceName = "PCControlSocketService"
        Me.PCControlSocketServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.PCControlSocketServiceInstaller, Me.PCControlSocketServiceProcessInstaller})

    End Sub
    Friend WithEvents PCControlSocketServiceProcessInstaller As System.ServiceProcess.ServiceProcessInstaller
    Friend WithEvents PCControlSocketServiceInstaller As System.ServiceProcess.ServiceInstaller

End Class
