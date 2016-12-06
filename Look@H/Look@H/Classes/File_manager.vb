Option Explicit On

Imports System.IO

Public Class File_manager

    Public Function SaveTextToFile(ByVal my_data As String, ByVal file_path As String) As Boolean

        Dim Contents As String = ""
        Dim objWriter As StreamWriter
        Dim my_res_1 As Boolean = False
        Dim my_res_2 As Boolean = False

        Try
            objWriter = New StreamWriter(file_path)
            objWriter.Write(my_data)
            my_res_1 = True
        Catch
        End Try

        Try
            objWriter.Close()
            my_res_2 = True
        Catch
        End Try

        Return my_res_1 And my_res_2

    End Function

End Class
