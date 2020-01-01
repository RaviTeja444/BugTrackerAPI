using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Models
{
    public class Project
    {
        public string ProjectName { get; set; }

        public int ProjectSize { get; set; }
        public string Organization { get; set; }
        public string Description { get; set; }
    }
}
