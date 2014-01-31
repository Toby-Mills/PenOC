Partial Class CompetitorEdit
	'    Inherits System.Web.UI.Page
	Inherits PageViewStateZip
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
    Protected WithEvents CompetitorListSimilar As CompetitorList

    Private c_conDB As OleDb.OleDbConnection

	'Session variables
	Public Const RETURN_URL = "ReturnUrl"
	Public Const AUTOCLOSE = "AutoClose"

	'URL varibales
	Public Const COMPETITOR_ID = "idCompetitor"
	Public Const FIRSTNAME = "FirstName"
	Public Const SURNAME = "Surname"
	Public Const GENDER_ID = "idGender"
	Public Const RETURNID_CONTROL = "ReturnIDControl"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim intCompetitor As Integer
		Dim intGender As Integer

        If Not Page.IsPostBack Then
            LogPageAccess(c_conDB, Page)
            LogPageAccess(c_conDB, Page)
            If Session.Item("style") > "" Then
                Me.lnkStylesheet.Attributes.Add("href", Session.Item("style"))
            Else
                Me.lnkStylesheet.Attributes.Add("href", "styles.css")
            End If
            Me.txtID.Width = New System.Web.UI.WebControls.Unit(0)
            Me.txtReturnIDControl.Width = New System.Web.UI.WebControls.Unit(0)
            PopulateControls()
            With Me.CompetitorListSimilar
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Category) = True
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Gender) = True

                .DisplayColumn(CompetitorList.enumCompetiorListColumn.BirthDate) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Email) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.FirstName) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.FullNameLink) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Position) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Surname) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Telephone1) = False
                .DisplayColumn(CompetitorList.enumCompetiorListColumn.Telephone2) = False
            End With

            CalendarPopup.ShowCalendar(Me.cmdCalBirthDate, Me.txtBirthDate, g_strDateFormat, False)
            intCompetitor = Request.Item(COMPETITOR_ID)
            If intCompetitor > 0 Then
                LoadCompetitor(intCompetitor)
            Else
                txtFirstName.Text = Request.Item(FIRSTNAME)
                txtSurname.Text = Request.Item(SURNAME)
                intGender = Request.Item(GENDER_ID)
                If intGender > 0 Then
                    Me.rblGender.SelectedValue = intGender
                End If
            End If
            Me.CompetitorListSimilar.PageSize = -1

            txtReturnIDControl.Text = Request.Item(RETURNID_CONTROL)

            EnableEditing(True)

        End If
        ShowHourGlass(Page)
    End Sub

    Private Sub PopulateControls()
        Dim dtGender As DataTable

        g_lookupManager.PopulateWebComboBox(c_conDB, Me.cmbCategory, LookupManager.enumLookupTable.Category, False)

        g_lookupManager.PopulateWebRadioButtonList(c_conDB, Me.rblGender, LookupManager.enumLookupTable.Gender, False)
        Me.rblGender.SelectedValue = NULL_NUMBER

        Me.lblDateFormat.Text = "(" & g_strDateFormat & ")"

    End Sub

    Private Sub LoadCompetitor(ByVal intCompetitor As Integer)
        Dim dtCompetitor As DataTable
        Dim drCompetitor As DataRow
        Dim dtCourse As DataTable
        Dim strWHERE As String
        Dim strSurname As String
        Dim strFirstName As String
        Dim dteBirthDate As Date
        Dim intCategory As Integer
        Dim strTelephone1 As String
        Dim strTelephone2 As String
        Dim strEmail As String
        Dim intGender As Integer
        Dim strEmitNumber As Integer

		strWHERE = PenOCDB.WhereCompetitor_idCompetitor(intCompetitor)
        dtCompetitor = PenOCDB.GetTable_Competitor(c_conDB, strWHERE)
        If dtCompetitor.Rows.Count > 0 Then
            drCompetitor = dtCompetitor.Rows(0)
            With drCompetitor
                'Competitor ID
                Me.txtID.Text = .Item("idCompetitor")
                'Gender
                LoadDBValue(.Item("idGender"), intGender)
                Me.rblGender.SelectedValue = intGender
                'Surname
                Me.txtSurname.Text = .Item("Surname").ToString
                'FirstName
                Me.txtFirstName.Text = .Item("FirstName").ToString
                'Emit Number
                Me.txtEmitNumber.Text = .Item("EmitNumber").ToString
                'BirthDate
                LoadDBValue(.Item("BirthDate"), dteBirthDate)
                If Not dteBirthDate = Date.MinValue Then
                    Me.txtBirthDate.Text = dteBirthDate.ToString(g_strDateFormat)
                Else
                    Me.txtBirthDate.Text = ""
                End If
                'Category
                LoadDBValue(.Item("idCategory"), intCategory)
                If Not intCategory = NULL_NUMBER Then
                    Me.cmbCategory.SelectedValue = intCategory
                Else
                    Me.cmbCategory.ClearSelection()
                End If
                'Telephone1
                Me.txtTelephone1.Text = .Item("Telephone1").ToString
                'Telephone2
                Me.txtTelephone2.Text = .Item("Telephone2").ToString
                'Email
                Me.txtEmail.Text = .Item("Email").ToString
            End With
        End If

    End Sub

    Public Sub EnableEditing(ByVal blnEnable As Boolean)

        Me.txtSurname.Enabled = blnEnable
        Me.rblGender.Enabled = blnEnable
        Me.txtBirthDate.Enabled = blnEnable
        Me.cmdCalBirthDate.Enabled = blnEnable
        Me.cmbCategory.Enabled = blnEnable
        Me.txtTelephone1.Enabled = blnEnable
        Me.txtTelephone2.Enabled = blnEnable
        Me.txtEmail.Enabled = blnEnable

        If Me.rblGender.SelectedValue = enumGender.Group Then
            Me.txtFirstName.Enabled = False
        Else
            Me.txtFirstName.Enabled = blnEnable
        End If

        Me.cmdEdit.Enabled = Not blnEnable
        Me.cmdSave.Enabled = blnEnable
        Me.cmdCancel.Enabled = blnEnable

    End Sub

    Private Sub cmdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEdit.Click
        EnableEditing(True)
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim intCompetitor As Integer
        Dim blnSave As Boolean
        Dim dtSimilarCompetitors As DataTable

        If ValidateCompetitor() = True Then
            blnSave = True
            Me.pnlSimilarCompetitors.Visible = False

            If Me.rblGender.SelectedValue = enumGender.Group Then
                If Me.txtFirstName.Text > "" Then
                    WebMsgBox(Page, "Error", "A group may not have a 'First Name'")
                    blnSave = False
                End If
            End If

            If Me.txtID.Text = "" Then
                If blnSave Then
                    dtSimilarCompetitors = Me.SimilarCompetitors
                    If Not dtSimilarCompetitors.Rows.Count > 0 Then
                        intCompetitor = PenOCDB.NewCompetitor(c_conDB, Me.txtSurname.Text, Me.txtFirstName.Text)
                    Else
                        Me.CompetitorListSimilar.CompetitorsTable = dtSimilarCompetitors
                        Me.pnlSimilarCompetitors.Visible = True
                        blnSave = False
                    End If
                End If
            Else
                If CompetitorExists() Then
                    WebMsgBox(Page, "Warning", "A competitor with the name you entered already exists.")
                    blnSave = False
                End If
                intCompetitor = Me.txtID.Text
            End If

            If blnSave Then
                SaveCompetitor(intCompetitor)
                LoadCompetitor(intCompetitor)
                EnableEditing(False)
                CompletionAction()
            End If
        End If


    End Sub

    Private Function ValidateCompetitor() As Boolean
        Dim blnReturn As Boolean

        blnReturn = True
        Me.lblErrorMessage.Visible = False

        If Me.txtBirthDate.Text > "" Then
            Try
                Date.ParseExact(Me.txtBirthDate.Text, g_strDateFormat, Nothing)
            Catch ex As Exception
                Me.lblErrorMessage.Text = "Date of Birth format: " & g_strDateFormat
                blnReturn = False
            End Try
        End If

        If txtEmitNumber.Text > "" Then
            If Not IsNumeric(txtEmitNumber.Text) Then
                Me.lblErrorMessage.Text = "Emit Number must be numeric only"
                blnReturn = False
            End If
        End If

        If CompetitorExists() Then
            Me.lblErrorMessage.Text = "A competitor with the name you entered already exists."
            blnReturn = False
        End If

        If blnReturn = False Then
            Me.lblErrorMessage.Visible = True
        End If

        Return blnReturn

    End Function

    Private Sub SaveCompetitor(ByVal intCompetitor As Integer)
        Dim strSQL As String
        Dim dteBirthDate As Date
        Dim intCategory As Integer
        Dim intGender As Integer
        Dim intEmitNumber As Integer

        If IsDate(Me.txtBirthDate.Text) Then
            Try
                dteBirthDate = Date.ParseExact(Me.txtBirthDate.Text, g_strDateFormat, Nothing)
            Catch ex As Exception
                dteBirthDate = CDate(Me.txtBirthDate.Text)
            End Try
        Else
            dteBirthDate = Date.MinValue
        End If
        intCategory = CInt(Me.cmbCategory.SelectedValue)
        intGender = CInt(rblGender.SelectedValue)
        If IsNumeric(txtEmitNumber.Text) Then
            intEmitNumber = CInt(txtEmitNumber.Text)
        Else
            intEmitNumber = NULL_NUMBER
        End If

        strSQL = "UPDATE tblCompetitor SET " & _
            " strSurname = " & SQLFormat(Me.txtSurname.Text) & _
            ", strFirstName = " & SQLFormat(Me.txtFirstName.Text) & _
            ", dteBirthDate = " & SQLFormat(dteBirthDate) & _
            ", intEmitNumber = " & SQLFormat(intEmitNumber) & _
            ", intGender = " & SQLFormat(intGender) & _
            ", intCategory = " & SQLFormat(intCategory) & _
            ", strTelephone1 = " & SQLFormat(Me.txtTelephone1.Text) & _
            ", strTelephone2 = " & SQLFormat(Me.txtTelephone2.Text) & _
            ", strEmail = " & SQLFormat(Me.txtEmail.Text) & _
            " WHERE idCompetitor = " & intCompetitor

        ExecuteSQL(c_conDB, strSQL)

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Dim intCompetitor As Integer
        If Not Me.txtID.Text = "" Then
            intCompetitor = Me.txtID.Text
            LoadCompetitor(intCompetitor)
        End If
        EnableEditing(False)
        CompletionAction()
    End Sub

    Private Function SimilarCompetitors() As DataTable
        Dim strWHERE As String
        Dim dtReturn As DataTable

        strWHERE = PenOCDB.WhereCompetitor_NameSoundsLike(Me.txtSurname.Text, Me.txtFirstName.Text)
        dtReturn = PenOCDB.GetTable_Competitor(c_conDB, strWHERE)

        Return dtReturn

    End Function

    Private Function CompetitorExists() As Boolean
        Dim strWHERE As String
        Dim dtCompetitor As DataTable
        Dim blnReturn As Boolean

        blnReturn = False

        strWHERE = PenOCDB.WhereCompetitor_Surname(Me.txtSurname.Text)
        strWHERE = strWHERE & " AND " & PenOCDB.WhereCompetitor_FirstName(Me.txtFirstName.Text)
        If Me.txtID.Text > "" Then
            strWHERE = strWHERE & " AND NOT " & PenOCDB.WhereCompetitor_idCompetitor(Me.txtID.Text)
        End If

        dtCompetitor = PenOCDB.GetTable_Competitor(c_conDB, strWHERE)
        If dtCompetitor.Rows.Count > 0 Then
            blnReturn = True
        End If

        Return blnReturn

    End Function

    Private Sub cmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
        Dim intCompetitor As Integer

        intCompetitor = PenOCDB.NewCompetitor(c_conDB, Me.txtSurname.Text, Me.txtFirstName.Text)
        SaveCompetitor(intCompetitor)
        LoadCompetitor(intCompetitor)
        EnableEditing(False)
        CompletionAction()

    End Sub

    Private Sub cmdCancelNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelNew.Click
        Me.pnlSimilarCompetitors.Visible = False
    End Sub

    Private Sub CompletionAction()
        Dim strReturnURL As String
        Dim script(5) As String

        If Session.Item(RETURN_URL) > "" Then
            strReturnURL = Session.Item(RETURN_URL)
            Session.Remove(RETURN_URL)
            If strReturnURL = AUTOCLOSE Then
                CloseWebWindow(Page)
            Else
                Server.Transfer(strReturnURL)
            End If
        ElseIf Me.txtReturnIDControl.Text > "" Then
            'build the script
            script(1) = "<script>window.opener.document.forms[0]." + txtReturnIDControl.Text + ".value= '"
            script(2) = Me.txtID.Text & "'"
            script(3) = ";window.opener.document.forms[0].submit()"
            script(4) = ";self.close()"
            script(5) = "</" + "script>"

            'execute the script
            RegisterClientScriptBlock("test", Join(script, ""))
        Else
            Server.Transfer("AdminCompetitors.aspx")
        End If
    End Sub
End Class
