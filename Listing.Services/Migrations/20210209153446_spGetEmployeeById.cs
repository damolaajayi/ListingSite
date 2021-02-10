﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Listing.Services.Migrations
{
    public partial class spGetEmployeeById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create Procedure spGetEmployeeById
                                    @Id int
                                    as
                                    Begin
                                        Select * from Employees
                                        Where Id = @Id
                                    End";
            migrationBuilder.Sql(procedure);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Drop Procedure spGetEmployeeById";
            migrationBuilder.Sql(procedure);

        }
    }
}
