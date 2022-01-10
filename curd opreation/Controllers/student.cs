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
    public class studentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public studentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select studentId,studentName,studentclass from dbo.student";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("student");
            SqlDataReader sdr;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
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
        public JsonResult Post(student stu)
        {
            string query = @"insert into dbo.student values(@studentName,@studentclass)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("student");
            SqlDataReader sdr;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlParameter sqlParameter1 = cmd.Parameters.AddWithValue("@studentName", stu.studentname);
                    SqlParameter sqlParameter2 = cmd.Parameters.AddWithValue("@studentclass",  stu.studentclass);
                    sdr = cmd.ExecuteReader();
                    table.Load(sdr);
                    sdr.Close();
                    con.Close();

                }
            }
            return new JsonResult("insert successfully");
        }
        [HttpPut]
        public JsonResult Put(student stu)
        {
            string query = @"update dbo.student set studentName=@studentName where studentId=@studentId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("student");
            SqlDataReader sdr;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlParameter sqlParameter1 = cmd.Parameters.AddWithValue("@studentName", stu.studentname);
                    SqlParameter sqlParameter2 = cmd.Parameters.AddWithValue("@studentId",  stu.studentid);
                    sdr = cmd.ExecuteReader();
                    table.Load(sdr);
                    sdr.Close();
                    con.Close();

                }
            }
            return new JsonResult("update successfully");
        }
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            string query = @"delete dbo.student  where studentId=@studentId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("student");
            SqlDataReader sdr;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@studentid",id);
                   
                    sdr = cmd.ExecuteReader();
                    table.Load(sdr);
                    sdr.Close();
                    con.Close();

                }
            }
            return new JsonResult("delete successfully");
        }
    }
}
