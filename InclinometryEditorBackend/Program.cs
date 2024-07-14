using InclinometryEditorBackend;
using InclinometryEditorBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<InclinometryDBContext>();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication()
                .AddJwtBearer(x =>
                    {
                        x.Authority = "http://localhost:8080/realms/InclinometryAuth";
                        x.RequireHttpsMetadata = false;
                        x.MetadataAddress = $"http://localhost:8080/realms/InclinometryAuth/.well-known/openid-configuration";
                        //x.TokenValidationParameters = new TokenValidationParameters{
                        //    NameClaimType = $"{configuration["Keycloak:name_claim"]}",
                        //}

                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = "http://localhost:8080/realms/InclinometryAuth",
                            ValidateAudience = false,
                            ValidAudience = "inclinometryapp-client",
                            ValidateLifetime = true,
                            RequireExpirationTime = true,
                            ClockSkew = TimeSpan.Zero,
                        };

                        x.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                            $"{x.Authority}/.well-known/openid-configuration",
                        new OpenIdConnectConfigurationRetriever(),
                        new HttpDocumentRetriever { RequireHttps = false }
    );
                    }
);

builder.Services.AddAuthorization(o =>
{
    o.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

//builder.Services.AddCors(x =>
//{
//    x.AddPolicy(MyAllowSpecificOrigins,
//                         policy =>
//                         {
//                             policy.WithOrigins( "http://localhost:3000")
//                                                 .AllowAnyHeader()
//                                                 .AllowAnyMethod();
//                         });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "InclinometryEditor");
});

app.UseCors(x =>
{
    x.WithHeaders().AllowAnyHeader();
    x.WithOrigins("http://localhost:3000");
    x.WithMethods().AllowAnyMethod();
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();


//var db = new InclinometryDBContext();
//db.Database.EnsureDeleted();
//db.Database.EnsureCreated();

app.Run();
