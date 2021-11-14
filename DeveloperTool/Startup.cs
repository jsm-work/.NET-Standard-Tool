using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DeveloperTool
{
    public class Startup
    {
        #region CORS
        readonly string MyAllowSpecificOrigins = "CORS";
        #endregion

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region IP
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region FormatFilter (XML / JSON)
            services.AddMvc(options => { options.RespectBrowserAcceptHeader = true; })
                                    .AddXmlSerializerFormatters()
                                    .AddXmlDataContractSeria‌​lizerFormatters();
            #endregion

            #region Create Swagger Document
            //스웨거 문서정보 생성.
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1"
                    , new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "DeveloperTool API",
                        Description = "\'.NET-Standard-Tool\'DLL에서 제공하는 기능의 일부를 API 제공",
                        Version = "V1",
                        License = new Microsoft.OpenApi.Models.OpenApiLicense() {Name = "\'.NET-Standard-Tool\' GitHub", Url = new Uri("https://github.com/jeseokMun/.NET-Standard-Tool") }
                        });
                //애플리케이션의 기본 경로
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                //xml 경로
                o.IncludeXmlComments(xmlPath);
            });
            #endregion

            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins()
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            #endregion
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region Swagger UI
            //스웨거 미들웨어 설정
            app.UseSwagger();

            //스웨거 UI 활성화
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

                //접속 경로 / 미입력 시 swagger
                //c.RoutePrefix = "swagger";
            });
            #endregion

            #region CORS
            app.UseCors(MyAllowSpecificOrigins);
            #endregion
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
