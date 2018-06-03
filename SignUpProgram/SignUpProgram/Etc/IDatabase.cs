using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignUpProgram.Etc
{
    interface IDatabase
    {
        void ConnectDatabase(string sql);
        void DisconnectDatabase();
    }
}
