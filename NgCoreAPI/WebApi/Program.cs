using Application.Queries.DepartmentQueries;
using Infrastructure.DependencyContainers;
using Infrastructure.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebApi.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

#region Middleware Registration
builder.Services.AddControllers();
#endregion

#region db context registration
builder.Services.AddDbContext<NgCoreAppContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnectionString")));
#endregion

#region swagger config
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NGCOREAPP", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
});
#endregion

#region CORS and HTTP Accessor registration
builder.Services.AddCors(options => options.AddPolicy("AppCORSPolicy", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped(typeof(AzureADHelper));
#endregion

#region MediatR registration
builder.Services.AddMediatR(typeof(GetAllDepartmentQueryHandler).GetTypeInfo().Assembly);
#endregion

#region dependency container registration
ContextDependencyContainer.RegisterServices(builder.Services);
RepositoryDependencyContainer.RegisterServices(builder.Services);
#endregion

#region app build
var app = builder.Build();
#endregion


app.UseHttpsRedirection();

#region swagger

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NGCOREAPP");
});
app.UseDeveloperExceptionPage();

#endregion

app.MapControllers();
app.UseCors("AppCORSPolicy");
app.UseHttpsRedirection();
app.UseAntiXssMiddleware();

app.Run();















































//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
