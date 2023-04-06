using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLSV.Areas.Admin.Models
{
    public class Lop
    {
        [Key]
        [Required]
        public string maLop { get; set; }
        [Required]
        public string tenLop { get; set; }
        [Required]
        public string MaKH { get; set; }
        [Required]
        public string maNganhHoc { get; set; }
    }
}