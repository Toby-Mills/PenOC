Partial Class Lookup_Club
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
    Private c_dtClub As DataTable

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            RefreshGrid()
        End If
    End Sub

    Private Sub RefreshGrid()
        c_dtClub = GetTable_Club(c_conDB, "")

        Me.grdClub.DataSource = c_dtClub
        Me.grdClub.DataBind()
    End Sub

    Protected Sub cmdEdit_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itm As DataGridItem

        itm = sender.Parent.Parent

        grdClub.EditItemIndex = itm.ItemIndex
        RefreshGrid()

    End Sub

    Protected Sub cmdUpdate_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itm As DataGridItem
        Dim strSQL As String

        Dim intClub As Integer
        Dim strShortName As String
        Dim strFullName As String

        Dim txtTextBox As TextBox
        Dim strMessage As String

        itm = sender.parent.parent
        intClub = itm.Cells(2).Text
        txtTextBox = itm.Cells(3).Controls(0)
        strFullName = txtTextBox.Text
        txtTextBox = itm.Cells(4).Controls(0)
        strShortName = txtTextBox.Text

        If strMessage > "" Then
            WebMsgBox(Page, "Error", strMessage)
        Else
            strSQL = "UPDATE lutClub SET " & _
                " strShortName = " & SQLFormat(strShortName) & _
                ", strFullName = " & SQLFormat(strFullName) & _
                " WHERE (idClub = " & intClub & ")"

            ExecuteSQL(c_conDB, strSQL)
            Me.grdClub.EditItemIndex = -1

            RefreshLookups()
            RefreshGrid()

        End If

    End Sub

    Private Sub RefreshLookups()

        g_lookupManager.RefreshLookupTable(c_conDB, LookupManager.enumLookupTable.Club_Code)
        g_lookupManager.RefreshLookupTable(c_conDB, LookupManager.enumLookupTable.ClubFullName)
        g_lookupManager.RefreshLookupTable(c_conDB, LookupManager.enumLookupTable.ClubShortName)

    End Sub
    Protected Sub cmdCancel_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.grdClub.EditItemIndex = -1
        RefreshGrid()
    End Sub

    Protected Sub cmdDelete_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itm As DataGridItem
        Dim intClub As Integer

        itm = sender.Parent.Parent
        intClub = CInt(itm.Cells(2).Text)
        OpenPopUp(Page, "confirmdelete.aspx?" & ConfirmDelete.DELETE_TYPE & "=" & ConfirmDelete.enumDeleteType.Club & "&" & ConfirmDelete.OBJECT_ID & "=" & intClub, "Delete Club")

    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        If NewClub(c_conDB, Me.txtFullName.Text, Me.txtShortName.Text) > 0 Then
            RefreshGrid()
        End If

    End Sub
End Class
