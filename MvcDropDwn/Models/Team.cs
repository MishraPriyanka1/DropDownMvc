using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDropDwn.Models
{
    //gets and sets teamId,team
    public class Team
    {
        public string teamId { get; set; }
        public string teamName { get; set; }
        public string tournamentName { get; set; }
    }
}