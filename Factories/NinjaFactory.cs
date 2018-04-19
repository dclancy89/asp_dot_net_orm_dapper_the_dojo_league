using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using TheDojoLeague.Models;
 
namespace TheDojoLeague.Factory
{
    public class NinjaFactory : IFactory<Ninja>
    {
        private string connectionString;
        public NinjaFactory()
        {
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=TheDojoLeague;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }

        public void Add(Ninja ninja)
        {
            using (IDbConnection dbConnection = Connection) {
                string query =  @"INSERT INTO Ninjas (NinjaName, NinjaLevel, DojoId, Description, CreatedAt, UpdatedAt) 
                                  VALUES (@NinjaName, @NinjaLevel, @DojoId, @Description, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, ninja);
            }
        }

        public IEnumerable<Ninja> AllNinjas()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Ninja>("SELECT * FROM Ninjas");
            }
        }
    }
}