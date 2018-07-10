namespace QL_ThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HinhAnhHoatDong")]
    public partial class HinhAnhHoatDong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        public int HA_IDHinhAnh { get; set; }

        [Display(Name = "ID hoạt động")]
        public int? HD_IDHoatDong { get; set; }

        [StringLength(255)]
        [Display(Name = "Chú thích")]
        public string HA_ChuThich { get; set; }

        [StringLength(500)]
        [Display(Name = "Nội dung")]
        public string HA_NoiDung { get; set; }

        public virtual HoatDong HoatDong { get; set; }
    }
}
