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
''' <summary>
''' Instanciate the correct converter for Importing/Exporting data from a selected Model and Sub Theme.
''' </summary>
Public Class ConverterFactory

    ''' <summary>
    ''' Retrieves a converter corresponding to a selected Model and SubTheme
    ''' </summary>
    ''' <param name="Model">Selected Model used by Converter.</param>
    ''' <param name="SubTheme">Selected SubTheme to convert. Selected from select box component.</param>
    ''' <returns>A instanciated converter, corresponding to the selected Model/SubTheme pair.</returns>
    Public Function GetConverter(ByVal Model As String, ByVal SubTheme As String) As Converter
        Dim converter = Nothing

        Select Case Model

            'Case "Sarrah"
            '    converter = New SarrahConverter()
            Case "DSSAT"
                converter = New DssatConverter()
            Case "STICS"
                converter = New SticsConverter()
            Case "Celsius"
                converter = New CelsiusConverter()
                'Case "APSIM"
                '    'MessageBox.Show("APSIM UNDER CONSTRUCTION")
                '    converter = New ApsimConverter()
            Case Else
                Debug.WriteLine("Unknow selected Model")
        End Select

        Return converter
    End Function

End Class
