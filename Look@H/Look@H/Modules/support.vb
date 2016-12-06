Option Explicit On

Imports Microsoft.Win32



Module My_support

    Public my_manager As New Manager

    Public Structure ipp_settings

        Public server_ip As String
        Public server_ip_c As String
        Public id As String
        Public name As String
        Public pass_serv As String
    End Structure

    Public ip_settings() As ipp_settings

End Module
