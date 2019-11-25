Imports System.Data.Odbc
Public Class Form1
    Sub TampilGrid()
        bukakoneksi()

        DA = New OdbcDataAdapter("select * From tbl_mahasiswa", CONN)
        DS = New DataSet
        DA.Fill(DS, "tbl_mahasiswa")
        DataGridView1.DataSource = DS.Tables("tbl_mahasiswa")

        tutupkoneksi()
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MunculCombo()
        TampilGrid()
    End Sub
    Sub MunculCombo()
        ComboBox1.Items.Add("Ilmu Komputer")
        ComboBox1.Items.Add("Kimia")
        ComboBox1.Items.Add("Fisika")
        ComboBox1.Items.Add("Matematika")

    End Sub

    Sub KosongkanData()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            MsgBox("Silahkan Isi Semua Form")
        Else
            bukakoneksi()
            Dim simpan As String = "insert into tbl_mahasiswa values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & "')"

            CMD = New OdbcCommand(simpan, CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Input data berhasil")
            TampilGrid()
            KosongkanData()

            tutupkoneksi()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        TextBox1.MaxLength = 6
        If e.KeyChar = Chr(13) Then
            bukakoneksi()
            CMD = New OdbcCommand("Select * From tbl_mahasiswa where NIMMHS='" &
                    TextBox1.Text & "'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If Not RD.HasRows Then
                MsgBox("NIM Tidak Ada, Silahkan coba lagi!")
                TextBox1.Focus()
            Else
                TextBox2.Text = RD.Item("nama_mhs")
                TextBox3.Text = RD.Item("alamat_mhs")
                TextBox4.Text = RD.Item("telepon_mhs")
                ComboBox1.Text = RD.Item("jurusan_mhs")
                TextBox2.Focus()
            End If
        End If
    End Sub
End Class