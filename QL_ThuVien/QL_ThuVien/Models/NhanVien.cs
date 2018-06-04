namespace QL_ThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            PhieuYeuCaus = new HashSet<PhieuYeuCau>();
        }

        [Key]
        [StringLength(20)]
        public string NV_ID { get; set; }

        [StringLength(100)]
        public string NV_HOTEN { get; set; }

        public DateTime? NV_NGAYSINH { get; set; }

        public int? NV_GIOITINH { get; set; }

        [StringLength(50)]
        public string NV_EMAIL { get; set; }

        public long? NV_SDT { get; set; }

        [StringLength(100)]
        public string NV_QUEQUAN { get; set; }

        [StringLength(255)]
        public string NV_MATKHAU { get; set; }

        [StringLength(255)]
        public string NV_HINHANH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuYeuCau> PhieuYeuCaus { get; set; }
    }
}
