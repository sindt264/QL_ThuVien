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
        public string BD_SoThe { get; set; }

        [StringLength(200)]
        public string BD_HoVaTen { get; set; }

        public DateTime? BD_NgaySinh { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BD_SoCMND { get; set; }

        public DateTime? BD_CapNgay { get; set; }

        [StringLength(100)]
        public string BD_NoiCap { get; set; }

        [StringLength(100)]
        public string BD_TrinhDo { get; set; }

        [StringLength(100)]
        public string BD_NoiCongTacHocTap { get; set; }

        [StringLength(100)]
        public string BD_NgheNghiep { get; set; }

        [StringLength(100)]
        public string BD_ChucVu { get; set; }

        public DateTime? BD_HopDongLaoDong { get; set; }

        [StringLength(400)]
        public string BD_DiaChiNoiLamViec { get; set; }

        public long? BD_DTCoQuan { get; set; }

        public long? BD_DTDIDong { get; set; }

        [StringLength(200)]
        public string BD_Email { get; set; }

        [StringLength(400)]
        public string BD_ChoOHienTai { get; set; }

        public short? BD_GioiHanMuon { get; set; }

        public DateTime? BD_NgayCap { get; set; }

        [Column(TypeName = "image")]
        public byte[] BD_HinhAnh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuYeuCau> PhieuYeuCaus { get; set; }
    }
}
