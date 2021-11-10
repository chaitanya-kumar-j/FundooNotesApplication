using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserDataCommonLayer.Models;
using UserDataRepositoryLayer.Interfaces;

namespace UserDataRepositoryLayer.Services
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly string _connectionString;
        private readonly string _secretKey;
        public UserDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
            _secretKey = configuration.GetValue<string>("AppSettings:SecretKey");
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

        public Response Login(Login loginDetails)
        {
            try
            {
                DataSet dataSet = new DataSet();
                User user = new User();
                string storedProcedure = "spUserLogin";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email", loginDetails.Email);
                        cmd.Parameters.AddWithValue("@Password", loginDetails.Password);
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
                    }
                }
                if (user == null) return null;
                var token = generateJwtToken(user);

                return new Response()
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    City = user.City,
                    MobileNumber = user.MobileNumber,
                    Email = user.Email,
                    Token = token
                };
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
