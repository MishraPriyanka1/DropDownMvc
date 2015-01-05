using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDropDwn.Models
{
    public class Tournament
    {
        //gets  & sets the tournamentId,tournamentName from database
        public String tournamentId { get; set; }
        public String tournamentName { get; set; }        
    }
}