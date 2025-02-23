using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagement.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounted_Member_Subscriptions_Member_Subscriptions_Member_SubscriptionsId",
                table: "Discounted_Member_Subscriptions");

            migrationBuilder.AlterColumn<double>(
                name: "PaidPrice",
                table: "Member_Subscriptions",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DiscountValue",
                table: "Member_Subscriptions",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Member_SubscriptionsId",
                table: "Discounted_Member_Subscriptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounted_Member_Subscriptions_Member_Subscriptions_Member_SubscriptionsId",
                table: "Discounted_Member_Subscriptions",
                column: "Member_SubscriptionsId",
                principalTable: "Member_Subscriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounted_Member_Subscriptions_Member_Subscriptions_Member_SubscriptionsId",
                table: "Discounted_Member_Subscriptions");

            migrationBuilder.AlterColumn<double>(
                name: "PaidPrice",
                table: "Member_Subscriptions",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "DiscountValue",
                table: "Member_Subscriptions",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Member_SubscriptionsId",
                table: "Discounted_Member_Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Discounted_Member_Subscriptions_Member_Subscriptions_Member_SubscriptionsId",
                table: "Discounted_Member_Subscriptions",
                column: "Member_SubscriptionsId",
                principalTable: "Member_Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
