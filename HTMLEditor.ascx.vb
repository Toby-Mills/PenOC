Partial Class HTMLEditor
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

    Private c_strImageDirectory As String

    Public Property Text() As String
        Get
            Text = Me.txtText.Text
        End Get
        Set(ByVal Value As String)
            Me.txtText.Text = Value
        End Set
    End Property

    Public Property Enabled() As Boolean
        Get
            Enabled = Me.txtText.Enabled
        End Get
        Set(ByVal Value As Boolean)
            EnabledEditing(Value)
        End Set
    End Property

    Public Property ImageDirectory() As String
        Get
            ImageDirectory = c_strImageDirectory
        End Get
        Set(ByVal Value As String)
            c_strImageDirectory = Value
            cmdImage.Attributes("language") = "vbscript"
            cmdImage.Attributes.Add("OnClick", txtText.ClientID & ".value =  " & txtText.ClientID & ".value & ""<img src='""" & c_strImageDirectory & " & " & txtInput.ClientID & ".value & ""' />""" & vbCrLf & "window.event.returnValue = false")
        End Set
    End Property
    Private Sub EnabledEditing(ByVal blnEnable As Boolean)
        Me.txtText.Enabled = blnEnable
        Me.cmdBold.Enabled = blnEnable
        Me.cmdCenter.Enabled = blnEnable
        Me.cmdItalic.Enabled = blnEnable
        Me.cmdLineBreak.Enabled = blnEnable
        Me.cmdRule.Enabled = blnEnable
        Me.cmdUnderline.Enabled = blnEnable
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim txtText As TextBox
        Dim txtInput As TextBox

        If Not Page.IsPostBack Then


        txtText = Me.FindControl("txtText")
        txtInput = Me.FindControl("txtInput")

			Me.txtText.Attributes("Language") = "javascript"
			Me.txtText.Attributes.Add("OnClick", "storeCaret(this);")
			Me.txtText.Attributes.Add("OnKeyUp", "storeCaret(this);")
			Me.txtText.Attributes.Add("OnSelect", "storeCaret(this);")

			cmdBold.Attributes("language") = "javascript"
			cmdBold.Attributes.Add("OnClick", "insertAtCaret(" & Me.txtText.ClientID & ", '<B></B>');return false;")

			cmdItalic.Attributes("language") = "javascript"
			cmdItalic.Attributes.Add("OnClick", "insertAtCaret(" & Me.txtText.ClientID & ", '<I></I>');return false;")

			cmdUnderline.Attributes("language") = "javascript"
			cmdUnderline.Attributes.Add("OnClick", "insertAtCaret(" & Me.txtText.ClientID & ", '<U></U>');return false;")

			cmdRule.Attributes("language") = "javascript"
			cmdRule.Attributes.Add("OnClick", "insertAtCaret(" & Me.txtText.ClientID & ", '<HR />');return false;")

			cmdLineBreak.Attributes("language") = "javascript"
			cmdLineBreak.Attributes.Add("OnClick", "insertAtCaret(" & Me.txtText.ClientID & ", '<BR />');return false;")

			cmdImage.Attributes("language") = "javascript"
			cmdImage.Attributes.Add("OnClick", "insertAtCaret(" & Me.txtText.ClientID & ", '<img src=" & c_strImageDirectory & "/' + " & Me.txtInput.ClientID & ".value + ' />');return false;")

			cmdLink.Attributes("language") = "javascript"
			cmdLink.Attributes.Add("OnClick", "insertAtCaret(" & Me.txtText.ClientID & ", '<A href=' + " & Me.txtInput.ClientID & ".value + '></A>');return false;")

			cmdFile.Attributes("language") = "javascript"
			cmdFile.Attributes.Add("OnClick", "insertAtCaret(" & Me.txtText.ClientID & ", '<A href=File.aspx?idFile=' + " & Me.txtInput.ClientID & ".value + '></A>');return false;")

        Else

            c_strImageDirectory = Session.Item(Me.ID & "imagedirectory")

        End If
    End Sub

    Private Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Unload

        Session.Item(Me.ID & "imagedirectory") = c_strImageDirectory

    End Sub

End Class
