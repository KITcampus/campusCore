using System;
using System.Collections.Generic;
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
using System.Data.SQLite;

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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string id = txtId.Text.Trim();
            string pw = txtPw.Password.Trim();
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
            {
                MessageBox.Show("아이디와 비밀번호를 입력하세요.");
                return;
            }

            // SQLite 연결
            string connStr = "Data Source=Db/compusCore.db;Version=3;";

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
    }
}
