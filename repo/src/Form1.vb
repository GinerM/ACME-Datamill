'Copyright (c) 2021, CIRAD-AIDA
'Contributors : Michel GINER (michel.giner@cirad.fr) and François AFFHOLDER (francois.affholder@cirad.fr)
'All rights reserved.
'Redistribution and use in source and binary forms, with or without
'modification, are permitted provided that the following conditions are met:
'
'* Redistributions of source code must retain the above copyright
'  notice, this list of conditions and the following disclaimer.
'* Redistributions in binary form must reproduce the above copyright
'  notice, this list of conditions and the following disclaimer in the
'  documentation and/or other materials provided with the distribution.
'* Neither the name of the CIRAD nor the
'  names of its contributors may be used to endorse or promote products
'  derived from this software without specific prior written permission.
'
'THIS SOFTWARE IS PROVIDED BY THE REGENTS AND CONTRIBUTORS ``AS IS'' AND ANY
'EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
'WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
'DISCLAIMED. IN NO EVENT SHALL THE REGENTS AND CONTRIBUTORS BE LIABLE FOR ANY
'DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
'(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
'LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
'ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
'(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
'SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration
Public Module GlobalVariables
    Public storeNumMinSimu As Integer = 0
    Public storeNumMaxSimu As Integer = 0
    Public storeKeyDataN As Integer = 0 'variable containing value of column 'N' to read  
    Public RepSource As String = "D:\donneesFA\modelisation\Arise\dataMillArise\AppliDatamill"
    Public Connection As New OleDb.OleDbConnection
    'Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\ModelsDictionaryArise.accdb"
    Public MI_Connection As New OleDb.OleDbConnection
    ' MI_Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\MasterInput.accdb"
    Public FCChek As Boolean

