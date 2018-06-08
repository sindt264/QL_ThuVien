namespace QL_ThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuYeuCau")]
    public partial class PhieuYeuCau
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Mã phiếu")]
        public int PYC_IDPhieuYeuCau { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage ="Trường không thể rỗng !")]
        [Display(Name = "Số thẻ")]
        public string BD_SoThe { get; set; }

        [StringLength(20)]
        [Display(Name = "Số đăng ký cá biệt")]
        [Required(ErrorMessage = "Trường không thể rỗng !")]
        public string TL_SoDangKyCaBiet { get; set; }

        [StringLength(20)]
        [Display(Name = "Mã nhân viên")]
        public string NV_ID { get; set; }

        [Display(Name = "Ngày mượn")]
        public DateTime? PYC_NgayMuon { get; set; }

        [Display(Name = "Ngày trả")]
        public DateTime? PYC_NgayTra { get; set; }

        public virtual BanDoc BanDoc { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual TaiLieu TaiLieu { get; set; }
    }
}
