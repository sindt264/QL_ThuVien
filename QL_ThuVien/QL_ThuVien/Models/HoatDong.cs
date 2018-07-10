namespace QL_ThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoatDong")]
    public partial class HoatDong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoatDong()
        {
            HinhAnhHoatDongs = new HashSet<HinhAnhHoatDong>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Hoạt Động")]
        public int HD_IDHoatDong { get; set; }

        [StringLength(255)]
        [Display(Name = "Tiêu đề")]
        public string HD_ChuDe { get; set; }

        [StringLength(255)]
        [Display(Name = "Nội dung")]
        public string HD_NoiDung { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        public DateTime? HD_NgayHoatDong { get; set; }

        [Display(Name = "Ngày kết thúc")]
        public DateTime? HD_NgayKetThuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HinhAnhHoatDong> HinhAnhHoatDongs { get; set; }
    }
}
