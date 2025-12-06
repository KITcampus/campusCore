using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace campusCore
{
    class StudentDAO
    {
        public bool Login(string studentId, string password)
        {
            using (var conn = Dbcontact.GetConnection())
            {
                conn.Open();

                string sql = "SELECT COUNT(*) FROM StuInfo WHERE studentId = @id AND pw = @pw";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", studentId);
                    cmd.Parameters.AddWithValue("@pw", password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count == 1;
                }
            }
        }
    }
}
