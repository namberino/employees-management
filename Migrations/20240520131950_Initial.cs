using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FullProject.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhatDiMuons",
                columns: table => new
                {
                    ThangTinhCong = table.Column<string>(type: "char(7)", maxLength: 7, nullable: false),
                    SoGioTinhMuon = table.Column<int>(type: "int", nullable: false),
                    SoTienPhatMuon = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTienPhatNghiLam = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhatDiMuons", x => x.ThangTinhCong);
                });

            migrationBuilder.CreateTable(
                name: "PhongBans",
                columns: table => new
                {
                    MaPhongBan = table.Column<string>(type: "varchar(10)", nullable: false),
                    TenPhongBan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBans", x => x.MaPhongBan);
                });

            migrationBuilder.CreateTable(
                name: "ThietLaps",
                columns: table => new
                {
                    ThangTinhCong = table.Column<string>(type: "char(7)", maxLength: 7, nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "date", nullable: true),
                    NgayKetThuc = table.Column<DateTime>(type: "date", nullable: true),
                    NgayCongBatBuoc = table.Column<int>(type: "int", nullable: true),
                    NgayPhepToiDa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThietLaps", x => x.ThangTinhCong);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    MaNV = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    HoTen = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    Ngaysinh = table.Column<DateTime>(type: "date", nullable: true),
                    CCCD = table.Column<string>(type: "char(12)", maxLength: 12, nullable: true),
                    SoDienThoai = table.Column<string>(type: "VARCHAR(11)", nullable: true),
                    MailLamViec = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    ChucVu = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    MaPhongBan = table.Column<string>(type: "VARCHAR(10)", nullable: true),
                    LoaiNhanVien = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    BatDauLamViec = table.Column<DateTime>(type: "date", nullable: true),
                    ChinhThucLamViec = table.Column<DateTime>(type: "date", nullable: true),
                    YeuCauChamCong = table.Column<string>(type: "NVARCHAR(10)", nullable: true),
                    NgayNghiViec = table.Column<DateTime>(type: "date", nullable: true),
                    GioiTinh = table.Column<string>(type: "NVARCHAR(10)", nullable: true),
                    DiaChiTamChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiThuongChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrinhDoHocVan = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    STKNganHang = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    TenNganHang = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    MaSoThue = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    MaBaoHiem = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    TrangThai = table.Column<string>(type: "NVARCHAR(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.MaNV);
                    table.ForeignKey(
                        name: "FK_NhanViens_PhongBans_MaPhongBan",
                        column: x => x.MaPhongBan,
                        principalTable: "PhongBans",
                        principalColumn: "MaPhongBan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BangCongs",
                columns: table => new
                {
                    MaNV = table.Column<string>(type: "varchar(10)", nullable: false),
                    ThangTinhCong = table.Column<string>(type: "char(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BangCongs", x => new { x.MaNV, x.ThangTinhCong });
                    table.ForeignKey(
                        name: "FK_BangCongs_NhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BangCongs_ThietLaps_ThangTinhCong",
                        column: x => x.ThangTinhCong,
                        principalTable: "ThietLaps",
                        principalColumn: "ThangTinhCong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChamCongs",
                columns: table => new
                {
                    MaNV = table.Column<string>(type: "varchar(10)", nullable: false),
                    NgayChamCong = table.Column<DateTime>(type: "date", nullable: false),
                    GioVao = table.Column<TimeSpan>(type: "time", nullable: false),
                    GioRa = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChamCongs", x => new { x.MaNV, x.NgayChamCong });
                    table.ForeignKey(
                        name: "FK_ChamCongs_NhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Luongs",
                columns: table => new
                {
                    MaNV = table.Column<string>(type: "varchar(10)", nullable: false),
                    ThangLinhLuong = table.Column<string>(type: "char(7)", nullable: false),
                    LuongCung = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Thue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Luongs", x => new { x.MaNV, x.ThangLinhLuong });
                    table.ForeignKey(
                        name: "FK_Luongs_NhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Luongs_PhatDiMuons_MaNV",
                        column: x => x.ThangLinhLuong,
                        principalTable: "PhatDiMuons",
                        principalColumn: "ThangTinhCong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    TenTaiKhoan = table.Column<string>(type: "varchar(50)", nullable: false),
                    MatKhau = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    MaNV = table.Column<string>(type: "varchar(10)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.TenTaiKhoan);
                    table.ForeignKey(
                        name: "FK_TaiKhoans_NhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThuongPhats",
                columns: table => new
                {
                    MaThuongPhat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNV = table.Column<string>(type: "varchar(10)", nullable: false),
                    Loai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NguonThuongPhat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ngay = table.Column<DateTime>(type: "date", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuongPhats", x => x.MaThuongPhat);
                    table.ForeignKey(
                        name: "FK_ThuongPhats_NhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichSus",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(10)", nullable: false),
                    TenTaiKhoan = table.Column<string>(type: "varchar(50)", nullable: false),
                    NgayGio = table.Column<DateTime>(type: "datetime", nullable: false),
                    HoatDong = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSus", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LichSus_TaiKhoans_TenTaiKhoan",
                        column: x => x.TenTaiKhoan,
                        principalTable: "TaiKhoans",
                        principalColumn: "TenTaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BangCongs_ThangTinhCong",
                table: "BangCongs",
                column: "ThangTinhCong");

            migrationBuilder.CreateIndex(
                name: "IX_LichSus_TenTaiKhoan",
                table: "LichSus",
                column: "TenTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_MaPhongBan",
                table: "NhanViens",
                column: "MaPhongBan");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_MaNV",
                table: "TaiKhoans",
                column: "MaNV",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThuongPhats_MaNV",
                table: "ThuongPhats",
                column: "MaNV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BangCongs");

            migrationBuilder.DropTable(
                name: "ChamCongs");

            migrationBuilder.DropTable(
                name: "LichSus");

            migrationBuilder.DropTable(
                name: "Luongs");

            migrationBuilder.DropTable(
                name: "ThuongPhats");

            migrationBuilder.DropTable(
                name: "ThietLaps");

            migrationBuilder.DropTable(
                name: "TaiKhoans");

            migrationBuilder.DropTable(
                name: "PhatDiMuons");

            migrationBuilder.DropTable(
                name: "NhanViens");

            migrationBuilder.DropTable(
                name: "PhongBans");
        }
    }
}
