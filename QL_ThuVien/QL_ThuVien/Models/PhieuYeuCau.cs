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
        public int PYC_IDPhieuYeuCau { get; set; }

        [StringLength(10)]
        [Display(Name = "Số thẻ")]
        public string BD_SoThe { get; set; }

        [StringLength(200)]
        [Display(Name = "Số đăng ký cá biệt")]
        public string TL_SoDangKyCaBiet { get; set; }

        [StringLength(20)]
        [Display(Name = "ID nhân viên")]
        public string NV_ID { get; set; }

        [Display(Name = "Ngày mượn")]
        public DateTime? PYC_NgayMuon { get; set; }

        [Display(Name = "Ngày trả")]
        public DateTime? PYC_NgayTra { get; set; }

        [Display(Name = "Trễ")]
        public int? PYC_Tre { get; set; }

        [Display(Name = "Trạng thái")]
        public short? PYC_TrangThai { get; set; }

        public virtual BanDoc BanDoc { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual TaiLieu TaiLieu { get; set; }
    }
}
