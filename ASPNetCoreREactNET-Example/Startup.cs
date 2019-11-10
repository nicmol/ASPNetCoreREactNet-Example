using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using React.AspNet;


namespace ASPNetCoreREactNET_Example
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services) //changed from void to IServiceProvider
        {
            services.AddReact(); //added after installing React.AspNet NuGet package
            services.AddMvc(); //adds services Razor Pages
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//Registers IHttpContextAccessor for dependency Injection, 
                                                                               //Tells the MVC project to use React
            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Initalize ReactJS.NET. Must be before static files.
            app.UseReact(config =>
            {
                // If you want to use server-side rendering of React componenets,
                // add all the necessary JavaScript files here. this includes
                //your componenets as well as all of their dependencies
                //config
                //   .AddScript("~" / Source / Components / ExampleComponenet.jsx")
                //   .AddScript("~/Source/main.js");

                //If you use an external build too (for exampl, Babel,Webpack, Browserify, Gulp),
                //you can improve performance by disabling ReactJS.NET's version
                //of Babel and loading the pre-transpiled scripts

                //This is our set up for ASP.NET Core In older versions
                //you should initialize in App_Start/ReactConfig.
                config
                     .SetLoadBabel(true)
                     .AddScriptWithoutTransform("~/dist/bundle.js")
                     .AddScript("~/Source/Components/ExampleComponenet.jsx");
            });
            app.UseStaticFiles();

            //Added for routing configuration?
            app.UseMvc(routes =>
           {
               routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
           });

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
         
        }
    }
}
