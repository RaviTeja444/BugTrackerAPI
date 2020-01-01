using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Interface
{
    public interface IUtility
    {
        bool checkCredentials(string username, string password);

        string GenerateJSONWebToken(string username);

        string Email(string value);

        bool ValidateToken(string token);

        string GetUser(string token);
    }
}
