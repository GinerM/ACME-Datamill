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

Public Class SticsClimatConverter
    Inherits Converter


    Public Overrides Sub Export(DirectoryPath As String, Idsim As String, connection As OleDb.OleDbConnection, MI_connection As OleDb.OleDbConnection)

        ' Dim i As Integer
        Dim Dv As String
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
        If Not FCChek And IO.Directory.Exists(DirectoryPath) Then Exit Sub
        Dim fileName As String = "climat.txt"
        Dim fileContent As StringBuilder = New StringBuilder()
        Dim ST(10) As String
        Dim Site, Year As String
        ST = Idsim.Split("\")
        'DirectoryPath = ST(0) & "\" & ST(1) & "\" & ST(2) & "\" & ST(3) & "\" & ST(4) & "\" & ST(5) & "\" & ST(6) & "\" & ST(7)
        Site = ST(0)
        Year = ST(1)
        'ST = Year.Split(".")
        'Year = ST(1)
        Dim T As String = "Select   Variables.Champ, Variables.Default_Value_Datamill, Variables.defaultValueOtherSource, IIf(IsNull([defaultValueOtherSource]),[Default_Value_Datamill],[defaultValueOtherSource]) As dv From Variables Where (((Variables.model) = 'stics') And ((Variables.Table) = 'st_climat'));"
        Dim DT As New DataTable
        Dim rw() As DataRow
        Dim Cmd As New OleDb.OleDbDataAdapter(T, connection)
        Cmd.Fill(DT) ', "TChamp")
        'Climat query
        Dim fetchAllQuery As String
        Dim dataSet As New DataSet()
        Dim dataTable As New DataTable()
        Dim jour As String
        Dim mois As String
        Dim jjulien As String
        Dim mintemp As String
        Dim maxtemp As String
        Dim gradiation As String
        Dim ppet As String
        Dim precipitation As String
        Dim vent As String
        'For i = 0 To 1
        fetchAllQuery = "select * from RaClimateD where idPoint='" + Site + "' And (Year=" & Year & " or Year=" & Year + 1 & ") Order by w_date;"
        fileContent.Clear()
        'Init and use DataAdapter
        Using dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter(fetchAllQuery, MI_connection)
            ' Filling Dataset
            'dataSet = New DataSet
            'dataSet.Clear()
            dataTable.Clear()
            dataAdapter.Fill(dataTable) ', "st_climat")
            'dataTable = dataSet.Tables("st_climat")
            'read all line of Climat
            For Each row In dataTable.Rows
                fileContent.Append(row.item("IdPoint"))
                fileContent.Append(Chr(32))
                fileContent.Append(row.item("year"))
                'Mois
                mois = row.item("Nmonth")
                fileContent.Append(mois.PadLeft(3))
                'jour
                jour = row.item("NDayM")
                fileContent.Append(jour.PadLeft(3))
                'jour julien
                jjulien = (DateDiff(DateInterval.Day, CDate("01/01/" & row.item("year")), CDate(jour & "/" & mois & "/" & row.item("year"))) + 1)
                'jjulien = (DateDiff(DateInterval.Day, "#01/01/" & row.item("year") & "#", "#" moisjour & "/" & mois & "/" & row.item("year"))) + 1)
                fileContent.Append(jjulien.PadLeft(4))
                'minTemp
                mintemp = row.item("tmin")
                fileContent.Append(FormatNumber(mintemp, 1).PadLeft(7))
                'maxTemp
                maxtemp = row.item("tmax")
                fileContent.Append(FormatNumber(maxtemp, 1).PadLeft(7))
                'gradiation
                gradiation = row.item("srad")
                fileContent.Append(FormatNumber(gradiation, 3).PadLeft(7))
                'ppet
                'sline = sline & " " & FormatNumber(CDbl(Mid(L1, 35)) - 273.15, 3,,,).PadLeft(7, " ")
                ppet = row.item("EtpPM")
                fileContent.Append(FormatNumber(ppet, 3,,,).PadLeft(7))
                'precipitation
                precipitation = row.item("rain")
                fileContent.Append(FormatNumber(precipitation, 1).PadLeft(7))
                'vent
                vent = row.item("wind")
                fileContent.Append(FormatNumber(vent, 3,,,).PadLeft(7))
                'vapeurp
                rw = DT.Select("Champ='vapeurp'")
                Dv = rw(0)("dv").ToString
                fileContent.Append(Dv.PadLeft(7))
                'co2
                rw = DT.Select("Champ='co2'")
                Dv = rw(0)("dv").ToString
                fileContent.Append(Dv.PadLeft(7))
                fileContent.AppendLine()
            Next

        End Using

        Try
            ' Export file to specified directory
            WriteFile(DirectoryPath, fileName, fileContent.ToString())
        Catch ex As Exception
            MessageBox.Show("Error during writing file : " + ex.Message)
        End Try
        'Connection.Close()
        'MI_Connection.Close()
        'Next
    End Sub

    Public Overrides Sub Import(DirectoryPath As String, model As String)

    End Sub
End Class


