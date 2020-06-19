using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotesApi.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NotesApi.Database.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotesApi.Database.Interfaces;
using NotesApi.Models;
using NotesApi.Database.Repositories;
using NotesApi.Database.Infrastructer;
using NotesApi.Services;
using NotesApi.Response;
using NotesApi.Response.Result;
using NotesApi.Services.Interfaces;

namespace NotesApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //JWT
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            var appSettings = appSettingSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRepository<TaskNote>, TaskNoteRepository>();
            services.AddScoped<ITaskNoteService, TaskNoteService>();
            services.AddScoped<IRepository<Role>, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRepository<UserRole>, UserRoleRepository>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
           services.AddScoped<IAuthenticationService,AuthenticationService>();


            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("Notes");
            });
            services.AddCors();

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder =>
              builder.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();



        }
    }
}
