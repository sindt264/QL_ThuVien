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
        public int HA_IDHinhAnh { get; set; }

        public int? HD_IDHoatDong { get; set; }

        [StringLength(255)]
        public string HA_ChuThich { get; set; }

        [StringLength(255)]
        public string HA_NoiDung { get; set; }

        public virtual HoatDong HoatDong { get; set; }
    }
}
