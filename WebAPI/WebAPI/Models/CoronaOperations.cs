using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class CoronaOperations
    {
        public List<Datum> GetTheRecords(string sqlQuery)
        {
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder();
            //connString.UserID = "sa";
            //connString.Password = "Technology3";
            connString.DataSource = "DESKTOP-1DOCLJA\\SQLEXPRESS";
            connString.IntegratedSecurity = true; // if true then windows authentication
            connString.InitialCatalog = "Corona";
            List<Datum> theReply = new List<Datum>();
            using (SqlConnection connDB = new SqlConnection(connString.ConnectionString))
            {
                try
                {
                    connDB.Open();
                    var sqlCmd = new SqlCommand(sqlQuery, connDB);
                    var reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int index = 0;
                        Datum newData = new Datum();
                        newData.countrycode = reader.GetString(index++);
                        newData.date = reader.GetDateTime(index++).ToString();
                        newData.cases = reader.GetInt32(index++).ToString();
                        newData.deaths = reader.GetInt32(index++).ToString();
                        newData.recovered = reader.GetInt32(index++).ToString();
                        theReply.Add(newData);
                    }
                    reader.Close();
                    connDB.Close();
                }
                catch (SqlException ex)
                {
                    return (theReply);
                }

            }
            return (theReply);
        }

        public string InsertRecord(Datum datum)
        {
            SqlConnectionStringBuilder connString = new SqlConnectionStringBuilder();
            //connString.UserID = "sa";
            //connString.Password = "Technology3";
            connString.DataSource = "DESKTOP-1DOCLJA\\SQLEXPRESS";
            connString.IntegratedSecurity = true; // if true then windows authentication
            connString.InitialCatalog = "Corona";
            List<Datum> theReply = new List<Datum>();
            using (SqlConnection connDB = new SqlConnection(connString.ConnectionString))
            {
                connDB.Open();
                var sqlCmd = new SqlCommand("InsertData", connDB);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@CC", datum.countrycode);
                sqlCmd.Parameters.AddWithValue("@Date", datum.date);
                sqlCmd.Parameters.AddWithValue("@Cases", datum.cases);
                sqlCmd.Parameters.AddWithValue("@Death", datum.deaths);
                sqlCmd.Parameters.AddWithValue("@Recovered", datum.recovered);
                int i = sqlCmd.ExecuteNonQuery();
                connDB.Close();
                if (i >= 1)
                {
                   return "New Employee Added Successfully";

                }
                else
                {
                   return "Employee Not Added";

                }
                

            }
            
        }
    }
}