using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace curd_opreation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class classController : ControllerBase
    {
        private readonly IConfiguration _configuration;
            public classController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select deptid,deptname from dbo.department";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("student");
            SqlDataReader sdr;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query,con))
                {
                    sdr = cmd.ExecuteReader();
                    table.Load(sdr);
                    sdr.Close();
                    con.Close();

                }
            }
            return new JsonResult(table);


        }
       [HttpPost]
        public JsonResult Post(department dep)
        {
            string query=@"insert into dbo.department values (@deptname)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("student");
            SqlDataReader sdr;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlParameter sqlParameter = cmd.Parameters.AddWithValue("@deptname", dep.deptname);
                    sdr = cmd.ExecuteReader();
                    table.Load(sdr);
                    sdr.Close();
                    con.Close();

                }
            }
            return new JsonResult("addsuccessfully");


        }
        [HttpPut]
        public JsonResult Put(department dep)
        {
            string query = @"update  dbo.department set deptname=@deptname where deptid=@deptid";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("student");
            SqlDataReader sdr;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@deptid", dep.deptid);
                    cmd.Parameters.AddWithValue("@deptname", dep.deptname);
                    sdr = cmd.ExecuteReader();
                    table.Load(sdr);
                    sdr.Close();
                    con.Close();

                }
            }
            return new JsonResult("update successfully");


        }
        [HttpDelete]
        public JsonResult delete(int id)
        {
            string query = @"delete  dbo.department  where deptid=@deptid";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("student");
            SqlDataReader sdr;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@deptid",id);
        
                    sdr = cmd.ExecuteReader();
                    table.Load(sdr);
                    sdr.Close();
                    con.Close();

                }
            }
            return new JsonResult("delete successfull");


        }
    }

   
}
