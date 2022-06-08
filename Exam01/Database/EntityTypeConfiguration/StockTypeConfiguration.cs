using Exam01.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam01.Database.EntityTypeConfiguration;
public class StockTypeConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable("Stock");
        builder.HasKey(e=>e.Id)
        .HasName("Stock_Id_Pk");
        builder.Property(e=>e.Id)
        .HasColumnName("Id");
        builder.Property(e=> e.TC)
        .HasColumnName("TC")
        .IsRequired();
        builder.Property(e=> e.Tran)
        .HasColumnName("Tran")
        .IsRequired();
        builder.Property(e=> e.San)
        .HasColumnName("San")
        .IsRequired();
        builder.Property(e=> e.MuaG3)
        .HasColumnName("MuaG3");
        builder.Property(e=> e.MuaKL3)
        .HasColumnName("MuaKL3");
        builder.Property(e=> e.MuaG2)
        .HasColumnName("MuaG2");
        builder.Property(e=> e.MuaKL2)
        .HasColumnName("MuaKL2");
        builder.Property(e=> e.MuaG1)
        .HasColumnName("MuaG1");
        builder.Property(e=> e.MuaKL1)
        .HasColumnName("MuaKL1");
        builder.Property(e=> e.Gia)
        .HasColumnName("Gia");
        builder.Property(e=> e.KL)
        .HasColumnName("KL");
        builder.Property(e=> e.Percent)
        .HasColumnName("Percent");
        builder.Property(e=> e.BanG3)
        .HasColumnName("BanG3");
        builder.Property(e=> e.BanKL3)
        .HasColumnName("BanKL3");
        builder.Property(e=> e.BanG2)
        .HasColumnName("BanG2");
        builder.Property(e=> e.BanKL2)
        .HasColumnName("BanKL2");
        builder.Property(e=> e.BanG1)
        .HasColumnName("BanG1");
        builder.Property(e=> e.BanKL1)
        .HasColumnName("BanKL1");
        builder.Property(e=> e.TongKL)
        .HasColumnName("TongKL");
        builder.Property(e=> e.MoCua)
        .HasColumnName("MoCua");
        builder.Property(e=> e.CaoNhat)
        .HasColumnName("CaoNhat");
        builder.Property(e=> e.ThapNhat)
        .HasColumnName("ThapNhat");
        builder.Property(e=> e.NNMua)
        .HasColumnName("NNMua");
        builder.Property(e=> e.NNBan)
        .HasColumnName("NNBan");
        builder.Property(e=> e.RoomConLai)
        .HasColumnName("RoomConLai");
    }
}