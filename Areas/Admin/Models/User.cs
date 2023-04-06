using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;

namespace QLSV.Areas.Admin.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool CheckUserCredentials(string username, string password)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            using (var connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM [User] WHERE UserName = @username AND Password = @password", connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    int result = (int)command.ExecuteScalar();
                    if (result > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void AddUser(User user)
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            using (var connection = new SqlConnection(cs))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Users (UserName, Password, Email) VALUES (@username, @password, @email)", connection))
                {
                    command.Parameters.AddWithValue("@username", user.UserName);
                    command.Parameters.AddWithValue("@password", user.Password);
                    command.Parameters.AddWithValue("@email", user.Email);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}