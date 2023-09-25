Imports System.Data.SqlClient
Public Class _Default
    Inherits System.Web.UI.Page
    Dim strConnection As String = ConfigurationManager.ConnectionStrings("SQLConn").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnSAVE_Click(sender As Object, e As EventArgs) Handles btnSAVE.Click
        Dim strSQL As New StringBuilder
        strSQL.Append(" INSERT INTO SETTING_POSITION(USER_ID, POSITION) ")
        strSQL.Append(" VALUES( 1, '" & txtPOSITION.Text & "') ")
        If Update(strSQL.ToString) = True Then
            lblMessage.Text = "Position Saved"
        Else
            lblMessage.Text = "Position save failed"
        End If
    End Sub

    Private Function Update(ByVal sqlexecute As String) As Boolean
        Dim dbConn As SqlConnection = Nothing
        Dim dbCommand As SqlCommand
        Dim blnSuccess As Boolean = False
        Try
            dbConn = New SqlConnection(strConnection)
            dbConn.Open()
            dbCommand = New SqlCommand(sqlexecute, dbConn)
            dbCommand.CommandTimeout = 1000
            dbCommand.ExecuteNonQuery()
            dbCommand.Connection.Close()
            blnSuccess = True
        Catch ex As Exception
        Finally
            dbConn.Close()
        End Try
        Return blnSuccess

    End Function

End Class