Option Explicit On

Public Class Park_manager

    Private my_dataset As New Data.DataSet

    Public Function read_xml() As Boolean

        Dim i As Int16 = 0
        Dim j As Int16 = 0
        Dim my_tmp As String = ""
        Dim my_tmp_c As String = ""
        Dim my_parks() As String

        Try
            my_dataset.ReadXml(Application.StartupPath.ToString + "\park_settings.xml")
            my_tmp = my_dataset.Tables("Parcheggi").Rows(0).Item("identificativi").ToString
             If (my_tmp = "") Then Return False
            my_parks = Split(my_tmp, ",", , CompareMethod.Text)
            ReDim ip_settings(0 To UBound(my_parks))
            For i = 0 To UBound(my_parks)
                ip_settings(i).id = my_parks(i)
                ip_settings(i).name = my_dataset.Tables(my_parks(i)).Rows(0).Item("name").ToString
                ip_settings(i).server_ip = my_dataset.Tables(my_parks(i)).Rows(0).Item("server_ip").ToString
                ip_settings(i).pass_serv = my_dataset.Tables(my_parks(i)).Rows(0).Item("pass_serv").ToString
                ip_settings(i).server_ip_c = my_dataset.Tables(my_parks(i)).Rows(0).Item("server_ip_c").ToString
            Next
            Return True

        Catch
            Return False
        End Try

    End Function

End Class
