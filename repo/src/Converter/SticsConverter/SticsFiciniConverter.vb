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

Public Class SticsFiciniConverter
    Inherits Converter


    Public Overrides Sub Export(DirectoryPath As String, IdSim As String, connection As OleDb.OleDbConnection, MI_connection As OleDb.OleDbConnection)
        Dim fileName As String = "ficini.txt"
        Dim fileContent As StringBuilder = New StringBuilder()

        'Init Connection with connection string from app.config
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
        'DirectoryPath = ST(0) & "\" & ST(1) & "\" & ST(2) & "\" & ST(3) & "\" & ST(4) & "\" & ST(5) & "\" & ST(6) & "\" & ST(7)
        'Ficini query
        Dim T As String = "Select   Variables.Champ, Variables.Default_Value_Datamill, Variables.defaultValueOtherSource, IIf(IsNull([defaultValueOtherSource]),[Default_Value_Datamill],[defaultValueOtherSource]) As dv From Variables Where (((Variables.model) = 'stics') And ((Variables.Table) = 'st_ficini'));"
        Dim DT As New DataTable
        Dim rw() As DataRow
        Dim Sql As String

        Dim Cmd As New OleDb.OleDbDataAdapter(T, connection)
        Cmd.Fill(DT) ', "TChamp")

        Dim fetchAllQuery As String = "SELECT SimUnitList.idIni, Soil.IdSoil, Soil.SoilOption, Soil.Wwp, Soil.Wfc, Soil.bd, InitialConditions.WStockinit, InitialConditions.Ninit " _
        & "FROM InitialConditions INNER JOIN (Soil INNER JOIN SimUnitList ON Soil.IdSoil = SimUnitList.idsoil) ON InitialConditions.idIni = SimUnitList.idIni " _
        & " where idSim='" + IdSim + "';"


        'Init and use DataAdapter
        Using dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter(fetchAllQuery, MI_connection)
            ' Filling Dataset
            Dim dataSet As New DataSet()
            dataAdapter.Fill(dataSet, "st_ficini")
            Dim dataTable As DataTable = dataSet.Tables("st_ficini")

            'read all line of st_ficini
            For Each row In dataTable.Rows
                fileContent.AppendLine(":nbplantes:")
                rw = DT.Select("Champ='nbplantes'")
                fileContent.AppendLine(rw(0)("dv").ToString)
                fileContent.AppendLine(":plante:")
                rw = DT.Select("Champ='stade0'")
                fileContent.AppendLine(rw(0)("dv").ToString)
                rw = DT.Select("Champ='lai0'")
                fileContent.AppendLine(rw(0)("dv").ToString)
                rw = DT.Select("Champ='masec0'")
                fileContent.AppendLine(rw(0)("dv").ToString)
                rw = DT.Select("Champ='zrac0'")
                fileContent.AppendLine(rw(0)("dv").ToString)
                rw = DT.Select("Champ='magrain0'")
                fileContent.AppendLine(rw(0)("dv").ToString)
                rw = DT.Select("Champ='qnplante0'")
                fileContent.AppendLine(rw(0)("dv").ToString)
                rw = DT.Select("Champ='resperenne0'")
                fileContent.AppendLine(rw(0)("dv").ToString)
                fileContent.AppendLine(":densinitial:")
                rw = DT.Select("Champ='densinitial'")
                fileContent.AppendLine(rw(0)("dv").ToString & " 0.0 0.0 0.0 0.0")
                fileContent.AppendLine(":plante:")
                fileContent.AppendLine()
                fileContent.AppendLine()
                fileContent.AppendLine()
                fileContent.AppendLine()
                fileContent.AppendLine()
                fileContent.AppendLine()
                fileContent.AppendLine()
                fileContent.AppendLine()
                fileContent.AppendLine(":densinitial:")
                fileContent.AppendLine("     ")
                Sql = "Select * From soillayers where idsoil= '" & dataTable.Rows(0).Item("idsoil") & "' Order by NumLayer"
                Dim Adp As New OleDb.OleDbDataAdapter(Sql, MI_connection)
                Dim jeu As New DataSet
                Adp.Fill(jeu)
                fileContent.AppendLine(":hinit:")
                'if soilOption= "simple" then 
                '       (Wwp+Wstockinit*(Wfc-Wwp)/100)/bd for layer 1 
                'Else If soilOption = "detailed" Then 
                '       (SoilLayer.Wwp+Wstockinit*(SoilLayer.Wfc-SoilLayer.Wwp)/100)/SoilLayer.bd 
                '       For Each Of the five soil layers
                If LCase(dataTable.Rows(0).Item("soilOption")) = "simple" Then
                    'fileContent.Append(FormatNumber(dataTable.Rows(0).Item("Wfc") / dataTable.Rows(0).Item("Bd"), 4).PadLeft(8))
                    fileContent.Append(FormatNumber((dataTable.Rows(0).Item("Wwp") + dataTable.Rows(0).Item("WStockinit") * (dataTable.Rows(0).Item("Wfc") - dataTable.Rows(0).Item("Wwp")) / 100) / dataTable.Rows(0).Item("Bd"), 4).PadLeft(8))
                    fileContent.AppendLine(" 0.0 0.0 0.0 0.0")
                Else
                    For i = 0 To 4
                        If i < jeu.Tables(0).Rows.Count Then
                            row = jeu.Tables(0).Rows(i)
                            fileContent.Append(FormatNumber((row("Wwp") + dataTable.Rows(0).Item("WStockinit") * (row("Wfc") - row("Wwp")) / 100) / row("Bd"), 4).PadLeft(8))
                        Else
                            fileContent.Append(" 0.0")
                        End If
                    Next
                    fileContent.AppendLine()
                End If
                'fileContent.AppendLine(" 0.0 0.0 0.0 0.0")
                fileContent.AppendLine(":NO3init:")
                'if soilOption= "simple" then Ninit for layer 1 (and zero for the other layers)  else if  soilOption= "detailed" then Ninit/5 for each of the 5 layers
                If LCase(dataTable.Rows(0).Item("soilOption")) = "simple" Then
                    fileContent.AppendLine(FormatNumber(dataTable.Rows(0).Item("Ninit"), 1).ToString.PadLeft(5) & " 0.0 0.0 0.0 0.0")
                Else
                    For i = 0 To 4
                        If i < jeu.Tables(0).Rows.Count Then
                            fileContent.Append(FormatNumber(dataTable.Rows(0).Item("Ninit") / jeu.Tables(0).Rows.Count, 1).ToString.PadLeft(5))
                        Else
                            fileContent.Append(" 0.0")
                        End If
                    Next
                    fileContent.AppendLine()
                End If
                'rw = DT.Select("Champ='NO3initf'")
                'fileContent.AppendLine(rw(0)("dv").ToString & " 0.0 0.0 0.0 0.0")
                fileContent.AppendLine(":NH4init:")
                rw = DT.Select("Champ='NH4initf'")
                fileContent.AppendLine(rw(0)("dv").ToString & " 0.0 0.0 0.0 0.0")
            Next
            fileContent.AppendLine()
        End Using

        Try
            ' Export file to specified directory
            WriteFile(DirectoryPath, fileName, fileContent.ToString())
        Catch ex As Exception
            MessageBox.Show("Error during writing file : " + ex.Message)
        End Try
        'connection.Close()
        'MI_connection.Close()
    End Sub
    Public Overrides Sub Import(DirectoryPath As String, model As String)

    End Sub
End Class


