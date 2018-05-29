namespace QL_ThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuYeuCau")]
    public partial class PhieuYeuCau
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PYC_IDPhieuYeuCau { get; set; }

        [StringLength(10)]
        public string BD_SoThe { get; set; }

        [StringLength(20)]
        public string TL_SoDangKyCaBiet { get; set; }

        [StringLength(20)]
        public string NV_ID { get; set; }

        public DateTime? PYC_NgayMuon { get; set; }

        public DateTime? PYC_NgayTra { get; set; }

        public int? PYC_Tre { get; set; }

        public virtual BanDoc BanDoc { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual TaiLieu TaiLieu { get; set; }
    }
}
