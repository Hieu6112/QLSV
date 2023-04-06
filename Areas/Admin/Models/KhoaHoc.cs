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
    public class KhoaHoc
    {
        [Key]
        [Required]
        public string MaKH { get; set; }
        [Required]
        public string TenKH { get; set; }
        [Required]
        public int namBatDau { get; set; }
        [Required]
        public int namKetThuc { get; set; }
        [Required]
        public string MaHDT { get; set; }
    }

    public class KhoaHocDBContext
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public List<KhoaHoc> GetKhoaHocs()
        {
            List<KhoaHoc> KhoaHocList = new List<KhoaHoc>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("getKhoaHoc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                KhoaHoc KhoaHoc = new KhoaHoc();
                KhoaHoc.MaKH = dr.GetValue(0).ToString();
                KhoaHoc.TenKH = dr.GetValue(1).ToString();
                KhoaHoc.namBatDau = dr.GetValue(1).GetHashCode();
                KhoaHoc.namKetThuc = dr.GetValue(1).GetHashCode();
                KhoaHoc.MaHDT = dr.GetValue(1).ToString();
                KhoaHocList.Add(KhoaHoc);
            }
            con.Close();
            return KhoaHocList;
        }

        public List<KhoaHoc> GetPagedData(int start, int length, string SearchKey)
        {
            var Cristial = string.Empty;
            if (!string.IsNullOrEmpty(SearchKey))
            {
                Cristial += " AND (MaHDT LIKE '%'+ @search + '%' or TenKH LIKE '%'+ @search + '%' or namBatDau LIKE '%'+ @search + '%' or namKetThuc LIKE '%'+ @search + '%' or MaKH LIKE '%'+ @search + '%')";
            }
            string query = $"SELECT * FROM KhoaHoc  WHERE 2>1 {Cristial} ORDER BY MaKH OFFSET @start ROWS FETCH NEXT @length ROWS ONLY";

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

                List<KhoaHoc> KhoaHocList = new List<KhoaHoc>();
                foreach (DataRow row in dataTable.Rows)
                {
                    KhoaHoc obj = new KhoaHoc
                    {
                        MaKH = row["MaKH"].ToString(),
                        TenKH = row["TenKH"].ToString(),
                        namBatDau = row["namBatDau"].GetHashCode(),
                        namKetThuc = row["namBatDau"].GetHashCode(),
                        MaHDT = row["MaHDT"].ToString(),
                    };
                    KhoaHocList.Add(obj);
                }
                //Tra ve mang doi tuong
                return KhoaHocList;
            }
        }

        public int GetTotalRecords()
        {
            using (var connection = new SqlConnection(cs))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM KhoaHoc", connection))
                {
                    int total = (int)command.ExecuteScalar();
                    return total;
                }
            }
        }

        public bool DddKhoaHoc(KhoaHoc KhoaHoc)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("addKhoaHoc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaKH", KhoaHoc.MaKH);
            cmd.Parameters.AddWithValue("@TenKH", KhoaHoc.TenKH);
            cmd.Parameters.AddWithValue("@namBatDau", KhoaHoc.namBatDau);
            cmd.Parameters.AddWithValue("@namKetThuc", KhoaHoc.namKetThuc);
            cmd.Parameters.AddWithValue("@MaHDT", KhoaHoc.MaHDT);
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

        public bool UpdateKhoaHoc(KhoaHoc KhoaHoc)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("updateKhoaHoc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaKH", KhoaHoc.MaKH);
            cmd.Parameters.AddWithValue("@TenKH", KhoaHoc.TenKH);
            cmd.Parameters.AddWithValue("@namBatDau", KhoaHoc.namBatDau);
            cmd.Parameters.AddWithValue("@namKetThuc", KhoaHoc.namKetThuc);
            cmd.Parameters.AddWithValue("@MaHDT", KhoaHoc.MaHDT);
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

        public bool DeleteKhoaHoc(string MaKH)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("deleteKhoaHoc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaKH", MaKH);
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
