using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WebUI.Abstract;
using WebUI.Concrete;

namespace WebUI {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services) {
            services.AddMvc();
            services.AddSingleton<IArchiveManager, ArchiveManager>();
            services.AddSingleton<IFoldersService, FoldersService>();

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseMvc(route => {
                route.MapRoute(
                    "",
                    "{id}",
                    new {
                        Controller = "Folders",
                        Action = "ViewFolder"
                    });

                route.MapRoute(
                    "DownloadFolder",
                    "{id}/Download",
                    new {
                        Controller = "Folders",
                        Action = "Download"
                    });

                route.MapRoute(
                    "",
                    "UploadFile",
                    new {
                        Controller = "Folders",
                        Action = "UploadFile"
                    });

                route.MapRoute(
                    "",
                    "",
                    new {
                        Controller = "Folders",
                        Action = "Upload"
                    });

            });
        }
    }
}
