using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BaiTap_EF_DbFirst_ThanhGiang.Models;

public partial class QuanlynhanvienContext : DbContext
{
    public QuanlynhanvienContext()
    {
    }

    public QuanlynhanvienContext(DbContextOptions<QuanlynhanvienContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CongTy> CongTies { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<PhongBan> PhongBans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-9TOR6RJG;Initial Catalog=quanlynhanvien;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CongTy>(entity =>
        {
            entity.HasKey(e => e.MaCongTy).HasName("PK__CongTy__E452D3DBAF66AC84");

            entity.ToTable("CongTy");

            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.TenCongTy).HasMaxLength(100);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__NhanVien__77B2CA47B03B0289");

            entity.ToTable("NhanVien");

            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.TenNhanVien).HasMaxLength(100);

            entity.HasOne(d => d.MaPhongBanNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.MaPhongBan)
                .HasConstraintName("FK__NhanVien__MaPhon__29572725");
        });

        modelBuilder.Entity<PhongBan>(entity =>
        {
            entity.HasKey(e => e.MaPhongBan).HasName("PK__PhongBan__D0910CC85B20D95A");

            entity.ToTable("PhongBan");

            entity.Property(e => e.TenPhongBan).HasMaxLength(100);

            entity.HasOne(d => d.MaCongTyNavigation).WithMany(p => p.PhongBans)
                .HasForeignKey(d => d.MaCongTy)
                .HasConstraintName("FK__PhongBan__MaCong__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
