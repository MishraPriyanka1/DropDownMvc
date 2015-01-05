using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDropDwn
{
    //hardcode values are stored in constants class that can be enum,query
    public static class Constants
    {
        //query string to display the tournament Id and name from table TournamentType 
        public const string SelectTournament = "Select Id, Tournament from TournamentType";
        public const string Tournament = "Tournament";
        public const string Id = "Id";
       // public const string name = "Select Id, Tournament from TournamentType";

        //public const string SelectTeam = "select Team, Id from Team where Tournament ='";
        //query string to display teamId and name from table team where tournament name selected is in Team table 
        public const string SelectTeam = "select Team,Id from Team where Tournament=@name";
        public const string TeamId = "Id";
        public const string Team = "Team";
        //query string to display the Player Id and name from table Player where the Players have the team  name as to the team selected in team dropdownlist
        public const string SelectPlayer = "select Player,Id from Player where Team ='";
        public const string PlayerId = "Id";
        public const string Player = "Player";

        public const string spGetTeamByTournamentName = "spGetTeamByTournamentName";
     
    }

}