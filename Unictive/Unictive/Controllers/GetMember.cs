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
    public class GetMember : ControllerBase
    {
        private readonly ILogger<GetMember> _logger;

        public GetMember(ILogger<GetMember> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetMember")]
        public Member Get(string Email)
        {
            Member member = new Member();

            var objBuilder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
            IConfiguration conManager = objBuilder.Build();
            var connString = conManager.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                String sql = "exec USP_GETMEMBER '"+ Email + "'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            member.Id = reader.GetString(0);
                            member.Nama = reader.GetString(1);
                            member.Email = reader.GetString(2);
                            member.Phone = reader.GetString(3);
                        }

                        reader.NextResult();

                        List<Hobby> lh = new List<Hobby>();
                        while (reader.Read())
                        {
                            Hobby hobby = new Hobby();
                            hobby.Id = reader.GetString(0);
                            hobby.Nama = reader.GetString(1);

                            lh.Add(hobby);
                        }

                        member.ListHobby = lh;
                    }
                }
            }

            return member;
        }
    }
}