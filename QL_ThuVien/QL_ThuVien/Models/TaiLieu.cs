namespace QL_ThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiLieu")]
    public partial class TaiLieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiLieu()
        {
            PhieuYeuCaus = new HashSet<PhieuYeuCau>();
        }

        [Key]
        [StringLength(20)]
        public string TL_SoDangKyCaBiet { get; set; }

        public int? LTL_IDTheLoai { get; set; }

        [StringLength(250)]
        public string TL_TieuDe { get; set; }

        [StringLength(250)]
        public string TL_TacGia { get; set; }

        [StringLength(250)]
        public string TL_NhaXuatBan { get; set; }

        public DateTime? TL_NamSanXuat { get; set; }

        public int? TL_SoTrang { get; set; }

        [StringLength(500)]
        public string TL_TomTat { get; set; }

        [StringLength(50)]
        public string TL_KhoSach { get; set; }

        public short? TL_DuocMuon { get; set; }

        public short? TL_TrangThai { get; set; }

        [StringLength(255)]
        public string TL_HinhAnh { get; set; }

        public DateTime? TL_NgayNhap { get; set; }

        public virtual LoaiTaiLieu LoaiTaiLieu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuYeuCau> PhieuYeuCaus { get; set; }
    }
}
