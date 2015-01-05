using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MvcDropDwn.Connect
{

    public class GetConnection
    {

        private static string _sqlConnectionString;    //variable sqlConnection string is declared 
        public static string SqlConnectionString  //Property defined to set the sqlConnection
        {
            //getter method toretrieve the SqlConnection from webConfig
            get { return ConfigurationManager.ConnectionStrings["connectionId"].ConnectionString; }
            //setter method to set the value
            set { _sqlConnectionString = ConfigurationManager.ConnectionStrings["connectionId"].ConnectionString; }
        }
        //Method to create a connection
        public SqlConnection CreateNewConnection()
        {
            return new SqlConnection(SqlConnectionString);
        }
        //method of type SqlDataReader containg two parameters as the query from constants and SqlConnection
        public SqlDataReader ExecuteQuery(string query, SqlConnection con)
        {
            //class SqlCommand that takes input parameters string and connection
            SqlCommand myCommand = new SqlCommand(query, con);
            //SqlDataReader is a class has a variable that myReader that stores the values executed 
            SqlDataReader myReader = myCommand.ExecuteReader();
            return myReader;
        }
        //method has SqlDataReader as return type and ParameterisedQuery as List being one of the parameter
        public SqlDataReader ParameterisedQuery(string query, SqlConnection con, List<string> parameterList)
        {
            //SqlDataReader is a class that retrievs the data
            SqlDataReader myReader = null;
            try
            {
                //SqlCommand is again a class that has two parameters string and connection con
                SqlCommand myCommand = new SqlCommand(query, con);
                //Parameters are added with value in the selected item directly to myCommand
                myCommand.Parameters.AddWithValue("@name", parameterList[0].ToString());
                //command is executed and stored in myReader
                myReader = myCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

            }

            return myReader;
        }

        public SqlDataReader ParameterShowQuery(string query, SqlConnection con, string name)
        {
            SqlDataReader myReader = null;
            try
            {
                //SqlComaand  class with  variable myCommand has two parameters
                SqlCommand myCommand = new SqlCommand(query, con);
                //
                myCommand.Connection = con;
                //SqlParameter class with a variable 
                SqlParameter pmtrName = new SqlParameter();
                //Parameter in the class
                pmtrName.ParameterName = "@name";
                //value of the parameter
                pmtrName.Value = name;
                //The databasetype of parameter
                pmtrName.SqlDbType = SqlDbType.VarChar;
                //Parameter size
                pmtrName.Size = 50;
                //Input type of parameter
                pmtrName.Direction = ParameterDirection.Input;
                //add name to the Parameters
                myCommand.Parameters.Add(pmtrName);
                //ExecuteReader executes the parameters inside myCommand
                myReader = myCommand.ExecuteReader();
            }
            catch (Exception ex)
            {

                string errMessage = ex.Message;
            }

            return myReader;

        }
        public SqlDataReader RunStoredProcParams(string spGetTeamByTournamentName, SqlConnection con, string name)
        {
            


            string Tournament = @name;
            SqlDataReader myReader = null;

            try
            {

                con = new
                    SqlConnection(SqlConnectionString);
                con.Open();


                SqlCommand cmd = new SqlCommand(
                    spGetTeamByTournamentName, con);

                // 2. set the command object so it knows
                // to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which
                // will be passed to the stored procedure
                cmd.Parameters.Add(
                    new SqlParameter("@name", name));
               

                myReader = cmd.ExecuteReader();



            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
            }
            return myReader;

        }
    }
}

