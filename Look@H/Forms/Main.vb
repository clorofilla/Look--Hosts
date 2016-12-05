Option Explicit On

Imports System.Threading

Public Class Main

    Private selected_park As String = ""

    Private my_database As New lib_dbhandler.DataBase_Handling
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadTree1()
        Timer1.Enabled = True
        Timer1.Interval = 18000
        Timer1.Start()
    End Sub

    Public Sub rdbDisconnessione_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub
    Public Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ping1()
    End Sub
    Private my_dataset As New Data.DataSet
    Private Sub LoadTree1()
        Dim i As Int16
        Dim j As Integer
        Dim mNode As New TreeNode
        Dim my_tmp As String = ""
        Dim my_tmp_c As String = ""

        Dim my_datatable As New DataTable
        Dim my_query As String

        TreeView1.Nodes.Clear()
        TreeView1.BeginUpdate()
        my_park_manager.read_xml()

        my_dataset.ReadXml(Application.StartupPath.ToString + "\park_settings.xml")
        my_tmp = my_dataset.Tables("database").Rows(0).Item("db_connection_string").ToString

        If (my_database.is_connect() = False) Then
            If (my_database.Connect(my_tmp) = False) Then Err.Raise(65535, , "Unable to connect to the database")
        End If
        my_datatable.Clear()
        my_query = "select * from ms_disconnect;"
        If (my_database.Select_DataTable(my_datatable, my_query) = False) Then Err.Raise(65535, , "Cannot extract table")


        ReDim ip_settings(my_datatable.Rows.Count)
        For i = 0 To my_datatable.Rows.Count - 1
            ip_settings(i).id = my_datatable.Rows(i)("id").ToString
            ip_settings(i).name = my_datatable.Rows(i)("id").ToString & "-" & my_datatable.Rows(i)("descrizione").ToString
            ip_settings(i).server_ip = my_datatable.Rows(i)("ip").ToString
            TreeView1.Nodes.Add(ip_settings(i).name.ToString & " ..." & ip_settings(i).server_ip.ToString)
        Next
        TreeView1.EndUpdate()
    End Sub

    Private Sub ping1()
        Dim i As Integer
        Dim j As Integer


        Dim Imagelist1 As New System.Windows.Forms.ImageList
        Dim my_datatable As New DataTable
        Dim my_queryip As String
        Dim my_queryok As String
        Dim my_queryko As String

        Dim my_tmp As String = ""
        my_tmp = my_dataset.Tables("database").Rows(0).Item("db_connection_string").ToString
        If (my_database.is_connect() = False) Then
            If (my_database.Connect(my_tmp) = False) Then Err.Raise(65535, , "Unable to connect to the database")
        End If
        my_datatable.Clear()
        my_queryip = "select * from ms_disconnect;"
        If (my_database.Select_DataTable(my_datatable, my_queryip) = False) Then Err.Raise(65535, , "Cannot extract table")

        ReDim ip_settings(my_datatable.Rows.Count)
        TreeView1.Nodes.Clear()

        ListBox1.Items.Clear()
       
        For i = 0 To UBound(ip_settings) - 1
            Application.DoEvents()
                Label3.Enabled = True
                ip_settings(i).id = my_datatable.Rows(i)("id").ToString
                ip_settings(i).name = my_datatable.Rows(i)("id").ToString & "-" & my_datatable.Rows(i)("descrizione").ToString
                ip_settings(i).server_ip = my_datatable.Rows(i)("ip").ToString

                my_queryok = "update ms_disconnect set ping='0' where id='" & ip_settings(i).id & "'"
                my_queryko = "update ms_disconnect set ping='1' where id='" & ip_settings(i).id & "'"

                If My.Computer.Network.Ping(ip_settings(i).server_ip, 4000) = True Then
                    TreeView1.Nodes.Add(ip_settings(i).name & "..." & ip_settings(i).server_ip.ToString & "...OK!")
                    If ListBox2.Items.Contains("PING OK " & ip_settings(i).name) Then
                    ElseIf ListBox2.Items.Contains("PING FAIL " & ip_settings(i).name) Then
                        ListBox2.Items.Remove("PING FAIL " & ip_settings(i).name)
                        ListBox2.Items.Add("PING OK " & ip_settings(i).name)
                    Else
                        ListBox2.Items.Add("PING OK " & ip_settings(i).name)
                    End If
                    If (my_database.ExecAction(my_queryok) = False) Then
                        Err.Raise(65535, , "Unable to execute the following query: " & my_queryok.ToString())
                    Else
                    End If
                ElseIf My.Computer.Network.Ping(ip_settings(i).server_ip, 4000) = True Then
                    TreeView1.Nodes.Add(ip_settings(i).name & "..." & ip_settings(i).server_ip.ToString & "...OK!")
                    If ListBox2.Items.Contains("PING OK " & ip_settings(i).name) Then
                    ElseIf ListBox2.Items.Contains("PING FAIL " & ip_settings(i).name) Then
                        ListBox2.Items.Remove("PING FAIL " & ip_settings(i).name)
                        ListBox2.Items.Add("PING OK " & ip_settings(i).name)
                    Else
                        ListBox2.Items.Add("PING OK " & ip_settings(i).name)
                    End If
                    If (my_database.ExecAction(my_queryok) = False) Then
                        Err.Raise(65535, , "Unable to execute the following query: " & my_queryok.ToString())
                    Else
                    End If
                ElseIf My.Computer.Network.Ping(ip_settings(i).server_ip, 4000) = True Then
                    TreeView1.Nodes.Add(ip_settings(i).name & "..." & ip_settings(i).server_ip.ToString & "...OK!")
                    If ListBox2.Items.Contains("PING OK " & ip_settings(i).name) Then
                    ElseIf ListBox2.Items.Contains("PING FAIL " & ip_settings(i).name) Then
                        ListBox2.Items.Remove("PING FAIL " & ip_settings(i).name)
                        ListBox2.Items.Add("PING OK " & ip_settings(i).name)
                    Else
                        ListBox2.Items.Add("PING OK " & ip_settings(i).name)
                    End If
                    If (my_database.ExecAction(my_queryok) = False) Then
                        Err.Raise(65535, , "Unable to execute the following query: " & my_queryok.ToString())
                    Else
                    End If
                Else
                    If ListBox2.Items.Contains("PING FAIL " & ip_settings(i).name) Then
                    ElseIf ListBox2.Items.Contains("PING OK " & ip_settings(i).name) Then
                        ListBox2.Items.Remove("PING OK " & ip_settings(i).name)
                        ListBox2.Items.Add("PING FAIL " & ip_settings(i).name)
                    Else
                        ListBox2.Items.Add("PING FAIL " & ip_settings(i).name)
                    End If
                    TreeView1.Nodes.Add(ip_settings(i).name & "..." & ip_settings(i).server_ip.ToString & "...FAIL!")
                    TreeView1.Nodes(i).BackColor = Color.Red
                    ListBox1.Items.Add("PING FAIL " & ip_settings(i).name)
                    If (my_database.ExecAction(my_queryko) = False) Then
                        Err.Raise(65535, , "Unable to execute the following query: " & my_queryko.ToString())
                    Else
                    End If
                End If
        Next

        TreeView1.EndUpdate()
        Label3.Enabled = False
        Application.DoEvents()
    End Sub
  
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ping1()
        Application.DoEvents()
    End Sub

    Private Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
        Me.Close()
    End Sub
End Class

