using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using React.AspNet;

namespace tutorial_code_razor
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
			services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName)
			.AddChakraCore();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddReact();

            services.AddSession();
            services.AddMemoryCache();
            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

			app.UseReact(config =>
			{
				// If you want to use server-side rendering of React components,
				// add all the necessary JavaScript files here. This includes
				// your components as well as all of their dependencies.
				// See http://reactjs.net/ for more information. Example:
				config
				  .AddScript("~/js/remarkable.min.js")
				  .AddScript("~/js/tutorial.jsx")
				  .SetJsonSerializerSettings(new JsonSerializerSettings
				  {
					  StringEscapeHandling = StringEscapeHandling.EscapeHtml,
					  ContractResolver = new CamelCasePropertyNamesContractResolver()
				  });

				// If you use an external build too (for example, Babel, Webpack,
				// Browserify or Gulp), you can improve performance by disabling
				// ReactJS.NET's version of Babel and loading the pre-transpiled
				// scripts. Example:
				//config
				//  .SetLoadBabel(false)
				//  .AddScriptWithoutTransform("~/Scripts/bundle.server.js");
			});

			app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
