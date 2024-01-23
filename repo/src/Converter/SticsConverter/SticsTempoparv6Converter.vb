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

Public Class SticsTempoparv6Converter
    Inherits Converter


    Public Overrides Sub Export(DirectoryPath As String, Idsim As String, connection As OleDb.OleDbConnection, MI_connection As OleDb.OleDbConnection)
        Dim fileName As String = "tempoparv6.sti"
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

        'Tempopar query
        Dim T As String = "Select   Variables.Champ, Variables.Default_Value_Datamill, Variables.defaultValueOtherSource, IIf(IsNull([defaultValueOtherSource]),[Default_Value_Datamill],[defaultValueOtherSource]) As dv From Variables Where (((Variables.model)='stics') AND ((Variables.Table)='st_tempoparV6'));"
        Dim DT As New DataTable
        Dim Cmd As New OleDb.OleDbDataAdapter(T, connection)
        Cmd.Fill(DT) ', "TChamp")


        'FormatSticsData(fileContent, DT, "CroCo", 5, 11)
        FormatSticsData(fileContent, DT, "codetempfauche")
        FormatSticsData(fileContent, DT, "coefracoupe1", 1, 1)
        FormatSticsData(fileContent, DT, "coefracoupe2", 1, 1)
        FormatSticsData(fileContent, DT, "codepluiepoquet")
        FormatSticsData(fileContent, DT, "nbjoursrrversirrig")
        FormatSticsData(fileContent, DT, "swfacmin", 1)
        FormatSticsData(fileContent, DT, "codetranspitalle")
        FormatSticsData(fileContent, DT, "codedyntalle1", -1, 1)
        FormatSticsData(fileContent, DT, "SurfApex1", -1, 1)
        FormatSticsData(fileContent, DT, "SeuilMorTalle1", 2, 1)
        FormatSticsData(fileContent, DT, "SigmaDisTalle1", 1, 1)
        FormatSticsData(fileContent, DT, "VitReconsPeupl1", -1, 1)
        FormatSticsData(fileContent, DT, "SeuilReconsPeupl1", 1, 1)
        FormatSticsData(fileContent, DT, "MaxTalle1", 1, 1)
        FormatSticsData(fileContent, DT, "SeuilLAIapex1", 1, 1)
        FormatSticsData(fileContent, DT, "tigefeuilcoupe1", 1, 1)
        FormatSticsData(fileContent, DT, "codedyntalle2", 1, 1)
        FormatSticsData(fileContent, DT, "SurfApex2", -1, 1)
        FormatSticsData(fileContent, DT, "SeuilMorTalle2", 1, 1)
        FormatSticsData(fileContent, DT, "SigmaDisTalle2", 1, 1)
        FormatSticsData(fileContent, DT, "VitReconsPeupl2", -1, 1)
        FormatSticsData(fileContent, DT, "SeuilReconsPeupl2", 1, 1)
        FormatSticsData(fileContent, DT, "MaxTalle2", 1, 1)
        FormatSticsData(fileContent, DT, "SeuilLAIapex2", 1, 1)
        FormatSticsData(fileContent, DT, "tigefeuilcoupe2", 1, 1)
        FormatSticsData(fileContent, DT, "resplmax1", 2, 1)
        FormatSticsData(fileContent, DT, "resplmax2", 2, 1)
        FormatSticsData(fileContent, DT, "codemontaison1", -1, 1)
        FormatSticsData(fileContent, DT, "codemontaison2", -1, 1)
        'fileContent.Append("code_adapt_MO_CC")
        'fileContent.AppendLine()
        'FormatSticsData(fileContent, DT,("code_adapt_MO_CC")))
        'fileContent.AppendLine()
        'fileContent.Append("periode_adapt_CC")
        'fileContent.AppendLine()
        'FormatSticsData(fileContent, DT,("periode_adapt_CC")))
        'fileContent.AppendLine()
        'fileContent.Append("an_debut_serie_histo")
        'fileContent.AppendLine()
        'FormatSticsData(fileContent, DT,("an_debut_serie_histo")))
        'fileContent.AppendLine()
        'fileContent.Append("an_fin_serie_histo")
        'fileContent.AppendLine()
        'FormatSticsData(fileContent, DT,("an_fin_serie_histo")))
        'fileContent.AppendLine()
        'fileContent.Append("param_tmoy_histo")
        'fileContent.AppendLine()
        'FormatSticsData(fileContent, DT,("param_tmoy_histo", 1))
        'fileContent.AppendLine()
        'fileContent.Append("code_adaptCC_miner")
        'fileContent.AppendLine()
        'FormatSticsData(fileContent, DT,("code_adaptCC_miner")))
        'fileContent.AppendLine()
        'fileContent.Append("code_adaptCC_nit")
        'fileContent.AppendLine()
        'FormatSticsData(fileContent, DT,("code_adaptCC_nit")))
        'fileContent.AppendLine()
        'fileContent.Append("code_adaptCC_denit")
        'fileContent.AppendLine()
        'FormatSticsData(fileContent, DT,("code_adaptCC_denit")))
        'fileContent.AppendLine()
        'fileContent.Append("TREFdenit1")
        'fileContent.AppendLine()
        'FormatSticsData(fileContent, DT,("TREFdenit1")))
        'fileContent.AppendLine()
        'fileContent.Append("TREFdenit2")
        'fileContent.AppendLine()
        'FormatSticsData(fileContent, DT,("TREFdenit2")))
        'fileContent.AppendLine()
        FormatSticsData(fileContent, DT, "nbj_pr_apres_semis")
        FormatSticsData(fileContent, DT, "eau_mini_decisemis")
        FormatSticsData(fileContent, DT, "humirac_decisemis", 2)
        FormatSticsData(fileContent, DT, "codecalferti")
        FormatSticsData(fileContent, DT, "ratiolN", 5)
        FormatSticsData(fileContent, DT, "dosimxN", 5)
        FormatSticsData(fileContent, DT, "codetesthumN")
        FormatSticsData(fileContent, DT, "codeNmindec")
        FormatSticsData(fileContent, DT, "rapNmindec", 5)
        FormatSticsData(fileContent, DT, "fNmindecmin", 5)
        FormatSticsData(fileContent, DT, "codetrosee")
        FormatSticsData(fileContent, DT, "codeSWDRH")
        FormatSticsData(fileContent, DT, "P_codedate_irrigauto")
        FormatSticsData(fileContent, DT, "datedeb_irrigauto")
        FormatSticsData(fileContent, DT, "datefin_irrigauto")
        FormatSticsData(fileContent, DT, "stage_start_irrigauto")
        FormatSticsData(fileContent, DT, "stage_end_irrigauto")
        FormatSticsData(fileContent, DT, "codemortalracine")
        FormatSticsData(fileContent, DT, "option_thinning")
        FormatSticsData(fileContent, DT, "option_engrais_multiple")
        FormatSticsData(fileContent, DT, "option_pature")
        FormatSticsData(fileContent, DT, "coderes_pature")
        FormatSticsData(fileContent, DT, "pertes_restit_ext")
        FormatSticsData(fileContent, DT, "Crespc_pature")
        FormatSticsData(fileContent, DT, "Nminres_pature")
        FormatSticsData(fileContent, DT, "eaures_pature")
        FormatSticsData(fileContent, DT, "coef_calcul_qres")
        FormatSticsData(fileContent, DT, "engrais_pature")
        FormatSticsData(fileContent, DT, "coef_calcul_doseN")
        FormatSticsData(fileContent, DT, "codemineralOM")
        FormatSticsData(fileContent, DT, "GMIN1")
        FormatSticsData(fileContent, DT, "GMIN2")
        FormatSticsData(fileContent, DT, "GMIN3")
        FormatSticsData(fileContent, DT, "GMIN4")
        FormatSticsData(fileContent, DT, "GMIN5")
        FormatSticsData(fileContent, DT, "GMIN6")
        FormatSticsData(fileContent, DT, "GMIN7")
        fileContent.AppendLine()

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
            fieldName = Mid(fieldName, 1, fieldName.Length - 1) & "(" & Mid(fieldName, fieldName.Length) & ")"
            'champ = champ + fieldIt.ToString()
        End If

        'fetch data
        rw = row.select("Champ='" & champ & "'")
        If rw.Count = 0 Then MsgBox(champ)
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
