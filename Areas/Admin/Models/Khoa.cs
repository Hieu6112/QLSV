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
    public class Khoa
    {
        [Key]
        [Required]
        public string maKhoa { get; set; }
        [Required]
        public string tenKhoa { get; set; }
    }

    public class KhoaDBContext
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public List<Khoa> GetKhoas()
        {
            List<Khoa> KhoaList = new List<Khoa>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("getKhoa", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Khoa khoa = new Khoa();
                khoa.maKhoa = dr.GetValue(0).ToString();
                khoa.tenKhoa = dr.GetValue(1).ToString();
                KhoaList.Add(khoa);
            }
            con.Close();
            return KhoaList;
        }

        public List<Khoa> GetPagedData(int start, int length, string SearchKey)
        {
            var Cristial = string.Empty;
            if (!string.IsNullOrEmpty(SearchKey))
            {
                Cristial += " AND (maKhoa LIKE '%'+ @search + '%' or tenKhoa LIKE '%'+ @search + '%')";
            }
            string query = $"SELECT * FROM Khoa  WHERE 2>1 {Cristial} ORDER BY maKhoa OFFSET @start ROWS FETCH NEXT @length ROWS ONLY";

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

                List<Khoa> KhoaList = new List<Khoa>();
                foreach (DataRow row in dataTable.Rows)
                {
                    Khoa obj = new Khoa
                    {
                        maKhoa = row["maKhoa"].ToString(),
                        tenKhoa = row["tenKhoa"].ToString(),
                    };
                    KhoaList.Add(obj);
                }
                //Tra ve mang doi tuong
                return KhoaList;
            }
        }

        public int GetTotalRecords()
        {
            using (var connection = new SqlConnection(cs))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Khoa", connection))
                {
                    int total = (int)command.ExecuteScalar();
                    return total;
                }
            }
        }
        public bool DddKhoa(Khoa khoa)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("addKhoa", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@maKhoa", khoa.maKhoa);
            cmd.Parameters.AddWithValue("@tenKhoa", khoa.tenKhoa);
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

        //public void UpdateKhoa(string maKhoa, string tenKhoa)
        //{
        //    using (var connection = new SqlConnection(cs))
        //    {
        //        connection.Open();

        //        SqlCommand command = new SqlCommand("UPDATE Khoa SET tenKhoa=@tenKhoa WHERE maKhoa=@maKhoa", connection);
        //        command.Parameters.AddWithValue("@tenKhoa", tenKhoa);
        //        command.Parameters.AddWithValue("@maKhoa", maKhoa);

        //        command.ExecuteNonQuery();
        //    }
        //}

        public bool UpdateKhoa(Khoa khoa)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("updateKhoa", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@maKhoa", khoa.maKhoa);
            cmd.Parameters.AddWithValue("@tenKhoa", khoa.tenKhoa);
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

        public bool DeleteKhoa(string maKhoa)
        {
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("deleteKhoa", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@maKhoa", maKhoa);
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

