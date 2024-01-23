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

Public Class SticsStationConverter
    Inherits Converter
    Public Overrides Sub Export(DirectoryPath As String, Idsim As String, connection As OleDb.OleDbConnection, MI_connection As OleDb.OleDbConnection)
        Dim fileName As String = "station.txt"
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
        'Station query
        Dim T As String = "Select   Variables.Champ, Variables.Default_Value_Datamill, Variables.defaultValueOtherSource, IIf(IsNull([defaultValueOtherSource]),[Default_Value_Datamill],[defaultValueOtherSource]) As dv From Variables Where (((Variables.model) = 'stics') And ((Variables.Table) = 'st_station'));"
        Dim DT As New DataTable

        Dim Cmd As New OleDb.OleDbDataAdapter(T, connection)
        Cmd.Fill(DT) ', "TChamp")
        Dim fetchAllQuery As String = "SELECT SimUnitList.idsim, Coordinates.altitude, Coordinates.latitudeDD FROM Coordinates INNER JOIN SimUnitList ON Coordinates.idPoint = SimUnitList.idPoint where idSim='" + Idsim + "' ;"

        'Init and use DataAdapter
        Using dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter(fetchAllQuery, MI_connection)

            ' Filling Dataset
            Dim dataSet As New DataSet()
            dataAdapter.Fill(dataSet, "st_station")
            Dim dataTable As DataTable = dataSet.Tables("st_station")


            'read all line of st_station
            For Each row In dataTable.Rows

                FormatSticsData(fileContent, DT, "zr")
                FormatSticsData(fileContent, DT, "NH3ref")
                fileContent.AppendLine("latitude")
                fileContent.AppendLine(row.item("latitudeDD"))

                FormatSticsData(fileContent, DT, "patm")
                FormatSticsData(fileContent, DT, "aclim", 6)
                FormatSticsData(fileContent, DT, "codeetp")
                FormatSticsData(fileContent, DT, "alphapt")
                FormatSticsData(fileContent, DT, "codeclichange")
                FormatSticsData(fileContent, DT, "codaltitude")
                FormatSticsData(fileContent, DT, "altistation")
                'FormatSticsData(fileContent, DT, "altisimul")
                fileContent.AppendLine("altisimul")
                If IsDBNull(row.item("altitude")) Then
                    fileContent.AppendLine("-99")
                Else
                    fileContent.AppendLine(row.item("altitude"))
                End If

                FormatSticsData(fileContent, DT, "gradtn")
                FormatSticsData(fileContent, DT, "gradtx")
                FormatSticsData(fileContent, DT, "altinversion")
                FormatSticsData(fileContent, DT, "gradtninv")
                FormatSticsData(fileContent, DT, "cielclair")
                FormatSticsData(fileContent, DT, "codadret")
                FormatSticsData(fileContent, DT, "ombragetx")
                FormatSticsData(fileContent, DT, "ra")
                FormatSticsData(fileContent, DT, "albveg")
                FormatSticsData(fileContent, DT, "aangst")
                FormatSticsData(fileContent, DT, "bangst")
                FormatSticsData(fileContent, DT, "corecTrosee")
                FormatSticsData(fileContent, DT, "codecaltemp")
                FormatSticsData(fileContent, DT, "codernet")
                FormatSticsData(fileContent, DT, "coefdevil")
                FormatSticsData(fileContent, DT, "aks")
                FormatSticsData(fileContent, DT, "bks")
                FormatSticsData(fileContent, DT, "cvent")
                FormatSticsData(fileContent, DT, "phiv0")
                FormatSticsData(fileContent, DT, "coefrnet")

            Next

        End Using

        Try
            ' Export file to specified directory
            WriteFile(DirectoryPath, fileName, fileContent.ToString())
        Catch ex As Exception
            MessageBox.Show("Error during writing file : " + ex.Message)
        End Try

        fileName = "snow_variables.txt"
        fileContent = New StringBuilder()
        fileContent.Append("   0.00000000       0.00000000       0.00000000       0.00000000 ")
        fileContent.AppendLine()
        Try
            ' Export file to specified directory
            WriteFile(DirectoryPath, fileName, fileContent.ToString())
        Catch ex As Exception
            MessageBox.Show("Error during writing file : " + ex.Message)
        End Try
        fileName = "prof.mod"
        fileContent = New StringBuilder()
        fileContent.AppendLine("2")
        fileContent.AppendLine("tsol(iz)")
        fileContent.AppendLine("10")
        fileContent.AppendLine("01 01 2000")
        Try
            ' Export file to specified directory
            WriteFile(DirectoryPath, fileName, fileContent.ToString())
        Catch ex As Exception
            MessageBox.Show("Error during writing file : " + ex.Message)
        End Try
        fileName = "rap.mod"
        fileContent = New StringBuilder()
        fileContent.AppendLine("1")
        fileContent.AppendLine("1")
        fileContent.AppendLine("2")
        fileContent.AppendLine("1")
        fileContent.AppendLine("rec")
        fileContent.AppendLine("masec(n)")
        fileContent.AppendLine("mafruit")
        fileContent.AppendLine("chargefruit")
        fileContent.AppendLine("iplts")
        fileContent.AppendLine("ilevs")
        fileContent.AppendLine("iflos")
        fileContent.AppendLine("imats")
        fileContent.AppendLine("irecs")
        fileContent.AppendLine("laimax")
        fileContent.AppendLine("QNplante")
        fileContent.AppendLine("Qles")
        fileContent.AppendLine("QNapp") '        fileContent.AppendLine("soilN")
        fileContent.AppendLine("ces")
        fileContent.AppendLine("cep")
        Try
            ' Export file to specified directory
            WriteFile(DirectoryPath, fileName, fileContent.ToString())
        Catch ex As Exception
            MessageBox.Show("Error during writing file : " + ex.Message)
        End Try
        'fileName = "var.mod"
        'fileContent = New StringBuilder()
        'fileContent.AppendLine("lai(n)")
        'fileContent.AppendLine("masec(n)")
        'fileContent.AppendLine("mafruit")
        'fileContent.AppendLine("HR(1)")
        'fileContent.AppendLine("HR(2)")
        'fileContent.AppendLine("HR(3)")
        'fileContent.AppendLine("HR(4)")
        'fileContent.AppendLine("HR(5)")
        'fileContent.AppendLine("resmes")
        'fileContent.AppendLine("drain")
        'fileContent.AppendLine("esol")
        'fileContent.AppendLine("et")
        'fileContent.AppendLine("zrac")
        'fileContent.AppendLine("tcult")
        'fileContent.AppendLine("AZnit(1)")
        'fileContent.AppendLine("AZnit(2)")
        'fileContent.AppendLine("AZnit(3)")
        'fileContent.AppendLine("AZnit(4)")
        'fileContent.AppendLine("AZnit(5)")
        'fileContent.AppendLine("Qles")
        'fileContent.AppendLine("QNplante")
        'fileContent.AppendLine("azomes")
        'fileContent.AppendLine("inn")
        'fileContent.AppendLine("chargefruit")
        'fileContent.AppendLine("AZamm(1)")
        'fileContent.AppendLine("AZamm(2)")
        'fileContent.AppendLine("AZamm(3)")
        'fileContent.AppendLine("AZamm(4)")
        'fileContent.AppendLine("AZamm(5)")
        ''fileContent.AppendLine("leaching_from_plt")
        'fileContent.AppendLine("CNgrain")
        'fileContent.AppendLine("concNO3les")
        'fileContent.AppendLine("drat")
        'fileContent.AppendLine("fapar")
        'fileContent.AppendLine("hauteur")
        'fileContent.AppendLine("Hmax")
        'fileContent.AppendLine("humidite")
        'fileContent.AppendLine("LRACH(1)")
        'fileContent.AppendLine("LRACH(2)")
        'fileContent.AppendLine("LRACH(3)")
        'fileContent.AppendLine("LRACH(4)")
        'fileContent.AppendLine("LRACH(5)")
        'fileContent.AppendLine("mafrais")
        'fileContent.AppendLine("pdsfruitfrais")
        'fileContent.AppendLine("Qdrain")
        'fileContent.AppendLine("rnet")
        'fileContent.AppendLine("QNapp")
        ''fileContent.AppendLine("soilN")
        'fileContent.AppendLine("ces")
        'fileContent.AppendLine("cep")
        ''fileContent.AppendLine("QNplante")
        ''fileContent.AppendLine("Qles")
        ''fileContent.AppendLine("soilN")
        'Try
        '    ' Export file to specified directory
        '    WriteFile(DirectoryPath, fileName, fileContent.ToString())
        'Catch ex As Exception
        '    MessageBox.Show("Error during writing file : " + ex.Message)
        'End Try
        'Connection.Close()
        'MI_Connection.Close()
    End Sub

    Public Sub FormatSticsData(ByRef fileContent As StringBuilder, ByRef row As Object, ByVal champ As String, Optional ByVal precision As Integer = 5, Optional ByVal fieldIt As Integer = 0)
        Dim res As String
        Dim typeData As String
        Dim rw() As DataRow
        Dim data As Object
        Dim fieldName As String

        fieldName = champ
        'For repeated fields, build field name 
        If (fieldIt <> 0) Then
            champ = champ + fieldIt.ToString()
        End If

        'fetch data
        rw = row.select("Champ='" & champ & "'")
        data = rw(0)("dv")
        res = ""
        typeData = data.GetType().ToString()

        'if type is string or int
        If ((typeData = "System.String") Or (typeData = "System.Int32")) Then
            res = data.ToString()
        End If
        'if type is real
        If (typeData = "System.Single") Then
            Dim tmp As Single
            'Convert object to double
            tmp = Convert.ToDouble(data)
            If precision > 0 And precision < 7 Then
                res = FormatNumber(tmp, precision)
            Else
                res = tmp.ToString("0.###e+0", CultureInfo.InvariantCulture)
            End If
        End If
        'if cell is null
        If (typeData = "System.DBNull") Then
            res = ""
        End If
        'Print data in file
        fileContent.Append(fieldName)
        fileContent.AppendLine()
        fileContent.Append(res)
        fileContent.AppendLine()
    End Sub

    Public Overrides Sub Import(DirectoryPath As String, model As String)

    End Sub
End Class
