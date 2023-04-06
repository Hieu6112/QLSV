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
    public class NganhHoc
    {
        [Key]
        [Required]
        public string maNganhHoc { get; set; }
        [Required]
        public string tenNganhHoc { get; set; }
        [Required]
        public string maKhoa { get; set; }

    }
    public class NganhHocDBContext
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public List<NganhHoc> GetNganhHocs()
        {
            List<NganhHoc> NganhHocList = new List<NganhHoc>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("getNganhHoc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                NganhHoc NganhHoc = new NganhHoc();
                NganhHoc.maNganhHoc = dr.GetValue(0).ToString();
                NganhHoc.tenNganhHoc = dr.GetValue(1).ToString();
                NganhHoc.maKhoa = dr.GetValue(1).ToString();
                NganhHocList.Add(NganhHoc);
            }
            con.Close();
            return NganhHocList;
        }

        public List<NganhHoc> GetPagedData(int start, int length, string SearchKey)
        {
            var Cristial = string.Empty;
            if (!string.IsNullOrEmpty(SearchKey))
            {
                Cristial += " AND (maKhoa LIKE '%'+ @search + '%' or maNganhHoc LIKE '%'+ @search + '%' or tenNganhHoc LIKE '%'+ @search + '%')";
            }
            string query = $"SELECT * FROM NganhHoc  WHERE 2>1 {Cristial} ORDER BY maNganhHoc OFFSET @start ROWS FETCH NEXT @length ROWS ONLY";

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

                List<NganhHoc> NganhHocList = new List<NganhHoc>();
                foreach (DataRow row in dataTable.Rows)
                {
                    NganhHoc obj = new NganhHoc
                    {
                        maNganhHoc = row["maNganhHoc"].ToString(),
                        tenNganhHoc = row["tenNganhHoc"].ToString(),
                        maKhoa = row["maKhoa"].ToString(),
                    };
                    NganhHocList.Add(obj);
                }
                //Tra ve mang doi tuong
                return NganhHocList;
            }
        }

        public int GetTotalRecords()
        {
            using (var connection = new SqlConnection(cs))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM NganhHoc", connection))
                {
                    int total = (int)command.ExecuteScalar();
                    return total;
                }
            }
        }

        public bool DddNganhHoc(NganhHoc NganhHoc)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("addNganhHoc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@maNganhHoc", NganhHoc.maNganhHoc);
            cmd.Parameters.AddWithValue("@tenNganhHoc", NganhHoc.tenNganhHoc);
            cmd.Parameters.AddWithValue("@maKhoa", NganhHoc.maKhoa);
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

        public bool UpdateNganhHoc(NganhHoc NganhHoc)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("updateNganhHoc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@maNganhHoc", NganhHoc.maNganhHoc);
            cmd.Parameters.AddWithValue("@tenNganhHoc", NganhHoc.tenNganhHoc);
            cmd.Parameters.AddWithValue("@maKhoa", NganhHoc.maKhoa);
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

        public bool DeleteNganhHoc(string maNganhHoc)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("deleteNganhHoc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@maNganhHoc", maNganhHoc);
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
