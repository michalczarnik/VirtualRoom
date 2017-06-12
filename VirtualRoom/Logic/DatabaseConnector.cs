using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace VirtualRoom.Logic
{
    public class DatabaseConnector
    {
        const string connectionurl = "Server=mysql03.dcsweb.pl:3306;Database=1216_testowa;Uid=1216_tester;Pwd=testtest1";
        public DatabaseConnector()
        {
            MySqlConnection mSQL = new MySqlConnection(connectionurl);
            mSQL.Open();
        }
    }
}