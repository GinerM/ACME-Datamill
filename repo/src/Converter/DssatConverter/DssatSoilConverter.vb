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
Public Class DssatSoilConverter
    Inherits Converter


    Public Overrides Sub Export(DirectoryPath As String, Idsim As String, connection As OleDb.OleDbConnection, MI_connection As OleDb.OleDbConnection)
        'Init Connection with connection string from app.config

        'Dim Connection As New OleDb.OleDbConnection
        'Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\ModelsDictionaryArise.accdb"
        'Dim MI_Connection = New OleDb.OleDbConnection()
        'MI_Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\MasterInput.accdb"
        'Application.DoEvents()
        'Try
        '    'While Connection.State <> ConnectionState.Open
        '    Connection.Open()
        '    'End While

        '    'While MI_Connection.State <> ConnectionState.Open
        '    MI_Connection.Open()
        '    'End While
        'Catch ex As Exception
        '    MessageBox.Show("Connection Error3 : " + ex.Message)
        'End Try
        Dim idSoil As String
        Dim i As Integer
        Dim ST(11) As String
        Dim Site, Year, Mngt As String
        ST = Idsim.Split("\")
        'DirectoryPath = ST(0) & "\" & ST(1) & "\" & ST(2) & "\" & ST(3) & "\" & ST(4) & "\" & ST(5) & "\" & ST(6) & "\" & ST(7)
        Idsim = ST(0)
        idSoil = ST(1)
        'Year = ST(9)
        'ST = Year.Split(".")
        Site = ST(2)
        Year = ST(3)
        Mngt = Mid(ST(4), 1, 4)
        Dim T As String = "Select Variables.Champ, Variables.Default_Value_Datamill, Variables.defaultValueOtherSource, IIf(IsNull([defaultValueOtherSource]),[Default_Value_Datamill],[defaultValueOtherSource]) As dv From Variables Where (((Variables.model) = 'dssat') And ((Variables.Table) like 'dssat_soil_%'));"

        Dim DT As New DataSet()
        Dim Dv As String
        Dim rw() As DataRow
        Dim Cmd As New OleDb.OleDbDataAdapter(T, connection)
        Cmd.Fill(DT, "TChamp")      'weather_site query
        'Dim fetchAllQuery As String = "select * from Coordinates where IdPoint='" & Site & "';"
        Dim fetchAllQuery As String = "SELECT DISTINCT Coordinates.*, RunoffTypes.CurveNumber, Soil.albedo " _
        & "From Coordinates INNER Join ((RunoffTypes INNER Join Soil On RunoffTypes.RunoffType = Soil.RunoffType) " _
        & "INNER Join SimUnitList On Soil.IdSoil = SimUnitList.idsoil) ON Coordinates.idPoint = SimUnitList.idPoint where SimUnitList.IdSim='" & Idsim & "';"
        Dim fileContent As StringBuilder = New StringBuilder()
        'Init and use DataAdapter
        Dim fileName As String = ""
        Using dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter(fetchAllQuery, MI_connection)
            ' Filling Dataset
            Dim dataSet As New DataSet()
            dataAdapter.Fill(dataSet, "dssat_soil_site")
            Dim dataTable As DataTable = dataSet.Tables("dssat_soil_site")
            'read all line of dssat_weather_site
            For Each row In dataTable.Rows
                'fileContent.Append(FileHeader) ' Write File header
                rw = DT.Tables(0).Select("Champ='filename'")
                Dv = rw(0)("dv").ToString
                'fileContent.Append(Dv.PadLeft(5))
                fileName = "XX.SOL"
                'idData = row.item("id")
                Dim siteColumnsHeader1 As String = "@SITE        COUNTRY          LAT     LONG SCS FAMILY"
                Dim siteColumnsHeader2 As String = "@ SCOM  SALB  SLU1  SLDR  SLRO  SLNF  SLPF  SMHB  SMPX  SMKE"
                'fileContent.AppendLine() ' Append a line break
                'fileContent.Append("*Soils: " & row.item("latitudeDD") & " " & row.item("LongitudeDD") & " ISRIC AfricaSoilGrid")
                fileContent.Append("*Soils: Mali")
                fileContent.AppendLine()
                fileContent.Append("*XX" & Mngt & "0101")
                fileContent.AppendLine()
                fileContent.Append(siteColumnsHeader1)
                fileContent.AppendLine()
                fileContent.Append(" ")
                fileContent.Append("XX" & Mngt & "0101   ") 'site
                'fileContent.Append(row.item("idpoint").padleft(13))
                fileContent.Append(" ")
                rw = DT.Tables(0).Select("Champ='Country'")
                Dv = rw(0)("dv").ToString
                fileContent.Append(Dv.PadLeft(10))
                'fileContent.Append(Chr(9))
                fileContent.Append(row.item("latitudeDD").ToString.PadLeft(8))
                'fileContent.Append(Chr(9))
                fileContent.Append(row.item("longitudeDD").ToString.PadLeft(8))
                fileContent.Append("  ")
                rw = DT.Tables(0).Select("Champ='scs family'")
                Dv = rw(0)("dv").ToString
                fileContent.Append(Dv.PadLeft(25))
                fileContent.AppendLine() ' Append a line break.
                fileContent.Append(siteColumnsHeader2)
                fileContent.AppendLine()
                fileContent.Append(" ")
                rw = DT.Tables(0).Select("Champ='scom'")
                Dv = rw(0)("dv").ToString
                fileContent.Append(Dv.PadLeft(5))
                'fileContent.Append(formatItem_Lg(row.item("scom"), 5))
                'fileContent.Append(Chr(9))
                'salb
                fileContent.Append(row.item("albedo").ToString.PadLeft(6))
                rw = DT.Tables(0).Select("Champ='slu1'")
                Dv = rw(0)("dv").ToString
                fileContent.Append(Dv.PadLeft(6))
                'fileContent.Append(formatItem_Lg(row.item("slu1"), 6))
                'fileContent.Append(Chr(9))
                rw = DT.Tables(0).Select("Champ='sldr'")
                Dv = rw(0)("dv").ToString
                fileContent.Append(Dv.PadLeft(6))
                'fileContent.Append(formatItem_Lg(row.item("sldr"), 6))
                'fileContent.Append(Chr(9))
                'slro
                fileContent.Append(row.item("Curvenumber").ToString.PadLeft(6))
                'fileContent.Append(formatItem_Lg(row.item("slro"), 6))
                'fileContent.Append(Chr(9))
                rw = DT.Tables(0).Select("Champ='slnf'")
                Dv = rw(0)("dv").ToString
                fileContent.Append(Dv.PadLeft(6))
                'fileContent.Append(formatItem_Lg(row.item("slnf"), 6))
                'fileContent.Append(Chr(9))
                rw = DT.Tables(0).Select("Champ='slpf'")
                Dv = rw(0)("dv").ToString
                fileContent.Append(Dv.PadLeft(6))
                'fileContent.Append(formatItem_Lg(row.item("slpf"), 6))
                fileContent.Append(" ")
                rw = DT.Tables(0).Select("Champ='smhb'")
                Dv = rw(0)("dv").ToString
                fileContent.Append(Dv.PadLeft(5))
                'fileContent.Append(formatItem_Lg(row.item("smhb"), 5))
                fileContent.Append(" ")
                rw = DT.Tables(0).Select("Champ='smpx'")
                Dv = rw(0)("dv").ToString
                fileContent.Append(Dv.PadLeft(5))
                'fileContent.Append(formatItem_Lg(row.item("smpx"), 5))
                fileContent.Append(" ")
                rw = DT.Tables(0).Select("Champ='smke'")
                Dv = rw(0)("dv").ToString
                fileContent.Append(Dv.PadLeft(5))
                'fileContent.Append(formatItem_Lg(row.item("smke"), 5))
                fileContent.AppendLine()

                'soildata
                Dim dataColumnsHeader As String = "@  SLB  SLMH  SLLL  SDUL  SSAT  SRGF  SSKS  SBDM  SLOC  SLCL  SLSI  SLCF  SLNI  SLHW  SLHB  SCEC  SADC"
                fileContent.Append(String.Join(Chr(9), dataColumnsHeader))
                fileContent.AppendLine() ' Append a line break.

                'Init and use DataAdapter
                'Dim fetchAllQuery1 As String = "SELECT Soil.*, SoilTypes.* FROM SoilTypes INNER JOIN Soil ON SoilTypes.SoilTextureType = Soil.SoilTextureType where Soil.idSoil = '" + idSoil + "' ;"
                Dim fetchAllQuery1 As String = "Select Soil.*, SoilLayers.*, SoilTypes.* FROM(SoilTypes INNER JOIN Soil On SoilTypes.SoilTextureType = Soil.SoilTextureType) LEFT JOIN SoilLayers On Soil.IdSoil = SoilLayers.idsoil where Soil.idSoil = '" + idSoil + "' ;"
                ' MsgBox(fetchAllQuery1)
                Using dataAdapter1 As OleDb.OleDbDataAdapter = New OleDbDataAdapter(fetchAllQuery1, MI_connection)
                    Dim dataSet1 As New DataSet()
                    Dim occurence As DataRow
                    dataAdapter1.Fill(dataSet1, "dssat_soil_data")
                    Dim dataTable1 As DataTable = dataSet1.Tables("dssat_soil_data")
                    If LCase(dataTable1.Rows(0).Item("soiloption")) = "simple" Then
                        ''read all line of dssat_weather_site
                        occurence = dataTable1.Rows(0)
                        For i = 0 To 1 ' Each occurence As DataRow In dataTable1.Rows
                            'slb
                            If i = 0 Then
                                fileContent.Append("30".PadLeft(6))
                            Else
                                fileContent.Append(occurence.Item("SoilTotalDepth").ToString.PadLeft(6))
                            End If
                            fileContent.Append(" ")
                            'slmh
                            rw = DT.Tables(0).Select("Champ='slmh'")
                            Dv = rw(0)("dv").ToString
                            fileContent.Append(Dv.PadLeft(5))
                            'slll
                            fileContent.Append(FormatNumber((occurence.Item("Soil.Wwp") / 100), 3).ToString.PadLeft(6))
                            'sdul
                            fileContent.Append(FormatNumber((occurence.Item("Soil.Wfc") / 100), 3).ToString.PadLeft(6))
                            'ssat
                            rw = DT.Tables(0).Select("Champ='ssat'")
                            Dv = rw(0)("dv").ToString
                            fileContent.Append(Dv.PadLeft(6))
                            'srgf
                            rw = DT.Tables(0).Select("Champ='srgf'")
                            Dv = rw(0)("dv").ToString
                            fileContent.Append(Dv.PadLeft(6))
                            'ssks
                            rw = DT.Tables(0).Select("Champ='ssks'")
                            Dv = rw(0)("dv").ToString
                            fileContent.Append(Dv.PadLeft(6))
                            'sbdm
                            fileContent.Append(FormatNumber(occurence.Item("Soil.bd"), 3).ToString.PadLeft(6))
                            'sloc
                            If i = 0 Then
                                fileContent.Append(FormatNumber(occurence.Item("Soil.OrganicC"), 3).ToString.PadLeft(6))
                            Else
                                fileContent.Append("0".PadLeft(6))
                            End If
                            'slcl
                            fileContent.Append(occurence.Item("Soiltypes.Clay").ToString.PadLeft(6))
                            'slsi
                            fileContent.Append(occurence.Item("SoilTypes.Silt").ToString.PadLeft(6))
                            'slcf
                            fileContent.Append(occurence.Item("Soil.Cf").ToString.PadLeft(6))
                            'slni
                            If i = 0 Then
                                fileContent.Append(occurence.Item("OrganicNStock").ToString.PadLeft(6))
                            Else
                                fileContent.Append("0".PadLeft(6))
                            End If
                            'slhw
                            fileContent.Append(occurence.Item("Soil.pH").ToString.PadLeft(6))
                            'slhb
                            rw = DT.Tables(0).Select("Champ='slhb'")
                            Dv = rw(0)("dv").ToString
                            fileContent.Append(Dv.PadLeft(6))
                            'scec
                            rw = DT.Tables(0).Select("Champ='scec'")
                            Dv = rw(0)("dv").ToString
                            fileContent.Append(Dv.PadLeft(6))
                            'sadc
                            rw = DT.Tables(0).Select("Champ='sadc'")
                            Dv = rw(0)("dv").ToString
                            fileContent.Append(Dv.PadLeft(6))

                            fileContent.AppendLine()
                        Next
                        '                        fileContent.AppendLine()
                    Else
                        For Each occurence1 As DataRow In dataTable1.Rows
                            'slb
                            fileContent.Append(occurence1.Item("Ldown").ToString.PadLeft(6))
                            fileContent.Append(" ")
                            'slmh
                            rw = DT.Tables(0).Select("Champ='slmh'")
                            Dv = rw(0)("dv").ToString
                            fileContent.Append(Dv.PadLeft(5))
                            'slll
                            fileContent.Append(FormatNumber((occurence1.Item("SoilLayers.Wwp") / 100), 3).ToString.PadLeft(6))
                            'sdul
                            fileContent.Append(FormatNumber((occurence1.Item("SoilLayers.Wfc") / 100), 3).ToString.PadLeft(6))
                            'ssat
                            rw = DT.Tables(0).Select("Champ='ssat'")
                            Dv = rw(0)("dv").ToString
                            fileContent.Append(Dv.PadLeft(6))
                            'srgf
                            rw = DT.Tables(0).Select("Champ='srgf'")
                            Dv = rw(0)("dv").ToString
                            fileContent.Append(Dv.PadLeft(6))
                            'ssks
                            rw = DT.Tables(0).Select("Champ='ssks'")
                            Dv = rw(0)("dv").ToString
                            fileContent.Append(Dv.PadLeft(6))
                            'sbdm
                            fileContent.Append(FormatNumber(occurence1.Item("SoilLayers.bd"), 3).ToString.PadLeft(6))
                            'sloc
                            fileContent.Append(FormatNumber(occurence1.Item("SoilLayers.OrganicC"), 3).ToString.PadLeft(6))
                            'slcl
                            fileContent.Append(occurence1.Item("SoilLayers.Clay").ToString.PadLeft(6))
                            'slsi
                            fileContent.Append(occurence1.Item("SoilLayers.Silt").ToString.PadLeft(6))
                            'slcf
                            fileContent.Append(occurence1.Item("SoilLayers.Cf").ToString.PadLeft(6))
                            'slni
                            fileContent.Append(occurence1.Item("TotalN").ToString.PadLeft(6))
                            'slhw
                            fileContent.Append(occurence1.Item("SoilLayers.pH").ToString.PadLeft(6))
                            'slhb
                            rw = DT.Tables(0).Select("Champ='slhb'")
                            Dv = rw(0)("dv").ToString
                            fileContent.Append(Dv.PadLeft(6))
                            'scec
                            rw = DT.Tables(0).Select("Champ='scec'")
                            Dv = rw(0)("dv").ToString
                            fileContent.Append(Dv.PadLeft(6))
                            'sadc
                            rw = DT.Tables(0).Select("Champ='sadc'")
                            Dv = rw(0)("dv").ToString
                            fileContent.Append(Dv.PadLeft(6))

                            fileContent.AppendLine()
                        Next
                    End If
                    fileContent.AppendLine()
                End Using
            Next
            Try
                ' Export file to specified directory
                WriteFile(DirectoryPath, fileName, fileContent.ToString())
            Catch ex As Exception
                MessageBox.Show("Error during writing file " & ex.Message)
            End Try
        End Using
        'Connection.Close()
        'MI_Connection.Close()
    End Sub

    Public Overrides Sub Import(DirectoryPath As String, model As String)

    End Sub
End Class
