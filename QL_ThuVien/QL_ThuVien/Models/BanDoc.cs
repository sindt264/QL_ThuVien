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
        [Required(ErrorMessage = "Số thể không được bỏ trống")]
        [StringLength(10, ErrorMessage = "Số thể không được bỏ trống và độ dài lớn hớn {2}", MinimumLength = 4)]
        [Display(Name = "Số thẻ")]

        public string BD_SoThe { get; set; }

        [StringLength(200)]
        [Display(Name = "Họ và tên")]
        public string BD_HoVaTen { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> BD_NgaySinh { get; set; }

        [Column(TypeName = "numeric")]
        [Display(Name = "Số CMND")]
        public decimal? BD_SoCMND { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày cấp")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> BD_CapNgay { get; set; }

        [StringLength(100)]
        [Display(Name = "Nơi cấp")]
        public string BD_NoiCap { get; set; }

        [StringLength(100)]
        [Display(Name = "Trình độ")]
        public string BD_TrinhDo { get; set; }

        [StringLength(100)]
        [Display(Name = "Nơi công tác")]
        public string BD_NoiCongTacHocTap { get; set; }

        [StringLength(100)]
        [Display(Name = "Nghề nghiệp")]
        public string BD_NgheNghiep { get; set; }

        [StringLength(100)]
        [Display(Name = "Chức vụ")]
        public string BD_ChucVu { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Thời hạn lao động")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> BD_HopDongLaoDong { get; set; }


        [StringLength(400)]
        [Display(Name = "Địa chỉ")]
        public string BD_DiaChiNoiLamViec { get; set; }

        [Display(Name = "SDT cơ quan")]
        public long? BD_DTCoQuan { get; set; }
        [Display(Name = "SDT di dộng")]
        public long? BD_DTDIDong { get; set; }

        [StringLength(200)]
        [Display(Name = "Email")]
        public string BD_Email { get; set; }

        [StringLength(400)]
        [Display(Name = "Chỗ ở hiện tại")]
        public string BD_ChoOHienTai { get; set; }
        [Display(Name = "Số lượng")]
        public short? BD_GioiHanMuon { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày cấp thẻ")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> BD_NgayCap { get; set; }

        [Display(Name = "Hình ảnh")]
        [StringLength(255)]
        public string BD_HinhAnh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuYeuCau> PhieuYeuCaus { get; set; }
    }
}

