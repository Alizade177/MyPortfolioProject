using Microsoft.AspNetCore.Mvc;
using MyPortfolioProject.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MyPortfolioProject.Controllers
{
    public class HomeController : Controller
    {
        private  readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
      public IActionResult Index() 
      {
            List<Skill> skills = new List<Skill>();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(_configuration.GetConnectionString("SqlServerConnection"));
                SqlCommand command = new SqlCommand("SELECT * FROM Skills", connection);
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    skills.Add(new Skill()
                    {
                        Id = (int)dataReader["Id"],
                        SkillName = (string)dataReader["SkillName"],
                        SkillDescription = (string)dataReader["SkillDescription"],
                        SkillValue = (int)dataReader["SkillValue"]

                    });

                }
                
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
           
          
             return View(skills);  
      }
    }
}