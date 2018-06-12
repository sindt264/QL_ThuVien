namespace QL_ThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoaiTaiLieu")]
    public partial class LoaiTaiLieu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LTL_IDTheLoai { get; set; }

        [StringLength(250)]
        public string LTL_TuKhoa { get; set; }
    }
}
