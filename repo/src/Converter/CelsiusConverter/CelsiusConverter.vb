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
Public Class CelsiusConverter
    Inherits Converter
    Public Sub New()
        Connection = New OleDb.OleDbConnection
        Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\Celsius\CelsiusV3nov17_dataArise.accdb"
        MI_Connection = New OleDb.OleDbConnection()
        MI_Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\MasterInput.accdb"
    End Sub
    Public Overrides Sub Export(DirectoryPath As String, idsimx As String, connection As OleDb.OleDbConnection, MI_connection As OleDb.OleDbConnection)

        Dim Sql3, Sql1, Sql2, Sql4, Sql5 As String
        Dim i As Integer
        Dim Sorg, SMin As Double
        Dim R, R2, R3, R4 As DataRow
        Dim restrictions(3) As String
        Dim Ap_ADP As New OleDb.OleDbDataAdapter()
        Dim Ap_ADP1 As New OleDb.OleDbDataAdapter()
        Dim Ap_ADP2 As New OleDb.OleDbDataAdapter()
        Dim Ap_ADP3 As New OleDb.OleDbDataAdapter()
        Dim Jeu As New DataSet ' Associé aux noms des simulations et a R
        Dim Jeu1 As New DataSet
        Dim Jeu2 As New DataSet
        Dim Jeu3 As New DataSet
        Dim Jeu4 As New DataSet
        'Dim MI_Connection = New OleDb.OleDbConnection
        'MI_Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\MasterInput.accdb"
        'Dim Connection = New OleDb.OleDbConnection
        'Connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & RepSource & "\Celsius\CelsiusV3nov17_dataArise.accdb"
        Dim fileC1 As StringBuilder = New StringBuilder()
        WriteFile(DirectoryPath & "\", "debut.txt", "exit")
        Try
            'Open DB connection
            connection.Open()
            MI_connection.Open()
        Catch ex As Exception
            MessageBox.Show("Connection Error : " + ex.Message)
        End Try
        '---------------------------Copie toutes les données Météo
        'Jeu1.Clear()
        'Sql1 = "Select idPoint,year,DOY,Nmonth,NdayM,srad,tmax,tmin,tmoy,rain,Etppm from RAclimateD Where  Idpoint in (select distinct idpoint from simunitlist)"
        'Ap_ADP1 = New OleDb.OleDbDataAdapter(Sql1, MI_connection)
        'Ap_ADP1.Fill(Jeu1)
        'Sql2 = "Delete * from Dweather"

        'Dim command As New OleDbCommand(Sql2, connection)
        'command.ExecuteNonQuery()
        'For i = 0 To Jeu1.Tables(0).Rows.Count - 1
        '    R = Jeu1.Tables(0).Rows(i)
        '    Sql2 = "insert into Dweather (IdDClim,idjourclim,annee,jda,mois,jour,rg,tmax,tmin,tmoy,plu,Etp) values ('" & R("idPoint") & "','" & R("idPoint") & "." & R("Year").ToString & "." & R("DOY").ToString & "'," & R("Year") & "," & R("DOY") & "," & R("Nmonth") & "," & R("NdayM") & "," & R("srad") & "," & R("tmax") & "," & R("tmin") & "," & R("tmoy") & "," & R("rain") & "," & R("Etppm") & ")"
        '    command = New OleDbCommand(Sql2, connection)
        '    command.ExecuteNonQuery()
        '    If Form1.msgErr_expCelsius_export.Text <> "Weather " & R.Item("idpoint") Then
        '        Form1.msgErr_expCelsius_export.Text = "Weather " & R.Item("idpoint")
        '        Form1.msgErr_expCelsius_export.Refresh()
        '    End If
        '    command.Dispose()
        'Next
        '----------------------------
        'Apres-----------------------------------
        Dim command As New OleDbCommand("", connection)
        Sql2 = "select distinct idpoint from simunitlist"
        Ap_ADP2 = New OleDb.OleDbDataAdapter(Sql2, MI_connection)
        Ap_ADP2.Fill(Jeu2)
        For j = 0 To Jeu2.Tables(0).Rows.Count - 1
            Sql3 = "select IdDClim from Dweather Where IdDClim='" & Jeu2.Tables(0).Rows(j)(0).ToString & "'"
            Ap_ADP3 = New OleDb.OleDbDataAdapter(Sql3, connection)
            Ap_ADP3.Fill(Jeu3)

            If (Jeu3.Tables(0).Rows.Count = 0) Or FCChek Then
                'MsgBox("climat")
                command = New OleDbCommand("", connection)
                command.CommandText = "DELETE * From Dweather Where (((Dweather.idDclim) = '" & Jeu2.Tables(0).Rows(j)(0).ToString & "'));"
                command.ExecuteNonQuery()
                Application.DoEvents()
                Jeu1.Clear()
                Sql1 = "Select idPoint,year,DOY,Nmonth,NdayM,srad,tmax,tmin,tmoy,rain,Etppm from RAclimateD Where  Idpoint='" & Jeu2.Tables(0).Rows(j)(0).ToString & "' order by w_date"
                Ap_ADP1 = New OleDb.OleDbDataAdapter(Sql1, MI_connection)
                Ap_ADP1.Fill(Jeu1)
                For i = 0 To Jeu1.Tables(0).Rows.Count - 1
                    R = Jeu1.Tables(0).Rows(i)
                    Sql2 = "insert into Dweather (IdDClim,idjourclim,annee,jda,mois,jour,rg,tmax,tmin,tmoy,plu,Etp) values ('" & R("idPoint") & "','" & R("idPoint") & "." & R("Year").ToString & "." & R("DOY").ToString & "'," & R("Year") & "," & R("DOY") & "," & R("Nmonth") & "," & R("NdayM") & "," & R("srad") & "," & R("tmax") & "," & R("tmin") & "," & R("tmoy") & "," & R("rain") & "," & R("Etppm") & ")"
                    command = New OleDbCommand(Sql2, connection)
                    command.ExecuteNonQuery()
                    If Form1.msgErr_expCelsius_export.Text <> "Weather " & R.Item("idpoint") Then
                        Form1.msgErr_expCelsius_export.Text = "Weather " & R.Item("idpoint")
                        Form1.msgErr_expCelsius_export.Refresh()
                    End If
                    command.Dispose()
                Next
            End If
        Next
        '-----------------------
        Jeu1.Clear()
        Jeu2.Clear()
        Sql1 = "Select idPoint,latitudeDD,longitudeDD,altitude from Coordinates"
        Ap_ADP1 = New OleDb.OleDbDataAdapter(Sql1, MI_connection)
        Ap_ADP1.Fill(Jeu1)
        Sql2 = "Delete * from ListPAnnexes"
        command = New OleDbCommand(Sql2, connection)
        command.ExecuteNonQuery()
        command.Dispose()
        Sql3 = "Select * from ListPAnnexesDV"
        Ap_ADP2 = New OleDb.OleDbDataAdapter(Sql3, connection)
        Ap_ADP2.Fill(Jeu2)
        R2 = Jeu2.Tables(0).Rows(0)
        For i = 0 To Jeu1.Tables(0).Rows.Count - 1
            R = Jeu1.Tables(0).Rows(i)
            If IsDBNull(R("altitude")) Then
                R("altitude") = -99
            End If
            Sql2 = "insert into ListPAnnexes (IdDClim,latitudeDD,longitude,altitude,CO2c,ConcNPlu) values ('" & R("idPoint") & "'," & R("latitudeDD") & "," & R("longitudeDD") & "," & R("altitude") & "," & R2("CO2c") & "," & R2("ConcNPlu") & ")"
            command = New OleDbCommand(Sql2, connection)
            command.ExecuteNonQuery()
            Form1.msgErr_expCelsius_export.Text = "ListPAnnexes " & R("idpoint")
            Form1.msgErr_expCelsius_export.Refresh()
            command.Dispose()
        Next
        '----------------------------    
        Jeu1.Clear()
        Jeu2.Clear()
        Sql1 = "Select IdIni,Wstockinit from InitialConditions"
        Ap_ADP1 = New OleDb.OleDbDataAdapter(Sql1, MI_connection)
        Ap_ADP1.Fill(Jeu1)
        Sql2 = "Delete * from ParamIni"
        command = New OleDbCommand(Sql2, connection)
        command.ExecuteNonQuery()
        command.Dispose()
        Sql3 = "Select * from ParamIniDV"
        Ap_ADP2 = New OleDb.OleDbDataAdapter(Sql3, connection)
        Ap_ADP2.Fill(Jeu2)
        R2 = Jeu2.Tables(0).Rows(0)
        For i = 0 To Jeu1.Tables(0).Rows.Count - 1
            R = Jeu1.Tables(0).Rows(i)
            Sql2 = "insert into ParamIni (IdIni,Qpaillisinit,Stockinit,iniSolhautON) values ('" & R("IdIni") & "'," & R2("Qpaillisinit") & "," & R("Wstockinit") & "," & R2("iniSolhautON") & ")"
            command = New OleDbCommand(Sql2, connection)
            command.ExecuteNonQuery()
            Form1.msgErr_expCelsius_export.Text = "ParamIni " & R.Item("idini")
            Form1.msgErr_expCelsius_export.Refresh()
            command.Dispose()
        Next
        '----------------------------    
        Jeu1.Clear()
        Jeu2.Clear()
        Jeu3.Clear()
        Sql1 = "SELECT CropManagement.idMangt, CropManagement.sdens, CropManagement.sowingdate, CropManagement.OFertiPolicyCode," &
        "CropManagement.InoFertiPolicyCode, ListCultivars.IdcultivarCelsius FROM ListCultivars INNER JOIN CropManagement ON ListCultivars.IdCultivar = CropManagement.Idcultivar;"
        Ap_ADP1 = New OleDb.OleDbDataAdapter(Sql1, MI_connection)
        Ap_ADP1.Fill(Jeu1)
        Sql2 = "Delete * from Tech_Commun"
        command = New OleDbCommand(Sql2, connection)
        command.ExecuteNonQuery()
        command.Dispose()
        Sql2 = "Delete * from Tech_perCrop"
        command = New OleDbCommand(Sql2, connection)
        command.ExecuteNonQuery()
        command.Dispose()
        Sql3 = "Select * from Tech_CommunDV"
        Ap_ADP2 = New OleDb.OleDbDataAdapter(Sql3, connection)
        Ap_ADP2.Fill(Jeu2)
        R2 = Jeu2.Tables(0).Rows(0)
        Sql4 = "Select * from Tech_perCropDV"
        Ap_ADP2 = New OleDb.OleDbDataAdapter(Sql4, connection)
        Ap_ADP2.Fill(Jeu3)
        R3 = Jeu3.Tables(0).Rows(0)
        For i = 0 To Jeu1.Tables(0).Rows.Count - 1
            'CodParamMulch :IdResiduesCelsius corresponding to Typeresidues of first OFnumber in OrganicFOperations 
            '               For which In_OnManure="on" And OFertiPolicyCode=CropManangement.OFertiPolicyCode
            '               (If ResiduesInOn = "on").If no such Case Then  "1"
            'imulch : sowingdate+OrganicFOperations.Dferti of first OFnumber in OrganicFOperations 
            '           For which In_OnManure="on" and OFertiPolicyCode=CropManangement.OFertiPolicyCode
            'QpaillisApport : Qmanure for first OFnumber in OrganicFOperations 
            '                   For which In_OnManure="on" And OFertiPolicyCode=CropManangement.OFertiPolicyCode
            '               If ResiduesInOn = "on".If no such Case Then  0
            R = Jeu1.Tables(0).Rows(i)
            Sql5 = "SELECT OrganicFOperations.OFertiPolicyCode, OrganicFOperations.OFNumber, OrganicFOperations.Dferti, " _
                & "OrganicFOperations.Qmanure, OrganicFOperations.TypeResidues, OrganicFOperations.In_OnManure, " _
                & "ListResidues.IdResidueCelsius FROM ListResidues INNER JOIN OrganicFOperations ON ListResidues.TypeResidues = " _
                & "OrganicFOperations.TypeResidues Where ((OrganicFOperations.In_OnManure='on') and ((OrganicFOperations.OFertiPolicyCode) = '" & R("OfertiPolicyCode") & "' )) order by OFNumber;"
            Jeu4.Clear()
            Ap_ADP2 = New OleDb.OleDbDataAdapter(Sql5, MI_connection)
            Ap_ADP2.Fill(Jeu4)
            If Jeu4.Tables(0).Rows.Count > 0 Then
                R4 = Jeu4.Tables(0).Rows(0)
                'If ResiduesInOn="On" then Qresidues else 0
                R2("imulch") = R("sowingdate") + R4("Dferti")
                R2("QPaillisApport") = R4("Qmanure")
                R2("CodParamMulch") = R4("IdResidueCelsius") 'command.ExecuteScalar
            Else
                R2("imulch") = 0
                R2("CodParamMulch") = "1"
                R2("QPaillisApport") = 0
                'End If
            End If
            '            Sum of InorganicFOperations.N (for the NumInorganicFerti of the InorgFertiPolicyCode)
            Sql1 = "SELECT Sum(InOrganicFOperations.N) AS SommeDeNFerti FROM InOrganicFOperations GROUP BY InOrganicFOperations.InOrgFertiPolicyCode HAVING (((InOrganicFOperations.InOrgFertiPolicyCode)='" & R("InOFertiPolicyCode") & "'));"
            command = New OleDbCommand(Sql1, MI_connection)
            SMin = command.ExecuteScalar()
            command.Dispose()
            'Sum of OrganicFOperations.Nferti x Qmanure (for the  NumOrganicFerti of the OFertiPolicyCode)
            Sql1 = "SELECT Sum(OrganicFOperations.NFerti * OrganicFOperations.QManure) AS SommeDeNFerti FROM OrganicFOperations GROUP BY OrganicFOperations.OFertiPolicyCode HAVING (((OrganicFOperations.OFertiPolicyCode)='" & R("OFertiPolicyCode") & "'));"
            command = New OleDbCommand(Sql1, MI_connection)
            Sorg = command.ExecuteScalar()
            'Sorg = 1
            command.Dispose()
            Sql2 = "insert into Tech_Commun (IdTech_Com,imulch,CodParamMulch,QPaillisApport,AltiCult, DriveRuiObs, fertiminON, fertiorgON, IrrigON, NbCult,NomSC,SerreTunnelON,tApportMon,tApportMinN) values " &
            "('" & R("idMangt") & "'," & R2("imulch") & "," & R2("CodParamMulch") & "," & R2("QPaillisApport") & "," & R2("AltiCult") & "," & R2("DriveRuiObs") & "," & R2("fertiminON") & "," & R2("fertiorgON") & "," & R2("IrrigON") & "," & R2("NbCult") & ",'" & R2("NomSC") & "'," & R2("SerreTunnelON") & "," & Sorg & "," & SMin & ")"
            command = New OleDbCommand(Sql2, connection)
            command.ExecuteNonQuery()
            command.Dispose()
            'codePspecies, DbutoirNouvSemis, irepiqu, NumCrop, NumCultivar, RepiquageON, SemisAutoDebut, SeuilCumPrecip, TypInstal
            Sql2 = "insert into Tech_perCrop (idTech_Com,idTechPerCrop,DensSem,isem,codePspecies, DbutoirNouvSemis, irepiqu,NumCrop,NumCultivar,RepiquageON,SemisAutoDebut,SeuilCumPrecip,TypInstal,Densrepiqu,Ilev,IdCultivar) values " &
            "('" & R("idMangt") & "','" & R("idMangt") & "'," & R("sdens") & "," & R("sowingdate") & ",'" & R3("codePspecies") & "'," & R3("DbutoirNouvSemis") &
            "," & R3("irepiqu") & "," & R3("NumCrop") & ",'" & R3("NumCultivar") & "'," & R3("RepiquageON") & "," & R3("SemisAutoDebut") & "," & R3("SeuilCumPrecip") &
            "," & R3("TypInstal") & "," & R("sdens") & "," & CInt(R("sowingdate")) + 5 & "," & R("IdcultivarCelsius") & ")"
            command = New OleDbCommand(Sql2, connection)
            command.ExecuteNonQuery()
            If Form1.msgErr_expCelsius_export.Text <> "Tech_commun " & R.Item("idpoint") Then
                Form1.msgErr_expCelsius_export.Text = "Tech_commun " & R.Item("idpoint")
                Form1.msgErr_expCelsius_export.Refresh()
            End If
            command.Dispose()
        Next
        '---------------------------- 
        Jeu1.Clear()
        Sql1 = "SELECT SimUnitList.idIni, SimUnitList.idsim, SimUnitList.idMangt, SimUnitList.idsoil, SimUnitList.idPoint,StartYear,StartDay,EndYear,EndDay,idOption, Coordinates.latitudeDD, CropManagement.sowingdate " _
        & " FROM CropManagement INNER JOIN (InitialConditions INNER JOIN (Coordinates INNER JOIN SimUnitList ON Coordinates.idPoint = SimUnitList.idPoint) ON InitialConditions.idIni = SimUnitList.idIni) " _
        & " ON CropManagement.idMangt = SimUnitList.idMangt;"

        '"Select  idMangt, InitialConditions.idIni, idsim,IdSoil,Coordinates.idPoint,StartYear,EndDay,Coordinates.latitudeDD FROM InitialConditions INNER JOIN (Coordinates INNER JOIN SimUnitList On Coordinates.idPoint = SimUnitList.idPoint) On InitialConditions.idIni = SimUnitList.idIni;"
        Ap_ADP1 = New OleDb.OleDbDataAdapter(Sql1, MI_connection)
        Ap_ADP1.Fill(Jeu1)
        Sql2 = "Delete * from SimUnitList"
        command = New OleDbCommand(Sql2, connection)
        command.ExecuteNonQuery()
        command.Dispose()
        For i = 0 To Jeu1.Tables(0).Rows.Count - 1
            R = Jeu1.Tables(0).Rows(i)
            Sql2 = "insert into SimUnitList (idTech_Com,IdIni,idSim,IdSoil,IdWeather,StartYear,EndDay,codCC,EndYear,idCodModel,idGenParam,StartDay) values " &
        "('" & R("idMangt") & "','" & R("idIni") & "','" & R("idsim") & "','" & R("IdSoil") & "','" & R("IdPoint") & "'," &
         R("StartYear") & "," & R("EndDay") & ",'0'," & CInt(R("Endyear")) & "," & CInt(R("idOption")) & ",1," & R("StartDay") & ")"
            'R("StartYear") & "," & R("EndDay") & ",'0'," & IIf(R("LatitudeDD") > 0, R("StartYear"), CInt(R("StartYear")) + 1) & ",1,1," & IIf(R("LatitudeDD") > 0, 50, 240) & ")"
            'MsgBox(Sql2)
            command = New OleDbCommand(Sql2, connection)
            command.ExecuteNonQuery()
            If Form1.msgErr_expCelsius_export.Text <> "SimUnitList " & R.Item("idpoint") Then
                Form1.msgErr_expCelsius_export.Text = "SimUnitList " & R.Item("idpoint")
                Form1.msgErr_expCelsius_export.Refresh()
            End If
            command.Dispose()
        Next
        '----------------------------
        Jeu1.Clear()
        Jeu2.Clear()
        'Sql1 = "Select IdSoil, bd, OrganicNStock, RunoffType, Slope, SoilRDepth, SoilTextureType, SoilTotalDepth,cf,Wfc,Wwp FROM Soil;"
        Sql1 = "SELECT IdSoil, bd, OrganicNStock, RunoffTypes.CodRunoffCelsius as RunOffType, Slope, SoilRDepth, SoilTextureType, SoilTotalDepth, cf, Wfc, Wwp FROM RunoffTypes INNER JOIN Soil ON RunoffTypes.RunoffType = Soil.RunoffType;"
        Ap_ADP1 = New OleDb.OleDbDataAdapter(Sql1, MI_connection)
        Ap_ADP1.Fill(Jeu1)
        Sql2 = "Delete * from Soil"
        command = New OleDbCommand(Sql2, connection)
        command.ExecuteNonQuery()
        command.Dispose()
        Sql2 = "Delete * from Soil_Layers"
        command = New OleDbCommand(Sql2, connection)
        command.ExecuteNonQuery()
        command.Dispose()
        Sql3 = "Select * from SoilDV"
        Ap_ADP2 = New OleDb.OleDbDataAdapter(Sql3, connection)
        Ap_ADP2.Fill(Jeu2)
        R2 = Jeu2.Tables(0).Rows(0)
        For i = 0 To Jeu1.Tables(0).Rows.Count - 1
            R = Jeu1.Tables(0).Rows(i)
            Sql2 = "insert into Soil (IdSoil,StockN,TypeRui,Zmes,ZObstacleRac,FMin,NbCouches,SeuilEvap,txMinN,Zsurf) values " &
            "('" & R("IdSoil") & "'," & R("OrganicNStock") & ",'" & R("RunoffType") & "'," & R("SoilTotalDepth") & "," & R("SoilRDepth") & "," & R2("FMin") & ",1," & R2("SeuilEvap") & "," & R2("txminN") & "," & R2("Zsurf") & ")"
            command = New OleDbCommand(Sql2, connection)
            command.ExecuteNonQuery()
            command.Dispose()
            Sql2 = "insert into Soil_Layers (IdSoil,numcouche,da,epc,hcc,hmin) values " &
            "('" & R("IdSoil") & "',1," & R("bd") & "," & R("SoilTotalDepth") & "," & CDbl((1 - R("cf") / 100) * R("wfc")) / CDbl(R("bd")) & "," & CDbl((1 - R("cf") / 100) * R("wwp")) / CDbl(R("bd")) & ")"
            command = New OleDbCommand(Sql2, connection)
            command.ExecuteNonQuery()
            command.Dispose()
            If Form1.msgErr_expCelsius_export.Text <> "Soil " & R.Item("idSoil") Then
                Form1.msgErr_expCelsius_export.Text = "Soil " & R.Item("idSoil")
                Form1.msgErr_expCelsius_export.Refresh()
            End If
        Next
        '----------------------------
        connection.Close()
        MI_connection.Close()
        WriteFile(DirectoryPath & "\", "fin.txt", "exit")
        'MsgBox("Fini")
    End Sub

    Public Overrides Sub Import(DirectoryPath As String, model As String)
        Throw New NotImplementedException()
    End Sub

    'Public Overrides Sub Export(DirectoryPath As String)
    '    Throw New NotImplementedException()
    'End Sub
End Class
