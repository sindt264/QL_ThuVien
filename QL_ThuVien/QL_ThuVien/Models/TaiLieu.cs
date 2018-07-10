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
        [Required(ErrorMessage = "Tiêu đề không được trống")]
        [StringLength(200)]
        [Display(Name = "Số đăng ký cá biệt")]
        public string TL_SoDangKyCaBiet { get; set; }


        [StringLength(500)]
        [Display(Name = "Chủ đề")]
        public string TL_ChuDe { get; set; }


        [StringLength(250)]
        [Display(Name = "Tiêu đề")]
        public string TL_TieuDe { get; set; }

        [StringLength(250)]
        [Display(Name = "Tác giả")]
        public string TL_TacGia { get; set; }

        [StringLength(250)]
        [Display(Name = "Nhà xuất bản")]
        public string TL_NhaXuatBan { get; set; }

        [Display(Name = "Năm xuất bản")]
        public DateTime? TL_NamSanXuat { get; set; }

        [Display(Name = "Số trang")]
        public int? TL_SoTrang { get; set; }

        [StringLength(500)]
        [Display(Name = "Tóm tắt")]
        public string TL_TomTat { get; set; }

        [StringLength(50)]
        [Display(Name = "Khổ sách")]
        public string TL_KhoSach { get; set; }

        [Display(Name = "Trạng thái")]
        public short? TL_TrangThai { get; set; }

        [StringLength(500)]
        [Display(Name = "Hình ảnh")]
        public string TL_HinhAnh { get; set; }

        [Display(Name = "Ngày nhập")]
        public DateTime? TL_NgayNhap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuYeuCau> PhieuYeuCaus { get; set; }
    }
}
