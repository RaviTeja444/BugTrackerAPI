using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using BugTrackerAPI.Interface;

namespace BugTrackerAPI.Repository
{
    public class Bugrepository:IBugRepository
    {
        public string createProject(string orgname, string projsize, string projname,string user)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Startup.constring))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Insert into project(projectid,projectsize,projectname,username,orgname) values(@id,@projsize,@projname,@user,@orgname)", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@projsize", projsize);
                    sqlCommand.Parameters.AddWithValue("@projname", projname);
                    sqlCommand.Parameters.AddWithValue("@user", user);
                    sqlCommand.Parameters.AddWithValue("@orgname", orgname);
                    var id = Guid.NewGuid();
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    var res = Convert.ToString(sqlCommand.ExecuteNonQuery());
                    return Convert.ToString(id);
                }
                catch(Exception e)
                { }
                finally
                {
                    sqlConnection.Close();
                }
                return "";
                
            }
        }

        public string createUserStory(string UserStoryName, string priority, string points, string description, string comments, string projectid)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Startup.constring))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("Insert into userstory(userstoryid,userstoryname,userstorydescription,userstorycomments,uspriority,projectid) values(@userstoryid,@userstoryname,@userstorydescription,@userstorycomments,@uspriority,@projectid)", sqlConnection);
                var id = Guid.NewGuid();
                sqlCommand.Parameters.AddWithValue("@userstoryid", id);
                sqlCommand.Parameters.AddWithValue("@userstoryname", UserStoryName);
                sqlCommand.Parameters.AddWithValue("@userstorydescription", description);
                sqlCommand.Parameters.AddWithValue("@userstorycomments", comments);
                sqlCommand.Parameters.AddWithValue("@uspriority", priority);
                sqlCommand.Parameters.AddWithValue("@projectid", projectid);
                var res = Convert.ToString(sqlCommand.ExecuteNonQuery());
                sqlConnection.Close();
                return Convert.ToString(id);
            }
        }

       

        public string createDefect(string DefectName, string priority, string UserStoryNumber, string description, string comments,string usid)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Startup.constring))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("Insert into defect(defectid,defectname,priority,description,comments,userstoryid) values(@defectid,@defectname,@priority,@description,@comments,@userstoryid)", sqlConnection);
                var id = Guid.NewGuid();
                sqlCommand.Parameters.AddWithValue("@defectid", id);
                sqlCommand.Parameters.AddWithValue("@defectname", DefectName);
                sqlCommand.Parameters.AddWithValue("@priority", priority);
                sqlCommand.Parameters.AddWithValue("@description", description);
                sqlCommand.Parameters.AddWithValue("@comments", comments);
                sqlCommand.Parameters.AddWithValue("@userstoryid", usid);
                var res = Convert.ToString(sqlCommand.ExecuteNonQuery());
                sqlConnection.Close();
                return Convert.ToString(id);
            }
        }

        public string SaveUS(string usname, string usstatus, string UserStoryNumber, string description)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Startup.constring))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("update userstory set userstoryname=@usname,USstatus=@usstatus,userstorydescription=@descr where userstorynumber=@usno", sqlConnection);
      
                sqlCommand.Parameters.AddWithValue("@descr", description);
                sqlCommand.Parameters.AddWithValue("@usstatus", usstatus);
                sqlCommand.Parameters.AddWithValue("@usname", usname);
                sqlCommand.Parameters.AddWithValue("@usno", UserStoryNumber);
                var res = Convert.ToString(sqlCommand.ExecuteNonQuery());
                sqlConnection.Close();
                return Convert.ToString(res);
            }
        }
        public string SaveDefect(string DefectName, string defno, string defstatus, string description)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Startup.constring))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("update defect set DefectName=@defname,Defstatus=@status,DefectDescription=@descr where DefectNumber=@defno", sqlConnection);
                var id = Guid.NewGuid();
                sqlCommand.Parameters.AddWithValue("@defname", DefectName);
                sqlCommand.Parameters.AddWithValue("@status", defstatus);
                sqlCommand.Parameters.AddWithValue("@descr", description);
                sqlCommand.Parameters.AddWithValue("@defno", defno);
  
                var res = Convert.ToString(sqlCommand.ExecuteNonQuery());
                sqlConnection.Close();
                return Convert.ToString(id);
            }
        }

        public string GetDetails(string uname)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(Startup.constring))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("Select p.projectname,p.projectdescription,u.userstoryname,u.userstorynumber,u.userstorydescription,d.defectname,d.defectnumber,d.defectdescription from project p join userstory u on p.projectid=u.projectid join defect d on u.userstoryid=d.userstoryid and p.username=@uname", sqlConnection);
                
                sqlCommand.Parameters.AddWithValue("@uname", uname);
                using(SqlDataAdapter adapter=new SqlDataAdapter(sqlCommand))
                {
                    adapter.Fill(dataTable);
                }
                sqlConnection.Close();
                return Convert.ToString(dataTable);
            }
        }

        public string GetProject(string uname)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(Startup.constring))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("Select top 1 projectid from project where username=@uname", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@uname", uname);
                string value = Convert.ToString(sqlCommand.ExecuteScalar());
                sqlConnection.Close();
                return value;
            }
        }

        public string GetUS(string uname)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(Startup.constring))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("Select top 1 u.usertstoryid from project p join userstory u on p.projectid=u.projectid where p.username=@uname", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@uname", uname);
                string value = Convert.ToString(sqlCommand.ExecuteScalar());
                sqlConnection.Close();
                return value;
            }
        }

    }
}
