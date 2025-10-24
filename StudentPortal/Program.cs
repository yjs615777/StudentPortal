using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Mappings;
using StudentPortal.Middleware;
using StudentPortal.Repositories;
using StudentPortal.Services;
using StudentPortal.Validations;
using FluentValidation.AspNetCore;
using FluentValidation;
using AutoMapper;

namespace StudentPortal
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SchoolContext>(opt =>
            opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));


            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<StudentProfile>();   // �� ������
            });



            // FluentValidation (�ڵ� �� ����)
            builder.Services.AddControllers();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<StudentCreateRequestValidator>();

            // DI
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IStudentService, StudentService>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // ���� ���� �̵����
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Student}/{action=Index}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SchoolContext>();
                Console.WriteLine($"DB���� ���� ����: {db.Database.CanConnect()}");
                db.Database.EnsureCreated();
            }

            app.Run();
        }
    }
}
