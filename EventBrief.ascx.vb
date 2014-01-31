Partial Class EventBrief
    Inherits System.Web.UI.UserControl

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

    Public Event Edit()

    Public Property DisplayEditButton() As Boolean
        Get
            DisplayEditButton = cmdEdit.Visible
        End Get
        Set(ByVal Value As Boolean)
            cmdEdit.Visible = Value
        End Set
    End Property

    Public Sub LoadEvent(ByRef conDB As OleDb.OleDbConnection, ByVal intEvent As Integer)
        Dim dtEvent As DataTable
        Dim drEvent As DataRow

        Try
            dtEvent = PenOCDB.GetTable_Event(conDB, PenOCDB.WhereEvent_idEvent(intEvent), "")
            If dtEvent.Rows.Count > 0 Then
                drEvent = dtEvent.Rows(0)
                lblDate.Text = drEvent("Date").ToString
                lblName.Text = drEvent("EventName").ToString
                lblVenue.Text = drEvent("Venue").ToString
                lblPlanner.Text = drEvent("Planner").ToString
                lblController.Text = drEvent("Controller").ToString
            End If
        Catch ex As Exception
        Finally
            If Not dtEvent Is Nothing Then
                dtEvent = Nothing
            End If
        End Try
    End Sub

    Private Sub cmdEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        RaiseEvent Edit()
    End Sub
End Class
