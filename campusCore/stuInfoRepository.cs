using campusCore.Common;
using Microsoft.Data.Sqlite;
using System.Windows;

namespace campusCore.Repository
{
    // Repository: DB에 직접 접근하여 데이터를 가져오거나 저장하는 역할 (ViewModel과 DB 사이 중간 계층)
    public class StuInfoRepository
    {
        // SQLite DB 파일 위치 설정
        private readonly string connectionString = "Data Source=Db/CompusCore.db;";

        // studentId(학번)를 기준으로 학생 데이터를 조회하는 함수
        public StuInfoDto GetStudent(int studentId)
        {
            StuInfoDto student = null; // 조회된 결과를 담을 DTO (처음엔 null)

            // DB 연결을 using으로 감싸면 자동으로 Dispose(연결 종료)됨
            using (var conn = new SqliteConnection(connectionString))
            {
                conn.Open(); // DB 연결

                // SQL 명령(쿼리) 준비
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM StuInfo WHERE studentId = @id"; // 조건 검색
                cmd.Parameters.AddWithValue("@id", studentId); // SQL Injection 방지를 위한 파라미터 바인딩

                // SQL 실행 → 결과 Reader로 반환됨
                using (var reader = cmd.ExecuteReader())
                {
                    // 데이터가 존재하면 true
                    if (reader.Read())
                    {
                        // DTO 생성 후 DB에서 값 채움
                        student = new StuInfoDto
                        {
                            // studentId 컬럼 값을 정수형으로 가져와 DTO에 넣기
                            studentId = reader.GetInt32(reader.GetOrdinal("studentId")),
                            name = reader.GetString(reader.GetOrdinal("name")),
                            grade = reader.GetInt32(reader.GetOrdinal("grade")),
                            class_ = reader.GetString(reader.GetOrdinal("class")),
                            residentNum = reader.GetString(reader.GetOrdinal("residentNum"))
                        };
                    }
                    else
                    {
                        // 조회 결과 없으면 메시지 출력
                        MessageBox.Show("학생 없음");
                    }
                }
            }

            // ViewModel로 DTO 반환
            return student;
        }
    }
}
