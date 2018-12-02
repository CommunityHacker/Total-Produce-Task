using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary1
{
    public class Service1 : IService1
    {

        private string ConnectionString()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Smile\Documents\People.mdf;Integrated Security=True;Connect Timeout=30";
            return connectionString;
        }

        public List<Person> GetData()
        {
            List<Person> returnList = new List<Person>();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * From Person;", connection))
                    {
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            returnList.Add(new Person(dr.GetInt32(dr.GetOrdinal("ID")), dr.GetString(dr.GetOrdinal("Name")), dr.GetString(dr.GetOrdinal("Surname"))));
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception){throw;}       
         
            return returnList;
        }

        public string InsertPerson(Person person)
        {
            string state = "Failed";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Person (name,surname) VALUES (@name,@surname);", connection))
                    {
                        cmd.Parameters.AddWithValue("@name", person.Name);
                        cmd.Parameters.AddWithValue("@surname", person.Surname);
                        cmd.ExecuteReader();
                        state = "Complete";
                    }
                }
            }
            catch (Exception e){state = e.Message;}
            
            return state;
        }

        public string UpdatePerson(Person person)
        {
            string state = "Failed";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Person SET name = @name, surname = @surname WHERE id = @id;", connection))
                    {
                        cmd.Parameters.AddWithValue("@id", person.Id);
                        cmd.Parameters.AddWithValue("@name", person.Name);
                        cmd.Parameters.AddWithValue("@surname", person.Surname);
                        cmd.ExecuteReader();
                        state = "Complete";
                    }
                }
            }
            catch (Exception e){state = e.Message;}
           
            return state;
        }

        public string DeletePerson(int value)
        {
            string state = "Failed";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Person WHERE id = @id;", connection))
                    {
                        cmd.Parameters.AddWithValue("@id", value);
                        cmd.ExecuteReader();
                        state = "Complete";
                    }
                }
            }
            catch (Exception e) { state = e.Message; }

            return state;
        }
    }
}
