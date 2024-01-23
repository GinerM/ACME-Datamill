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

Public Class DssatConverter
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

    Public Overrides Sub Export(DirectoryPath As String, IdSim As String, connection As OleDb.OleDbConnection, MI_connection As OleDb.OleDbConnection)

        Dim fileC1 As StringBuilder = New StringBuilder()
        WriteFile(DirectoryPath & "\", "debut.txt", "exit")

        Try
            'on ouvre la connection
            connection.Open()
            MI_connection.Open()
        Catch ex As Exception
            MessageBox.Show("Connection Error1")
        End Try
        'weather_site query
        Dim Q1 As String = "select * from SimUnitList;"

        'Init and use DataAdapter
        Dim DASL As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter(Q1, MI_connection)
        Dim DS As New DataSet()
        DASL.Fill(DS, "dssat_SL")
        Dim dataTable As DataTable = DS.Tables("dssat_SL")

        Dim myOptions As ParallelOptions = New ParallelOptions()
        myOptions.MaxDegreeOfParallelism = Form1.NbCore.Value
        System.Threading.Tasks.Parallel.For(0, dataTable.Rows.Count, myOptions, Sub(i)
                                                                                    Dim row As DataRow
                                                                                    row = dataTable.Rows(i)

                                                                                    Dim dssatWeatherConverter As Converter = New DssatWeatherConverter
                                                                                    Try
                                                                                        dssatWeatherConverter.Export(DirectoryPath & "\" & row.Item("idsim"), row.Item("IdPoint") & "\" & row.Item("StartYear") & "\" & row.Item("IdMangt"), connection, MI_connection)
                                                                                    Catch ex As Exception
                                                                                        MessageBox.Show("Error during Export DSSAT WEATHER ")
                                                                                    End Try
                                                                                    Application.DoEvents()
                                                                                    Dim dssatSoilConverter As Converter = New DssatSoilConverter
                                                                                    Try
                                                                                        dssatSoilConverter.Export(DirectoryPath & "\" & row.Item("idsim"), row.Item("idsim") & "\" & row.Item("idsoil") & "\" & row.Item("IdPoint") & "\" & row.Item("StartYear") & "\" & row.Item("IdMangt"), connection, MI_connection)
                                                                                    Catch ex As Exception
                                                                                        MessageBox.Show("Error during Export DSSAT SOIL")
                                                                                    End Try
                                                                                    Application.DoEvents()
                                                                                    'Dim dssatCultivarConverter As Converter = New DssatCultivarConverter
                                                                                    'Try
                                                                                    '    dssatCultivarConverter.Export(DirectoryPath & "\" & row.Item("idsim") & "\" & row.Item("idsim"))
                                                                                    'Catch ex As Exception
                                                                                    '    MessageBox.Show("Error during Export DSSAT CULTIVAR")
                                                                                    'End Try
                                                                                    'Application.DoEvents()
                                                                                    Dim dssatSgxConverter As Converter = New DssatXConverter
                                                                                    Try
                                                                                        dssatSgxConverter.Export(DirectoryPath & "\" & row.Item("idsim"), row.Item("idsim") & "\" & row.Item("IdMangt"), connection, MI_connection)
                                                                                    Catch ex As Exception
                                                                                        MessageBox.Show("Error during Export DSSAT X")
                                                                                    End Try
                                                                                    Form1.msgErr_expDssat_export.Text = row.Item("idsim")
                                                                                    Form1.msgErr_expDssat_export.Refresh()
                                                                                    'fileC1.AppendLine("Cd " & DirectoryPath & "\" & row.item("Folder"))
                                                                                    Try
                                                                                        ' Export file to specified directory
                                                                                        'MsgBox(DirectoryPath & "\" & row.item("idsim"), row.item("idsim") & ".bat", "C:\DSSAT47\DSCSM047.EXE B DSSBatch.v47" & vbCrLf & "exit")
                                                                                        WriteFile(DirectoryPath & "\" & row.Item("idsim"), row.Item("idsim") & ".bat", "C:\DSSAT47\DSCSM047.EXE B DSSBatch.v47" & vbCrLf & "exit")
                                                                                    Catch ex As Exception
                                                                                        MessageBox.Show("Error during writing file")
                                                                                    End Try

                                                                                End Sub
                                                                            )
        For Each row In dataTable.Rows
            fileC1.AppendLine("Start /d " & DirectoryPath & "\" & row.Item("idsim") & " " & row.Item("idsim") & ".bat")
        Next
        Try
            ' Export file to specified directory
            WriteFile(DirectoryPath, "Dssat.bat", fileC1.ToString())
        Catch ex As Exception
            MessageBox.Show("Error during writing file")
        End Try
        connection.Close()
        MI_connection.Close()
        WriteFile(DirectoryPath & "\", "fin.txt", "exit")
    End Sub

    Public Overrides Sub Import(DirectoryPath As String, model As String)
        MessageBox.Show("import dssat")
    End Sub

    
   
End Class
