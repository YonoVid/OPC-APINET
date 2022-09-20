//1.- Using to work with EntityFramework
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversityApiBackend;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Services;

var builder = WebApplication.CreateBuilder(args);

//2.- Connection with SQL Express
const string CONNECTIONNAME = "UniversityDb";

var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

//3.- Add context
builder.Services.AddDbContext<UniversityDbContext>(options => options.UseSqlServer(connectionString));

//7.- Add service of JWT Authorization
builder.Services.AddJwtTokenService(builder.Configuration);

// Add services to the container.
//A.- Localization
//Set folder of localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


builder.Services.AddControllers();

//4.- Add custom services
builder.Services.AddScoped<IStudentsService, StudentsService>();
builder.Services.AddScoped<ICoursesService, CoursesService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IChaptersService, ChaptersService>();
//TODO


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//8.- Add authorization
builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
    });

//9.- Config Swagger to take care of Authorization of JWt

builder.Services.AddSwaggerGen(options =>
    {
        //Defining security for authorization
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using Bearer Scheme"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new String[]{}
                }     
            });


    });

//5.- CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

var app = builder.Build();


//B.- Supported cultures
var supportedCultures = builder.Configuration.GetSection("AllowedLenguages")?.GetChildren()?.Select(x => x.Value)?.ToArray();
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

//3.- Add localization to app
app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//6.- Tell app to use a CORS
app.UseCors("CorsPolicy");

app.Run();
