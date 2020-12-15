using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace dotnetExamTrainer.Migrations
{
    public partial class ContextRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionText = table.Column<string>(nullable: true),
                    AnswerText = table.Column<string[]>(nullable: true),
                    RightAnswer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TakenExams",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: true),
                    Taken = table.Column<DateTime>(nullable: false),
                    AmountAnsweredRight = table.Column<int>(nullable: false),
                    AmountAnsweredWrong = table.Column<int>(nullable: false),
                    QuestionsAnsweredRight = table.Column<int[]>(nullable: true),
                    QuestionsAnsweredWrong = table.Column<int[]>(nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "TakenExams");
        }
    }
}
