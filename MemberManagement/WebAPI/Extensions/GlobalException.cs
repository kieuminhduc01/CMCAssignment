using WebAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Http.Features;

namespace WebAPI.Extensions
{
    public static class GlobalException
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(
                  appError =>
                  {
                      appError.Run(
                          async context =>
                          {
                              context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                              context.Response.ContentType = "application/json";

                              var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                              var contextRequest = context.Features.Get<IHttpRequestFeature>();

                              if (contextFeature != null)
                              {
                                  await context.Response.WriteAsync(new ErrorDetail()
                                  {
                                      StatusCode = context.Response.StatusCode,
                                      Message = contextFeature.Error.Message,
                                  }.ToString());
                              }
                          }
                      );
                  }
              );
        }
    }
}
