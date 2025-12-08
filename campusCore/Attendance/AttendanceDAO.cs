using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace campusCore.Attendance
{
    public class AttendanceDAO
    {
        private static readonly string ConnStr = "Data Source=Db/CompusCore.db;Version=3;";

        public static string GetStatus(string studentId, string date)
        {
            string status = null;

            using (SQLiteConnection conn = new SQLiteConnection(ConnStr))
            {
                conn.Open();

                string sql = @"SELECT status 
                               FROM attendance 
                               WHERE studentId = @id AND attendanceDate = @date";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", studentId);
                    cmd.Parameters.AddWithValue("@date", date);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        status = result.ToString();
                }
            }

            return status;
        }
    }
}
