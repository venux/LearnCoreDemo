using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseKnowledgeDemo.DB;
using BaseKnowledgeDemo.DependencyInjectionDemo;
using BaseKnowledgeDemo.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BaseKnowledgeDemo
{
    /// <summary>
    /// Startup（约定命名） 类配置服务和应用的请求管道。
    /// 注入 IHostingEnvironment 的替代方法是使用基于约定的方法。
    /// 应用可以为不同的环境单独定义 Startup 类（例如，StartupDevelopment），相应 Startup 类会在运行时得到选择。
    /// 优先考虑名称后缀与当前环境相匹配的类。 
    /// 如果应用在开发环境中运行并包含 Startup 类和 StartupDevelopment 类，则使用 StartupDevelopment 类。
    /// </summary>
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            Env = env;
            LoggerFactory = loggerFactory;
        }

        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 环境
        /// </summary>
        public IHostingEnvironment Env { get; }

        /// <summary>
        /// 日志
        /// </summary>
        public ILoggerFactory LoggerFactory { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 配置应用的服务（可选）
        /// </summary>
        /// <param name="services"></param>
        /// <remarks>
        /// 在Configure方法配置应用服务之前，由Web主机调用。
        /// 典型：调用所有Add{Service}方法，而后调用services.Configure{Service}方法。
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //框架默认已注册了ILogger<TCategoryName>

            //注册自定义DI的Demo,服务生存周期为单个请求
            services.AddScoped<IMyDependency, MyDependency>();

            //使用Startup筛选器扩展
            services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 创建应用的请求处理管道，用于指定应用响应HTTP请求的方式（必须）
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var logger = LoggerFactory.CreateLogger<Startup>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDirectoryBrowser();

                logger.LogInformation("Environment：Develop");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();

                logger.LogInformation($"Environment：{Env.EnvironmentName}");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
