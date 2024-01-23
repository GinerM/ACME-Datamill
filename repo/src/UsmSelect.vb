Imports System.Windows.Forms
Imports System.Configuration
Imports System.Data.OleDb
Imports System.Text

Public Class UsmSelect

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    'Private Sub UsmSelect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    OK_Button.Enabled = False
    '    UsmListBox.MultiColumn = False
    '    ' Set the selection mode to one row
    '    UsmListBox.SelectionMode = SelectionMode.One


    '    Dim usm As String
    '    'Init Connection with connection string from app.config
    '    Dim Connection As New OleDb.OleDbConnection
    '    Connection.ConnectionString = ConfigurationManager.ConnectionStrings("Data_Arise").ConnectionString

    '    Try
    '        'on ouvre la connection
    '        'Open DB connection
    '        Connection.Open()
    '        'weather_site query
    '        Dim fetchAllQuery As String = "select * from st_usm;"

    '        'Init and use DataAdapter
    '        Using dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter(fetchAllQuery, Connection)

    '            ' Filling Dataset
    '            Dim dataSet As New DataSet()
    '            dataAdapter.Fill(dataSet, "st_usm")
    '            Dim dataTable As DataTable = dataSet.Tables("st_usm")

    '            ' Shutdown the painting of the ListBox as items are added.
    '            UsmListBox.BeginUpdate()

    '            'read all line of st_usm
    '            For Each row In dataTable.Rows
    '                usm = row.item("nom_usm")
    '                'add items to listbox
    '                UsmListBox.Items.Add(usm)
    '            Next

    '            ' Allow the ListBox to repaint and display the new items.
    '            UsmListBox.EndUpdate()

    '        End Using
    '        Connection.Close()

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try





    'End Sub


    'Private Sub UsmListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles UsmListBox.SelectedIndexChanged

    '    If (UsmListBox.SelectedItem IsNot Nothing) Then
    '        OK_Button.Enabled = True
    '    End If

    'End Sub
End Class
