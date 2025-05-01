using System.Collections;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Runtime.CompilerServices;
using WebApp4310HTML.Models;

namespace WebApp4310HTML
{
    /// <summary>
    /// THIS CLASS HELPS TO CONNECT TO THE MS SQLSERVER DATABASE
    /// </summary>
    public class DatabaseHelper
    {
        const string connectionString = "Data Source=DESKTOP-HV72VOI\\SQLEXPRESS; Initial Catalog=WebApp4310HTML; Integrated Security=true; TrustServerCertificate = True; MultipleActiveResultSets=true";
        // CONNECTION POOLING
        static readonly SqlConnection sqlConnection = new SqlConnection(connectionString);

        
        public static Student GetStudentByEmail(string email)
        {
            const string query = "Select * from Student where email = @email";

            // HOW DO WE RUN THIS QUERY AGAINST OUR DATABASE
            var queryCommand = new SqlCommand(query);
            queryCommand.Connection = sqlConnection;
            queryCommand.Parameters.AddWithValue("@email", email);

            // NOW THAT QUERY COMMAND HAS BEEN ADJUSTED WITH THE ACTUAL ARGUMENT
            // OPEN A CONNECTION TO THE DATABASE IF A CONNECTION IS NOT ALREADY OPEN

            if(sqlConnection.State != ConnectionState.Open) sqlConnection.Open();

            // NOW THAT A CONNECTION IS OPEN, WE ARE GOING TO EXECUTE OUR QUERY COMMAND
            var reader = queryCommand.ExecuteReader();

            // WHEN EXECUTING A READER, IT RETURNS A SEQUEL OF THINGS CALLED A RESULTSET
            bool found = false;
            // DECLARATION, DECLARING THE VARIABLE "student" TO BE OF THE TYPE/CLASS "Student"
            Student student = new Student();

            while (reader.Read() & !found)
            {
                student.FirstName = reader["FirstName"].ToString();
                student.LastName = reader["LastName"].ToString();
                student.DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString());
                student.Major = reader["Major"].ToString();
                student.Department = reader["Department"].ToString();
                student.Gender = reader["Gender"].ToString();
                student.Email = reader["email"].ToString();
                student.DesignatedLevel = reader["DesignatedLevel"].ToString();
                student.Password = reader["Password"].ToString();
                found = true;
            }


            return student;


        }


        public static void InsertNew(Student student)
        {
            const string insertQuery = @"INSERT INTO[dbo].[Student]
                                               ([FirstName]
                                               , [LastName]
                                               , [DateofBirth]
                                               , [Major]
                                               , [Department]
                                               , [Gender]
                                               , [email]
                                               , [DesignatedLevel]
                                               , [Password])
                                         VALUES
                                               ( @FirstName
                                               , @LastName
                                               , @DateofBirth
                                               , @Major
                                               , @Department
                                               , @Gender
                                               , @email
                                               , @DesignatedLevel
                                               , @Password)";

            // HOW DO WE RUN THIS QUERY AGAINST OUR DATABASE
            var queryCommand = new SqlCommand(insertQuery);
            queryCommand.Connection = sqlConnection;

            queryCommand.Parameters.AddWithValue("@FirstName", student.FirstName);
            queryCommand.Parameters.AddWithValue("@LastName", student.LastName);
            queryCommand.Parameters.AddWithValue("@DateofBirth", student.DateOfBirth);
            queryCommand.Parameters.AddWithValue("@Major", student.Major);
            queryCommand.Parameters.AddWithValue("@Department", student.Department);
            queryCommand.Parameters.AddWithValue("@Gender", student.Gender);
            queryCommand.Parameters.AddWithValue("@email", student.Email);
            queryCommand.Parameters.AddWithValue("@DesignatedLevel", student.DesignatedLevel);
            queryCommand.Parameters.AddWithValue("@Password", student.Password);

            // IF CONNECTION IS NOT OPEN, WE WILL OPEN A CONNECTION TO THE DATABASE
            if (sqlConnection.State != ConnectionState.Open) sqlConnection.Open();
            queryCommand.ExecuteNonQuery();
        }

        public static void InsertNewHistory(History history)
        {
            const string insertQuery = @"INSERT INTO[dbo].[History]
                                               ([CompanyName]
                                               , [Location]
                                               , [JobTitle]
                                               , [StartDate]
                                               , [EndDate]
                                               , [Description]
                                         VALUES
                                               ( @CompanyName
                                               , @Location
                                               , @jobTitle
                                               , @StartDate
                                               , @EndDate
                                               , @Description";

            // HOW DO WE RUN THIS QUERY AGAINST OUR DATABASE
            var queryCommand = new SqlCommand(insertQuery);
            queryCommand.Connection = sqlConnection;

            queryCommand.Parameters.AddWithValue("@CompanyName", history.CompanyName);
            queryCommand.Parameters.AddWithValue("@Location", history.Location);
            queryCommand.Parameters.AddWithValue("@JobTitle", history.JobTitle);
            queryCommand.Parameters.AddWithValue("@StartDate", history.StartDate);
            queryCommand.Parameters.AddWithValue("@EndDate", history.EndDate);
            queryCommand.Parameters.AddWithValue("@Description", history.Description);

            // IF CONNECTION IS NOT OPEN, WE WILL OPEN A CONNECTION TO THE DATABASE
            if (sqlConnection.State != ConnectionState.Open) sqlConnection.Open();
            queryCommand.ExecuteNonQuery();
        }

    }
}
