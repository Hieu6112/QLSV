using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
namespace QLSV.Areas.Admin.Models
{
    public class HeDaoTao
    {
        [Key]
        [Required]
        public string MaHDT { get; set; }
        [Required]
        public string TenHDT { get; set; }
    }

    public class HDTDBContext
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public List<HeDaoTao> GetHeDaoTaos()
        {
            List<HeDaoTao> HeDaoTaoList = new List<HeDaoTao>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("getHeDaoTao", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                HeDaoTao HeDaoTao = new HeDaoTao();
                HeDaoTao.MaHDT = dr.GetValue(0).ToString();
                HeDaoTao.TenHDT = dr.GetValue(1).ToString();
                HeDaoTaoList.Add(HeDaoTao);
            }
            con.Close();
            return HeDaoTaoList;
        }

        public List<HeDaoTao> GetPagedData(int start, int length, string SearchKey)
        {
            var Cristial = string.Empty;
            if (!string.IsNullOrEmpty(SearchKey))
            {
                Cristial += " AND (MaHDT LIKE '%'+ @search + '%' or TenHDT LIKE '%'+ @search + '%')";
            }
            string query = $"SELECT * FROM HeDaoTao  WHERE 2>1 {Cristial} ORDER BY MaHDT OFFSET @start ROWS FETCH NEXT @length ROWS ONLY";

            using (var connection = new SqlConnection(cs))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@start", start);
                command.Parameters.AddWithValue("@length", length);
                command.Parameters.AddWithValue("@search", SearchKey);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                List<HeDaoTao> HeDaoTaoList = new List<HeDaoTao>();
                foreach (DataRow row in dataTable.Rows)
                {
                    HeDaoTao obj = new HeDaoTao
                    {
                        MaHDT = row["MaHDT"].ToString(),
                        TenHDT = row["TenHDT"].ToString(),
                    };
                    HeDaoTaoList.Add(obj);
                }
                //Tra ve mang doi tuong
                return HeDaoTaoList;
            }
        }

        public int GetTotalRecords()
        {
            using (var connection = new SqlConnection(cs))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM HeDaoTao", connection))
                {
                    int total = (int)command.ExecuteScalar();
                    return total;
                }
            }
        }
        public bool DddHeDaoTao(HeDaoTao HeDaoTao)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("addHeDaoTao", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaHDT", HeDaoTao.MaHDT);
            cmd.Parameters.AddWithValue("@TenHDT", HeDaoTao.TenHDT);
            con.Open();

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateHeDaoTao(HeDaoTao HeDaoTao)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("updateHeDaoTao", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaHDT", HeDaoTao.MaHDT);
            cmd.Parameters.AddWithValue("@TenHDT", HeDaoTao.TenHDT);
            con.Open();

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteHeDaoTao(string MaHDT)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("deleteHeDaoTao", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaHDT", MaHDT);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}