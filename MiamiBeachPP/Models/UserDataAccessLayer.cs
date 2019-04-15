using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MiamiBeachPP.Models;

namespace MiamiBeachPP.Models
{
    public class UserDataAccessLayer
    {
        public static IConfiguration Configuration { get; set; }

        //To Read ConnectionString from appsettings.json file  
        private static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            string connectionString = Configuration["Logging:ConnectionStrings:myConString"];

            return connectionString;

        }

        string connectionString = GetConnectionString();
        // private static object optionsBuilder;

        //To Register a new user   
        public string RegisterUser(UserDetails user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spRegisterUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@UserID", user.UserID);
                cmd.Parameters.AddWithValue("@UserPassword", user.Password);
                cmd.Parameters.AddWithValue("@UserRole", user.UserRole);

                con.Open();
                string result = cmd.ExecuteScalar().ToString();
                con.Close();

                return result;
            }
        }

        //To Validate the login  
        public string ValidateLogin(UserDetails user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spValidateUserLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LoginID", user.UserID);
                cmd.Parameters.AddWithValue("@LoginPassword", user.Password);
                //cmd.Parameters.AddWithValue("@UserRole", user.UserRole).Direction = ParameterDirection.Output;
                con.Open();
                string result = cmd.ExecuteScalar().ToString();

                //string role = Convert.ToString(cmd.Parameters["@UserRole"].Value);
                con.Close();

                return result;
            }
        }
         
    }
}