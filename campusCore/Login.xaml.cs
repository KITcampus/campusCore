using campusCore.Common;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace campusCore
{
    /// <summary>
    /// Login.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            // 저장된 아이디 불러오기
            if (!string.IsNullOrEmpty(Properties.Settings.Default.SavedId))
            {
                txtId.Text = Properties.Settings.Default.SavedId;
                chkSaveId.IsChecked = true;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // 아이디 저장 로직
            if (chkSaveId.IsChecked == true)
            {
                Properties.Settings.Default.SavedId = txtId.Text;
                Properties.Settings.Default.IsSaveId = true;
            }
            else
            {
                Properties.Settings.Default.SavedId = "";
                Properties.Settings.Default.IsSaveId = false;
            }

            Properties.Settings.Default.Save();

            // 로그인 로직
            string id = txtId.Text.Trim();
            string pw = txtPw.Password.Trim();
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
            {
                MessageBox.Show("아이디와 비밀번호를 입력하세요.");
                return;
            }

            // SQLite 연결
            string connStr = "Data Source=Db/CompusCore.db;Version=3;";

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();

                string sql = "SELECT COUNT(*) FROM StuInfo WHERE studentId = @id AND pw = @pw";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@pw", pw);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count == 1)
                    {
                        // 세션에 로그인한 학생 아이디 저장
                        UserSession.StudentId = id;

                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("아이디 또는 비밀번호가 올바르지 않습니다.");
                    }
                }
            }
        }
        private void txtId_TextChanged(object sender, TextChangedEventArgs e)
        {
            phId.Visibility = string.IsNullOrEmpty(txtId.Text) ? Visibility.Visible : Visibility.Hidden;
        }
    
    }
   
}
