Option Explicit On
Public Class Main

    Private selected_park As String = ""

    Private Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
        End
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        popola_combo_parcheggi()
        LoadTree1()
        Button3.Enabled = False
        Button2.Enabled = False
        Timer1.Enabled = True
        Timer1.Interval = 500000
        Timer1.Start()
    End Sub

    Public Sub rdbDisconnessione_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub
    Public Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Button2.Enabled = False
        Button3.Enabled = False
        Application.DoEvents()
        ping1()
        Timer1.Interval = 500000

    End Sub
    Private my_dataset As New Data.DataSet
    Private Sub LoadTree1()
        Dim i As Int16
        Dim j As Integer
        Dim my_serv_c() As String
        Dim mNode As New TreeNode
        Dim my_tmp As String = ""
        Dim my_tmp_c As String = ""
        Dim my_parks() As String

        TreeView1.Nodes.Clear()
        TreeView1.BeginUpdate()
        my_park_manager.read_xml()
        my_dataset.ReadXml(Application.StartupPath.ToString + "\park_settings.xml")
        my_tmp = my_dataset.Tables("Parcheggi").Rows(0).Item("identificativi").ToString

        my_parks = Split(my_tmp, ",", , CompareMethod.Text)
        ReDim park_settings(0 To UBound(my_parks))
        For i = 0 To UBound(my_parks)
            park_settings(i).id = my_parks(i)
            park_settings(i).name = my_dataset.Tables(my_parks(i)).Rows(0).Item("name").ToString
            park_settings(i).server_ip = my_dataset.Tables(my_parks(i)).Rows(0).Item("server_ip").ToString
            park_settings(i).pass_serv = my_dataset.Tables(my_parks(i)).Rows(0).Item("pass_serv").ToString
            park_settings(i).server_ip_c = my_dataset.Tables(my_parks(i)).Rows(0).Item("server_ip_c").ToString
            TreeView1.Nodes.Add(park_settings(i).name.ToString & " ..." & park_settings(i).server_ip.ToString)
            my_serv_c = Split(park_settings(i).server_ip_c, ",", , CompareMethod.Text)
            For j = 0 To UBound(my_serv_c)
                mNode = TreeView1.Nodes(i)
                mNode.Nodes.Add("CASSA " & j + 1 & "..." & my_serv_c(j).ToString)
            Next j
        Next
        TreeView1.EndUpdate()
    End Sub
    
    Private Sub ping1()
        Dim i As Integer
        Dim j As Integer
        Dim my_serv_c() As String
        Dim mNode As TreeNode
        Dim Imagelist1 As New System.Windows.Forms.ImageList

        TextBox1.Visible = False
        Label4.Visible = False
        Button2.Enabled = False
        Button3.Enabled = False
        my_park_manager.read_xml()
        TreeView1.Nodes.Clear()
        'TreeView1.BeginUpdate()
        ListBox1.Items.Clear()
        ListBox3.Items.Clear()
        For i = 0 To UBound(park_settings)
            'TreeView1.BeginUpdate()
            Label3.Enabled = True
            'ProgressBar1.Step = i
            my_park_manager.read_xml()
            My.Computer.Network.Ping(park_settings(i).server_ip, 4000)
            My.Computer.Network.Ping(park_settings(i).server_ip, 4000)
            If My.Computer.Network.Ping(park_settings(i).server_ip) = True Then
                TreeView1.Nodes.Add(park_settings(i).name & "..." & park_settings(i).server_ip.ToString & "...OK!")
                'my_serv_c = Split(park_settings(i).server_ip_c, ",", , CompareMethod.Binary)
                If ListBox2.Items.Contains("PING OK " & park_settings(i).name) Then
                ElseIf ListBox2.Items.Contains("PING FAIL " & park_settings(i).name) Then
                    ListBox2.Items.Remove("PING FAIL " & park_settings(i).name)
                    ListBox2.Items.Add("PING OK " & park_settings(i).name)
                    My.Computer.Audio.Play("C:\Programmi\Look@H\27882__Stickinthemud__Bike_Horn_double_toot.wav")
                Else
                    ListBox2.Items.Add("PING OK " & park_settings(i).name)
                End If
            ElseIf My.Computer.Network.Ping(park_settings(i).server_ip) = True Then
                TreeView1.Nodes.Add(park_settings(i).name & "..." & park_settings(i).server_ip.ToString & "...OK!")
                my_serv_c = Split(park_settings(i).server_ip_c, ",", , CompareMethod.Binary)
                If ListBox2.Items.Contains("PING OK " & park_settings(i).name) Then
                ElseIf ListBox2.Items.Contains("PING FAIL " & park_settings(i).name) Then
                    ListBox2.Items.Remove("PING FAIL " & park_settings(i).name)
                    ListBox2.Items.Add("PING OK " & park_settings(i).name)
                    My.Computer.Audio.Play("C:\Programmi\Look@H\27882__Stickinthemud__Bike_Horn_double_toot.wav")
                Else
                    ListBox2.Items.Add("PING OK " & park_settings(i).name)
                End If
            ElseIf My.Computer.Network.Ping(park_settings(i).server_ip) = True Then
                TreeView1.Nodes.Add(park_settings(i).name & "..." & park_settings(i).server_ip.ToString & "...OK!")
                my_serv_c = Split(park_settings(i).server_ip_c, ",", , CompareMethod.Binary)
                If ListBox2.Items.Contains("PING OK " & park_settings(i).name) Then
                ElseIf ListBox2.Items.Contains("PING FAIL " & park_settings(i).name) Then
                    ListBox2.Items.Remove("PING FAIL " & park_settings(i).name)
                    ListBox2.Items.Add("PING OK " & park_settings(i).name)
                    My.Computer.Audio.Play("C:\Programmi\Look@H\27882__Stickinthemud__Bike_Horn_double_toot.wav")
                Else
                    ListBox2.Items.Add("PING OK " & park_settings(i).name)
                End If
                Application.DoEvents()
            Else
                If ListBox2.Items.Contains("PING FAIL " & park_settings(i).name) Then
                ElseIf ListBox2.Items.Contains("PING OK " & park_settings(i).name) Then
                    ListBox2.Items.Remove("PING OK " & park_settings(i).name)
                    ListBox2.Items.Add("PING FAIL " & park_settings(i).name)
                    My.Computer.Audio.Play("C:\Programmi\Look@H\27882__Stickinthemud__Bike_Horn_double_toot.wav")
                Else
                    ListBox2.Items.Add("PING FAIL " & park_settings(i).name)
                    My.Computer.Audio.Play("C:\Programmi\Look@H\27882__Stickinthemud__Bike_Horn_double_toot.wav")
                End If
                TreeView1.Nodes.Add(park_settings(i).name & "..." & park_settings(i).server_ip.ToString & "...FAIL!")
                TreeView1.Nodes(i).BackColor = Color.Red
                ListBox1.Items.Add("PING FAIL " & park_settings(i).name)
            End If
            Application.DoEvents()
            my_serv_c = Split(park_settings(i).server_ip_c, ",", , CompareMethod.Binary)
            For j = 0 To UBound(my_serv_c)
                My.Computer.Network.Ping(my_serv_c(j), 6000)
                My.Computer.Network.Ping(my_serv_c(j), 6000)
                If My.Computer.Network.Ping(my_serv_c(j)) = True Then
                    mNode = TreeView1.Nodes(i)
                    mNode.Nodes.Add("CASSA " & j + 1 & "..." & my_serv_c(j).ToString & "...OK!")
                ElseIf My.Computer.Network.Ping(my_serv_c(j)) = True Then
                    mNode = TreeView1.Nodes(i)
                    mNode.Nodes.Add("CASSA " & j + 1 & "..." & my_serv_c(j).ToString & "...OK!")
                Else
                    mNode = TreeView1.Nodes(i)
                    mNode.Nodes.Add("CASSA " & j + 1 & "..." & my_serv_c(j).ToString & "...FAIL!")
                    ListBox1.Items.Add("PING FAIL...Cassa " & j + 1 & " di " & park_settings(i).name)
                End If
            Next j
            Application.DoEvents()
            TreeView1.EndUpdate()
        Next
        Application.DoEvents()
        Label3.Enabled = False
        TreeView1.EndUpdate()
        ListBox3.Items.Clear()
    End Sub
    Private Sub ping()
        Dim i As Integer
        Dim j As Integer
        Dim my_serv_c() As String
        Dim mNode As TreeNode
        Dim Imagelist1 As New System.Windows.Forms.ImageList

        TextBox1.Visible = False
        Label4.Visible = False
        my_park_manager.read_xml()
        my_serv_c = Split(park_settings(i).server_ip_c, ",", , CompareMethod.Binary)
        ListBox3.Items.Clear()
        For i = 0 To UBound(park_settings)
            Label2.Enabled = True
            My.Computer.Network.Ping(park_settings(i).server_ip, 10000)
            If My.Computer.Network.Ping(park_settings(i).server_ip) Then
            Else
                If My.Computer.Network.Ping(park_settings(i).server_ip) Then
                Else
                    If My.Computer.Network.Ping(park_settings(i).server_ip) Then
                    Else
                        my_serv_c = Split(park_settings(i).server_ip_c, ",", , CompareMethod.Binary)
                        ListBox3.Items.Add("PING FAIL " & park_settings(i).name)
                        'My.Computer.Audio.Play("C:\Documents and Settings\ermesa\Desktop\eseguibili\Look@H\27882__Stickinthemud__Bike_Horn_double_toot.wav")
                    End If
                End If
            End If
            Application.DoEvents()
            my_serv_c = Split(park_settings(i).server_ip_c, ",", , CompareMethod.Text)
            For j = 0 To UBound(my_serv_c)
                My.Computer.Network.Ping(my_serv_c(j), 10000)
                If My.Computer.Network.Ping(my_serv_c(j)) Then
                Else
                    If My.Computer.Network.Ping(my_serv_c(j)) Then
                    Else
                        If My.Computer.Network.Ping(my_serv_c(j)) Then
                        Else
                            ListBox3.Items.Add("PING FAIL...Cassa " & j + 1 & " di " & park_settings(i).name)
                            'My.Computer.Audio.Play("C:\Documents and Settings\ermesa\Desktop\eseguibili\Look@H\27882__Stickinthemud__Bike_Horn_double_toot.wav")
                        End If
                    End If
                End If
                Application.DoEvents()
            Next j
            Application.DoEvents()
        Next
        Application.DoEvents()
        Label2.Enabled = False
        TreeView1.EndUpdate()
    End Sub
    Private Sub popola_combo_parcheggi()

        Dim i As Int16

        Try
            cmbPark.Items.Clear()
            my_park_manager.read_xml()
            For i = 0 To UBound(park_settings)
                cmbPark.Items.Add(park_settings(i).id & " " & park_settings(i).name)

            Next
        Catch 'ex As Exception

        End Try

    End Sub

    Private Sub cmbPark_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPark.SelectedIndexChanged
        Dim my_serv_c() As String
        Dim i As Integer

        TextBox1.Visible = False
        Label4.Visible = False
        Label2.Enabled = True
        Application.DoEvents()
        My.Computer.Network.Ping(park_settings(cmbPark.SelectedIndex).server_ip, 6000)
        If My.Computer.Network.Ping(park_settings(cmbPark.SelectedIndex).server_ip) Then
            MsgBox("PING " & park_settings(cmbPark.SelectedIndex).name.ToString & "...OK")
        Else
            If My.Computer.Network.Ping(park_settings(cmbPark.SelectedIndex).server_ip) Then
                MsgBox("PING " & park_settings(cmbPark.SelectedIndex).name.ToString & "...OK")
            Else
                If My.Computer.Network.Ping(park_settings(cmbPark.SelectedIndex).server_ip) Then
                    MsgBox("PING " & park_settings(cmbPark.SelectedIndex).name.ToString & "...OK")
                Else
                    MsgBox("PING FAIL Server..." & park_settings(cmbPark.SelectedIndex).name)
                End If
            End If
        End If
        my_park_manager.read_xml()
        my_serv_c = Split(park_settings(cmbPark.SelectedIndex).server_ip_c, ",", , CompareMethod.Binary)
        For i = 0 To UBound(my_serv_c)
            Label2.Enabled = True
            My.Computer.Network.Ping(my_serv_c(i), 6000)
            If My.Computer.Network.Ping(my_serv_c(i)) Then
                MsgBox("PING Cassa " & i + 1 & " ...OK!")
            Else
                If My.Computer.Network.Ping(my_serv_c(i)) Then
                    MsgBox("PING Cassa " & i + 1 & " ...OK!")
                Else
                    MsgBox("PING Cassa " & i + 1 & " ...Fail!")
                End If
            End If
        Next i
        Application.DoEvents()
        Label2.Enabled = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ping()
        Application.DoEvents()
    End Sub

   
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim my_serv_c() As String
        Dim i As Integer
        Dim indirizzoip As String
        Dim Programma As String
        Dim TaskID As Double

        TextBox1.Visible = False
        Label4.Visible = False
        Label2.Enabled = True
        Button3.Enabled = False
        If TreeView1.Nodes(TreeView1.SelectedNode.Parent.Index).Index <> 100 Then
            ListBox3.Items.Clear()
            my_park_manager.read_xml()
            my_serv_c = Split(park_settings(TreeView1.SelectedNode.Parent.Index).server_ip_c, ",", , CompareMethod.Binary)
            For i = 0 To UBound(my_serv_c)
                my_serv_c = Split(park_settings(TreeView1.SelectedNode.Parent.Index).server_ip_c, ",", , CompareMethod.Binary)
                If TreeView1.SelectedNode.Index = i Then
                    indirizzoip = my_serv_c(i)
                    Programma = "C:\Programmi\RealVNC\VNC4\vncviewer.exe " & indirizzoip.ToString
                    TaskID = Shell(Programma, 1)
                    My.Computer.Network.Ping(my_serv_c(i), 8000)
                    If My.Computer.Network.Ping(my_serv_c(i)) = True Then
                        ListBox3.Items.Add("PING Cassa " & i + 1 & " di " & park_settings(TreeView1.SelectedNode.Parent.Index).name & "...OK!")
                    Else
                        If My.Computer.Network.Ping(my_serv_c(i)) = True Then
                            ListBox3.Items.Add("PING Cassa " & i + 1 & " di " & park_settings(TreeView1.SelectedNode.Parent.Index).name & "...OK!")
                        Else
                            ListBox3.Items.Add("PING Cassa " & i + 1 & " di " & park_settings(TreeView1.SelectedNode.Parent.Index).name & "...Fail!")
                        End If
                    End If
                    'ListBox3.Items.Clear()
                End If
            Next
            Application.DoEvents()
            Button2.Enabled = False
            Button3.Enabled = False
            Label2.Enabled = False
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim my_serv_c() As String
        Dim i As Integer

        TextBox1.Visible = True
        Label4.Visible = True
        TextBox1.Text = park_settings(TreeView1.SelectedNode.Index).pass_serv.ToString
        Button2.Enabled = False
        Label2.Enabled = True
        ListBox3.Items.Clear()
        My.Computer.Network.Ping(park_settings(TreeView1.SelectedNode.Index).server_ip, 6000)
        Dim indirizzoip As String
        Dim Programma As String
        Dim TaskID As Double
        'On Error Resume Next
        'Programma = "C:\Users\malik\Desktop\vnc-4_1_3-x86_win32_viewer.exe"
        indirizzoip = park_settings(TreeView1.SelectedNode.Index).server_ip
        Programma = "C:\Programmi\RealVNC\VNC4\vncviewer.exe " & indirizzoip.ToString
        TaskID = Shell(Programma, 1)
        If My.Computer.Network.Ping(park_settings(TreeView1.SelectedNode.Index).server_ip) Then
            ListBox3.Items.Add("PING " & park_settings(TreeView1.SelectedNode.Index).name.ToString & " OK")
        Else
            If My.Computer.Network.Ping(park_settings(TreeView1.SelectedNode.Index).server_ip) Then
                ListBox3.Items.Add("PING " & park_settings(TreeView1.SelectedNode.Index).name.ToString & " OK")
            Else
                ListBox3.Items.Add("PING FAIL " & park_settings(TreeView1.SelectedNode.Index).name)
            End If
        End If
        my_park_manager.read_xml()
        my_serv_c = Split(park_settings(TreeView1.SelectedNode.Index).server_ip_c, ",", , CompareMethod.Binary)
        For i = 0 To UBound(my_serv_c)
            Label2.Enabled = True
            ProgressBar1.Step = i
            TreeView1.SelectedNode = TreeView1.Nodes(TreeView1.SelectedNode.Index)
            My.Computer.Network.Ping(my_serv_c(i), 10000)
            If My.Computer.Network.Ping(my_serv_c(i)) Then
                ListBox3.Items.Add("PING Cassa " & i + 1 & "...OK!")
            Else
                If My.Computer.Network.Ping(my_serv_c(i)) Then
                    ListBox3.Items.Add("PING Cassa " & i + 1 & "...OK!")
                Else
                    If My.Computer.Network.Ping(my_serv_c(i)) Then
                        ListBox3.Items.Add("PING Cassa " & i + 1 & "...OK!")
                    Else
                        ListBox3.Items.Add("PING Cassa " & i + 1 & "...Fail!")
                    End If
                End If
            End If
        Next i
        Label4.Visible = True
        TextBox1.Visible = True
        Application.DoEvents()
        Label2.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = False
        Exit Sub

    End Sub 

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect

        'Button2.Enabled = False
        'If TreeView1.Nodes(TreeView1.SelectedNode.Parent.Index).Index Then
        TextBox1.Visible = False
        Label4.Visible = False
        If TreeView1.SelectedNode.Level = 0 Then
            Button2.Enabled = False
            Button3.Enabled = True
        ElseIf TreeView1.SelectedNode.Level = 1 Then
            Button2.Enabled = True
            Button3.Enabled = False
        End If
        'Button3.Enabled = True
    End Sub
End Class

