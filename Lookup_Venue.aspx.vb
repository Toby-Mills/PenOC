Partial Class Lookup_Venue
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private c_conDB As OleDb.OleDbConnection
    Private c_dtVenue As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            RefreshGrid()
        End If
    End Sub

    Private Sub RefreshGrid()
        c_dtVenue = GetTable_Venue(c_conDB, "")

        Me.grdVenue.DataSource = c_dtVenue
        Me.grdVenue.DataBind()
    End Sub

    Protected Sub cmdEdit_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itm As DataGridItem

        itm = sender.Parent.Parent

        grdVenue.EditItemIndex = itm.ItemIndex
        RefreshGrid()

    End Sub

    Protected Sub cmdUpdate_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itm As DataGridItem
        Dim strSQL As String

        Dim intVenue As Integer
        Dim strName As String

        Dim txtTextBox As TextBox
        Dim strMessage As String

        itm = sender.parent.parent
        intVenue = itm.Cells(2).Text
        txtTextBox = itm.Cells(3).Controls(0)
        strName = txtTextBox.Text
        
        If strMessage > "" Then
            WebMsgBox(Page, "Error", strMessage)
        Else
            strSQL = "UPDATE lutVenue SET " & _
                " strName = " & SQLFormat(strName) & _
                " WHERE (idVenue = " & intVenue & ")"

            ExecuteSQL(c_conDB, strSQL)
            Me.grdVenue.EditItemIndex = -1

            g_lookupManager.RefreshLookupTable(c_conDB, LookupManager.enumLookupTable.Venue)
            RefreshGrid()

        End If

    End Sub

    Protected Sub cmdCancel_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.grdVenue.EditItemIndex = -1
        RefreshGrid()
    End Sub

    Protected Sub cmdDelete_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itm As DataGridItem
        Dim intVenue As Integer

        itm = sender.Parent.Parent
        intVenue = CInt(itm.Cells(2).Text)
        OpenPopUp(Page, "confirmdelete.aspx?" & ConfirmDelete.DELETE_TYPE & "=" & ConfirmDelete.enumDeleteType.Venue & "&" & ConfirmDelete.OBJECT_ID & "=" & intVenue, "Delete Venue")

    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        If NewVenue(c_conDB, Me.txtName.Text) > 0 Then
            g_lookupManager.RefreshLookupTable(c_conDB, LookupManager.enumLookupTable.Venue)
            RefreshGrid()
        End If

    End Sub
End Class
