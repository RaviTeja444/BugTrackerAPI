using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Interface
{
    public interface IBugRepository
    {
        string createProject(string orgname, string projsize, string projname, string user);

        string createUserStory(string UserStoryName, string priority, string points, string description, string comments, string projectid);

        string createDefect(string DefectName, string priority, string UserStoryNumber, string description, string comments, string usid);

        string GetDetails(string uname);

        string GetProject(string uname);

        string GetUS(string uname);
        string SaveDefect(string DefectName, string defno, string defstatus, string description);
        string SaveUS(string usname, string usstatus, string UserStoryNumber, string description);
    }
}
