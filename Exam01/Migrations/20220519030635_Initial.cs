using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam01.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TC = table.Column<double>(type: "REAL", nullable: false),
                    Tran = table.Column<double>(type: "REAL", nullable: false),
                    San = table.Column<double>(type: "REAL", nullable: false),
                    MuaG3 = table.Column<double>(type: "REAL", nullable: false),
                    MuaKL3 = table.Column<int>(type: "INTEGER", nullable: false),
                    MuaG2 = table.Column<double>(type: "REAL", nullable: false),
                    MuaKL2 = table.Column<int>(type: "INTEGER", nullable: false),
                    MuaG1 = table.Column<double>(type: "REAL", nullable: false),
                    MuaKL1 = table.Column<int>(type: "INTEGER", nullable: false),
                    Gia = table.Column<double>(type: "REAL", nullable: false),
                    KL = table.Column<int>(type: "INTEGER", nullable: false),
                    Percent = table.Column<double>(type: "REAL", nullable: false),
                    BanG3 = table.Column<double>(type: "REAL", nullable: false),
                    BanKL3 = table.Column<int>(type: "INTEGER", nullable: false),
                    BanG2 = table.Column<double>(type: "REAL", nullable: false),
                    BanKL2 = table.Column<int>(type: "INTEGER", nullable: false),
                    BanG1 = table.Column<double>(type: "REAL", nullable: false),
                    BanKL1 = table.Column<int>(type: "INTEGER", nullable: false),
                    TongKL = table.Column<int>(type: "INTEGER", nullable: false),
                    MoCua = table.Column<double>(type: "REAL", nullable: false),
                    CaoNhat = table.Column<double>(type: "REAL", nullable: false),
                    ThapNhat = table.Column<double>(type: "REAL", nullable: false),
                    NNMua = table.Column<int>(type: "INTEGER", nullable: false),
                    NNBan = table.Column<int>(type: "INTEGER", nullable: false),
                    RoomConLai = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Stock_Id_Pk", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    TemPass = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Avatar = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("User_Id_Pk", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "User_Email_Uq",
                table: "User",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
