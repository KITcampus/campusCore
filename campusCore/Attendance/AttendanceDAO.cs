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
        // 로그인한 학생의 출석 상태를 가져옴
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
        // 마지막 출석 날짜를 가져옴
        public static DateTime LastAttDate(string studentId)
        {
            DateTime lastDate = DateTime.MinValue;

            using (var conn = new SQLiteConnection(ConnStr))
            {
                conn.Open();

                string sql = @"SELECT attendanceDate 
                                 FROM attendance 
                                WHERE studentId = @id 
                                ORDER BY attendanceDate DESC 
                                LIMIT 1";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", studentId);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        lastDate = DateTime.Parse(result.ToString());
                    }
                }
            }
            return lastDate;
        }
        // 출석 기록이 이미 DB에 있는지 확인
        public static bool IsExist(string studentId, string date)
        {
            using (var conn = new SQLiteConnection(ConnStr))
            {
                conn.Open();

                string sql = @"SELECT COUNT(*) 
                                 FROM attendance 
                                WHERE studentId = @id AND attendanceDate = @date";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", studentId);
                    cmd.Parameters.AddWithValue("@date", date);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        public static void FillAbsentDates(string studentId)
        {
            DateTime today = DateTime.Today;

            // 마지막 출석 날짜 가져오기
            DateTime lastDate = LastAttDate(studentId);

            // 기록이 하나도 없다면 → lastDate를 어제로 설정
            if (lastDate == DateTime.MinValue)
                lastDate = today.AddDays(-1);

            // lastDate 다음 날부터 오늘 전날까지 결석 처리
            for (DateTime dt = lastDate.AddDays(1); dt < today; dt = dt.AddDays(1))
            {
                // 주말은 스킵
                if (dt.DayOfWeek == DayOfWeek.Saturday ||
                    dt.DayOfWeek == DayOfWeek.Sunday)
                    continue;

                string dateStr = dt.ToString("yyyy-MM-dd");

                // 이미 존재하면 패스
                if (IsExist(studentId, dateStr))
                    continue;

                // 결석 자동 INSERT
                InsertAbsent(studentId, dateStr);
            }
        }
        public static void InsertAbsent(string studentId, string date)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConnStr))
            {
                conn.Open();

                string sql = @"INSERT INTO attendance (studentId, attendanceDate, status)
                               VALUES (@id, @date, '결석')";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", studentId);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
