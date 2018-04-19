using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using TheDojoLeague.Models;
 
namespace TheDojoLeague.Factory
{
    public class DojoFactory : IFactory<Dojo>
    {
        private string connectionString;
        public DojoFactory()
        {
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=TheDojoLeague;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }

        public void Add(Dojo dojo)
        {
            using (IDbConnection dbConnection = Connection) {
                string query =  @"INSERT INTO Dojos (DojoName, DojoLocation, Description, CreatedAt, UpdatedAt) 
                                  VALUES (@DojoName, @DojoLocation, @Description, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, dojo);
            }
        }

        public IEnumerable<Dojo> AllDojos()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Dojo>("SELECT * FROM Dojos");
            }
        }

        public Dojo GetDojo(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string query = 
                @"
                    SELECT * FROM Dojos WHERE Id = @Id;
                    SELECT * FROM Ninjas WHERE DojoId = @Id;
                ";
                using (var multi = dbConnection.QueryMultiple(query, new{Id = id}))
                {
                    var dojo = multi.Read<Dojo>().Single();
                    dojo.ninjas = multi.Read<Ninja>().ToList();
                    return dojo;
                }

            }
        }

        public void BanishNinja(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string query = $@"UPDATE Ninjas
                                  SET DojoId = 1
                                  WHERE Id = {id};";

                dbConnection.Execute(query);
            }
        }

        public void RecruitNinja(int dojoid, int ninjaid)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string query = $@"UPDATE Ninjas
                                  SET DojoId = {dojoid}
                                  WHERE Id = {ninjaid};";

                dbConnection.Execute(query);
            }
        }
    }
}