using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleApp.DAL;
using ExampleApp.DAL.Core;
using ExampleApp.DAL.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace ExampleApp.API
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
      services.AddDbContext<ExampleAppDbContext>(options => options.UseInMemoryDatabase("ExampleAppDb"));
      services.AddOData();
      services.AddScoped<UnitOfWork>();

      services.AddMvc(op =>
      {
        foreach (var formatter in op.OutputFormatters
            .OfType<ODataOutputFormatter>()
            .Where(it => !it.SupportedMediaTypes.Any()))
        {
          formatter.SupportedMediaTypes.Add(
              new MediaTypeHeaderValue("application/prs.mock-odata"));
        }
        foreach (var formatter in op.InputFormatters
            .OfType<ODataInputFormatter>()
            .Where(it => !it.SupportedMediaTypes.Any()))
        {
          formatter.SupportedMediaTypes.Add(
              new MediaTypeHeaderValue("application/prs.mock-odata"));
        }
      })
    .AddJsonOptions(options =>
    {
      options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    })
    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info { Title = "ExampleApp API", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();

      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExampleApp API V1");
      });

      app.UseMvc(routeBuilder =>
      {
        routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
        routeBuilder.EnableDependencyInjection();
        routeBuilder.Count().Expand().Filter().MaxTop(100).OrderBy().Select();
      });
    }

    private static IEdmModel GetEdmModel()
    {
      var builder = new ODataConventionModelBuilder();
      builder.EntitySet<Device>("Device");
      builder.EntitySet<DeviceType>("DeviceTypes");
      builder.EntitySet<DeviceTypeProperty>("DeviceTypeProperties");
      builder.EntitySet<DevicePropertyValue>("DevicePropertyValues");
      return builder.GetEdmModel();
    }
  }
}
