using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Shuddhi;
using Shuddhi.Repository;
using Shuddhi.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Services
string connStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IVendor, VendorService>();
builder.Services.AddScoped<IMaster, MasterService>();
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<ILogin, LoginService>();
builder.Services.AddScoped<IPickupRequest, PikupRequest>();
builder.Services.AddScoped<IFAQRepository, FAQRepository>();
builder.Services.AddScoped<ITokenManager, TokenManagerService>();
builder.Services.AddScoped<IDashboard, Dashboard_Service>();
builder.Services.AddScoped<IOTPService, OTPService>();
builder.Services.AddScoped<ISocietyService, Society>();
builder.Services.AddHttpContextAccessor();

// Model validation error response
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage)
            .ToList();

        return new BadRequestObjectResult(new
        {
            Status = false,
            Message = errors.FirstOrDefault()
        });
    };
});

// IIS request limit: 100 MB
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 104857600;
});

// Multipart/form limit: 100 MB
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600;
});

// Kestrel request limit: 100 MB
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 104857600;
});

// JWT setup (existing configuration retained)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// CORS: allow the Angular development origins (scheme and port must match exactly)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "http://localhost:60655", "https://localhost:60655")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Explicit routing before CORS ensures the policy is applied to API endpoints
app.UseRouting();
app.UseCors("AllowAngular");

// Existing static-file paths retained
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "User_Documents")),
    RequestPath = "/User_Documents"
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Vendor_Documents")),
    RequestPath = "/Vendor_Documents"
});

// Enable the JWT middleware configured above.
// This makes [Authorize] attributes effective if any controllers use them.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
