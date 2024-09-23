using Amazon.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Storage.Domain.Interfaces;
using Storage.Domain.Services;
using Storage.Infrastructure.Context;
using Storage.Infrastructure.Repositories;
using Storage.Infrastructure.Services;
using StorageMicroservice.Extensions;
using StorageMicroservice.Helpers;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddScoped<ISFileService, SFileService>();
builder.Services.AddScoped<ISFileRepository, SFileRepository>();
builder.Services.AddScoped<IFileValidationService, FileValidationService>();

//AWS S3
// Register AWS S3 Client
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();

// Register S3 service
builder.Services.AddScoped<IS3Service>(sp =>
{
    var s3Client = sp.GetRequiredService<IAmazonS3>();
    var bucketName = builder.Configuration["AWS:BucketName"]; 
    return new S3Service(s3Client, bucketName);
});

//JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    };
                });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB Connection string
builder.Services.AddDbContext<StorageContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorHandlingMiddleware();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
