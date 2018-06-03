using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;

namespace SignUpProgram
{
    class Exceptions
    {
        private static Exceptions exceptions;
        Database database;

        private Exceptions()
        {
            database = Database.getInstance();
        }

        public static Exceptions getInstance()
        {
            if (exceptions == null)
            {
                exceptions = new Exceptions();
            }
            return exceptions;
        }

        //입력받은 Data가 데이터베이스에 있는 데이터와 일치하는지 확인하는 메소드
        public bool isDuplicated(string table, string attribute, string Data) 
        {
            string sql = "select " + attribute + " from " + table + ";";

            foreach (DataRow row in database.ExceptionsIsDuplicated(sql, table).Tables[0].Rows)
            {
                if (Convert.ToString(row[attribute]) == Data)
                    return true;
            }
            return false;
        }

        public bool CheckCorrectAddress(string input)
        {
            if (Regex.IsMatch(input, @"^([가-힣]*[-]?\d?\s*)*([0-9]+[-]?[0-9]*)$"))
                return true;
            else
                return false;
        }
    }
}
