using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using SignUpProgram.Etc;

namespace SignUpProgram
{
    class Database : IDatabase
    {
        private static Database database;
        MemberVO Member = new MemberVO();

        string databaseConnect = "Server=localhost;Database=signup;Uid=root;Pwd=6782";      //데이터베이스 연결, 비밀번호 확인할 것
        MySqlConnection connect;
        MySqlCommand command;
        MySqlDataReader reader;
        MySqlDataAdapter adapter;

        private Database() { }

        public static Database getInstance()
        {
            if (database == null)
            {
                database = new Database();
            }
            return database;
        }
        //중복되는 데이터가 존재하는지 확인하기위해 데이터셋을 리턴 후 Exceptions에서 중복되는 값 체크
        public DataSet ExceptionsIsDuplicated(string sql, string table)
        {
            connect = new MySqlConnection(databaseConnect);
            adapter = new MySqlDataAdapter(sql, connect);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset, table);

            return dataset;
        }
        //해당하는 데이터의 회원정보를 데이터베이스에서 가져옴
        public MemberVO GetMemberInfo(string table, string attribute, string data)
        {
            string sql = "select * from member where " + attribute + " = '" + data + "';";

            this.ConnectDatabase(sql);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Member.MemberNAME = reader["name"].ToString();
                Member.MemberFIRSTRESIDENTNUMBER = reader["firstresidentnumber"].ToString();
                Member.MemberSECONDRESIDENTNUMBER = reader["secondresidentnumber"].ToString();
                Member.MemberUSERNAME = reader["username"].ToString();
                Member.MemberEMAIL = reader["email"].ToString();
                Member.MemberPASSWORD = reader["password"].ToString();
                Member.MemberPHONENUMBER = reader["phonenumber"].ToString();
                Member.MemberADDRESS = reader["address"].ToString();
                Member.MemberSPECIFICADDRESS = reader["specificaddress"].ToString();
            }
            reader.Close();
            DisconnectDatabase();
            return Member;
        }
        //회원정보 데이터베이스에 삽입 / 회원가입
        public void InsertMember(MemberVO Member)
        {
            string sql = "insert into member values('" + Member.MemberNAME + "', '" + Member.MemberFIRSTRESIDENTNUMBER + "', '" + Member.MemberSECONDRESIDENTNUMBER
                + "', '" + Member.MemberUSERNAME + "', '" + Member.MemberEMAIL + "', '" + Member.MemberPASSWORD + "', '" + Member.MemberPHONENUMBER
                + "', '" + Member.MemberADDRESS + "', '" + Member.MemberSPECIFICADDRESS + "');";

            ConnectDatabase(sql);
            command.ExecuteNonQuery();
            DisconnectDatabase();
        }
        //데이터베이스에서 회원정보 삭제 / 탈퇴
        public void DeleteMember(MemberVO Member)
        {
            string sql = "delete from member where username = '" + Member.MemberUSERNAME + "';";

            ConnectDatabase(sql);
            command.ExecuteNonQuery();
            DisconnectDatabase();
        }
        //회원정보 데이터베이스에서 업데이트 / 회원정보 수정
        public void UpdateMember(MemberVO Member)
        {
            string sql = "update member set name = '" + Member.MemberNAME + "', email = '" + Member.MemberEMAIL + "', password = '" + Member.MemberPASSWORD
                + "', address = '" + Member.MemberADDRESS + "', specificaddress = '" + Member.MemberSPECIFICADDRESS + "' where username = '" + Member.MemberUSERNAME + "';";

            ConnectDatabase(sql);
            command.ExecuteNonQuery();
            DisconnectDatabase();
        }

        public string this[MemberVO member]
        {
            get
            {
                return member.MemberNAME;
            }
        }

        //interface IDatabase.ConnectDatabase 메소드 구현
        public void ConnectDatabase(string sql)
        {
            connect = new MySqlConnection(databaseConnect);
            connect.Open();
            command = new MySqlCommand(sql, connect);
        }

        //interface IDatabase.DisconnectDatabase 메소드 구현
        public void DisconnectDatabase()
        {
            connect.Close();
        }
    }
}
