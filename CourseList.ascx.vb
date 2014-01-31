Partial Class CourseList
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
    Private c_dtCourses As DataTable
    Private c_conDB As OleDb.OleDbConnection
    Private c_intSelectedCourse As Integer
    Private c_blnHighlightSelected As Boolean

    Public Enum enumCourseListColumn
        EventID = 0
        CourseID = 1
        Delete = 2
        EventDate = 3
        EventName = 4
        Venue = 5
        CourseName = 6
		CourseNameLink = 7
		Length = 8
		Climb = 9
		Controls = 10
		Technical = 11
		Competitors = 12
		Winner = 13
		WinningTime = 14
        Log = 15
        Splits = 16
    End Enum

    Public Event CourseSelected()
    Public Event CourseDeleted()

    Public Property DisplayColumn(ByVal intColumn As enumCourseListColumn) As Boolean
        Get
            DisplayColumn = grdCourse.Columns(intColumn).Visible
        End Get
        Set(ByVal Value As Boolean)
            grdCourse.Columns(intColumn).Visible = Value
        End Set
    End Property

    Public Property Selectable() As Boolean
        Get
            Selectable = grdCourse.Columns(enumCourseListColumn.CourseNameLink).Visible
        End Get
        Set(ByVal Value As Boolean)
            grdCourse.Columns(Me.enumCourseListColumn.CourseNameLink).Visible = Value
            grdCourse.Columns(Me.enumCourseListColumn.CourseName).Visible = Not (Value)
        End Set
    End Property

    Public Property HighlightSelected() As Boolean
        Get
            HighlightSelected = c_blnHighlightSelected
        End Get
        Set(ByVal Value As Boolean)
            c_blnHighlightSelected = Value
        End Set
    End Property

    Public ReadOnly Property SelectedCourse() As Integer
        Get
            SelectedCourse = c_intSelectedCourse
        End Get
    End Property

    Public Property CourseTable() As DataTable
        Get
            CourseTable = c_dtCourses
        End Get
        Set(ByVal Value As DataTable)
            c_dtCourses = Value
            RefreshGrid()
        End Set
    End Property

    Public Property AllowPaging() As Boolean
        Get
            AllowPaging = Me.grdCourse.AllowPaging
        End Get
        Set(ByVal Value As Boolean)
            Me.grdCourse.AllowPaging = Value
        End Set
    End Property

    Public Property PageSize() As Integer
        Get
            PageSize = Me.grdCourse.PageSize
        End Get
        Set(ByVal Value As Integer)
            Me.grdCourse.PageSize = Value
        End Set
    End Property

    Public Function SelectCourse(ByVal intCourse As Integer)
        Dim itm As DataGridItem

        For Each itm In Me.grdCourse.Items
            If itm.Cells(Me.enumCourseListColumn.CourseID).Text = intCourse Then
                If Me.c_blnHighlightSelected Then
                    Me.grdCourse.SelectedIndex = itm.ItemIndex
                    RefreshGrid()
                    Exit For
                End If
            End If
        Next

    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then

        Else
            Me.c_dtCourses = Session.Item(Me.ID & "coursetable")
            c_blnHighlightSelected = Session.Item(Me.ID & "highlightselected")
            RefreshGrid()
        End If
    End Sub

    Public Function RefreshGrid()
        Me.grdCourse.DataSource = c_dtCourses
        Me.grdCourse.DataBind()
    End Function

    Private Sub grdCourse_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdCourse.PageIndexChanged
        Me.grdCourse.CurrentPageIndex = e.NewPageIndex
        RefreshGrid()
    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload
        Session.Item(Me.ID & "coursetable") = Me.c_dtCourses
        Session.Item(Me.ID & "highlightselected") = c_blnHighlightSelected
    End Sub

    Private Sub grdCourse_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdCourse.ItemCommand
        Dim intCourse As Integer

        Select Case e.CommandName
            Case "Select"
                intCourse = e.Item.Cells(enumCourseListColumn.CourseID).Text
                c_intSelectedCourse = intCourse
                If c_blnHighlightSelected Then
                    Me.grdCourse.SelectedIndex = e.Item.ItemIndex
                    RefreshGrid()
                End If
                RaiseEvent CourseSelected()
        End Select
    End Sub

    Protected Sub cmdDelete_OnClick(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim itm As DataGridItem
        Dim intCourse As Integer

        itm = sender.Parent.Parent
        intCourse = CInt(itm.Cells(enumCourseListColumn.CourseID).Text)

        Me.c_intSelectedCourse = intCourse
        RaiseEvent CourseDeleted()

    End Sub


End Class
