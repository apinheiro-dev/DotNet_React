using Microsoft.EntityFrameworkCore;
using StudentsWebApi.DataContext;
using StudentsWebApi.Service.StudentService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using StudentsWebApi.Models;
using StudentsWebApi.Repositories;
using StudentsWebApi.Service.UserService;
using Microsoft.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(builder.Configuration["jwt:secretKey"]);

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

//[Authorize]

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("Admin", policy => policy.RequireRole("manager"));
//    options.AddPolicy("Student", policy => policy.RequireRole("student"));
//});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen( i =>
{
    i.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Desafio .Net | React",
        Version = "v1",
        Description = "Criação de API usando .NET 6 no Back-end e Front-end em React com Autenticação."
    });
});

builder.Services.AddScoped<IStudentInterface, StudentService>();
//builder.Services.AddScoped<IUserInterface, UserService>();

builder.Services.AddCors();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoSql"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(opcoes => opcoes.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
//app.UseAuthorization();


app.MapPost("/api/auth/login", (UserModel userModel) =>
{
    var user = UserRepository.Get(userModel.NomeUsuario, userModel.Senha);

    if (user == null)
        return Results.NotFound(new { message = "Login e/ou senha inválido(s)" });

    var token = UserService.GerarToken(user);

    user.Senha = "";

    return Results.Ok(new
    {
        user = user,
        token = token
    });
});

app.MapControllers();

app.Run();
