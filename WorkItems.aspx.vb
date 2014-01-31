Partial Class WorkItems
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

    Private Enum enumWorkItemListColumn
        WorkItemID = 0
        Delete = 1
        Resolved = 2
        WorkItem = 3
    End Enum
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            PopulateWorkItems()
        End If
    End Sub

    Protected Sub cmdDelete_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itm As DataGridItem
        Dim intWorkItem As Integer

        itm = sender.Parent.Parent
        intWorkItem = CInt(itm.Cells(Me.enumWorkItemListColumn.WorkItemID).Text)

        deleteworkitem(c_conDB, intWorkItem)
        PopulateWorkItems()

    End Sub

    Private Sub PopulateWorkItems()
        Dim dtWorkItems As DataTable
        dtWorkItems = GetTable_WorkItem(c_condb, "")

        Me.grdWorkItem.DataSource = dtWorkItems
        Me.grdWorkItem.DataBind()

    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        AddWorkItem(c_conDB, Me.txtWorkItem.Text)
        PopulateWorkItems()
    End Sub


    Public Sub chkResolved_OnCheckChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dgiWorkItem As DataGridItem
        Dim CheckBox As CheckBox
        Dim intWorkItem As Integer
        Dim blnResolved As Boolean

        'get the datagird item
        dgiWorkItem = sender.parent.parent


        If Not dgiWorkItem Is Nothing Then
            'get the guid of the selected row
            intWorkItem = dgiWorkItem.Cells(enumWorkItemListColumn.WorkItemID).Text

            'get boolean to set status of rule
            'to either enabled or disabled
            CheckBox = dgiWorkItem.Cells(enumWorkItemListColumn.Resolved).Controls(1)

            blnResolved = CheckBox.Checked
            SetWorkItemResolved(c_conDB, intWorkItem, blnResolved)
            PopulateWorkItems()
        End If

    End Sub
End Class
