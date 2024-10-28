using System;
using System.Data.SqlClient;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// For production scenarios, consider keeping Swagger configurations behind the environment check
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

string connectionString = app.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")!;

app.MapGet("/Myoffice_ACPD", () =>
{
    var rows = new List<string>();

    using var conn = new SqlConnection(connectionString);
    conn.Open();

    var command = new SqlCommand("SELECT * FROM Myoffice_ACPD", conn);
    using SqlDataReader reader = command.ExecuteReader();

    if (reader.HasRows)
    {
        while (reader.Read())
        {
            rows.Add($"{reader.GetString(0)},{reader.GetString(1)},{reader.GetString(2)},{reader.GetString(3)}" +
                $",{reader.GetByte(4)},{reader.GetBoolean(5)},{reader.GetString(6)},{reader.GetString(7)},{reader.GetString(8)}" +
                $"{reader.GetString(9)},{reader.GetDateTime(10)},{reader.GetString(11)},{reader.GetDateTime(12)},{reader.GetString(13)}");
        }
    }

    return rows;
})
.WithName("GetMyoffice_ACPDs")
.WithOpenApi();

app.MapPost("/Myoffice_ACPD", (Myoffice_ACPD myoffice_ACPD) =>
{
    using var conn = new SqlConnection(connectionString);
    conn.Open();

    var command = new SqlCommand(
        "INSERT INTO Myoffice_ACPD " +
        "(acpd_cname, acpd_ename,acpd_sname,acpd_email,acpd_status,acpd_stop,acpd_stopMeom,acpd_LoginID,acpd_LoginPW,acpd_memo,acpd_nowdatetime,acpd_nowid,acpd_updatetime,acpd_updid) " +
        "VALUES " +
        "(@acpd_cname, @acpd_ename,@acpd_sname,@acpd_email,@acpd_status,@acpd_stop,@acpd_stopMeom,@acpd_LoginID,@acpd_LoginPW,@acpd_memo,@acpd_nowdatetime,@acpd_nowid,@acpd_updatetime,@acpd_updid)",
        conn);

    command.Parameters.Clear();
    command.Parameters.AddWithValue("@acpd_cname", myoffice_ACPD.acpd_cname);
    command.Parameters.AddWithValue("@acpd_ename", myoffice_ACPD.acpd_ename);
    command.Parameters.AddWithValue("@acpd_sname", myoffice_ACPD.acpd_sname);
    command.Parameters.AddWithValue("@acpd_email", myoffice_ACPD.acpd_email);
    command.Parameters.AddWithValue("@acpd_status", myoffice_ACPD.acpd_status);
    command.Parameters.AddWithValue("@acpd_stop", myoffice_ACPD.acpd_stop);
    command.Parameters.AddWithValue("@acpd_stopMeom", myoffice_ACPD.acpd_stopMeom);
    command.Parameters.AddWithValue("@acpd_LoginID", myoffice_ACPD.acpd_LoginID);
    command.Parameters.AddWithValue("@acpd_LoginPW", myoffice_ACPD.acpd_LoginPW);
    command.Parameters.AddWithValue("@acpd_memo", myoffice_ACPD.acpd_memo);
    command.Parameters.AddWithValue("@acpd_nowdatetime", myoffice_ACPD.acpd_nowdatetime);
    command.Parameters.AddWithValue("@acpd_nowid", myoffice_ACPD.acpd_nowid);
    command.Parameters.AddWithValue("@acpd_updatetime", myoffice_ACPD.acpd_updatetime);
    command.Parameters.AddWithValue("@acpd_updid", myoffice_ACPD.acpd_updid);
    using SqlDataReader reader = command.ExecuteReader();
})
.WithName("CreateMyoffice_ACPD")
.WithOpenApi();

app.Run();