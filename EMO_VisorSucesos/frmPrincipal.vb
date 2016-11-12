Public Class frmPrincipal
    'Declaración de variables
    Dim bolComprobacionPrevias As Boolean

    Private Sub frmPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Function fcnComprobacionesPrevias(strNombreMaquina As String) As Boolean
        'Comprueba que existen los registros
        fcnComprobacionesPrevias = True
        If Not EventLog.Exists("System", "strNombreMaquina") Then
            MsgBox("El Registro de Sistema no existe")
            fcnComprobacionesPrevias = False
        End If
        If Not EventLog.Exists("Application", "strNombreMaquina") Then
            MsgBox("El Registro de Aplicación no existe")
            fcnComprobacionesPrevias = False
        End If
    End Function

    Private Function fncLeerRegistro(strNombreRegistro As String, strNombreMaquina As String)
        'Lee el registro indicado

        'Declaración de variables
        Dim eLog As EventLog = New EventLog
        Dim elEntry As EventLogEntry
        Dim lEntryActual As Long = 0

        eLog.Log = strNombreRegistro
        eLog.MachineName = strNombreMaquina

        For Each elEntry In eLog.Entries
            lEntryActual += 1
            txtEventLog.Text = lEntryActual.ToString & " / " & eLog.Entries.Count.ToString
            txtEventLog.Refresh()
            ProgressBar1.Maximum = eLog.Entries.Count
            ProgressBar1.Value = lEntryActual
            'If elEntry.Source = Application.ProductName Then
            '    MsgBox(elEntry.Message)
            'End If
        Next
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Comprobacion previas
        bolComprobacionPrevias = fcnComprobacionesPrevias("INTRANET.EMO.ES.EMOSISSV15")
        If bolComprobacionPrevias = True Then
            'Lee el registro
            fncLeerRegistro("Application", "INTRANET.EMO.ES.EMOSISSV15")
        End If
    End Sub
End Class