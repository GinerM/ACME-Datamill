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
Imports System
Imports System.IO
Imports System.Text
Imports System.Globalization
Imports System.Configuration
Imports System.Data.OleDb
Imports System.Threading.Tasks




Public Class SticsConverter
    Inherits Converter
    Public Sub New()
        'MasterInput_Connection = New SqliteConnection()
        'MasterInput_Connection.ConnectionString = GlobalVariables.dbMasterInput

        'ModelDictionary_Connection = New SqliteConnection()
        'ModelDictionary_Connection.ConnectionString = GlobalVariables.dbModelsDictionary
        Connection = New OleDb.OleDbConnection
        Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\ModelsDictionaryArise.accdb"
        MI_Connection = New OleDb.OleDbConnection()
        MI_Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\MasterInput.accdb"

    End Sub
    Public Overrides Sub Export(DirectoryPath As String, Idsim As String, connection As OleDb.OleDbConnection, MI_connection As OleDb.OleDbConnection)

        Dim fileC1 As StringBuilder = New StringBuilder()

        WriteFile(DirectoryPath & "\", "debut.txt", "exit")
        Try
            '    'on ouvre la connection
            connection.Open()
            MI_connection.Open()
        Catch ex As Exception
            MessageBox.Show("Connection Error")
        End Try
        'weather_site query
        Dim Q1 As String = "SELECT SimUnitList.*, ListCultOption.FicPlt FROM (ListCultOption INNER JOIN (ListCultivars INNER JOIN CropManagement ON ListCultivars.IdCultivar = CropManagement.Idcultivar) ON ListCultOption.CodePSpecies = ListCultivars.CodePSpecies) INNER JOIN SimUnitList ON CropManagement.idMangt = SimUnitList.idMangt;"

        'Init and use DataAdapter
        Dim DASL As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter(Q1, MI_connection)
        'Dim DS As New DataSet()
        Dim DT As New DataTable ' = DS.Tables("Stics_SL")
        DASL.Fill(DT) ', "Stics_SL")


        'For Each row1 In DT.Rows
        Dim myOptions As ParallelOptions = New ParallelOptions()
        myOptions.MaxDegreeOfParallelism = Form1.NbCore.Value
        System.Threading.Tasks.Parallel.For(0, DT.Rows.Count, myOptions, Sub(i)
                                                                             Dim row1 As DataRow
                                                                             row1 = DT.Rows(i)
                                                                             'Climat
                                                                             Dim climatConverter As Converter = New SticsClimatConverter
                                                                             Try
                                                                                 climatConverter.Export(DirectoryPath & "\" & row1.Item("idsim"), row1.Item("IdPoint") & "\" & row1.Item("StartYear"), connection, MI_connection)
                                                                             Catch ex As Exception
                                                                                 MessageBox.Show("Error during Export STICS Climat : " + ex.Message)
                                                                             End Try
                                                                             'Tempoparv6
                                                                             Dim tempoparv6Converter As Converter = New SticsTempoparv6Converter
                                                                             Try
                                                                                 tempoparv6Converter.Export(DirectoryPath & "\" & row1.Item("idsim"), row1.Item("idsim"), connection, MI_connection)
                                                                             Catch ex As Exception
                                                                                 MessageBox.Show("Error during Export STICS Tempoparv6 : " + ex.Message)
                                                                             End Try
                                                                             'Station
                                                                             Dim stationConverter As Converter = New SticsStationConverter
                                                                             Try
                                                                                 stationConverter.Export(DirectoryPath & "\" & row1.Item("idsim"), row1.Item("idsim"), connection, MI_connection)
                                                                             Catch ex As Exception
                                                                                 MessageBox.Show("Error during Export STICS Station : " + ex.Message)
                                                                             End Try
                                                                             'New_travail
                                                                             Dim newTravailConverter As Converter = New SticsNewTravailConverter
                                                                             Try
                                                                                 newTravailConverter.Export(DirectoryPath & "\" & row1.Item("idsim"), row1.Item("idsim"), connection, MI_connection)
                                                                             Catch ex As Exception
                                                                                 MessageBox.Show("Error during Export STICS New_travail : " + ex.Message)
                                                                             End Try
                                                                             'Param sol
                                                                             Dim paramSolConverter As Converter = New SticsParamSolConverter
                                                                             Try
                                                                                 paramSolConverter.Export(DirectoryPath & "\" & row1.Item("idsim"), row1.Item("idsim"), connection, MI_connection)
                                                                             Catch ex As Exception
                                                                                 MessageBox.Show("Error during Export STICS Param Sol : " + ex.Message)
                                                                             End Try
                                                                             'Ficini
                                                                             Dim ficiniConverter As Converter = New SticsFiciniConverter
                                                                             Try
                                                                                 ficiniConverter.Export(DirectoryPath & "\" & row1.Item("idsim"), row1.Item("idsim"), connection, MI_connection)
                                                                             Catch ex As Exception
                                                                                 MessageBox.Show("Error during Export STICS Ficini : " + ex.Message)
                                                                             End Try

                                                                             'Ficplt1
                                                                             'Dim ficplt1Converter As Converter = New SticsFicplt1Converter
                                                                             'Try
                                                                             ' ficplt1Converter.Export(DirectoryPath & "\" & row1.item("idsim") & "\" & row1.item("idsim"))
                                                                             ' Catch ex As Exception
                                                                             'MessageBox.Show("Error during Export STICS Ficplt1 : " + ex.Message)
                                                                             'End Try

                                                                             'Fictec1
                                                                             Dim fictec1Converter As Converter = New SticsFictec1Converter
                                                                             Try
                                                                                 fictec1Converter.Export(DirectoryPath & "\" & row1.Item("idsim"), row1.Item("idsim"), connection, MI_connection)
                                                                             Catch ex As Exception
                                                                                 MessageBox.Show("Error during Export STICS Fictec1 : " + ex.Message)
                                                                             End Try
                                                                             'SticsTempoparConverter
                                                                             Dim tempoparConverter As Converter = New SticsTempoparConverter
                                                                             Try
                                                                                 tempoparConverter.Export(DirectoryPath & "\" & row1.Item("IdSim"), row1.Item("IdSim"), connection, MI_connection)
                                                                             Catch ex As Exception
                                                                                 MessageBox.Show("Error during Export STICS Tempopar : " + ex.Message)
                                                                             End Try
                                                                             Form1.msgErr_expStics_export.Text = row1.Item("idsim")
                                                                             Form1.msgErr_expStics_export.Refresh()
                                                                             'création du bat
                                                                             WriteFile(DirectoryPath & "\" & row1.Item("idsim"), row1.Item("idsim") & ".bat", "copy ..\var.mod /Y" & vbCrLf & "copy ..\" & row1.Item("FicPlt") & " ficplt1.txt /Y" & vbCrLf & "Del mod*.sti" & vbCrLf & "..\stics_modulo" & vbCrLf & "exit")
                                                                         End Sub
                                                                         )
        'Création du bat global de lancement
        For Each row1 In DT.Rows
            fileC1.AppendLine("Start /d " & DirectoryPath & "\" & row1.Item("idsim") & " " & row1.Item("idsim") & ".bat")
        Next
        Try
            ' Export file to specified directory
            WriteFile(DirectoryPath, "Stics.bat", fileC1.ToString())
        Catch ex As Exception
            MessageBox.Show("Error during writing file")
        End Try
        connection.Close()
        MI_connection.Close()
        WriteFile(DirectoryPath & "\", "fin.txt", "exit")
    End Sub

    Public Overrides Sub Import(DirectoryPath As String, model As String)
        MessageBox.Show("import Stics")
    End Sub
End Class

