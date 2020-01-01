using BugTrackerAPI.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Principal;
using System.Threading;

namespace BugTrackerAPI.Data
{
    public class Utility:IUtility
    {
        private IConfiguration _config;
        public Utility(IConfiguration configuration)
        {
            _config = configuration;
        }

        public bool checkCredentials(string username,string password)
        {
            using (SqlConnection sql = new SqlConnection(Startup.constring))
            {
                int result = 0;
                SqlCommand sqlCommand = new SqlCommand("select count(*) from aspnetusers where username=@username", sql);
                sqlCommand.Parameters.AddWithValue("@username",username);
                try
                {
                    sql.Open();
                    result = Convert.ToInt16(sqlCommand.ExecuteScalar());
                }
                catch(Exception e)
                { }

                if(result > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public string GenerateJSONWebToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> listclaims = new List<Claim>();
            var claims = new Claim("Name",username);
            listclaims.Add(claims);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              listclaims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string Email(string name)
        {
            string token = string.Empty;
            WebRequest web = WebRequest.Create("https://localhost:5001/api/values" + "?value" +name);
            web.Method = "POST";
            web.ContentType = "text/plain";
            var response = (HttpWebResponse)web.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var reader = new StreamReader(response.GetResponseStream());
                var result = reader.ReadToEnd();
                //var jo = JObject.Parse(result);
                //token = jo["access_token"].ToString();
                //return token;
            }
            return token;
        }

        public string GetUser(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            // var jsonToken = handler.ReadToken(stream);
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;

            return tokenS.Claims.First(claim => claim.Type == "Name").Value;
        }

        public bool ValidateToken(string token)
        {

            var handler = new JwtSecurityTokenHandler();
            // var jsonToken = handler.ReadToken(stream);
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;

            var jti = tokenS.Claims.First(claim => claim.Type == "Name").Value;
            if (string.IsNullOrEmpty(jti))
                return false;
            //HttpContext
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(tokenS.Claims);
            string[] array = { "Admin" };
            GenericPrincipal genericPrincipal= new GenericPrincipal(claimsIdentity,array);
            Thread.CurrentPrincipal = genericPrincipal;
            
            return true;
            //JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            //TokenValidationParameters validationParameters = TokenValidationParameters();
            //SecurityToken securityToken;
            //IPrincipal principal;
            //try
            //{
            //    // token validation
            //    principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
            //    // Reading the "verificationKey" claim value:
            //    var vk = principal.Claims.SingleOrDefault(c => c.Type == "verificationKey").Value;
            //}
            //catch
            //{
            //    principal = null; // token validation error
            //}
            //IdentityModelEventSource.ShowPII = true;

            //SecurityToken validatedToken;
            //TokenValidationParameters validationParameters = new TokenValidationParameters();

            //validationParameters.ValidateLifetime = true;

            //validationParameters.ValidAudience = _audience.ToLower();
            //validationParameters.ValidIssuer = _issuer.ToLower();
            //validationParameters.IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));

            //ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out validatedToken);


            //return principal;
            //SecurityToken validatedToken;
            //JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            //var user = handler.ValidateToken(token, validationParameters, out validatedToken);
            //username = null;

            //var simplePrinciple = JwtManager.GetPrincipal(token);
            //var identity = simplePrinciple?.Identity as ClaimsIdentity;

            //if (identity == null)
            //    return false;

            //if (!identity.IsAuthenticated)
            //    return false;

            //var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            //username = usernameClaim?.Value;

            //if (string.IsNullOrEmpty(username))
            //    return false;

            //// More validate to check whether username exists in system

            //return true;
        }
}
}
