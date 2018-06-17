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
        [StringLength(10)]
        [Display(Name = "Số thẻ")]
        public string BD_SoThe { get; set; }

        [StringLength(200)]
        [Display(Name = "Hẹ & Tên")]
        public string BD_HoVaTen { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime? BD_NgaySinh { get; set; }

        [Column(TypeName = "numeric")]
        [Display(Name = "CMND")]
        public decimal? BD_SoCMND { get; set; }

        [Display(Name = "Ngày cấp")]
        public DateTime? BD_CapNgay { get; set; }

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

        public DateTime? BD_HopDongLaoDong { get; set; }

        [StringLength(400)]
        public string BD_DiaChiNoiLamViec { get; set; }

        public long? BD_DTDIDong { get; set; }

        [StringLength(200)]
        public string BD_Email { get; set; }

        [StringLength(400)]
        public string BD_ChoOHienTai { get; set; }

        [Display(Name = "Giới hạn mượn")]
        public short? BD_GioiHanMuon { get; set; }

        [StringLength(500)]
        [Display(Name = "Hình ảnh")]
        public string BD_HinhAnh { get; set; }

        [Display(Name = "Ngày cấp thẻ")]
        public DateTime? BD_NgayCapThe { get; set; }

        [Display(Name = "Thời hạn thẻ")]
        public DateTime? BD_THSDThe { get; set; }

        [Display(Name = "Thời gian mượn")]
        public DateTime? BD_ThoiGianMuon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuYeuCau> PhieuYeuCaus { get; set; }
    }
}
