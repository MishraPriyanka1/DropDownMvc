using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDropDwn.Connect;
using System.Data.SqlClient;
using MvcDropDwn.Models;

namespace MvcDropDwn.Controllers
{

    public class HomeController : Controller
    {
        GetConnection objGetConnection;
        // method to populate the dropdownlist of tournament and to return the objectlist object of tournament to the view
        public ActionResult Index()
        {
            // Get the connection string by the help of object  objGetConnection 
            objGetConnection = new GetConnection();
            //    object of tournament list is created 
            TournamentList objTournamentList = new TournamentList();
            //new list is created for the elements that are retrieved by myReader
            objTournamentList.tournamentList = new List<Tournament>();
            Tournament objTournament;

            SqlConnection connection = objGetConnection.CreateNewConnection(); // variable connection of SqlConnection type fetches the databases data

            try
            {
                connection.Open();  //connection is established
                // the method contains two input parameters as string and the returned list is stored in variable myReader
                SqlDataReader myReader = objGetConnection.ExecuteQuery(Constants.SelectTournament, connection);
                // the values in  the myreader are iterated
                while (myReader.Read())
                {
                    //the elements of the tournament list in myReader are referenced by an object of Tournament
                    objTournament = new Tournament();
                    //The Tournamentlist has two columns defined Id and Name both are converted to string to display in dropdownlist
                    objTournament.tournamentId = Convert.ToString(myReader[Constants.Id]);
                    objTournament.tournamentName = Convert.ToString(myReader[Constants.Tournament]);
                    //the id and name retrieved are added to the list of  objTournamentList.tournamentList
                    objTournamentList.tournamentList.Add(objTournament);
                }
            }
            //exceptions are handled in catch block
            catch (Exception ex)
            {
                string exMessage = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            //object that is returned to the view
            return View(objTournamentList);

        }
        // Method for Team dropdown

        public JsonResult Team(string name)
        {
            //object of GetConnection is made
            objGetConnection = new GetConnection();
            //object of TeamList
            TeamList objTeamList = new TeamList();
            //object for the elements of TeamList
            objTeamList.teamList = new List<Team>();
            Team objTeam;
            SqlConnection connection = objGetConnection.CreateNewConnection();
            try
            {
                connection.Open();
                //for the parameterised sql query list is created to store string element
               // List<string> tournament = new List<string>();
                //name that is string stored in data1 with id name in view is added to the list
               // tournament.Add(name);
                //myReader variable stores the returned values via ExecuteQuery method

                //SqlDataReader myReader = objGetConnection.ExecuteQuery(Constants.SelectTeam + name + "'", connection);
                //The ParameterisedQuery defined in GetConnection returns the value of the corresponding Team elements and stores in myReader
                SqlDataReader myReader = objGetConnection.RunStoredProcParams(Constants.spGetTeamByTournamentName, connection, name);
                //SqlDataReader myReader = objGetConnection.ParameterisedQuery(Constants.SelectTeam, connection,);
                //reading the values 
                while (myReader.Read())
                {
                    //object of the Team is created Team is in model folder
                    objTeam = new Team();
                    //The teamId is converted to string 
                    objTeam.teamId = Convert.ToString(myReader[Constants.TeamId]);
                    //The teamName is converted to string to display
                    objTeam.teamName = Convert.ToString(myReader[Constants.Team]);
                    //the number of elements in the object objTeam are added to the List ObjTeamList that is contained in teamList
                    objTeamList.teamList.Add(objTeam);
                }
            }
            //used to handle exception
            catch (Exception ex)
            {
                string exMessage = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            //json type object in the objTeamList.teamList is returned to the view
            return Json(objTeamList.teamList);
        }
        //Method for player dropdownlist
        public JsonResult Player(string name)
        {

            objGetConnection = new GetConnection();
            //object  of PlayerList is created
            PlayerList objPlayerList = new PlayerList();
            //the elements of the playerList are inserted into the new list Player by the help of object objPlayerList
            objPlayerList.playerList = new List<Player>();
            //reference of Player
            Player objPlayer;
            //connection established by the createConnection method in GetConnection class
            SqlConnection connection = objGetConnection.CreateNewConnection();
            try
            {
                connection.Open();
                //The ExecuteQuery method is called to return the corresponding list of players as per team
                SqlDataReader myReader = objGetConnection.ExecuteQuery(Constants.SelectPlayer + name + "'", connection);
                // SqlDataReader myReader = objGetConnection.ExecuteQuery(Constants.SelectPlayer + name + "' ",con);
                // the returned values in myReader are iterated 
                while (myReader.Read())
                {
                    //object of Player List is created
                    objPlayer = new Player();
                    //The playerId retrieved is converted to string for display in html dropdownlist
                    objPlayer.playerId = Convert.ToString(myReader[Constants.PlayerId]);
                    //The string value is also converted to string
                    objPlayer.playerName = Convert.ToString(myReader[Constants.Player]);
                    //name and value pair in variable objPlayer are added to the list
                    objPlayerList.playerList.Add(objPlayer);

                }
            }
            // GetConnection.GetConnectionString().Close();
            catch (Exception ex)
            {
                string exMessage = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return Json(objPlayerList.playerList);
        }
    }
}


