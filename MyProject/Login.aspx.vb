Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page
    Dim strConnection As String = ConfigurationManager.ConnectionStrings("SQLConn").ConnectionString


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If HttpContext.Current.User.Identity.IsAuthenticated Then
                Response.Redirect("Default.aspx")
            End If
        End If
    End Sub

    Private Sub Login1_Authenticate(sender As Object, e As AuthenticateEventArgs) Handles Login1.Authenticate
        Dim dtUser As DataTable = Query(" SELECT * FROM SETTING_USERS WHERE USERNAME = '" & Login1.UserName & "'").Tables(0)
        If dtUser.Rows.Count > 0 Then
            If dtUser.Rows(0).Item("PASSWORD") = Login1.Password Then
                FormsAuthentication.Initialize()
                e.Authenticated = True
            Else
                Login1.FailureText = "Password incorrect!"
            End If
        Else
            Login1.FailureText = "No user found"
        End If
    End Sub

    Private Function Query(ByVal SQLQuery As String) As DataSet
        Dim dbConn As SqlConnection = Nothing
        Dim dsQuery As New DataSet
        Dim dbCommand As SqlDataAdapter

        Try
            'Connect to SQL
            dbConn = New SqlConnection(strConnection)
            dbConn.Open()

            'Execute query
            dbCommand = New SqlDataAdapter(SQLQuery, dbConn)
            dbCommand.SelectCommand.CommandTimeout = 3000

            'Fill data
            dbCommand.Fill(dsQuery)

        Catch ex As Exception
            Throw ex
        Finally
            dbConn.Close()
        End Try
        Return dsQuery
    End Function

End Class