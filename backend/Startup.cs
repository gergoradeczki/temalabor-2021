﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using temalabor_2021_todo_backend.Data;
using temalabor_2021_todo_backend.DAL;

namespace temalabor_2021_todo_backend
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IColumnRepository, ColumnRepository>();
            services.AddScoped<ITodoRepository, TodoRepository>();

            services.AddDbContext<AppDbContext>
                (options => options.UseSqlServer(TestConn.SqlConnectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddCors(opt =>
            {
                opt.AddPolicy("policy", builder =>
                {
                    builder.WithOrigins("http://localhost:3000",
                                        "http://192.168.0.52:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}