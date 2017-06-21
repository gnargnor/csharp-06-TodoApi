using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Models;

namespace TodoApi
{
    public class Startup
    {   
        //configures services - this is the services controller. uses services as its dot notation namespace - kind of like controller as
        public void ConfigureServices(IServiceCollection services)
        {
            //injects in-memory database use into the service controller
            services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase());
            services.AddMvc();
        }
        //configures the app - uses app as the dot notation namespace - kind of like controller as
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
