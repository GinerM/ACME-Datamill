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

Public Class SticsNewTravailConverter
    Inherits Converter


    Public Overrides Sub Export(DirectoryPath As String, Idsim As String, connection As OleDb.OleDbConnection, MI_connection As OleDb.OleDbConnection)
        Dim fileName As String = "new_travail.usm"
        Dim fileContent As StringBuilder = New StringBuilder()
        Dim Dv As String
        Dim Bissext As Integer
        'Dim Connection As New OleDb.OleDbConnection
        'Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\ModelsDictionaryArise.accdb"
        'Dim MI_Connection = New OleDb.OleDbConnection()
        'MI_Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\MasterInput.accdb"
        'Try
        '    'Open DB connection
        '    Connection.Open()
        '    MI_Connection.Open()
        'Catch ex As Exception
        '    MessageBox.Show("Connection Error : " + ex.Message)
        'End Try
        'Dim ST(3) As String
        'ST = DirectoryPath.Split("\")
        'MsgBox(DirectoryPath)
        'DirectoryPath = ST(0) & "\" & ST(1) & "\" & ST(2) & "\" & ST(3) & "\" & ST(4) & "\" & ST(5) & "\" & ST(6) & "\" & ST(7)
        'tempoparv6 query
        Dim fetchAllQuery As String = "SELECT SimUnitList.idsim, SimUnitList.idPoint, SimUnitList.StartYear,SimUnitList.StartDay,SimUnitList.EndDay,SimUnitList.Endyear, SimUnitList.idsoil, SimUnitList.idMangt, SimUnitList.idIni, Coordinates.LatitudeDD, CropManagement.sowingdate, " _
        & " ListCultivars.SpeciesName FROM InitialConditions INNER JOIN ((ListCultivars INNER JOIN CropManagement ON ListCultivars.IdCultivar = CropManagement.Idcultivar) INNER JOIN (Coordinates INNER " _
        & "Join SimUnitList ON Coordinates.idPoint = SimUnitList.idPoint) ON CropManagement.idMangt = SimUnitList.idMangt) ON InitialConditions.idIni = SimUnitList.idIni Where idsim ='" + Idsim + "';"
        Dim T As String = "Select   Variables.Champ, Variables.Default_Value_Datamill, Variables.defaultValueOtherSource, IIf(IsNull([defaultValueOtherSource]),[Default_Value_Datamill],[defaultValueOtherSource]) As dv From Variables Where (((Variables.model) = 'stics') And ((Variables.Table) = 'st_new_travail'));"
        Dim DT As New DataTable
        Dim dataSet As New DataSet
        Dim rw() As DataRow
        Dim Cmd As New OleDb.OleDbDataAdapter(T, connection)
        Cmd.Fill(DT) ', "TChamp")        'Init and use DataAdapter
        Dim DA As New OleDb.OleDbDataAdapter(fetchAllQuery, MI_connection)
        'MsgBox(fetchAllQuery)
        ' Filling Dataset
        Dim DSTrav As New DataSet
        DA.Fill(DSTrav)
        Dim DTable As DataTable = DSTrav.Tables(0)
        'read all line of new_travail

        fileContent.AppendLine(":codesimul")
        rw = DT.Select("Champ='codesimul'")
        Dv = rw(0)("dv").ToString
        fileContent.AppendLine(Dv)
        fileContent.AppendLine(":codeoptim") 'dv 0
        rw = DT.Select("Champ='codeoptim'")
        Dv = rw(0)("dv").ToString
        fileContent.AppendLine(Dv)
        fileContent.AppendLine(":codesuite") 'dv 0
        rw = DT.Select("Champ='codesuite'")
        Dv = rw(0)("dv").ToString
        fileContent.AppendLine(Dv)
        fileContent.AppendLine(":nbplantes") 'dv 1
        rw = DT.Select("Champ='nbplantes'")
        Dv = rw(0)("dv").ToString
        fileContent.AppendLine(Dv)
        fileContent.AppendLine(":nom")
        fileContent.AppendLine(DTable.Rows(0)("SpeciesName"))
        fileContent.AppendLine(":datedebut")
        'fileContent.AppendLine(DTable.Rows(0)("sowingdate"))
        fileContent.AppendLine(DTable.Rows(0)("startday"))
        fileContent.AppendLine(":datefin") 'endday
        'If DTable.Rows(0)("latitudeDD") < 0 Then
        If CInt(DTable.Rows(0).Item("StartYear")) Mod 4 = 0 Then
            Bissext = 1
        Else
            Bissext = 0
        End If

        If CInt(DTable.Rows(0)("StartYear")) <> CInt(DTable.Rows(0)("Endyear")) Then
            fileContent.AppendLine(DTable.Rows(0)("endday") + 365 + Bissext)
        Else
            fileContent.AppendLine(DTable.Rows(0)("endday"))
        End If
        fileContent.AppendLine(":finit") 'idini
        fileContent.AppendLine("ficini.txt") '        fileContent.AppendLine(DTable.Rows(0)("idini"))
        fileContent.AppendLine(":numsol")
        fileContent.AppendLine("1")
        fileContent.AppendLine(":nomsol")
        fileContent.AppendLine("param.sol")
        'fileContent.AppendLine(DTable.Rows(0)("idsoil"))
        fileContent.AppendLine(":fstation")
        fileContent.AppendLine("station.txt")
        fileContent.AppendLine(":fclim1")
        fileContent.AppendLine("cli" & DTable.Rows(0)("idpoint") & "j." & DTable.Rows(0)("StartYear"))
        fileContent.AppendLine(":fclim2")
        fileContent.AppendLine("cli" & DTable.Rows(0)("idpoint") & "j." & (CInt(DTable.Rows(0)("StartYear")) + 1).ToString)
        fileContent.AppendLine(":nbans")
        'If DTable.Rows(0)("latitudeDD") < 0 Then
        If CInt(DTable.Rows(0)("StartYear")) <> CInt(DTable.Rows(0)("Endyear")) Then
            fileContent.AppendLine("2")
        Else
            fileContent.AppendLine("1")
        End If
        fileContent.AppendLine(":culturean")
        'If DTable.Rows(0)("latitudeDD") < 0 Then
        If CInt(DTable.Rows(0)("StartYear")) <> CInt(DTable.Rows(0)("Endyear")) Then
            fileContent.AppendLine("2")
        Else
            fileContent.AppendLine("1")
        End If
        fileContent.AppendLine(":fplt1")
        fileContent.AppendLine("ficplt1.txt") 'fileContent.AppendLine(DTable.Rows(0)("SpeciesName"))
        fileContent.AppendLine(":ftec1")
        fileContent.AppendLine("fictec1.txt") 'fileContent.AppendLine(DTable.Rows(0)("idmangt"))
        fileContent.AppendLine(":flai1")
        'fileContent.AppendLine(Dv)
        rw = DT.Select("Champ='flai1'")
        Dv = rw(0)("dv").ToString
        fileContent.AppendLine(Dv)
        ' Next
        Try
            ' Export file to specified directory
            WriteFile(DirectoryPath, fileName, fileContent.ToString())
        Catch ex As Exception
            MessageBox.Show("Error during writing file : " + ex.Message)
        End Try
        'Connection.Close()
        'MI_Connection.Close()
    End Sub

    Public Overrides Sub Import(DirectoryPath As String, model As String)

    End Sub
End Class

