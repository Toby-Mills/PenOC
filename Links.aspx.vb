Public Partial Class Links
    Inherits System.Web.UI.Page

    Private c_conDB As OleDb.OleDbConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LogPageAccess(c_conDB, Page)
        If Session.Item("style") > "" Then
            Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
        Else
            Me.lnkStylesheet.Attributes.Add("href", "styles.css")
        End If
    End Sub

End Class