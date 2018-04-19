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
                string query = $@"SELECT * FROM Ninjas
                                  JOIN Dojos ON Ninjas.DojoId = Dojos.Id";

                var myNinjas = dbConnection.Query<Ninja, Dojo, Ninja>(query, (ninja, dojo) => { ninja.dojo = dojo; return ninja; });
                return myNinjas;
            }
        }

        public Ninja GetNinja(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string query = $@"SELECT * FROM Ninjas
                                  JOIN Dojos ON Ninjas.DojoId = Dojos.Id
                                  WHERE Ninjas.Id = {id}";

                

                var myNinja = dbConnection.Query<Ninja, Dojo, Ninja>(query, (ninja, dojo) => {ninja.dojo = dojo; return ninja;});
                return myNinja.Single();
            }
        }
    }
}