using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLSV.Areas.Admin.Models
{
    public class SinhVien
    {
        [Key]
        [Required]
        public string MaSV { get; set; }
        [Required]
        public string hoTen { get; set; }
        [Required]
        public string gioiTinh { get; set; }
        [Required]
        public int namSinh { get; set; }
        [Required]
        public string diaChi { get; set; }
        [Required]
        public string danToc { get; set; }
        [Required]
        public string maLop { get; set; }
    }
}