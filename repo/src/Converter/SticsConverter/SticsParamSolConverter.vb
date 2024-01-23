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

Public Class SticsParamSolConverter
    Inherits Converter


    Public Overrides Sub Export(DirectoryPath As String, Idsim As String, connection As OleDb.OleDbConnection, MI_connection As OleDb.OleDbConnection)
        Dim fileName As String = "param.sol"
        Dim fileContent As StringBuilder = New StringBuilder()
        Dim Dv As String
        Dim Sql As String
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
        'Param_sol query
        Dim T As String = "Select   Variables.Champ, Variables.Default_Value_Datamill, Variables.defaultValueOtherSource, IIf(IsNull([defaultValueOtherSource]),[Default_Value_Datamill],[defaultValueOtherSource]) As dv From Variables Where (((Variables.model) = 'stics') And ((Variables.Table) = 'st_param_sol'));"
        Dim DT As New DataTable
        Dim rw() As DataRow
        Dim Cmd As New OleDb.OleDbDataAdapter(T, connection)
        Cmd.Fill(DT) ', "TChamp")
        'Init and use DataAdapter
        Dim fetchAllQuery As String = "SELECT Soil.IdSoil,Soil.SoilOption, Soil.OrganicC,Soil.OrganicNStock, Soil.SoilRDepth, Soil.SoilTotalDepth, Soil.SoilTextureType, Soil.Wwp, Soil.Wfc, Soil.bd, Soil.albedo, Soil.Ph, Soil.cf, RunoffTypes.RunoffCoefBSoil, SoilTypes.Clay" _
        & " FROM SoilTypes INNER JOIN (RunoffTypes INNER JOIN (Soil INNER JOIN SimUnitList ON Soil.IdSoil = SimUnitList.idsoil) ON RunoffTypes.RunoffType = Soil.RunoffType) ON SoilTypes.SoilTextureType = Soil.SoilTextureType" _
        & " where idSim='" + Idsim + "';"
        Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter(fetchAllQuery, MI_connection)
        ' Filling Dataset
        Dim dataSet As New DataSet
        dataAdapter.Fill(dataSet, "st_param_sol")
        Dim dataTable As DataTable = dataSet.Tables("st_param_sol")
        'read all line of st_param_sol
        For Each row In dataTable.Rows
            fileContent.Append("1".PadLeft(5))
            fileContent.Append("Sol".PadLeft(6))
            'rw = DT.Select("Champ='argi'")
            'Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(dataTable.Rows(0).Item("Clay"), 4).PadLeft(8))
            fileContent.Append(FormatNumber(dataTable.Rows(0).Item("OrganicNStock"), 4).PadLeft(8))
            rw = DT.Select("Champ='profhum'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 4).PadLeft(8))

            rw = DT.Select("Champ='calc'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 4).PadLeft(8))

            'rw = DT.Select("Champ='ph'")
            'Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(dataTable.Rows(0).Item("Ph"), 4).PadLeft(8))

            rw = DT.Select("Champ='concseuil'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 4).PadLeft(8))

            'Cmd.CommandText = "select top 1 albedo from St_param_sol"
            'rw = DT.Select("Champ='albedo'")
            'Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(dataTable.Rows(0).Item("Albedo"), 4).PadLeft(8))

            rw = DT.Select("Champ='q0'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 4).PadLeft(8))

            'rw = DT.Select("Champ='ruisolnu'")
            'Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(dataTable.Rows(0).Item("RunoffCoefBSoil"), 4).PadLeft(8))

            fileContent.Append(FormatNumber(dataTable.Rows(0).Item("SoilRDepth"), 4).PadLeft(9))

            rw = DT.Select("Champ='pluiebat'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 4).PadLeft(8))

            rw = DT.Select("Champ='mulchbat'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 4).PadLeft(8))

            rw = DT.Select("Champ='zesx'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 4).PadLeft(8))

            rw = DT.Select("Champ='cfes'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 4).PadLeft(8))

            'Cmd.CommandText = "select top 1 z0solnu from St_param_sol"
            rw = DT.Select("Champ='z0solnu'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 4).PadLeft(8))
            'csurnsol=soil.OrganicC/soil.OrganicNStock
            'rw = DT.Select("Champ='csurnsol'")
            ' Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(dataTable.Rows(0).Item("OrganicC") / dataTable.Rows(0).Item("OrganicNStock"), 4).PadLeft(8))

            rw = DT.Select("Champ='penterui'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 4).PadLeft(8))

            fileContent.AppendLine()

            fileContent.Append("1".PadLeft(5))
            rw = DT.Select("Champ='codecailloux'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 0).PadLeft(4))
            rw = DT.Select("Champ='codemacropor'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 0).PadLeft(2))
            rw = DT.Select("Champ='codefente'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 0).PadLeft(2))
            rw = DT.Select("Champ='codrainage'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 0).PadLeft(2))
            rw = DT.Select("Champ='coderemontcap'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 0).PadLeft(2))
            rw = DT.Select("Champ='codenitrif'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 0).PadLeft(2))
            rw = DT.Select("Champ='codedenit'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 0).PadLeft(2))
            fileContent.AppendLine()

            fileContent.Append("1".PadLeft(5))
            rw = DT.Select("Champ='profimper'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 4).PadLeft(8))
            rw = DT.Select("Champ='ecartdrain'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 4).PadLeft(8))
            rw = DT.Select("Champ='ksol'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 4).PadLeft(8))
            rw = DT.Select("Champ='profdrain'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 4).PadLeft(8))
            rw = DT.Select("Champ='capiljour'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 4).PadLeft(8))
            rw = DT.Select("Champ='humcapil'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 4).PadLeft(8))
            rw = DT.Select("Champ='profdenit'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 0).PadLeft(5))
            rw = DT.Select("Champ='vpotdenit'")
            Dv = rw(0)("dv").ToString
            fileContent.Append(FormatNumber(CDbl(Dv), 0).PadLeft(5))
            fileContent.AppendLine()
            'profil du sol
            'If LCase(dataTable.Rows(0).Item("soilOption")) = "simple" Then

            'End If
            'Dim Sql As String
            Sql = "Select * From soillayers where idsoil= '" & dataTable.Rows(0).Item("idsoil") & "' Order by NumLayer"
            'MsgBox(dataTable.Rows(0).Item("idsoil"))
            Dim Adp As OleDb.OleDbDataAdapter = New OleDbDataAdapter(Sql, MI_connection)
            ' Filling Dataset
            'Dim dataSet2 As New DataSet

            Dim dataLayer As New DataTable ' = dataSet2.Tables("st_param_sol")
            Adp.Fill(dataLayer) ', "st_param_sol")
            For i = 0 To 4
                'epc hcc hmin daf cailloux
                If LCase(dataTable.Rows(0).Item("soilOption")) = "simple" Then
                    fileContent.Append("1")
                    If i = 0 Then
                        fileContent.Append(FormatNumber(dataTable.Rows(0).Item("SoilTotalDepth"), 4).PadLeft(10))
                    Else
                        fileContent.Append("  0.00 ")
                    End If
                    fileContent.Append(FormatNumber(dataTable.Rows(0).Item("Wfc") / dataTable.Rows(0).Item("Bd"), 2).PadLeft(8))
                    fileContent.Append(FormatNumber(dataTable.Rows(0).Item("Wwp") / dataTable.Rows(0).Item("Bd"), 2).PadLeft(8))
                    fileContent.Append(FormatNumber(dataTable.Rows(0).Item("Bd"), 2).PadLeft(8))
                    fileContent.Append(FormatNumber(dataTable.Rows(0).Item("cf"), 2).PadLeft(8))
                    'rw = DT.Select("Champ='cailloux'")
                    'Dv = rw(0)("dv").ToString
                    'fileContent.Append(FormatNumber(CDbl(Dv), 2).PadLeft(8))
                    rw = DT.Select("Champ='typecailloux'")
                    Dv = rw(0)("dv").ToString
                    fileContent.Append(FormatNumber(CInt(Dv), 0).PadLeft(8))
                    rw = DT.Select("Champ='infil'")
                    Dv = rw(0)("dv").ToString
                    fileContent.Append(FormatNumber(CInt(Dv), 0).PadLeft(5))
                    rw = DT.Select("Champ='epd'")
                    Dv = rw(0)("dv").ToString
                    fileContent.Append(FormatNumber(CInt(Dv), 0).PadLeft(5))
                    fileContent.AppendLine()
                Else
                    If i < dataLayer.Rows.Count Then
                        fileContent.Append("1")
                        'epc hcc hmin daf cailloux
                        fileContent.Append(FormatNumber(dataLayer.Rows(i).Item("Ldown") - dataLayer.Rows(i).Item("LUp"), 4).PadLeft(10))
                        fileContent.Append(FormatNumber(dataLayer.Rows(i).Item("Wfc") / dataLayer.Rows(i).Item("Bd"), 2).PadLeft(8))
                        fileContent.Append(FormatNumber(dataLayer.Rows(i).Item("Wwp") / dataLayer.Rows(i).Item("Bd"), 2).PadLeft(8))
                        fileContent.Append(FormatNumber(dataLayer.Rows(i).Item("Bd"), 2).PadLeft(8))
                        fileContent.Append(FormatNumber(dataLayer.Rows(i).Item("cf"), 2).PadLeft(8))
                        'MsgBox(FormatNumber(dataLayer.Rows(i).Item("cf"), 2).PadLeft(8))
                        'rw = DT.Select("Champ='cailloux'")
                        'Dv = rw(0)("dv").ToString
                        'fileContent.Append(FormatNumber(CDbl(Dv), 2).PadLeft(8))
                        rw = DT.Select("Champ='typecailloux'")
                        Dv = rw(0)("dv").ToString
                        fileContent.Append(FormatNumber(CInt(Dv), 0).PadLeft(8))
                        rw = DT.Select("Champ='infil'")
                        Dv = rw(0)("dv").ToString
                        fileContent.Append(FormatNumber(CInt(Dv), 0).PadLeft(5))
                        rw = DT.Select("Champ='epd'")
                        Dv = rw(0)("dv").ToString
                        fileContent.Append(FormatNumber(CInt(Dv), 0).PadLeft(5))
                        fileContent.AppendLine()
                    Else
                        fileContent.Append("1")
                        fileContent.Append("  0.00 ")
                        fileContent.Append("  0.00 ")
                        fileContent.Append("  0.00 ")
                        fileContent.Append("  0.00 ")
                        fileContent.Append("  0.00 ")
                        'fileContent.Append(FormatNumber(dataLayer.Rows(i).Item("cf"), 2).PadLeft(8)) '???
                        'rw = DT.Select("Champ='cailloux'")
                        'Dv = rw(0)("dv").ToString
                        'fileContent.Append(FormatNumber(CDbl(Dv), 2).PadLeft(8))
                        rw = DT.Select("Champ='typecailloux'")
                        Dv = rw(0)("dv").ToString
                        fileContent.Append(FormatNumber(CInt(Dv), 0).PadLeft(8))
                        rw = DT.Select("Champ='infil'")
                        Dv = rw(0)("dv").ToString
                        fileContent.Append(FormatNumber(CInt(Dv), 0).PadLeft(5))
                        rw = DT.Select("Champ='epd'")
                        Dv = rw(0)("dv").ToString
                        fileContent.Append(FormatNumber(CInt(Dv), 0).PadLeft(5))
                        fileContent.AppendLine()

                    End If
                End If

            Next


        Next

        'End Using

        Try
            ' Export file to specified directory
            WriteFile(DirectoryPath, fileName, fileContent.ToString())
        Catch ex As Exception
            MessageBox.Show("Error during writing file :  " + ex.Message)
        End Try
        'Connection.Close()
        'MI_Connection.Close()
    End Sub
    Public Overrides Sub Import(DirectoryPath As String, model As String)

    End Sub
End Class


