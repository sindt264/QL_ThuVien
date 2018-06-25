namespace QL_ThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BanDoc")]
    public partial class BanDoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BanDoc()
        {
            PhieuYeuCaus = new HashSet<PhieuYeuCau>();
        }

        [Key]
        [Display(Name = "Số thẻ")]
        [Required(ErrorMessage = "Số thể không được bỏ trống")]
        [StringLength(10, ErrorMessage = "Số thể không được bỏ trống và độ dài lớn hớn {2}", MinimumLength = 4)]
        public string BD_SoThe { get; set; }

        [StringLength(200)]
        [Required(ErrorMessage = "Họ & tên không được bỏ trống")]
        [Display(Name = "Họ & Tên")]
        public string BD_HoVaTen { get; set; }

        [Display(Name = "Ngày sinh")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> BD_NgaySinh { get; set; }

        [Display(Name = "CMND")]
        public decimal? BD_SoCMND { get; set; }

        [Display(Name = "Ngày cấp")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> BD_CapNgay { get; set; }

        [StringLength(100)]
        [Display(Name = "Nơi cấp")]
        public string BD_NoiCap { get; set; }

        [StringLength(100)]
        [Display(Name = "trình độ")]
        public string BD_TrinhDo { get; set; }

        [StringLength(400)]
        [Display(Name = "Nơi công tác(học tập)")]
        public string BD_NoiCongTacHocTap { get; set; }

        [StringLength(100)]
        [Display(Name = "Nghề nghiệp")]
        public string BD_NgheNghiep { get; set; }

        [Display(Name = "Thời hạn lao động")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> BD_HopDongLaoDong { get; set; }

        [StringLength(400)]
        [Display(Name = "Điạ chỉ")]
        public string BD_DiaChiNoiLamViec { get; set; }

        [Display(Name = "Số di động")]
        public long? BD_DTDIDong { get; set; }

        [StringLength(200)]
        [Display(Name = "Email")]
        public string BD_Email { get; set; }

        [StringLength(400)]
        [Display(Name = "Chổ ở hiện tại")]
        public string BD_ChoOHienTai { get; set; }

        [Display(Name = "Giới hạn mượn")]
        public short? BD_GioiHanMuon { get; set; }

        [StringLength(500)]
        [Display(Name = "Hình ảnh")]
        public string BD_HinhAnh { get; set; }

        [Display(Name = "Ngày cấp thẻ")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> BD_NgayCapThe { get; set; }

        [Display(Name = "Thời hạn thẻ")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> BD_THSDThe { get; set; }

        [Display(Name = "Thời gian mượn")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> BD_ThoiGianMuon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuYeuCau> PhieuYeuCaus { get; set; }
    }
}
