namespace QL_ThuVien.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public virtual DbSet<BanDoc> BanDocs { get; set; }
        public virtual DbSet<HinhAnhHoatDong> HinhAnhHoatDongs { get; set; }
        public virtual DbSet<HoatDong> HoatDongs { get; set; }
        public virtual DbSet<LoaiTaiLieu> LoaiTaiLieux { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhieuYeuCau> PhieuYeuCaus { get; set; }
        public virtual DbSet<TaiLieu> TaiLieux { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BanDoc>()
                .Property(e => e.BD_SoThe)
                .IsUnicode(false);

            modelBuilder.Entity<BanDoc>()
                .Property(e => e.BD_SoCMND)
                .HasPrecision(10, 0);

            modelBuilder.Entity<BanDoc>()
                .Property(e => e.BD_Email)
                .IsUnicode(false);

            modelBuilder.Entity<HinhAnhHoatDong>()
                .Property(e => e.HA_Links)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.NV_ID)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.NV_EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.NV_MATKHAU)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuYeuCau>()
                .Property(e => e.BD_SoThe)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuYeuCau>()
                .Property(e => e.TL_SoDangKyCaBiet)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuYeuCau>()
                .Property(e => e.NV_ID)
                .IsUnicode(false);

            modelBuilder.Entity<TaiLieu>()
                .Property(e => e.TL_SoDangKyCaBiet)
                .IsUnicode(false);

            modelBuilder.Entity<TaiLieu>()
                .Property(e => e.TL_KhoSach)
                .IsUnicode(false);

            modelBuilder.Entity<TaiLieu>()
                .Property(e => e.TL_HinhAnh)
                .IsUnicode(false);
        }
    }
}
