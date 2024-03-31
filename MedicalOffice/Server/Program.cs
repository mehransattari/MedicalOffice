
using MedicalOffice.Server.Helper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddBaseServices(builder);

builder.Services.AddSqlServerServices(builder);

builder.Services.AddCorsServices(builder);

builder.Services.AddAuthServices(builder);

var app = builder.Build();

app.AddAppCustom();


app.Run();
