using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Custra.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOpportunities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesPipelines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesPipelines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesPipelineStages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesPipelineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Probability = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesPipelineStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesPipelineStages_SalesPipelines_SalesPipelineId",
                        column: x => x.SalesPipelineId,
                        principalTable: "SalesPipelines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Opportunities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwnerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesPipelineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesPipelineStageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    EstimatedValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ExpectedCloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opportunities_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Opportunities_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunities_SalesPipelineStages_SalesPipelineStageId",
                        column: x => x.SalesPipelineStageId,
                        principalTable: "SalesPipelineStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunities_SalesPipelines_SalesPipelineId",
                        column: x => x.SalesPipelineId,
                        principalTable: "SalesPipelines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_ContactId",
                table: "Opportunities",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_CustomerId",
                table: "Opportunities",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_OrganizationId_CustomerId",
                table: "Opportunities",
                columns: new[] { "OrganizationId", "CustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_OrganizationId_OwnerUserId",
                table: "Opportunities",
                columns: new[] { "OrganizationId", "OwnerUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_OrganizationId_SalesPipelineId_SalesPipelineStageId",
                table: "Opportunities",
                columns: new[] { "OrganizationId", "SalesPipelineId", "SalesPipelineStageId" });

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_OrganizationId_Status",
                table: "Opportunities",
                columns: new[] { "OrganizationId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_SalesPipelineId",
                table: "Opportunities",
                column: "SalesPipelineId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_SalesPipelineStageId",
                table: "Opportunities",
                column: "SalesPipelineStageId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesPipelines_OrganizationId_Name",
                table: "SalesPipelines",
                columns: new[] { "OrganizationId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesPipelineStages_OrganizationId_SalesPipelineId_Name",
                table: "SalesPipelineStages",
                columns: new[] { "OrganizationId", "SalesPipelineId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesPipelineStages_OrganizationId_SalesPipelineId_Order",
                table: "SalesPipelineStages",
                columns: new[] { "OrganizationId", "SalesPipelineId", "Order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesPipelineStages_SalesPipelineId",
                table: "SalesPipelineStages",
                column: "SalesPipelineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opportunities");

            migrationBuilder.DropTable(
                name: "SalesPipelineStages");

            migrationBuilder.DropTable(
                name: "SalesPipelines");
        }
    }
}
