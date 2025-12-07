using campusCore.Common;
using Microsoft.Data.Sqlite;
using System.Windows;

namespace campusCore.Repository
{
    public class StuInfoRepository
    {
        private readonly string connectionString = "Data Source=Db/CompusCore.db;";

        public StuInfoDto GetStudent(int studentId)
        {
            StuInfoDto student = null;

            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM StuInfo WHERE studentId = @id";
                cmd.Parameters.AddWithValue("@id", studentId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) 
                    {
                        student = new StuInfoDto
                        {
                            studentId = reader.GetInt32(reader.GetOrdinal("studentId"))
                        };
                    }
                    else
                    {
                        MessageBox.Show("학생 없음");
                    }

                }
            }

            return student;
        }
    }
}
