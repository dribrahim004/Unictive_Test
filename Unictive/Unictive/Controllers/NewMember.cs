using Microsoft.AspNetCore.Mvc;
using Unictive.Model;
using Unictive.Global;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace Unictive.Controllers
{
    [ApiController, Authorize]
    [Route("[controller]")]
    public class NewMember : ControllerBase
    {
        private readonly ILogger<NewMember> _logger;

        public NewMember(ILogger<NewMember> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "NewMember")]
        public Response Get(Member member)
        {
            Response res = new Response();
            try
            {
                var objBuilder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
                IConfiguration conManager = objBuilder.Build();
                var connString = conManager.GetConnectionString("DefaultConnection");

                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();

                    String sql = "exec USP_MEMBER '" + member.Id + "','" + member.Nama + "','" + member.Email + "','" + member.Phone + "'";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();

                    String sql2 = "delete from MEMBER_HOBBY where ID_MEMBER='" + member.Id + "'";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.ExecuteNonQuery();

                    foreach (var data in member.ListHobby)
                    {
                        String sql3 = "exec USP_MEMBER_HOBBY '" + member.Id + "','" + data.Id + "'";
                        SqlCommand command3 = new SqlCommand(sql3, connection);
                        command3.ExecuteNonQuery();
                    }

                    res.responseCode = "1";
                    res.responseMsg = "success";
                }
            }
            catch (Exception ex)
            {
                res.responseCode = "-1";
                res.responseMsg = ex.Message.ToString();
            }

            return res;
        }
    }
}
