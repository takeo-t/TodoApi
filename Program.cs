using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);
//環境変数のロード
Env.Load();

//コントローラーの追加
builder.Services.AddControllers();

//データベース接続文字列の設定
var connectionString = Environment.GetEnvironmentVariable("TodoContext");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Database connection string is not set in environment variables.");
}
//データベースコンテキストの設定:
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseSqlServer(connectionString));
//APIエクスプローラーとSwaggerの追加
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS（クロスオリジンリソース共有）の設定
builder.Services.AddCors();

//アプリケーションのビルドと構成
var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>

    options.WithOrigins("http://localhost:3000")
           .AllowAnyMethod()
           .AllowAnyHeader()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
