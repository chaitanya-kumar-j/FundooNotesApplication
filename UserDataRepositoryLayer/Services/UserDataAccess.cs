using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using UserDataCommonLayer.Models;
using UserDataRepositoryLayer.Interfaces;

namespace UserDataRepositoryLayer.Services
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly string _connectionString;
        public UserDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }
        public List<User> GetAllUsers()
        {
            try
            {
                DataSet dataSet = new DataSet();
                List<User> usersList = new List<User>();
                string storedProcedure = "spViewAllUsers";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(storedProcedure, connection);
                    adapter.Fill(dataSet, "UserInfo");

                    foreach (DataRow row in dataSet.Tables["UserInfo"].Rows)
                    {
                        User user = new User();
                        user.UserId = (int)row["UserId"];
                        user.FirstName = (string)row["FirstName"];
                        user.LastName = (string)row["LastName"];
                        user.City = (string)row["City"];
                        user.MobileNumber = (string)row["MobileNumber"];
                        user.Email = (string)row["Email"];
                        user.Password = "*********";
                        user.RegistrationDateTime = (DateTime)row["RegistrationDateTime"];
                        usersList.Add(user);
                    }
                    connection.Close();
                }
                return usersList;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public User RegisterUser(NewUser newUser)
        {
            try
            {
                DataSet dataSet = new DataSet();
                User user = new User();
                string storedProcedure = "spRegistration";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", newUser.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", newUser.LastName);
                        cmd.Parameters.AddWithValue("@City", newUser.City);
                        cmd.Parameters.AddWithValue("@MobileNumber", newUser.MobileNumber);
                        cmd.Parameters.AddWithValue("@Email", newUser.Email);
                        cmd.Parameters.AddWithValue("@Password", newUser.Password);
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataSet, "UserInfo");
                        }
                    }
                    foreach (DataRow row in dataSet.Tables["UserInfo"].Rows)
                    {
                        user.UserId = (int)row["UserId"];
                        user.FirstName = (string)row["FirstName"];
                        user.LastName = (string)row["LastName"];
                        user.City = (string)row["City"];
                        user.MobileNumber = (string)row["MobileNumber"];
                        user.Email = (string)row["Email"];
                        user.Password = "*********";
                        user.RegistrationDateTime = (DateTime)row["RegistrationDateTime"];
                    }
                    connection.Close();
                }
                return user;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
