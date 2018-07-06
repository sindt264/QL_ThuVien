
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QL_ThuVien.Models
{
    public class Register
    {
        [Key]
        public string BD_SoThe { get; set; }
        [Display(Name = "Họ và tên")]
        public string BD_HoVaTen { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> BD_NgaySinh { get; set; }

        [StringLength(100)]
        [Display(Name = "Trình độ")]
        public string BD_TrinhDo { get; set; }

        [StringLength(400)]
        [Display(Name = "Nơi công tác,học tập")]
        public string BD_NoiCongTacHocTap { get; set; }

        [StringLength(100)]
        [Display(Name = "Nghề nghiệp")]
        public string BD_NgheNghiep { get; set; }

        [StringLength(400)]
        [Display(Name = "Chổ ở hiện tại")]
        public string BD_ChoOHienTai { get; set; }

        [Display(Name = "Số di động")]
        public long? BD_DTDIDong { get; set; }

        [StringLength(200)]
        [Display(Name = "Email")]
        public string BD_Email { get; set; }
    }
}