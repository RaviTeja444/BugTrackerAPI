using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BugTrackerAPI.Interface;
using BugTrackerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Swashbuckle.AspNetCore.Swagger;

namespace BugTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUtility _utility;

        private readonly IBugRepository _bugRepository;

        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ValuesController(IUtility utility,IBugRepository bugRepository)
        {
            _utility = utility;
            _bugRepository = bugRepository;
        }

        
        [HttpGet]
        public string Get()
        {
            
            return "Resp";
        }
        [HttpPost("/projectCreate")]
        public string projectCreate(string orgname,string projsize,string projname)
        {
            string authHeader = this.Request.Headers["Authorization"];
            var username = _utility.GetUser(authHeader);
            var projectid = _bugRepository.createProject(orgname,projsize,projname, username);
            return projectid;
        }

        [HttpPost("/USCreate")]
        public string USCreate(string UserStoryName, string priority, string points,string description,string comments,string projectid)
        {
            string authHeader = this.Request.Headers["Authorization"];
            var username = _utility.GetUser(authHeader);
            if (string.IsNullOrEmpty(projectid))
            {
                projectid = _bugRepository.GetProject(username);
            }
            var userstoryid = _bugRepository.createUserStory(UserStoryName, priority, points, description,comments,projectid);
            return userstoryid;
        }

        [HttpPost("/DefectCreate")]
        public string DefectCreate(string DefectName, string priority, string UserStoryNumber,string description,string comments,string usid)
        {
            string authHeader = this.Request.Headers["Authorization"];
            var username = _utility.GetUser(authHeader);
            if (string.IsNullOrEmpty(usid))
            {
                usid = _bugRepository.GetUS(username);
            }
            var defectid = _bugRepository.createDefect(DefectName, priority, UserStoryNumber, description, comments, usid);
            return defectid;

        }

        [HttpPost("/SaveUS")]
        public string SaveUS(string usname, string usstatus, string UserStoryNumber, string description)
        {
            string authHeader = this.Request.Headers["Authorization"];
            var username = _utility.GetUser(authHeader);
            
            var defectid = _bugRepository.SaveUS(usname,usstatus,UserStoryNumber,description);
            return defectid;

        }

        [HttpPost("/SaveDefect")]
        public string SaveDefect(string DefectName, string defno, string defstatus, string description)
        {
            string authHeader = this.Request.Headers["Authorization"];
            var username = _utility.GetUser(authHeader);
           
            var defectid = _bugRepository.SaveDefect(DefectName,defno,defstatus,description);
            return defectid;

        }

        [HttpGet("/Dashboard")]
        public string Dashboard()
        {
            string authHeader = this.Request.Headers["Authorization"];
            var username = _utility.GetUser(authHeader);
            var data = _bugRepository.GetDetails(username);
            return data;

        }

        [AllowAnonymous]
        // GET api/values
        [HttpPost("/token")]
        
        public string Token(string username, string password)
        {
            logger.Debug("token generation started");
            string token = string.Empty;
            bool result = _utility.checkCredentials(username, password);
            if (result)
            {
                token = _utility.GenerateJSONWebToken(username);
            }
            return token;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        //[JwtAuthentication]
        public string Get(int id)
        {
            string authHeader = this.Request.Headers["Authorization"];
            if(!_utility.ValidateToken(authHeader))
            {
                return "Unauthorized";
            }
            var user1 = HttpContext.User;
            var user = Thread.CurrentPrincipal;
            if(user.Identity.IsAuthenticated)
            {
                return _utility.Email(user.Identity.Name);
            }
            return user.Identity.Name;
        }

        // POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        
        [HttpPost("InsertProject/{project}")]
        public void InsertProject(Project project)
        {

        }
    }
}
