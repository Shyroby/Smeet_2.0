﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiDmS.DAL.Concrete;
using ApiDmS.DAL.IRepository;
using ApiDmS.Models.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApiDmS
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
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("defaultconnection")));
            services.AddMvc();

            // Register application services.
            services.AddScoped<IDocumentRepository, DocumentConcrete>();
            services.AddScoped<IaccessLevelAllowed, accessLevelAllowedConcrete>();
            services.AddScoped<ICategoryRepository, CategoryConcrete>();
            services.AddScoped<IFolderRepository, FolderConcrete>();
            services.AddScoped<IPriorityRepository, PriorityConcrete>();
            services.AddScoped<ITagRepository, TagConcrete>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
