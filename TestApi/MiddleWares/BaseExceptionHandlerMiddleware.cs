using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestApi.MiddleWares
{
    public class BaseExceptionHandlerMiddleware : IMiddleware
    {
        /// <summary>
        /// Represents the delegate responsible for the request.
        /// </summary>
     

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception)
            {
                //check whether we've started the response yet
                if (!context.Response.HasStarted)
                {
                    //since we haven't, create the response model
                    var model = new ResultModel<object>();
                    var response = new ObjectResult(model)
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    await context.Response.WriteAsJsonAsync(response).ConfigureAwait(false);

                    //send the 500 error back as our model
                    //var routeData = context.GetRouteData();
                    //var actionDescriptor = new ActionDescriptor();
                    //var actionContext = new ActionContext(context, routeData, actionDescriptor);
                    //await response.ExecuteResultAsync(actionContext);
                    //return;
                }
                else
                    throw;
            }
            }
    }
}