End Module
Public Class Form1
    '---------------------------------------------------------------
    '                               init variable sarrah
    '---------------------------------------------------------------

    'error message button import  
    Dim errorImportMess As String = ""
    Dim errorExportMess As String = ""
    Dim importPathFile As String = ""
    Dim selectedRB As String = ""

    'error message button export
    'variable pour memoriser dossier pour export
    Dim exportFile As String = ""
    'init nombre de fichier dans un répertoire
    Dim nbfile As Integer = 0
    'variable booleene pour rendre le bouton export available
    Dim buttonDisp As Boolean = False
    'variable boolenne indiquant si le dossier selectionné est correct
    Dim folderSelected As Boolean = False
    'Dim errorMess As String = "Correct export Folder selected - Use Browse button to export"
    Dim errorMess As String = ""
    'usm name for STICS export
    Dim usm As String = ""



    'quit button
    Private Sub Btn_dtMill_quit_Click(sender As System.Object, e As System.EventArgs) Handles Btn_dtMill_quit.Click
        Close()
    End Sub
    '-------------------------------------------------------------------------------------------------------
    '------------                          export sarrah   
    '---------------------------------------------------------------------------------------------------------

    ''bouton parcourir répertoire pour sélectionner un dossier pour exporter les fichiers
    'Private Sub Btn_expSarrah_browse_Click(sender As System.Object, e As System.EventArgs)
    '    ' -------------------------------------------------------------------------

    '    '-----------------------initialize variable ------------------------------
    '    nbfile = 0
    '    buttonDisp = False
    '    folderSelected = False
    '    errorMess = ""

    '    'init result label
    '    msgErr_expSarrah_export.Visible = False

    '    Try
    '        ' On affiche le formulaire et on teste si l'utilisateur a bien sélectionné un dossier.
    '        ' L'utilisateur aura donc cliqué sur le bouton OK.
    '        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            ' Récupère le chemin complet du dossier sélectionné par l'utilisateur
    '            'Dim dossier_selectionner As String = FolderBrowserDialog1.SelectedPath

    '            ' Affiche le chemin complet du dossier sélectionné par l'utilisateur dans la case (TextBox)
    '            'exportFile = dossier_selectionner
    '            exportFile = FolderBrowserDialog1.SelectedPath
    '            TB_Sarrah_folder.Text = exportFile
    '            ' Affiche le nom du dossier (seulement) sélectionné, à l'utilisateur
    '            ' Petite subtilité, il faut utiliser la fonction "IO.Path.GetFileName" sur le chemin d'un dossier
    '            ' pour en récupérer le nom du dossier ciblé.
    '            ' Alors que "IO.Path.GetDirectoryName" vous aurait affiché le chemin du dossier CONTENANT le dossier
    '            ' ciblé par le chemin indiqué en paramètre
    '            'MsgBox("Selected Folder : " & IO.Path.GetFileName(dossier_selectionner))

    '            'controler que le répertoire choisi est vide
    '            'Dim nbfile As Integer = Directory.GetFiles(exportFile, "*.*", SearchOption.AllDirectories).Length
    '            nbfile = Directory.GetFiles(exportFile, "*.*", SearchOption.AllDirectories).Length
    '            'MessageBox.Show(nbfile & " fichiers présent")

    '            'folder selected
    '            folderSelected = True
    '            'no file : button browse is available
    '            If nbfile = 0 Then
    '                msgErr_expSarrah_browse.ForeColor = Color.Green
    '                buttonDisp = True
    '            End If
    '            ' Si l'utilisateur n'a pas sélectionné de dossier, on lui affiche un avertissement
    '        Else
    '            'delete textbox, indicateur boolean à false, color errormess : red
    '            TB_Sarrah_folder.Clear()
    '            buttonDisp = False
    '            folderSelected = False
    '            msgErr_expSarrah_browse.ForeColor = Color.Red
    '        End If
    '        'exception: button unavailable
    '    Catch ex As Exception
    '        buttonDisp = False
    '    End Try
    '    '----------------------------------------------------------------------------------
    '    '-----------                       error message
    '    'button unavailable: error message
    '    If buttonDisp = False Then
    '        errorMess = "Selected folder must be empty to export"
    '        msgErr_expSarrah_browse.ForeColor = Color.Red
    '        Btn_expSarrah_export.Enabled = False
    '    Else
    '        'button available
    '        'msgErr_expSarrah_browse.ForeColor = Color.Green
    '        Btn_expSarrah_export.Enabled = True
    '    End If
    '    'incorrect folder: error message
    '    If folderSelected = False Then
    '        msgErr_expSarrah_browse.ForeColor = Color.Red
    '        errorMess = "No correct folder selected"
    '    End If
    '    '--------------------------------------------------------------------------------------
    '    '----------------                error message     ------------------------------------
    '    'display message box
    '    msgErr_expSarrah_browse.Text = errorMess
    '    msgErr_expSarrah_browse.Visible = True

    'End Sub

    ''export files button in selected folder - available if correct selected folder
    'Private Sub Btn_expSarrah_export_Click(sender As System.Object, e As System.EventArgs)

    '    Try
    '        Dim converterFactory As ConverterFactory = New ConverterFactory()
    '        Dim converter As Converter = converterFactory.GetConverter("Sarrah", "Soil")
    '        converter.Export(FolderBrowserDialog1.SelectedPath)

    '        'completed export : export button unavailable, label3 : export completed
    '        Btn_expSarrah_export.Enabled = False
    '        msgErr_expSarrah_browse.Text = ""

    '        msgErr_expSarrah_export.Visible = True
    '        msgErr_expSarrah_export.ForeColor = Color.Green
    '        msgErr_expSarrah_export.Text = "Export completed"

    '    Catch ex As Exception
    '        msgErr_expSarrah_export.Visible = True
    '        msgErr_expSarrah_export.ForeColor = Color.Red
    '        msgErr_expSarrah_export.Text = "Export aborted"

    '    End Try

    'End Sub

    'changement onglet 
    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) _
     Handles TabControl1.SelectedIndexChanged

        'selected index is 1 : intialize sarrah
        'If TabControl1.SelectedIndex = 1 Then
        '    'textbox folder
        '    TB_Sarrah_folder.Text = ""
        '    'err message
        '    msgErr_expSarrah_browse.Visible = False
        '    msgErr_expSarrah_browse.Text = ""
        '    'err message
        '    msgErr_expSarrah_export.Visible = False
        '    msgErr_expSarrah_export.Text = ""
        '    'btn export
        '    Btn_expSarrah_export.Enabled = False
        'End If

        'selected index is 2 : dssat
        If TabControl1.SelectedIndex = 1 Then
            'textbox folder
            'TB_Dssat_folder.Text = ""
            'err message
            'msgErr_expDssat_browse.Visible = False
            'msgErr_expDssat_browse.Text = ""
            'err message
            msgErr_expDssat_export.Visible = True
            msgErr_expDssat_export.Text = ""
            'btn export
            Btn_expDssat_export.Enabled = True
            Btn_Concat_Dssat.Enabled = True
        End If

        'selected index is 3 : stics
        If TabControl1.SelectedIndex = 2 Then
            'textbox folder
            'TB_Stics_folder.Text = ""
            'err message
            'msgErr_expStics_browse.Visible = False
            'msgErr_expStics_browse.Text = ""
            'err message
            msgErr_expStics_export.Visible = True
            msgErr_expStics_export.Text = ""
            'btn export
            Btn_expStics_export.Enabled = True
            Btn_Concat_Stics.Enabled = True
        End If

        'selected index is 4 : apsim
        'If TabControl1.SelectedIndex = 3 Then
        '    'MessageBox.Show("APSIM UNDER CONSTRUCTION ")
        '    'If TabControl1.SelectedIndex = 3 Then
        '    'MessageBox.Show("STICS UNDER CONSTRUCTION ")
        '    'textbox folder
        '    'TB_Apsim_folder.Text = ""
        '    'err message
        '    'msgErr_expApsim_browse.Visible = False
        '    'msgErr_expApsim_browse.Text = ""
        '    'err message
        '    msgErr_expApsim_export.Visible = True
        '    msgErr_expApsim_export.Text = ""
        '    'btn export
        '    Btn_expApsim_export.Enabled = True
        '    'End If
        'End If
        If TabControl1.SelectedIndex = 3 Then
            'textbox folder
            'TB_Celsius_folder.Text = ""
            'err message
            msgErr_expCelsius_export.Visible = False
            msgErr_expCelsius_export.Text = ""
            'err message
            msgErr_expCelsius_export.Visible = True
            msgErr_expCelsius_export.Text = ""
            'btn export
            Btn_expCelsius_export.Enabled = True
            Btn_Concat_Celsius.Enabled = True
        End If

    End Sub

    'changement onglet import/export de Sarrah
    'Private Sub TabControl2_SelectedIndexChanged(sender As Object, e As EventArgs) _


    '    'onglet sarrah - export
    '    If TabControl2.SelectedIndex = 0 Then
    '        'textbox folder
    '        TB_Sarrah_folder.Text = ""
    '        'err message
    '        msgErr_expSarrah_browse.Visible = False
    '        msgErr_expSarrah_browse.Text = ""
    '        'err message
    '        msgErr_expSarrah_export.Visible = False
    '        msgErr_expSarrah_export.Text = ""
    '        'btn export
    '        Btn_expSarrah_export.Enabled = False
    '    End If

    '    'onglet sarrah import
    '    If TabControl2.SelectedIndex = 1 Then


    '        MessageBox.Show("IMPORT SARRAH UNDER CONSTRUCTION ")
    '        'intialize folder file
    '        TextBox2.Text = ""
    '        'intialize error message file
    '        msgErr_impSarrah_browse.Text = ""
    '        msgErr_impSarrah_browse.Visible = False
    '        'button import not available
    '        Btn_impSarrah_import.Enabled = False
    '        'initialise error message import
    '        msgErr_impSarrah_import.Text = ""
    '        msgErr_impSarrah_import.Visible = False
    '        'radio button not available
    '        uncheckRB()
    '        NotEnabledRB()
    '        Btn_impSarrah_import.Enabled = False

    '        'error message select file
    '        errorImportMess = ""
    '    End If

    'End Sub

    'changement onglet import/export de dssat
    'Private Sub TabControl3_SelectedIndexChanged(sender As Object, e As EventArgs) _


    '    'onglet dssat - export
    '    If TabControl3.SelectedIndex = 0 Then
    '        'textbox folder
    '        'TB_Dssat_folder.Text = ""
    '        'err message
    '        'msgErr_expDssat_browse.Visible = False
    '        'msgErr_expDssat_browse.Text = ""
    '        'err message
    '        msgErr_expDssat_export.Visible = False
    '        msgErr_expDssat_export.Text = ""
    '        'btn export
    '        Btn_expDssat_export.Enabled = False
    '    End If

    '    'onglet Dssat import
    '    If TabControl3.SelectedIndex = 1 Then
    '        MessageBox.Show("IMPORT DSSAT UNDER CONSTRUCTION ")
    '        '    'intialize folder file
    '        '    TextBox4.Text = ""
    '        '    'intialize error message file
    '        '    msgErr_impDssat_browse.Text = ""
    '        '    msgErr_impDssat_browse.Visible = False
    '        '    'button import not available
    '        '    Btn_impDssat_import.Enabled = False
    '        '    'initialise error message import
    '        '    msgErr_impDssat_import.Text = ""
    '        '    msgErr_impDssat_import.Visible = False
    '        '    'radio button not available
    '        '    uncheckRB()
    '        '    NotEnabledRB()
    '        '    Btn_impDssat_import.Enabled = False

    '        '    'error message select file
    '        '    errorImportMess = ""
    '    End If

    'End Sub

    'changement onglet stics import/Export
    '
    'Private Sub TabControl4_SelectedIndexChanged(sender As Object, e As EventArgs) _


    '    'onglet Stics - export
    '    If TabControl4.SelectedIndex = 0 Then
    '        'textbox folder
    '        'TB_Stics_folder.Text = ""
    '        'err message
    '        'msgErr_expStics_browse.Visible = False
    '        'msgErr_expStics_browse.Text = ""
    '        'err message
    '        msgErr_expStics_export.Visible = False
    '        msgErr_expStics_export.Text = ""
    '        'btn export
    '        Btn_expStics_export.Enabled = False
    '    End If

    '    'onglet Stics import
    '    If TabControl4.SelectedIndex = 1 Then
    '        MessageBox.Show("IMPORT STICS UNDER CONSTRUCTION ")
    '    End If
    '    '    'intialize folder file
    '    '    TextBox4.Text = ""
    '    '    'intialize error message file
    '    '    msgErr_impStics_browse.Text = ""
    '    '    msgErr_impStics_browse.Visible = False
    '    '    'button import not available
    '    '    Btn_impStics_import.Enabled = False
    '    '    'initialise error message import
    '    '    msgErr_impStics_import.Text = ""
    '    '    msgErr_impStics_import.Visible = False
    '    '    'radio button not available
    '    '    uncheckRB()
    '    '    NotEnabledRB()
    '    '    Btn_impStics_import.Enabled = False

    '    '    'error message select file
    '    '    errorImportMess = ""
    '    'End If

    'End Sub


    'changement onglet Apsim import/Export
    '
    'Private Sub TabControl5_SelectedIndexChanged(sender As Object, e As EventArgs) _


    '    'onglet Apsim - export
    '    If TabControl5.SelectedIndex = 0 Then
    '        'textbox folder
    '        'TB_Apsim_folder.Text = ""
    '        'err message
    '        'msgErr_expApsim_browse.Visible = False
    '        'msgErr_expApsim_browse.Text = ""
    '        'err message
    '        msgErr_expApsim_export.Visible = False
    '        msgErr_expApsim_export.Text = ""
    '        'btn export
    '        Btn_expApsim_export.Enabled = False
    '    End If

    '    'onglet Apsim import
    '    If TabControl5.SelectedIndex = 1 Then
    '        MessageBox.Show("IMPORT APSIM UNDER CONSTRUCTION ")
    '    End If
    '    '    'intialize folder file
    '    '    TextBox5.Text = ""
    '    '    'intialize error message file
    '    '    msgErr_impApsim_browse.Text = ""
    '    '    msgErr_impApsim_browse.Visible = False
    '    '    'button import not available
    '    '    Btn_impApsim_import.Enabled = False
    '    '    'initialise error message import
    '    '    msgErr_impApsim_import.Text = ""
    '    '    msgErr_impApsim_import.Visible = False
    '    '    'radio button not available
    '    '    uncheckRB()
    '    '    NotEnabledRB()
    '    '    Btn_impApsim_import.Enabled = False

    '    '    'error message select file
    '    '    errorImportMess = ""
    '    'End If

    'End Sub

    '--------------------------------------------------------------------------------------------------------------
    '                                    import sarrah
    '--------------------------------------------------------------------------------------------------------------

    'Private Sub Btn_impSarrah_browse_Click(sender As System.Object, e As System.EventArgs)
    '    'initialize error message
    '    errorImportMess = ""
    '    Dim selectFile As String = ""
    '    Dim Extension As String = ""
    '    'Extension attendu pour fichier à importer
    '    Dim txtExtension As String = ".txt"

    '    'radio button initialise
    '    uncheckRB()
    '    NotEnabledRB()
    '    Btn_impSarrah_import.Enabled = False

    '    'open file dialog
    '    fileToImport.ShowDialog()

    '    'If (fileToImport.FileName IsNot Nothing) Then
    '    If fileToImport.FileName = "" Then
    '        errorImportMess = "No file selected"
    '    End If

    '    If (fileToImport.FileName <> "") Then
    '        'import folder
    '        importPathFile = fileToImport.FileName

    '        'Dim JusteNom As String = System.IO.Path.GetFileNameWithoutExtension(OpenFileDialog1.FileName)
    '        selectFile = System.IO.Path.GetFileName(fileToImport.FileName)

    '        'Dim JusteDossier As String = System.IO.Path.GetDirectoryName(OpenFileDialog1.FileName)
    '        Extension = System.IO.Path.GetExtension(fileToImport.FileName)
    '        'Dim PosExt As String = NomExtension.IndexOf(".")
    '        'MessageBox.Show("file = " & JusteNom)
    '        'MessageBox.Show("file+ext = " & NomExtension)
    '        'MessageBox.Show("dossier = " & JusteDossier)
    '        'MessageBox.Show("pos ext = " & PosExt)
    '        'MessageBox.Show("extension = " & Extension)

    '        'ctrl extension fichier correcte
    '        If Extension <> txtExtension Then
    '            errorImportMess = "Incorrect file - Select another"
    '        End If

    '    End If
    '    'Dim rb As Boolean = ctrlRBchecked(RadioButton1, RadioButton2, RadioButton3, RadioButton4, RadioButton5, RadioButton6, RadioButton7, RadioButton8, RadioButton9)

    '    'alim textbox folder
    '    TextBox2.Text = selectFile

    '    'pas d erreur : radio bouton deviennent disponibles
    '    If errorImportMess = "" Then
    '        enabledRB()
    '    End If

    '    'error message browse
    '    msgErr_impSarrah_browse.Text = errorImportMess
    '    msgErr_impSarrah_browse.Visible = True
    'End Sub
    'Private Sub Btn_impSarrah_import_Click(sender As System.Object, e As System.EventArgs)
    '    'controler que le fichier choisi correspond au model
    '    'a ajouter

    '    Dim converterFactory As ConverterFactory = New ConverterFactory()
    '    Dim converter As Converter = converterFactory.GetConverter("Sarrah", "Soil")
    '    'converter.Import(FolderBrowserDialog1.SelectedPath, "sarrah")
    '    converter.Import(importPathFile, selectedRB)

    'End Sub
    'Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs)
    '    Btn_impSarrah_import.Enabled = True
    '    selectedRB = RadioButton1.Text
    'End Sub
    'Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs)
    '    Btn_impSarrah_import.Enabled = True
    '    selectedRB = RadioButton2.Text
    'End Sub
    'Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs)
    '    Btn_impSarrah_import.Enabled = True
    '    selectedRB = RadioButton3.Text
    'End Sub
    'Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs)
    '    Btn_impSarrah_import.Enabled = True
    '    selectedRB = RadioButton4.Text
    'End Sub
    'Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs)
    '    Btn_impSarrah_import.Enabled = True
    '    selectedRB = RadioButton5.Text
    'End Sub
    'Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs)
    '    Btn_impSarrah_import.Enabled = True
    '    selectedRB = RadioButton6.Text
    'End Sub
    'Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs)
    '    Btn_impSarrah_import.Enabled = True
    '    selectedRB = RadioButton7.Text
    'End Sub
    'Private Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs)
    '    Btn_impSarrah_import.Enabled = True
    '    selectedRB = RadioButton8.Text
    'End Sub
    'Private Sub RadioButton9_CheckedChanged(sender As Object, e As EventArgs)
    '    Btn_impSarrah_import.Enabled = True
    '    selectedRB = RadioButton9.Text
    'End Sub

    ''' <summary>
    ''' import function : all radio buttons to false
    ''' </summary>
    ''' <returns>nothing</returns>
    ''' <remarks></remarks>
    'Public Function uncheckRB()

    '    RadioButton1.Checked = False
    '    RadioButton2.Checked = False
    '    RadioButton3.Checked = False
    '    RadioButton4.Checked = False
    '    RadioButton5.Checked = False
    '    RadioButton6.Checked = False
    '    RadioButton7.Checked = False
    '    RadioButton8.Checked = False
    '    RadioButton9.Checked = False

    '    Return Nothing
    'End Function

    ''' <summary>
    ''' import function : all radio buttons to true
    ''' </summary>
    ''' <returns>nothing</returns>
    ''' <remarks></remarks>
    'Public Function enabledRB()
    '    RadioButton1.Enabled = True
    '    RadioButton2.Enabled = True
    '    RadioButton3.Enabled = True
    '    RadioButton4.Enabled = True
    '    RadioButton5.Enabled = True
    '    RadioButton6.Enabled = True
    '    RadioButton7.Enabled = True
    '    RadioButton8.Enabled = True
    '    RadioButton9.Enabled = True
    '    Return Nothing

    'End Function
    ''' <summary>
    ''' import function : all radio buttons enabled to false
    ''' </summary>
    ''' <returns>nothing</returns>
    ''' <remarks></remarks>
    'Public Function NotEnabledRB()
    '    RadioButton1.Enabled = False
    '    RadioButton2.Enabled = False
    '    RadioButton3.Enabled = False
    '    RadioButton4.Enabled = False
    '    RadioButton5.Enabled = False
    '    RadioButton6.Enabled = False
    '    RadioButton7.Enabled = False
    '    RadioButton8.Enabled = False
    '    RadioButton9.Enabled = False
    '    Return Nothing

    'End Function

    ' ------------------------------------------------------------------------------------------------
    ' ---------                               DSSAT

    ''Private Sub Btn_expDssat_browse_Click(sender As System.Object, e As System.EventArgs)

    ''    '-----------------------initialize variable ------------------------------
    ''    nbfile = 0
    ''    buttonDisp = False
    ''    folderSelected = False
    ''    errorMess = ""

    ''    'init result label
    ''    msgErr_expDssat_export.Visible = False

    ''    Try
    ''        ' On affiche le formulaire et on teste si l'utilisateur a bien sélectionné un dossier.
    ''        ' L'utilisateur aura donc cliqué sur le bouton OK.
    ''        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
    ''            ' Récupère le chemin complet du dossier sélectionné par l'utilisateur
    ''            'Dim dossier_selectionner As String = FolderBrowserDialog1.SelectedPath

    ''            ' Affiche le chemin complet du dossier sélectionné par l'utilisateur dans la case (TextBox)
    ''            'exportFile = dossier_selectionner
    ''            exportFile = FolderBrowserDialog1.SelectedPath
    ''            TB_Dssat_folder.Text = exportFile
    ''            ' Affiche le nom du dossier (seulement) sélectionné, à l'utilisateur
    ''            ' Petite subtilité, il faut utiliser la fonction "IO.Path.GetFileName" sur le chemin d'un dossier
    ''            ' pour en récupérer le nom du dossier ciblé.
    ''            ' Alors que "IO.Path.GetDirectoryName" vous aurait affiché le chemin du dossier CONTENANT le dossier
    ''            ' ciblé par le chemin indiqué en paramètre
    ''            'MsgBox("Selected Folder : " & IO.Path.GetFileName(dossier_selectionner))

    ''            'controler que le répertoire choisi est vide
    ''            'Dim nbfile As Integer = Directory.GetFiles(exportFile, "*.*", SearchOption.AllDirectories).Length
    ''            nbfile = Directory.GetFiles(exportFile, "*.*", SearchOption.AllDirectories).Length
    ''            'MessageBox.Show(nbfile & " fichiers présent")

    ''            'folder selected
    ''            folderSelected = True
    ''            'no file : button browse is available
    ''            If nbfile = 0 Then
    ''                msgErr_expDssat_browse.ForeColor = Color.Green
    ''                buttonDisp = True
    ''            End If
    ''            ' Si l'utilisateur n'a pas sélectionné de dossier, on lui affiche un avertissement
    ''        Else
    ''            'delete textbox, indicateur boolean à false, color errormess : red
    ''            TB_Dssat_folder.Clear()
    ''            buttonDisp = False
    ''            folderSelected = False
    ''            msgErr_expDssat_browse.ForeColor = Color.Red
    ''        End If
    ''        'exception: button unavailable
    ''    Catch ex As Exception
    ''        buttonDisp = False
    ''    End Try
    ''    '----------------------------------------------------------------------------------
    ''    '-----------                       error message
    ''    'button unavailable: error message
    ''    If buttonDisp = False Then
    ''        errorMess = "Selected folder must be empty to export"
    ''        msgErr_expDssat_browse.ForeColor = Color.Red
    ''        Btn_expDssat_export.Enabled = False
    ''    Else
    ''        'button available
    ''        'msgErr_expSarrah_browse.ForeColor = Color.Green
    ''        Btn_expDssat_export.Enabled = True
    ''    End If
    ''    'incorrect folder: error message
    ''    If folderSelected = False Then
    ''        msgErr_expDssat_browse.ForeColor = Color.Red
    ''        errorMess = "No correct folder selected"
    ''    End If
    ''    '--------------------------------------------------------------------------------------
    ''    '----------------                error message     ------------------------------------
    ''    'display message box
    ''    'msgErr_expDssat_browse.Text = errorMess
    ''    'msgErr_expDssat_browse.Visible = True
    ''End Sub

    Private Sub Btn_expDssat_export_Click(sender As System.Object, e As System.EventArgs) Handles Btn_expDssat_export.Click

        Try
            Dim converterFactory As ConverterFactory = New ConverterFactory()
            Dim converter As Converter = converterFactory.GetConverter("DSSAT", "Weather")

            converter.Export(RepSource & "\Dssat", "", Connection, MI_Connection)

            'completed export : export button unavailable, label3 : export completed
            Btn_expDssat_export.Enabled = True
            'msgErr_expDssat_browse.Text = ""

            msgErr_expDssat_export.Visible = True
            msgErr_expDssat_export.ForeColor = Color.Green
            msgErr_expDssat_export.Text = "Export completed"

        Catch ex As Exception
            msgErr_expDssat_export.Visible = True
            msgErr_expDssat_export.ForeColor = Color.Red
            msgErr_expDssat_export.Text = "Export aborted"

        End Try
    End Sub
    '----------------------------------------------------------------------------------------
    '--------------                             onglet STICS
    '----------------------------------------------------------------------------------------

    'Private Sub Btn_expStics_browse_Click(sender As System.Object, e As System.EventArgs)
    '    '-----------------------initialize variable ------------------------------
    '    nbfile = 0
    '    buttonDisp = False
    '    folderSelected = False
    '    errorMess = ""
    '    Dim usmSelecter As New UsmSelect
    '    Dim usmSelected As Boolean = True


    '    'init result label
    '    msgErr_expStics_export.Visible = False

    '    Try
    '        ' On affiche le formulaire et on teste si l'utilisateur a bien sélectionné un dossier.
    '        ' L'utilisateur aura donc cliqué sur le bouton OK.
    '        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            ' Récupère le chemin complet du dossier sélectionné par l'utilisateur
    '            'Dim dossier_selectionner As String = FolderBrowserDialog1.SelectedPath

    '            ' Affiche le chemin complet du dossier sélectionné par l'utilisateur dans la case (TextBox)
    '            'exportFile = dossier_selectionner
    '            exportFile = FolderBrowserDialog1.SelectedPath
    '            'TB_Stics_folder.Text = exportFile
    '            ' Affiche le nom du dossier (seulement) sélectionné, à l'utilisateur
    '            ' Petite subtilité, il faut utiliser la fonction "IO.Path.GetFileName" sur le chemin d'un dossier
    '            ' pour en récupérer le nom du dossier ciblé.
    '            ' Alors que "IO.Path.GetDirectoryName" vous aurait affiché le chemin du dossier CONTENANT le dossier
    '            ' ciblé par le chemin indiqué en paramètre

    '            'folder selected
    '            folderSelected = True

    '            'Ask user to select usm
    '            usmSelecter.ShowDialog()

    '            'if an usm has been choosen
    '            If (usmSelecter.DialogResult = DialogResult.OK) Then

    '                usmSelected = True
    '                usm = usmSelecter.UsmListBox.SelectedItem.ToString()

    '                exportFile = exportFile + "\" + usm
    '                'TB_Stics_folder.Text = exportFile

    '                ' Determine whether the directory exists.
    '                If Directory.Exists(exportFile) Then
    '                    nbfile = Directory.GetFiles(exportFile, "*.*", SearchOption.AllDirectories).Length

    '                    If nbfile = 0 Then
    '                        'msgErr_expStics_browse.ForeColor = Color.Green
    '                        buttonDisp = True
    '                    End If
    '                Else
    '                    Directory.CreateDirectory(exportFile)
    '                    msgErr_expStics_browse.ForeColor = Color.Green
    '                    buttonDisp = True
    '                End If
    '            Else
    '                usmSelected = False
    '            End If
    '            ' Si l'utilisateur n'a pas sélectionné de dossier, on lui affiche un avertissement
    '        Else
    '            'delete textbox, indicateur boolean à false, color errormess : red
    '            TB_Stics_folder.Clear()
    '            buttonDisp = False
    '            folderSelected = False
    '            msgErr_expStics_browse.ForeColor = Color.Red
    '        End If
    '        'exception: button unavailable
    '    Catch ex As Exception
    '        buttonDisp = False
    '    End Try
    '    '----------------------------------------------------------------------------------
    '    '-----------                       error message
    '    'button unavailable: error message
    '    If buttonDisp = False Then
    '        errorMess = "Folder " + Chr(34) + exportFile + Chr(34) + " must be empty to export"
    '        msgErr_expStics_browse.ForeColor = Color.Red
    '        Btn_expStics_export.Enabled = False
    '    Else
    '        'button available
    '        'msgErr_expSarrah_browse.ForeColor = Color.Green
    '        Btn_expStics_export.Enabled = True
    '    End If

    '    'incorrect folder: error message
    '    If folderSelected = False Then
    '        msgErr_expStics_browse.ForeColor = Color.Red
    '        errorMess = "No correct folder selected"
    '    End If

    '    'incorrect usm: error message
    '    If usmSelected = False Then
    '        msgErr_expStics_browse.ForeColor = Color.Red
    '        errorMess = "No usm selected"
    '    End If
    '    '--------------------------------------------------------------------------------------
    '    '----------------                error message     ------------------------------------
    '    'display message box
    '    msgErr_expStics_browse.Text = errorMess
    '    msgErr_expStics_browse.Visible = True
    'End Sub

    Private Sub Btn_expStics_export_Click(sender As System.Object, e As System.EventArgs) Handles Btn_expStics_export.Click
        Try
            Dim converterFactory As ConverterFactory = New ConverterFactory()
            Dim converter As Converter = converterFactory.GetConverter("STICS", "Soil")
            converter.setUsm(usm)
            converter.Export(RepSource & "\Stics", "", Connection, MI_Connection)

            'completed export : export button unavailable, label3 : export completed
            Btn_expStics_export.Enabled = False
            'msgErr_expStics_browse.Text = ""

            msgErr_expStics_export.Visible = True
            msgErr_expStics_export.ForeColor = Color.Green
            msgErr_expStics_export.Text = "Export completed"

        Catch ex As Exception
            msgErr_expStics_export.Visible = True
            msgErr_expStics_export.ForeColor = Color.Red
            msgErr_expStics_export.Text = "Export aborted"

        End Try
        'End If



    End Sub
    '----------------------------------------------------------------------------------------
    '--------------                             onglet APSIM
    '----------------------------------------------------------------------------------------
    'Private Sub Btn_expApsim_browse_Click(sender As System.Object, e As System.EventArgs)
    '    nbfile = 0
    '    buttonDisp = False
    '    folderSelected = False
    '    errorMess = ""

    '    'init result label
    '    msgErr_expApsim_export.Visible = False

    '    Try
    '        ' On affiche le formulaire et on teste si l'utilisateur a bien sélectionné un dossier.
    '        ' L'utilisateur aura donc cliqué sur le bouton OK.
    '        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            ' Récupère le chemin complet du dossier sélectionné par l'utilisateur
    '            'Dim dossier_selectionner As String = FolderBrowserDialog1.SelectedPath

    '            ' Affiche le chemin complet du dossier sélectionné par l'utilisateur dans la case (TextBox)
    '            'exportFile = dossier_selectionner
    '            exportFile = FolderBrowserDialog1.SelectedPath
    '            TB_Apsim_folder.Text = exportFile
    '            ' Affiche le nom du dossier (seulement) sélectionné, à l'utilisateur
    '            ' Petite subtilité, il faut utiliser la fonction "IO.Path.GetFileName" sur le chemin d'un dossier
    '            ' pour en récupérer le nom du dossier ciblé.
    '            ' Alors que "IO.Path.GetDirectoryName" vous aurait affiché le chemin du dossier CONTENANT le dossier
    '            ' ciblé par le chemin indiqué en paramètre
    '            'MsgBox("Selected Folder : " & IO.Path.GetFileName(dossier_selectionner))

    '            'controler que le répertoire choisi est vide
    '            'Dim nbfile As Integer = Directory.GetFiles(exportFile, "*.*", SearchOption.AllDirectories).Length
    '            nbfile = Directory.GetFiles(exportFile, "*.*", SearchOption.AllDirectories).Length
    '            'MessageBox.Show(nbfile & " fichiers présent")

    '            'folder selected
    '            folderSelected = True
    '            'no file : button browse is available
    '            If nbfile = 0 Then
    '                msgErr_expApsim_browse.ForeColor = Color.Green
    '                buttonDisp = True
    '            End If
    '            ' Si l'utilisateur n'a pas sélectionné de dossier, on lui affiche un avertissement
    '        Else
    '            'delete textbox, indicateur boolean à false, color errormess : red
    '            TB_Apsim_folder.Clear()
    '            buttonDisp = False
    '            folderSelected = False
    '            msgErr_expApsim_browse.ForeColor = Color.Red
    '        End If
    '        'exception: button unavailable
    '    Catch ex As Exception
    '        buttonDisp = False
    '    End Try
    '    '----------------------------------------------------------------------------------
    '    '-----------                       error message
    '    'button unavailable: error message
    '    If buttonDisp = False Then
    '        errorMess = "Selected folder must be empty to export"
    '        msgErr_expApsim_browse.ForeColor = Color.Red
    '        Btn_expApsim_export.Enabled = False
    '    Else
    '        'button available
    '        'msgErr_expSarrah_browse.ForeColor = Color.Green
    '        Btn_expApsim_export.Enabled = True
    '    End If
    '    'incorrect folder: error message
    '    If folderSelected = False Then
    '        msgErr_expApsim_browse.ForeColor = Color.Red
    '        errorMess = "No correct folder selected"
    '    End If
    '    '--------------------------------------------------------------------------------------
    '    '----------------                error message     ------------------------------------
    '    'display message box
    '    msgErr_expApsim_browse.Text = errorMess
    '    msgErr_expApsim_browse.Visible = True
    'End Sub

    'Private Sub Btn_expApsim_export_Click(sender As System.Object, e As System.EventArgs)
    '    Try
    '        Dim converterFactory As ConverterFactory = New ConverterFactory()
    '        Dim converter As Converter = converterFactory.GetConverter("APSIM", "Soil")
    '        converter.Export(FolderBrowserDialog1.SelectedPath)

    '        'completed export : export button unavailable, label3 : export completed
    '        Btn_expApsim_export.Enabled = False
    '        'msgErr_expApsim_browse.Text = ""

    '        msgErr_expApsim_export.Visible = True
    '        msgErr_expApsim_export.ForeColor = Color.Green
    '        msgErr_expApsim_export.Text = "Export completed"

    '    Catch ex As Exception
    '        msgErr_expApsim_export.Visible = True
    '        msgErr_expApsim_export.ForeColor = Color.Red
    '        msgErr_expApsim_export.Text = "Export aborted"

    '    End Try
    'End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Dim St() As String

        Dim Tb, Ch As String
        'Dim St() As String
        Dim i, j As Integer
        Dim restrictions(3) As String
        Dim UT, Ut2 As DataTable
        Dim XTS_insert As OleDb.OleDbCommand
        'OFD1.InitialDirectory = Application.StartupPath
        'OFD1.DefaultExt = "*.mdb"
        'OFD1.Filter = "Bases de données (*.mdb)|*.*db|Tous (*.*)|*.*"
        'OFD1.Multiselect = True
        'If OFD1.ShowDialog() = DialogResult.Cancel Then Exit Sub
        'N = OFD1.FileNames.GetValue(0)
        'N = Mid(N, 1, InStrRev(N, "\"))
        'MsgBox(N)
        'MI_Connection = New OleDb.OleDbConnection()
        'MI_Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & N & "Data_Arise.accdb"
        'GC2.ConnectionString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source=" & N & "Tacsy.mdb"
        Dim Connection As New OleDb.OleDbConnection
        Connection.ConnectionString = ConfigurationManager.ConnectionStrings("Data_Arise").ConnectionString
        Connection.Open()
        Try
            restrictions(3) = "TABLE"
            UT = Connection.GetSchema("Tables", restrictions)
            'UT = GC2.GetSchema("Columns") ', restrictions)
            'DG.DataSource = UT
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'restrictions = New String()
        For i = 0 To UT.Rows.Count - 1
            If UT.Rows(i)(3).ToString() = "TABLE" Then
                'DG.DataSource = Nothing
                Tb = UT.Rows(i)(2).ToString()
                restrictions(3) = Nothing
                restrictions(2) = Tb 'Tb
                If Mid(Tb, 1, 1) <> "~" Then
                    Try
                        Ut2 = Connection.GetSchema("Columns", restrictions)
                        'DG.DataSource = Ut2
                        For j = 0 To Ut2.Rows.Count - 1
                            Ch = "Insert Into [XTS] (Tacsy_T , Tacsy_C ,Tacsy_D) Values ('" & Replace(Ut2.Rows(j)(2), "'", " ") & "','" & Replace(Ut2.Rows(j)(3), "'", " ") & "','" & Replace(Ut2.Rows(j)(27).ToString, "'", " ") & "')"
                            XTS_insert = New OleDb.OleDbCommand("", Connection)
                            XTS_insert.CommandText = Ch ' "Delete * From [" & Tb & "]"
                            '        Try
                            XTS_insert.ExecuteNonQuery()
                        Next
                    Catch ex As Exception
                        MsgBox(Tb & " " & ex.Message)
                    End Try
                End If
            End If
        Next

        MsgBox("Fini")

    End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    Private Sub Btn_expCelsius_export_Click(sender As System.Object, e As System.EventArgs) Handles Btn_expCelsius_export.Click
        Try
            Dim converterFactory As ConverterFactory = New ConverterFactory()
            Dim converter As Converter = converterFactory.GetConverter("Celsius", "")
            converter.setUsm(usm)
            converter.Export(RepSource & "\Celsius", "", Connection, MI_Connection)

            'completed export : export button unavailable, label3 : export completed
            Btn_expCelsius_export.Enabled = True
            msgErr_expCelsius_export.Text = ""

            msgErr_expCelsius_export.Visible = True
            msgErr_expCelsius_export.ForeColor = Color.Green
            msgErr_expCelsius_export.Text = "Export completed"

        Catch ex As Exception
            msgErr_expCelsius_export.Visible = True
            msgErr_expCelsius_export.ForeColor = Color.Red
            msgErr_expCelsius_export.Text = "Export aborted"

        End Try
        'End If


    End Sub

    Private Sub Btn_Concat_Stics_Click(sender As Object, e As EventArgs) Handles Btn_Concat_Stics.Click

        Dim converterFactory As ConverterFactory = New ConverterFactory()
        Dim converter As Converter = converterFactory.GetConverter("STICS", "Soil")
        'converter.setUsm(usm)
        Dim Directorypath As String = RepSource & "\Stics"
        'Dim Connection As New OleDb.OleDbConnection
        'Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\Data_Arise.accdb"
        Dim MI_Connection = New OleDb.OleDbConnection()
        MI_Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\MasterInput.accdb"

        Dim fileC1 As StringBuilder = New StringBuilder()
        If IO.File.Exists(Directorypath + "\" + "Concat_Stics.txt") Then IO.File.Delete(Directorypath + "\" + "Concat_Stics.txt")
        Try
            'on ouvre la connection
            'Connection.Open()
            MI_Connection.Open()
        Catch ex As Exception
            MessageBox.Show("Connection Error")
        End Try
        'weather_site query
        'Dim Q1 As String = "select * from Arise_Simul Where Model='STICS';"
        Dim Q1 As String = "select * from SimUnitList;"

        'Init and use DataAdapter
        Dim DASL As OleDb.OleDbDataAdapter = New OleDbDataAdapter(Q1, MI_Connection)
        Dim DS As New DataSet()
        DASL.Fill(DS, "Stics_SL")
        Dim DT As DataTable = DS.Tables("Stics_SL")
        Dim sr As IO.StreamReader
        Dim Lsplt As String()
        Dim IL As Integer
        Dim Sql As String
        Dim Cmd As OleDbCommand = New OleDbCommand("", MI_Connection)
        Cmd.CommandText = "Delete * from SummaryOutput where Model='Stics'"
        Cmd.ExecuteNonQuery()
        For Each row1 In DT.Rows
            Try
                msgErr_expStics_export.Text = row1.item("idsim")
                msgErr_expStics_export.Refresh()
                Dim Ligne2 As String
                Try
                    sr = New IO.StreamReader(Directorypath & "\" & row1.item("idsim").ToString & "\mod_rapport.sti")
                    Ligne2 = sr.ReadLine
                    Ligne2 = sr.ReadLine
                    Do
                        IL = Ligne2.Length
                        Ligne2 = Ligne2.Replace("  ", " ")
                    Loop Until IL = Ligne2.Length
                    Lsplt = Ligne2.Split(" ")
                    'MsgBox(Lsplt.Length)
                    'MsgBox(Lsplt(21))
                    'MsgBox(Lsplt(22))

                    Ligne2 = ""
                    Sql = "Insert into SummaryOutput (Model,Idsim,Texte,Planting,Emergence,Ant,Mat,Biom_ma,Yield,GNumber,MaxLai,Nleac,SoilN,CroN_ma,CumE,Transp) values ('Stics','" & row1.item("idsim") & "','" & Ligne2 & "','" & Lsplt(14) & "','" & Lsplt(15) & "','" & Lsplt(16) & "','" & Lsplt(17) & "','" & Lsplt(11) & "','" & Lsplt(12) & "','" & Lsplt(13) & "','" & Lsplt(19) & "','" & Lsplt(21) & "','" & Lsplt(22) & "','" & Lsplt(20) & "','" & Lsplt(23) & "','" & Lsplt(24) & "')"
                    Cmd.CommandText = Sql
                    Cmd.ExecuteNonQuery()
                Catch ex As Exception
                    'MsgBox(ex.Message)
                    Ligne2 = "No file mod_rapport "
                    Sql = "Insert into SummaryOutput (Model,Idsim,Texte) values ('Stics','" & row1.item("idsim") & "','" & Ligne2 & "')"
                    Cmd.CommandText = Sql
                    Cmd.ExecuteNonQuery()
                End Try

                'completed export : export button unavailable, label3 : export completed
                Btn_expStics_export.Enabled = False
                'msgErr_expStics_browse.Text = ""

                msgErr_expStics_export.Visible = True
                msgErr_expStics_export.ForeColor = Color.Green
                msgErr_expStics_export.Text = "Export completed"

            Catch ex As Exception
                msgErr_expStics_export.Visible = True
                msgErr_expStics_export.ForeColor = Color.Red
                msgErr_expStics_export.Text = "Export aborted"
                Sql = "Insert into SummaryOutput (Model,Idsim,Texte) values ('Stics','" & row1.item("idsim") & "','Possibly corrupted file')"
                Cmd.CommandText = Sql
                Cmd.ExecuteNonQuery()

            End Try
        Next
        'End If
    End Sub

    Private Sub Btn_Concat_Dssat_Click(sender As Object, e As EventArgs) Handles Btn_Concat_Dssat.Click

        Dim converterFactory As ConverterFactory = New ConverterFactory()
        Dim converter As Converter = converterFactory.GetConverter("STICS", "Soil")
        converter.setUsm(usm)
        Dim Directorypath As String = RepSource & "\Dssat"
        'Dim Connection As New OleDb.OleDbConnection
        'Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\Data_Arise.accdb"
        Dim MI_Connection = New OleDb.OleDbConnection()
        MI_Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\MasterInput.accdb"
        'Dim IL As Integer
        Dim fileC1 As StringBuilder = New StringBuilder()
        If IO.File.Exists(Directorypath + "\" + "Concat_Dssat.txt") Then IO.File.Delete(Directorypath + "\" + "Concat_Dssat.txt")
        Try
            'on ouvre la connection
            'Connection.Open()
            MI_Connection.Open()
        Catch ex As Exception
            MessageBox.Show("Connection Error")
        End Try
        'weather_site query
        'Dim Q1 As String = "select * from Arise_Simul Where Model='STICS';"
        Dim Q1 As String = "select * from SimUnitList;"

        'Init and use DataAdapter
        Dim DASL As OleDb.OleDbDataAdapter = New OleDbDataAdapter(Q1, MI_Connection)
        Dim DS As New DataSet()
        DASL.Fill(DS, "Stics_SL")
        Dim DT As DataTable = DS.Tables("Stics_SL")
        Dim sr As IO.StreamReader
        Dim Ligne2 As String
        Dim Lsplt As String()
        Dim IL, An, Bissext As Integer
        Dim Sql As String
        Dim Cmd As OleDbCommand = New OleDbCommand("", MI_Connection)
        Cmd.CommandText = "Delete * from SummaryOutput where Model='Dssat'"
        Cmd.ExecuteNonQuery()

        For Each row1 In DT.Rows
            Try
                msgErr_expDssat_export.Text = row1.item("idsim")
                An = CInt(row1("StartYear"))
                If An Mod 4 = 0 Then
                    Bissext = 1
                Else
                    Bissext = 0
                End If
                msgErr_expDssat_export.Refresh()
                Try
                    sr = New IO.StreamReader(Directorypath & "\" & row1.item("idsim").ToString & "\summary.out")
                    'Ligne2 = sr.ReadLine
                    Do
                        Ligne2 = sr.ReadLine
                        'Ligne2 = Ligne2.Replace("  ", " ")
                    Loop While (InStr(Ligne2, "@   ") = 0)
                    Ligne2 = sr.ReadLine
                    Do
                        IL = Ligne2.Length
                        Ligne2 = Ligne2.Replace("  ", " ")
                    Loop Until IL = Ligne2.Length
                    'MsgBox(row1.item("idsim") & " " & Ligne2)
                    Lsplt = Ligne2.Split(" ")
                    'An = CInt(Mid(Lsplt(14), 1, 4))
                    If Lsplt(14) > 0 Then
                        IL = CInt(Mid(Lsplt(14), 1, 4))
                        Lsplt(14) = Mid(Lsplt(14), 5)
                        If IL - An > 0 Then Lsplt(14) = CStr(CInt(Lsplt(14)) + 365 + Bissext)
                    End If
                    'Lsplt(14) = Mid(Lsplt(14), 5)
                    If Lsplt(15) > 0 Then
                        IL = CInt(Mid(Lsplt(15), 1, 4))
                        Lsplt(15) = Mid(Lsplt(15), 5)
                        If IL - An > 0 Then Lsplt(15) = CStr(CInt(Lsplt(15)) + 365 + Bissext)
                    End If
                    If Lsplt(16) > 0 Then
                        IL = CInt(Mid(Lsplt(16), 1, 4))
                        Lsplt(16) = Mid(Lsplt(16), 5)
                        If IL - An > 0 Then Lsplt(16) = CStr(CInt(Lsplt(16)) + 365 + Bissext)
                    End If
                    If Lsplt(17) > 0 Then
                        IL = CInt(Mid(Lsplt(17), 1, 4))
                        Lsplt(17) = Mid(Lsplt(17), 5)
                        If IL - An > 0 Then Lsplt(17) = CStr(CInt(Lsplt(17)) + 365 + Bissext)
                    End If
                    Lsplt(20) = CStr(CInt(Lsplt(20)) / 1000)
                    Lsplt(21) = CStr(CInt(Lsplt(21)) / 1000)
                    Ligne2 = ""
                    Sql = "Insert into SummaryOutput (Model,Idsim,Texte,Planting,Emergence,Ant,Mat,Biom_ma,Yield,GNumber,MaxLai,Nleac,SoilN,CroN_ma,CumE,Transp) values ('Dssat','" & row1.item("idsim") & "','" & Ligne2 & "','" & Lsplt(14) & "','" & Lsplt(15) & "','" & Lsplt(16) & "','" & Lsplt(17) & "','" & Lsplt(20) & "','" & Lsplt(21) & "','" & Lsplt(26) & "','" & Lsplt(29) & "','" & Lsplt(43) & "','" & Lsplt(44) & "','" & Lsplt(45) & "','" & Lsplt(84) & "','" & Lsplt(85) & "')"
                    Cmd.CommandText = Sql
                    Cmd.ExecuteNonQuery()

                Catch ex As Exception
                    'MsgBox(ex.Message)
                    Ligne2 = "No file summary "
                    Sql = "Insert into SummaryOutput (Model,Idsim,Texte) values ('Dssat','" & row1.item("idsim") & "','" & Ligne2 & "')"
                    Cmd.CommandText = Sql
                    Cmd.ExecuteNonQuery()

                End Try

                'Do
                '    IL = Ligne2.Length
                '    Ligne2 = Ligne2.Replace("  ", " ")
                'Loop Until IL = Ligne2.Length

                'If Not IsNothing(sr) Then sr.Close()
                'sr = Nothing
                'Try
                '    sr = New IO.StreamReader(Directorypath & "\" & row1.item("idsim").ToString & "\plantgro.out")
                '    'If Not (sr Is Nothing) Then
                '    'Ligne = sr.ReadLine
                '    Do
                '        Ligne = sr.ReadLine
                '        'Ligne = Ligne.Replace("  ", " ")
                '    Loop While InStr(Ligne, "@YEAR") = 0
                '    Ligne = sr.ReadLine

                '        Do
                '        'ind = ind + 1
                '        fileC1.AppendLine(row1.item("idsim").ToString.PadLeft(50) & " " & Ligne2 & " " & Ligne)
                '        Ligne = Trim(sr.ReadLine)
                '        If Ligne.Length = 0 Then Ligne = Nothing
                '    Loop Until Ligne Is Nothing
                '    'Else
                '    'End If
                'Catch ex As Exception
                '    fileC1.AppendLine(row1.item("idsim").ToString.PadLeft(50) & " " & Ligne2 & " " & " No file PlantGro")
                '    'MessageBox.Show("Error during reading file")
                'End Try
                'If Not IsNothing(sr) Then sr.Close()

                'Using outfile As StreamWriter = New StreamWriter(Directorypath + "\" + "Concat_Dssat.txt", True)
                '    outfile.Write(fileC1.ToString)
                '    fileC1.Clear()
                'End Using



                'Try
                '    ' Export file to specified directory
                '    converter.WriteFile(Directorypath, "Concat_Dssat.txt", fileC1.ToString())
                'Catch ex As Exception
                '    MessageBox.Show(ex.Message)
                '    MessageBox.Show("Error during writing file")
                'End Try

                'completed export : export button unavailable, label3 : export completed
                Btn_expDssat_export.Enabled = False
                'msgErr_expStics_browse.Text = ""

                msgErr_expDssat_export.Visible = True
                msgErr_expDssat_export.ForeColor = Color.Green
                msgErr_expDssat_export.Text = "Export completed"

            Catch ex As Exception
                MsgBox(ex.Message)
                msgErr_expDssat_export.Visible = True
                msgErr_expDssat_export.ForeColor = Color.Red
                msgErr_expDssat_export.Text = "Export aborted"
                Sql = "Insert into SummaryOutput (Model,Idsim,Texte) values ('Dssat','" & row1.item("idsim") & "','Possibly corrupted file')"
                Cmd.CommandText = Sql
                Cmd.ExecuteNonQuery()

            End Try
        Next
        'End If
    End Sub

    Private Sub Btn_Concat_Celsius_click(sender As Object, e As EventArgs) Handles Btn_Concat_Celsius.Click

        Dim converterFactory As ConverterFactory = New ConverterFactory()
        Dim converter As Converter = converterFactory.GetConverter("STICS", "Soil")
        'converter.setUsm(usm)
        Dim Directorypath As String = RepSource & "\Celsius"
        Dim Connection As New OleDb.OleDbConnection
        Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Directorypath & "\CelsiusV3nov17_dataArise.accdb"
        Dim MI_Connection = New OleDb.OleDbConnection()
        MI_Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\MasterInput.accdb"
        'Dim IL As Integer
        Dim fileC1 As StringBuilder = New StringBuilder()
        'If IO.File.Exists(Directorypath + "\" + "Concat_Stics.txt") Then IO.File.Delete(Directorypath + "\" + "Concat_Stics.txt")
        Try
            'on ouvre la connection
            'Connection.Open()
            MI_Connection.Open()
        Catch ex As Exception
            MessageBox.Show("Connection Error")
        End Try
        'weather_site query
        'Dim Q1 As String = "select * from Arise_Simul Where Model='STICS';"
        Dim Q1 As String = "select * from SimUnitList;"

        'Init and use DataAdapter
        Dim DASL As OleDb.OleDbDataAdapter = New OleDbDataAdapter(Q1, MI_Connection)
        Dim DS As New DataSet()
        DASL.Fill(DS, "Stics_SL")
        Dim DT As DataTable = DS.Tables("Stics_SL")
        'Dim sr As IO.StreamReader
        Dim Sql As String
        Dim Cmd As OleDbCommand = New OleDbCommand("", MI_Connection)
        Dim Cmd2 As OleDbDataAdapter = New OleDbDataAdapter("", Connection)
        Cmd.CommandText = "Delete * from SummaryOutput where Model='Celsius'"
        Cmd.ExecuteNonQuery()
        Dim Dt2 As DataTable
        Dim DR As DataRow()
        Dt2 = New DataTable
        Sql = "Select * from Outputsynt "
        Cmd2.SelectCommand.CommandText = Sql
        Cmd2.Fill(Dt2)
        For Each row1 In DT.Rows
            Try
                msgErr_expCelsius_export.Text = row1.item("idsim")
                msgErr_expCelsius_export.Refresh()
                Dim Ligne2 As String
                Try

                    DR = Dt2.Select("idsim ='" & row1.item("idsim") & "'")
                    'sr = New IO.StreamReader(Directorypath & "\" & row1.item("idsim").ToString & "\mod_rapport.sti")
                    'Ligne2 = DR(0).Item(9).ToString 'Cmd2.ExecuteScalar.ToString
                    Sql = "Insert into SummaryOutput (Model,Idsim,Texte,Planting,Emergence,Ant,Mat,Biom_ma,Yield,MaxLai,CumE,GNumber,SoilN,Transp) values ('Celsius','" & row1.item("idsim") & "','','" & DR(0).Item(23).ToString & "','" & DR(0).Item(1).ToString & "','" & DR(0).Item(4).ToString & "','" & DR(0).Item(6).ToString & "','" & DR(0).Item(8).ToString & "','" & DR(0).Item(9).ToString & "','" & DR(0).Item(10).ToString & "','" & DR(0).Item(11).ToString & "','" & DR(0).Item(20).ToString & "','" & DR(0).Item(41).ToString & "','" & DR(0).Item(43).ToString & "')"
                    Cmd.CommandText = Sql
                    Cmd.ExecuteNonQuery()
                Catch ex As Exception
                    'MsgBox(ex.Message)
                    Ligne2 = "No results in outputsynt"
                    Sql = "Insert into SummaryOutput (Model,Idsim,Texte) values ('Celsius','" & row1.item("idsim") & "','" & Ligne2 & "')"
                    Cmd.CommandText = Sql
                    Cmd.ExecuteNonQuery()
                End Try
                'Sql = "Insert into SummaryOutput (Model,Idsim,Texte) values ('Celsius','" & row1.item("idsim") & "','" & Ligne2 & "')"
                'Sql = "Insert into SummaryOutput (Model,Idsim,Texte,Planting,Emergence,Ant,Mat,Biom_ma,Yield,MaxLai,CumE,Transp) values ('Celsius','" & row1.item("idsim") & "','','" & DR(0).Item(23).ToString & "','" & DR(0).Item(1).ToString & "','" & DR(0).Item(4).ToString & "','" & DR(0).Item(6).ToString & "','" & DR(0).Item(8).ToString & "','" & DR(0).Item(9).ToString & "','" & "','" & DR(0).Item(10).ToString & "','" & DR(0).Item(9).ToString & "','" & DR(0).Item(11).ToString & "','" & DR(0).Item(43).ToString & "')"
                'Cmd.CommandText = Sql
                'Cmd.ExecuteNonQuery()
                'completed export : export button unavailable, label3 : export completed
                msgErr_expCelsius_export.Enabled = False
                'msgErr_expStics_browse.Text = ""

                msgErr_expCelsius_export.Visible = True
                msgErr_expCelsius_export.ForeColor = Color.Green
                msgErr_expCelsius_export.Text = "Export completed"

            Catch ex As Exception
                msgErr_expCelsius_export.Visible = True
                msgErr_expCelsius_export.ForeColor = Color.Red
                msgErr_expCelsius_export.Text = "Export aborted"
                Sql = "Insert into SummaryOutput (Model,Idsim,Texte) values ('Celsius','" & row1.item("idsim") & "','Possibly corrupted file')"
                Cmd.CommandText = Sql
                Cmd.ExecuteNonQuery()

            End Try
        Next
        'End If
    End Sub

    Private Sub DssatRun_Click(sender As Object, e As EventArgs) Handles DssatRun.Click
        Shell(RepSource & "\Dssat\Dssat.bat")
    End Sub

    Private Sub SticsRun_Click(sender As Object, e As EventArgs) Handles SticsRun.Click
        Shell(RepSource & "\Stics\Stics.bat")
    End Sub

    Private Sub CelsiusRun_Click(sender As Object, e As EventArgs) Handles CelsiusRun.Click
        'If Not IO.Directory.Exists(DirectoryPath) Then IO.Directory.CreateDirectory(DirectoryPath)
        Using outfile As StreamWriter = New StreamWriter(RepSource & "\Celsius\Celsius.bat", False)
            outfile.Write("Cd " & RepSource & "\Celsius" & vbCrLf & "CelsiusV3nov17_dataArise.accdb")
        End Using 'Shell(RepSource & "\Celsius\Celsius.bat")
        ChDir(RepSource)
        Shell(RepSource & "\Celsius\Celsius.bat")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FolderBrowserDialog1.SelectedPath = "C:\"
        FolderBrowserDialog1.ShowDialog()
        RepSource = FolderBrowserDialog1.SelectedPath
        TextBox13.Text = RepSource
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBox13.Text = RepSource
        FCChek = False
        ForceClimate.Checked = FCChek
    End Sub

    Private Sub TextBox13_TextChanged(sender As Object, e As EventArgs) Handles TextBox13.TextChanged
        RepSource = TextBox13.Text

    End Sub

    Private Sub ForceClimate_CheckedChanged(sender As Object, e As EventArgs) Handles ForceClimate.CheckedChanged
        FCChek = ForceClimate.Checked
    End Sub

End Class
