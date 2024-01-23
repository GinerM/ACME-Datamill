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


''' <summary>
''' specific converter : dssat weather
''' </summary>
''' <remarks>
''' 2 datatables are exported :
''' dssat_weather_site and dssat_weather_data
''' </remarks>
Public Class DssatWeatherConverter
    Inherits Converter


    Public Overrides Sub Export(DirectoryPath As String, idSim As String, connection As OleDb.OleDbConnection, MI_connection As OleDb.OleDbConnection)
        'Init Connection with connection string from app.config
        'Dim Connection As New OleDb.OleDbConnection
        'Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\ModelsDictionaryArise.accdb"
        'Dim MI_Connection = New OleDb.OleDbConnection()
        'MI_Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\MasterInput.accdb"
        'Application.DoEvents()
        'Try
        '    'Open DB connection
        '    'Do
        '    'While Connection.State <> ConnectionState.Open
        '    Connection.Open()
        '    'End While

        '    'While MI_Connection.State <> ConnectionState.Open
        '    MI_Connection.Open()
        '    'End While
        '    'Loop Until (Connection.State = ConnectionState.Open) And (MI_Connection.State = ConnectionState.Open)
        'Catch ex As Exception
        '    MessageBox.Show("Connection Error4 : " + ex.Message + " " + Connection.State.ToString + " " + MI_Connection.State.ToString)
        'End Try
        If Not FCChek And IO.Directory.Exists(DirectoryPath) Then Exit Sub

        Dim ST(10) As String
        Dim Site, Year, Mngt As String
        ST = DirectoryPath.Split("\")
        'DirectoryPath = ST(0) & "\" & ST(1) & "\" & ST(2) & "\" & ST(3) & "\" & ST(4) & "\" & ST(5) & "\" & ST(6) & "\" & ST(7)
        ST = idSim.Split("\")
        Site = ST(0)
        'Site.Replace(".", "_")
        Year = ST(1)
        Mngt = Mid(ST(2), 1, 4)
        'ST = Year.Split(".")
        'Year = ST(1)
        'weather_site query
        Dim T As String = "Select Variables.Champ, Variables.Default_Value_Datamill, Variables.defaultValueOtherSource, IIf(IsNull([defaultValueOtherSource]),[Default_Value_Datamill],[defaultValueOtherSource]) As dv From Variables Where (((Variables.model) = 'dssat') And ((Variables.Table) = 'dssat_weather_site'));"

        Dim DT As New DataSet()
        Dim Dv As String
        Dim rw() As DataRow
        Dim Cmd As New OleDb.OleDbDataAdapter(T, connection)
        Cmd.Fill(DT, "TChamp")
        Dim fetchAllQuery = "select * from Coordinates where idPoint='" + Site + "';"
        'Dim fetchAllQuery As String = "select * from dssat_weather_site where filename='" & ST(3) & "';"
        Dim Tdew As Single
        Dim fileNameArray(3) As String
        fileNameArray(0) = ""
        fileNameArray(1) = "00"
        fileNameArray(2) = "01"
        fileNameArray(3) = ".WTH"
        For i = 0 To 1
            Year = Year + i
            'Init and use DataAdapter
            'Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter(fetchAllQuery, MI_Connection)
            Using dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter(fetchAllQuery, MI_connection)
                ' Filling Dataset
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "dssat_weather_site")
                Dim dataTable As DataTable = dataSet.Tables("dssat_weather_site")
                Dim fileName As String = ""
                'read all line of dssat_weather_site
                For Each row In dataTable.Rows
                    Dim fileContent As StringBuilder = New StringBuilder()
                    'filename is composed by "insi"+"aa"+"number, usually 01
                    'aa is deducted from "date" : fisrt two characters
                    Dim siteColumnsHeader() As String = {"@", "INSI", "     LAT", "    LONG", " ELEV", "  TAV", "  AMP", "REFHT", "WNDHT"}

                    fileContent.Append("*WEATHER DATA : " & Site & "," & Year)
                    'fileContent.Append(row.item("header_weather_data"))
                    fileContent.AppendLine() ' Append a line break.
                    fileContent.Append(String.Join(Chr(32), siteColumnsHeader))
                    fileContent.AppendLine() ' Append a line break.

                    fileContent.Append(Chr(32))
                    fileContent.Append(Chr(32))
                    'fileContent.Append(formatItem_Lg((row.item("insi")), 6))
                    fileContent.Append(Mid(Site, 1, 4).PadRight(6)) 'formatItem_Lg((row.item("insi")), 6))
                    fileNameArray(0) = Mngt 'Mid(Site, 1, 4) 'formatItem(row.item("insi"))
                    fileNameArray(1) = Mid(Year, 3, 2)
                    fileContent.Append(Chr(32))
                    fileContent.Append(row.item("latitudeDD").ToString.PadLeft(6))
                    fileContent.Append(Chr(32))
                    fileContent.Append(row.item("longitudeDD").ToString.PadLeft(8))
                    fileContent.Append(Chr(32))
                    fileContent.Append(row.item("altitude").ToString.PadLeft(5))
                    'fileContent.Append(Chr(9))
                    fileContent.Append(Chr(32))
                    rw = DT.Tables(0).Select("Champ='tav'")
                    Dv = rw(0)("dv").ToString
                    fileContent.Append(Dv.PadLeft(5))
                    fileContent.Append(Chr(32))
                    rw = DT.Tables(0).Select("Champ='amp'")
                    Dv = rw(0)("dv").ToString
                    fileContent.Append(Dv.PadLeft(5))
                    fileContent.Append(Chr(32))
                    rw = DT.Tables(0).Select("Champ='refht'")
                    Dv = rw(0)("dv").ToString
                    fileContent.Append(Dv.PadLeft(5))
                    'fileContent.Append(Chr(9))
                    fileContent.Append(Chr(32))
                    rw = DT.Tables(0).Select("Champ='wndht'")
                    Dv = rw(0)("dv").ToString
                    fileContent.Append(Dv.PadLeft(5))
                    fileContent.AppendLine() ' Append a line break.

                    'weatherdata
                    Dim dataColumnsHeader() As String = {"@DATE", " SRAD", " TMAX", "  TMIN", " RAIN", " DEWP", " WIND", "  PAR", " EVAP", " RHUM"}
                    fileContent.Append(String.Join(Chr(32), dataColumnsHeader))
                    fileContent.AppendLine() ' Append a line break.
                    'Init and use DataAdapter
                    Dim fetchAllQuery1 As String = "select * from RaClimateD where idPoint='" + Site + "' And Year=" & Year & " Order by w_date;"

                    Using dataAdapter1 As OleDb.OleDbDataAdapter = New OleDbDataAdapter(fetchAllQuery1, MI_connection)
                        'Dim dataAdapter1 = New OleDbDataAdapter(fetchAllQuery1, MI_Connection)
                        Dim dataSet1 As New DataSet()
                        dataAdapter1.Fill(dataSet1, "dssat_weather_data")
                        Dim dataTable1 As DataTable = dataSet1.Tables("dssat_weather_data")
                        'MsgBox(Site & " " & Year & " " & dataTable1.Rows.Count & " " & fetchAllQuery1)
                        fileNameArray(2) = "01" '(i + 1).ToString.PadLeft(2, "0")
                        ''read all line of dssat_weather_site
                        For Each occurence As DataRow In dataTable1.Rows
                            fileContent.AppendLine() ' Append a line break.
                            'fileContent.Append(formatItem(occurence.Item("date")))
                            fileContent.Append(Mid(occurence.Item("year").ToString, 3, 2) & occurence.Item("doy").ToString.PadLeft(3, "0"))
                            'store fisrt two years
                            'fileNameArray(1) = formatItem(occurence.Item("date"))

                            fileContent.Append(Chr(32))
                            fileContent.Append(FormatNumber(occurence.Item("srad"), 2).ToString.PadLeft(5))
                            fileContent.Append(Chr(32))
                            fileContent.Append(occurence.Item("tmax").ToString.PadLeft(5))
                            fileContent.Append(Chr(32))
                            fileContent.Append(occurence.Item("tmin").ToString.PadLeft(6))
                            fileContent.Append(Chr(32))
                            fileContent.Append(FormatNumber(occurence.Item("rain"), 2).ToString.PadLeft(5))
                            fileContent.Append(Chr(32))
                            If IsDBNull(occurence.Item("Tdewmin")) Or IsDBNull(occurence.Item("Tdewmin")) Then
                                fileContent.Append("-999".PadLeft(5))
                            Else
                                Tdew = (CSng(occurence.Item("Tdewmin")) + CSng(occurence.Item("Tdewmax"))) / 2
                                fileContent.Append(FormatNumber(Tdew, 2).ToString.PadLeft(5)) 'dewp
                            End If
                            fileContent.Append(Chr(32))
                            fileContent.Append(FormatNumber(occurence.Item("wind") * 86.4, 1).PadLeft(5))
                            fileContent.Append(Chr(32))
                            fileContent.Append("     ") 'par
                            fileContent.Append(Chr(32))
                            fileContent.Append("     ") 'evap
                            fileContent.Append(Chr(32))
                            If IsDBNull(occurence.Item("rhum")) Then
                                fileContent.Append("-999".PadLeft(5))
                            Else
                                fileContent.Append(FormatNumber(occurence.Item("rhum"), 2).PadLeft(5))
                            End If
                        Next
                    End Using
                    'file name
                    'fileName = insiValue + yyFile + "01"
                    fileName = fileNameArray(0) + fileNameArray(1) + fileNameArray(2) + fileNameArray(3)
                    'MsgBox(fileName)
                    ' Export file to specified directory
                    WriteFile(DirectoryPath, fileName, fileContent.ToString())
                Next

            End Using
        Next
        'Connection.Close()
        'MI_Connection.Close()
        'Application.DoEvents()
    End Sub

    Public Overrides Sub Import(DirectoryPath As String, model As String)

    End Sub
End Class
