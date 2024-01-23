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

Public Class SticsFictec1Converter
    Inherits Converter


    Public Overrides Sub Export(DirectoryPath As String, Idsim As String, connection As OleDb.OleDbConnection, MI_connection As OleDb.OleDbConnection)
        Dim fileName As String = "fictec1.txt"
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
        'fictec1 query
        Dim T As String = "Select Variables.Champ, Variables.Default_Value_Datamill, Variables.defaultValueOtherSource, IIf(IsNull([defaultValueOtherSource]),[Default_Value_Datamill],[defaultValueOtherSource]) As dv From Variables Where (((Variables.model)='stics') AND ((Variables.Table)='st_fictec'));"
        Dim DT As New DataTable

        Dim Cmd As New OleDb.OleDbDataAdapter(T, connection)
        Cmd.Fill(DT) ', "TChamp")
        Dim fetchAllQuery As String = "SELECT SimUnitList.idsim, SimUnitList.idMangt, Soil.SoilTotalDepth, ListCultivars.idcultivarStics, CropManagement.sdens," _
        & " CropManagement.sowingdate, CropManagement.SoilTillPolicyCode FROM Soil INNER JOIN (ListCultivars INNER JOIN (CropManagement INNER JOIN SimUnitList ON CropManagement.idMangt = SimUnitList.idMangt)" _
        & " ON ListCultivars.IdCultivar = CropManagement.Idcultivar) ON Soil.IdSoil = SimUnitList.idsoil  where idSim='" + Idsim + "' ;"

        'Init and use DataAdapter
        Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter(fetchAllQuery, MI_connection)

        ' Filling Dataset
        Dim dataSet, DS2 As New DataSet
        dataAdapter.Fill(dataSet, "st_fictec")
        Dim dataTable As DataTable = dataSet.Tables("st_fictec")
        Dim DT2 As DataTable = dataSet.Tables("Inorg")
        Dim fetchallquery2 As String
        'Dim nbInterventions As Integer
        Dim rw As DataRow
        rw = dataTable.Rows(0)
        'read all lines of st_fictec
        'FormatSticsData(fileContent, DT, "supply of organic residus.nbinterventions", 1, 1)
        fileContent.Append("nbinterventions")
        fileContent.AppendLine()

        DS2.Clear()
        fetchallquery2 = "SELECT SimUnitList.idsim, CropManagement.sowingdate, OrganicFOperations.Dferti, OrganicFOperations.OFNumber, OrganicFOperations.CNferti, " _
                & "OrganicFOperations.NFerti, OrganicFOperations.Qmanure, OrganicFOperations.TypeResidues, ListResidues.idresidueStics, CropManagement.SoilTillPolicyCode " _
                & "FROM ListResidues INNER JOIN ((OrganicFertilizationPolicy INNER JOIN (CropManagement INNER JOIN SimUnitList ON CropManagement.idMangt = SimUnitList.idMangt) " _
                & "ON OrganicFertilizationPolicy.OFertiPolicyCode = CropManagement.OFertiPolicyCode) INNER JOIN OrganicFOperations ON OrganicFertilizationPolicy.OFertiPolicyCode " _
                & "= OrganicFOperations.OFertiPolicyCode) ON ListResidues.TypeResidues = OrganicFOperations.TypeResidues where idSim='" + Idsim + "' Order by OFNumber ;"
        Dim dataAdapter2 As OleDb.OleDbDataAdapter = New OleDbDataAdapter(fetchallquery2, MI_connection)
        dataAdapter2.Fill(DS2)
        'nbInterventions = DS2.Tables(0).Rows.Count
        If IsDBNull(DS2.Tables(0).Rows(0).Item("idresidueStics")) Then
            fileContent.AppendLine("0")
        Else
            fileContent.AppendLine(DS2.Tables(0).Rows.Count)
            'Display opp1 only if nbinterventions <> 0
            If (DS2.Tables(0).Rows.Count <> 0) Then
                For i = 0 To DS2.Tables(0).Rows.Count - 1
                    fileContent.AppendLine("opp1")
                    fileContent.Append(CInt(DS2.Tables(0).Rows(i).Item("sowingDate")) + CInt(DS2.Tables(0).Rows(i).Item("Dferti")))
                    fileContent.Append(" ")
                    'idresiduesStics corresponding to Typeresidues  of OFNumber for OFertiPolicyCode=CropManangement.OFertiPolicyCode
                    fileContent.Append(DS2.Tables(0).Rows(i).Item("idresidueStics"))
                    fileContent.Append(" ")
                    fileContent.Append(DS2.Tables(0).Rows(i).Item("qmanure") / 1000)
                    fileContent.Append(" ")
                    fileContent.Append(DS2.Tables(0).Rows(i).Item("CNferti") * DS2.Tables(0).Rows(i).Item("Nferti"))
                    fileContent.Append(" ")
                    fileContent.Append(DS2.Tables(0).Rows(i).Item("CNferti"))
                    fileContent.Append(" ")
                    fileContent.Append(DS2.Tables(0).Rows(i).Item("Nferti"))
                    fileContent.Append(" ")
                    fileContent.Append(FormatSticsRawData(DT, "supply of organic residus.eaures"))
                    fileContent.AppendLine()
                Next
                'fileContent.Append(FormatOppData(row.item("opp1").ToString()))
            End If
        End If

        'nbInterventions = FormatSticsRawData(row.item("nbinterventions2"))
        Dim Sql As String
        Sql = "SELECT SoilTillPolicy.SoilTillPolicyCode, SoilTillageOperations.STNumber, SoilTillPolicy.NumTillOperations, SoilTillageOperations.DepthResUp, SoilTillageOperations.DepthResLow, SoilTillageOperations.DSTill" _
            & " FROM SoilTillPolicy INNER JOIN SoilTillageOperations ON SoilTillPolicy.SoilTillPolicyCode = SoilTillageOperations.SoilTillPolicyCode " _
            & " where SoilTillPolicy.SoilTillPolicyCode= '" & dataTable.Rows(0).Item("SoilTillPolicyCode") & "'"
        Dim Adp As OleDb.OleDbDataAdapter = New OleDbDataAdapter(Sql, MI_connection)
        ' Filling Dataset
        Dim dataSet2 As New DataSet
        Adp.Fill(dataSet2, "st_param_sol")
        Dim dataTill As DataTable = dataSet2.Tables("st_param_sol")
        fileContent.AppendLine("nbinterventions") 'soil tillage
        fileContent.AppendLine(dataTill.Rows(0).Item("NumTillOperations"))
        If CInt(dataTill.Rows(0).Item("NumTillOperations")) > 0 Then
            For i = 0 To dataTill.Rows.Count - 1
                fileContent.AppendLine("opp1")
                fileContent.Append(rw("sowingdate") + dataTill.Rows(i).Item("DStill")) 'jultrav
                fileContent.Append(" ")
                fileContent.Append(dataTill.Rows(i).Item("DepthResUp")) 'profres
                fileContent.Append(" ")
                fileContent.Append(dataTill.Rows(i).Item("DepthResLow")) 'proftrav
                fileContent.AppendLine()
            Next
        End If
        'nbInterventions
        '1
        'opp1
        '30 0.00 20.00 
        'rajouter jultrav porfres...
        fileContent.AppendLine("iplt0")
        fileContent.Append(rw.Item("sowingdate"))
        fileContent.AppendLine()
        FormatSticsData(fileContent, DT, "profsem")
        fileContent.AppendLine("densitesem")
        fileContent.Append(rw.Item("Sdens"))
        fileContent.AppendLine()
        fileContent.AppendLine("variete")
        fileContent.Append(rw.Item("idcultivarstics"))
        fileContent.AppendLine()
        FormatSticsData(fileContent, DT, "codetradtec")
        FormatSticsData(fileContent, DT, "interrang")
        FormatSticsData(fileContent, DT, "orientrang")
        FormatSticsData(fileContent, DT, "codedecisemis")
        FormatSticsData(fileContent, DT, "nbjmaxapressemis")
        FormatSticsData(fileContent, DT, "nbjseuiltempref")
        FormatSticsData(fileContent, DT, "codestade")
        FormatSticsData(fileContent, DT, "ilev")
        FormatSticsData(fileContent, DT, "iamf")
        FormatSticsData(fileContent, DT, "ilax")
        FormatSticsData(fileContent, DT, "isen")
        FormatSticsData(fileContent, DT, "ilan")
        FormatSticsData(fileContent, DT, "iflo")
        FormatSticsData(fileContent, DT, "idrp")
        FormatSticsData(fileContent, DT, "imat")
        FormatSticsData(fileContent, DT, "irec")
        fileContent.AppendLine("irecbutoir")
        fileContent.Append(rw.Item("sowingdate") + 250)
        fileContent.AppendLine()
        FormatSticsData(fileContent, DT, "effirr")
        FormatSticsData(fileContent, DT, "codecalirrig")
        FormatSticsData(fileContent, DT, "ratiol")
        FormatSticsData(fileContent, DT, "dosimx")
        FormatSticsData(fileContent, DT, "doseirrigmin")
        FormatSticsData(fileContent, DT, "codedateappH2O")
        fileContent.AppendLine("nbinterventions") 'irrigation
        'nbInterventions = FormatSticsRawData(row.item("nbinterventions3"))
        fileContent.AppendLine(0)
        FormatSticsData(fileContent, DT, "codlocirrig")
        FormatSticsData(fileContent, DT, "locirrig")
        'FormatSticsData(fileContent, DT, "profmes")
        fileContent.AppendLine("profmes")
        fileContent.Append(rw.Item("SoilTotalDepth"))
        fileContent.AppendLine()
        FormatSticsData(fileContent, DT, "engrais")
        FormatSticsData(fileContent, DT, "concirr")
        FormatSticsData(fileContent, DT, "codedateappN")
        FormatSticsData(fileContent, DT, "codefracappN")
        FormatSticsData(fileContent, DT, "fertilisation.Qtot_N",, 1)
        Dim DS3 As New DataSet
        DS3.Clear()
        fetchallquery2 = "Select SimUnitList.idsim, InorganicFOperations.N, CropManagement.sowingdate, InorganicFOperations.Dferti, InorganicFertilizationPolicy.NumInorganicFerti " _
        & " FROM(InorganicFertilizationPolicy INNER JOIN InorganicFOperations On InorganicFertilizationPolicy.InorgFertiPolicyCode = InorganicFOperations.InorgFertiPolicyCode)" _
        & "INNER JOIN (CropManagement INNER JOIN SimUnitList On CropManagement.idMangt = SimUnitList.idMangt) On InorganicFertilizationPolicy.InorgFertiPolicyCode = " _
        & " CropManagement.InoFertiPolicyCode where idSim='" + Idsim + "' ;"
        dataAdapter2 = New OleDbDataAdapter(fetchallquery2, MI_connection)
        dataAdapter2.Fill(DS3)

        fileContent.AppendLine("nbinterventions")
        'nbInterventions = FormatSticsRawData(DT.item("nbinterventions4"))
        fileContent.AppendLine(DS3.Tables(0).Rows.Count)
        If DS3.Tables(0).Rows.Count > 0 Then
            For i = 0 To DS3.Tables(0).Rows.Count - 1
                fileContent.AppendLine("opp1")
                fileContent.Append(DS3.Tables(0).Rows(i).Item("sowingDate") + DS3.Tables(0).Rows(i).Item("Dferti"))
                fileContent.Append(" ")
                fileContent.Append(DS3.Tables(0).Rows(i).Item("N"))
                fileContent.AppendLine()
            Next
        End If

        FormatSticsData(fileContent, DT, "codlocferti")
        FormatSticsData(fileContent, DT, "locferti")
        FormatSticsData(fileContent, DT, "ressuite")
        FormatSticsData(fileContent, DT, "codceuille")
        FormatSticsData(fileContent, DT, "nbceuille")
        FormatSticsData(fileContent, DT, "cadencerec")
        FormatSticsData(fileContent, DT, "codrecolte")
        FormatSticsData(fileContent, DT, "codeaumin")
        FormatSticsData(fileContent, DT, "h2ograinmin")
        FormatSticsData(fileContent, DT, "h2ograinmax")
        FormatSticsData(fileContent, DT, "sucrerec")
        FormatSticsData(fileContent, DT, "CNgrainrec")
        FormatSticsData(fileContent, DT, "huilerec")
        FormatSticsData(fileContent, DT, "coderecolteassoc")
        FormatSticsData(fileContent, DT, "codedecirecolte")
        FormatSticsData(fileContent, DT, "nbjmaxapresrecolte")
        FormatSticsData(fileContent, DT, "codefauche")
        FormatSticsData(fileContent, DT, "mscoupemini")
        FormatSticsData(fileContent, DT, "codemodfauche")
        FormatSticsData(fileContent, DT, "hautcoupedefaut")
        FormatSticsData(fileContent, DT, "stadecoupedf")

        fileContent.AppendLine("nbinterventions")
        fileContent.AppendLine("0")
        fileContent.AppendLine("nbinterventions")
        fileContent.AppendLine("0")
        'nbInterventions = FormatSticsRawData(DT.item("nbinterventions5"))

        FormatSticsData(fileContent, DT, "codepaillage")
        FormatSticsData(fileContent, DT, "couvermulchplastique")
        FormatSticsData(fileContent, DT, "albedomulchplastique")
        FormatSticsData(fileContent, DT, "codrognage")
        FormatSticsData(fileContent, DT, "largrogne")
        FormatSticsData(fileContent, DT, "hautrogne")
        FormatSticsData(fileContent, DT, "biorognem")
        FormatSticsData(fileContent, DT, "codcalrogne")
        FormatSticsData(fileContent, DT, "julrogne")
        FormatSticsData(fileContent, DT, "margerogne")
        FormatSticsData(fileContent, DT, "codeclaircie")
        FormatSticsData(fileContent, DT, "juleclair")
        FormatSticsData(fileContent, DT, "nbinfloecl")
        FormatSticsData(fileContent, DT, "codeffeuil")
        FormatSticsData(fileContent, DT, "codhauteff")
        FormatSticsData(fileContent, DT, "codcaleffeuil")
        FormatSticsData(fileContent, DT, "laidebeff")
        FormatSticsData(fileContent, DT, "effeuil")
        FormatSticsData(fileContent, DT, "juleffeuil")
        FormatSticsData(fileContent, DT, "laieffeuil")
        FormatSticsData(fileContent, DT, "codetaille")
        FormatSticsData(fileContent, DT, "jultaille")
        FormatSticsData(fileContent, DT, "codepalissage")
        FormatSticsData(fileContent, DT, "hautmaxtec")
        FormatSticsData(fileContent, DT, "largtec")
        FormatSticsData(fileContent, DT, "codabri")
        FormatSticsData(fileContent, DT, "transplastic")
        FormatSticsData(fileContent, DT, "surfouvre1")
        FormatSticsData(fileContent, DT, "julouvre2")
        FormatSticsData(fileContent, DT, "surfouvre2")
        FormatSticsData(fileContent, DT, "julouvre3")
        FormatSticsData(fileContent, DT, "surfouvre3")
        FormatSticsData(fileContent, DT, "codeDST")
        FormatSticsData(fileContent, DT, "dachisel")
        FormatSticsData(fileContent, DT, "dalabour")
        FormatSticsData(fileContent, DT, "rugochisel")
        FormatSticsData(fileContent, DT, "rugolabour")
        FormatSticsData(fileContent, DT, "codeDSTtass")
        FormatSticsData(fileContent, DT, "profhumsemoir")
        FormatSticsData(fileContent, DT, "dasemis")
        FormatSticsData(fileContent, DT, "profhumrecolteuse")
        FormatSticsData(fileContent, DT, "darecolte")
        FormatSticsData(fileContent, DT, "codeDSTnbcouche")


        Try
            ' Export file to specified directory
            WriteFile(DirectoryPath, fileName, fileContent.ToString())
        Catch ex As Exception
            MessageBox.Show("Error during writing file : " + ex.Message)
        End Try
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
            'champ = champ + fieldIt.ToString()
            InStr(fieldName, ".")
            fieldName = Mid(fieldName, InStr(fieldName, ".") + 1)
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
    Public Function FormatSticsRawData(ByVal data As Object, ByVal champ As String, Optional ByVal precision As Integer = 1) As String
        Dim res As String
        Dim typeData As String
        Dim rw2() As DataRow
        rw2 = data.Select("champ='" & champ & "'")
        If rw2.Count = 0 Then MsgBox(champ)
        res = rw2(0).Item("dv").ToString
        'res = ""
        typeData = res.GetType().ToString()


        Return res
    End Function
    Public Overrides Sub Import(DirectoryPath As String, model As String)

    End Sub
End Class


