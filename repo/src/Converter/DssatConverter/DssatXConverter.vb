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
Public Class DssatXConverter
    Inherits Converter

    Public Overrides Sub Export(DirectoryPath As String, idSim As String, connection As OleDb.OleDbConnection, MI_connection As OleDb.OleDbConnection)

        'Dim Connection As New OleDb.OleDbConnection
        'Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\ModelsDictionaryArise.accdb"
        'Dim MI_Connection = New OleDb.OleDbConnection()
        'MI_Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\MasterInput.accdb"
        'Application.DoEvents()
        'Try
        '    'Open DB connection
        '    'While Connection.State <> ConnectionState.Open
        '    Connection.Open()
        '    'End While

        '    'While MI_Connection.State <> ConnectionState.Open
        '    MI_Connection.Open()
        '    'End While
        'Catch ex As Exception
        '    MessageBox.Show("Connection Error5 : " + ex.Message)
        'End Try
        'Dim idSim, idMangt As String
        Dim ST(10) As String
        Dim idMangt As String
        ST = idSim.Split("\")
        'DirectoryPath = ST(0) & "\" & ST(1) & "\" & ST(2) & "\" & ST(3) & "\" & ST(4) & "\" & ST(5) & "\" & ST(6) & "\" & ST(7)
        'Site = ST(8)
        idMangt = ST(1)
        'Year = ST(7)
        idSim = ST(0)
        'ST = Year.Split(".")
        'Site = ST(0)
        'Year = ST(1)
        Dim T As String = "Select Variables.Champ, Variables.Default_Value_Datamill, Variables.defaultValueOtherSource, IIf(IsNull([defaultValueOtherSource]),[Default_Value_Datamill],[defaultValueOtherSource]) As dv From Variables Where (((Variables.model) = 'dssat') And ((Variables.Table) like 'dssat_x_%'));"

        Dim DT As New DataSet()
        Dim Dv As String
        Dim rw() As DataRow
        Dim Cmd As New OleDb.OleDbDataAdapter(T, connection)
        Cmd.Fill(DT, "TChamp")      'weather_site query

        'dssat_x_exp query
        'Dim fetchAllQuery As String = "select * from dssat_x_exp where filename='" & ST(3) & "';"

        'Init and use DataAdapter
        'Using dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter(fetchAllQuery, Connection)

        ' Filling Dataset
        'Dim dataSet As New DataSet()
        'dataAdapter.Fill(dataSet, "dssat_x_exp")
        'Dim dataTable As DataTable = dataSet.Tables("dssat_x_exp")
        Dim fileName As String = ""
        Dim idData As Integer
        Dim header As String = ""
        Dim siteColumnsHeader() As String = {"@", "PAREA", " PRNO", " PLEN", " PLDR", " PLSP", " PLAY", "HAREA", " HRNO", " HLEN", "HARM........."}

        Dim fileContent As StringBuilder = New StringBuilder()
        'fileContent.Append(FileHeader) ' Write File header

        'read all line of dssat_x_exp
        'For Each row In dataTable.Rows
        rw = DT.Tables(0).Select("Champ='filename'")
        Dv = rw(0)("dv").ToString
        'fileContent.Append(Dv.PadLeft(5))
        fileName = Dv.ToString
        'idData = row.item("id")
        rw = DT.Tables(0).Select("Champ='header'")
        Dv = rw(0)("dv").ToString
        fileContent.Append(Dv)
        'fileContent.Append(row.item("header"))
        'store header to retrieve name
        header = Dv ' row.item("header")


        fileContent.AppendLine() ' Append a line break.
        fileContent.AppendLine() ' Append a line break.
        fileContent.Append("*GENERAL")
        fileContent.AppendLine() ' Append a line break.
        fileContent.Append("@PEOPLE")
        fileContent.AppendLine() ' Append a line break.
        rw = DT.Tables(0).Select("Champ='PEOPLE'")
        Dv = rw(0)("dv").ToString
        fileContent.Append(Dv)
        'fileContent.Append(row.item("PEOPLE"))
        fileContent.AppendLine() ' Append a line break.
        fileContent.Append("@ADDRESS")
        fileContent.AppendLine() ' Append a line break.
        rw = DT.Tables(0).Select("Champ='ADDRESS'")
        Dv = rw(0)("dv").ToString
        fileContent.Append(Dv)
        'fileContent.Append(row.item("ADDRESS"))
        fileContent.AppendLine() ' Append a line break.
        fileContent.Append("@SITE")
        fileContent.AppendLine() ' Append a line break.
        rw = DT.Tables(0).Select("Champ='SITE'")
        Dv = rw(0)("dv").ToString
        fileContent.Append(Dv)
        'fileContent.Append(row.item("SITE"))
        fileContent.AppendLine() ' Append a line break.
        fileContent.Append(String.Join(Chr(32), siteColumnsHeader))
        'fileContent.Append("@ PAREA  PRNO  PLEN  PLDR  PLSP  PLAY HAREA  HRNO  HLEN  HARM.........")
        fileContent.AppendLine() ' Append a line break.
        fileContent.Append(Chr(32))
        rw = DT.Tables(0).Select("Champ='PAREA'")
        Dv = rw(0)("dv").ToString
        fileContent.Append(Dv.PadLeft(6))
        'fileContent.Append(formatItem_Lg(row.Item("PAREA"), 6))
        fileContent.Append(Chr(32))
        rw = DT.Tables(0).Select("Champ='PRNO'")
        Dv = rw(0)("dv").ToString
        fileContent.Append(Dv.PadLeft(5))
        'fileContent.Append(formatItem_Lg(row.Item("PRNO"), 5))
        fileContent.Append(Chr(32))
        rw = DT.Tables(0).Select("Champ='PLEN'")
        Dv = rw(0)("dv").ToString
        fileContent.Append(Dv.PadLeft(5))
        'fileContent.Append(formatItem_Lg(row.Item("PLEN"), 5))
        fileContent.Append(Chr(32))
        rw = DT.Tables(0).Select("Champ='PLDR'")
        Dv = rw(0)("dv").ToString
        fileContent.Append(Dv.PadLeft(5))
        'fileContent.Append(formatItem_Lg(row.Item("PLDR"), 5))
        fileContent.Append(Chr(32))
        rw = DT.Tables(0).Select("Champ='PLSP'")
        Dv = rw(0)("dv").ToString
        fileContent.Append(Dv.PadLeft(5))
        'fileContent.Append(formatItem_Lg(row.Item("PLSP"), 5))
        fileContent.Append(Chr(32))
        rw = DT.Tables(0).Select("Champ='PLAY'")
        Dv = rw(0)("dv").ToString
        fileContent.Append(Dv.PadLeft(5))
        'fileContent.Append(formatItem_Lg(row.Item("PLAY"), 5))
        fileContent.Append(Chr(32))
        rw = DT.Tables(0).Select("Champ='HAREA'")
        Dv = rw(0)("dv").ToString
        fileContent.Append(Dv.PadLeft(5))
        'fileContent.Append(formatItem_Lg(row.Item("HAREA"), 5))
        fileContent.Append(Chr(32))
        rw = DT.Tables(0).Select("Champ='HRNO'")
        Dv = rw(0)("dv").ToString
        fileContent.Append(Dv.PadLeft(5))
        'fileContent.Append(formatItem_Lg(row.Item("HRNO"), 5))
        fileContent.Append(Chr(32))
        rw = DT.Tables(0).Select("Champ='HLEN'")
        Dv = rw(0)("dv").ToString
        fileContent.Append(Dv.PadLeft(5))
        'fileContent.Append(formatItem_Lg(row.Item("HLEN"), 5))
        fileContent.Append(Chr(32))
        rw = DT.Tables(0).Select("Champ='HARM'")
        Dv = rw(0)("dv").ToString
        fileContent.Append(Dv)
        'fileContent.Append(formatItem(row.Item("HARM")))
        fileContent.AppendLine() ' Append a line break.
        'rw = DT.Tables(0).Select("Champ='PEOPLE'")
        'Dv = rw(0)("dv").ToString
        'fileContent.Append(Dv)
        fileContent.Append("@NOTES")
        fileContent.AppendLine() ' Append a line break.
        rw = DT.Tables(0).Select("Champ='NOTES'")
        Dv = rw(0)("dv").ToString
        fileContent.Append(Dv)
        'fileContent.Append(formatItem(row.item("NOTES")))
        fileContent.AppendLine() ' Append a line break.

        '---------------------------------------------------------------------------------------------
        '*TREATMENTS  
        'table dssat_x_treatment
        '---------------------------------------------------------------------------------------------

        Dim dssat_tableName As String = "dssat_x_treatment"
        Dim dssat_tableId As String = "dssat_x_exp_id"
        Dim dssat_tableId_value As String = idData.ToString
        writeBlockTreatment(dssat_tableName, idSim, dssat_tableId_value, fileContent, connection, MI_connection)
        '---------------------------------------------------------------------------------------------
        '*CULTIVARS"
        'table dssat_x_cultivar
        '---------------------------------------------------------------------------------------------
        dssat_tableName = "dssat_x_cultivar"
        dssat_tableId = "dssat_x_exp_id"
        writeBlockCultivar(dssat_tableName, idMangt, dssat_tableId_value, fileContent, connection, MI_connection)

        '---------------------------------------------------------------------------------------------
        '*FIELDS
        'table x_field
        '---------------------------------------------------------------------------------------------
        dssat_tableName = "dssat_x_field"
        dssat_tableId = "dssat_x_exp_id"
        writeBlockField(dssat_tableName, dssat_tableId, idMangt, fileContent, connection) 'site

        '---------------------------------------------------------------------------------------------
        'table soil_analysis
        dssat_tableName = "dssat_x_soil_analysis"
        dssat_tableId = "dssat_x_exp_id"
        writeBlockSoilAnalysis(dssat_tableName, dssat_tableId, dssat_tableId_value, fileContent, connection)

        '---------------------------------------------------------------------------------------------
        'table dssat_x_initial_condition
        dssat_tableName = "dssat_x_initial_condition"
        dssat_tableId = "dssat_x_exp_id"
        writeBlockInitialCondition(dssat_tableName, idSim, dssat_tableId_value, fileContent, connection, MI_connection)

        '---------------------------------------------------------------------------------------------
        'table dssat_x_planting_detail
        dssat_tableName = "dssat_x_planting_detail"
        dssat_tableId = "dssat_x_exp_id"
        writeBlockPlantingDetail(dssat_tableName, idSim, dssat_tableId_value, fileContent, connection, MI_connection)

        '---------------------------------------------------------------------------------------------
        'irrigation and water management
        'table dssat_x_irrigation_water
        '---------------------------------------------------------------------------------------------
        dssat_tableName = "dssat_x_irrigation_water"
        dssat_tableId = "dssat_x_exp_id"
        writeBlockIrrigationWater(dssat_tableName, dssat_tableId, dssat_tableId_value, fileContent, connection)

        '---------------------------------------------------------------------------------------------
        ' fertilizer
        ' table dssat_x_fertilizer
        '---------------------------------------------------------------------------------------------
        dssat_tableName = "dssat_x_fertilizer"
        '         dssat_tableId = "dssat_x_exp_id"
        writeBlockFertilizer(dssat_tableName, idSim, dssat_tableId_value, fileContent, connection, MI_connection)

        '---------------------------------------------------------------------------------------------
        'RESIDUES AND ORGANIC FERTILIZER
        'table dssat_x_residues
        '---------------------------------------------------------------------------------------------
        dssat_tableName = "dssat_x_residues"
        dssat_tableId = "dssat_x_exp_id"
        writeBlockResidues(dssat_tableName, idSim, dssat_tableId_value, fileContent, connection, MI_connection)

        '---------------------------------------------------------------------------------------------
        '*CHEMICAL APPLICATIONS
        'table(dssat_x_chemical_application)
        '---------------------------------------------------------------------------------------------
        dssat_tableName = "dssat_x_chemical_application"
        dssat_tableId = "dssat_x_exp_id"
        writeBlockChemicalApplication(dssat_tableName, dssat_tableId, dssat_tableId_value, fileContent, connection)

        '---------------------------------------------------------------------------------------------
        '*TILLAGE AND ROTATIONS
        ' table(dssat_x_tillage)
        '--------------------------------------------------------------------------------------------
        dssat_tableName = "dssat_x_tillage"
        dssat_tableId = "dssat_x_exp_id"
        writeBlockTillageRotation(dssat_tableName, idSim, dssat_tableId_value, fileContent, connection, MI_connection)

        '---------------------------------------------------------------------------------------------
        '*ENVIRONMENT MODIFICATIONS
        'table(dssat_x_environnement)
        '---------------------------------------------------------------------------------------------
        dssat_tableName = "dssat_x_environment"
        dssat_tableId = "dssat_x_exp_id"
        writeBlockEnvironment(dssat_tableName, dssat_tableId, dssat_tableId_value, fileContent, connection)

        '---------------------------------------------------------------------------------------------
        '*HARVEST DETAILS
        'table(dssat_x_harvest)
        '---------------------------------------------------------------------------------------------
        dssat_tableName = "dssat_x_harvest"
        dssat_tableId = "dssat_x_exp_id"
        writeBlockHarvest(dssat_tableName, idSim, dssat_tableId_value, fileContent, connection, MI_connection)

        '---------------------------------------------------------------------------------------------
        '*SIMULATION CONTROLS
        'Automatic Managment
        '---------------------------------------------------------------------------------------------
        fileContent.AppendLine() ' Append a line break.
        fileContent.Append("*SIMULATION CONTROLS")
        fileContent.AppendLine() ' Append a line break.

        writeBlockEndFile(fileContent, idSim, connection, MI_connection)

        '---------------------------------------------------------------------------------------------
        ' write file
        '---------------------------------------------------------------------------------------------
        Try
            ' Export file to specified directory
            WriteFile(DirectoryPath, fileName, fileContent.ToString())
            fileContent.Clear()
        Catch ex As Exception
            MessageBox.Show("Error during writing file")
        End Try
        '---------------------------------------------------------------------------------------------
        '*Fichier DSSBatch.v47  
        'table dssat_x_treatment
        '---------------------------------------------------------------------------------------------
        fileContent.Clear()
        dssat_tableName = "dssat_x_treatment"
        dssat_tableId = "dssat_x_exp_id"
        dssat_tableId_value = idData.ToString
        writeBlockTreatment2(dssat_tableName, fileName, dssat_tableId_value, idSim, fileContent, connection)
        Try
            ' Export file to specified directory
            WriteFile(DirectoryPath, "DSSBatch.v47", fileContent.ToString())
            fileContent.Clear()
        Catch ex As Exception
            MessageBox.Show("Error during writing file")
        End Try

        'next occurence of dssat_x_exp
        'Next
        'End Using

        'Connection.Close()
        'MI_Connection.Close()
    End Sub

    Public Overrides Sub Import(DirectoryPath As String, model As String)

    End Sub
End Class
