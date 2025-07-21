using EcommerceTask.Application.DTOs;
using EcommerceTask.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EcommerceTask.Api.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
                throw;
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (exception is ValidationException)
            {
                var result = new JsonResult(new
                DefaultResponseModel(false, exception.GetBaseException().Message));
                result.StatusCode = StatusCodes.Status400BadRequest;

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(result.Value);
            }
            else if (exception is CustomException)
            {
                var result = new JsonResult(new
                DefaultResponseModel(false, exception.GetBaseException().Message));
                result.StatusCode = (int)HttpStatusCode.BadRequest;

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(result.Value);
            }
            else
            {
                var result = new JsonResult(new
                DefaultResponseModel(false, "An error occured kindly contact the administrator"));
                result.StatusCode = (int)HttpStatusCode.BadRequest;
                result.ContentType = "application/json";

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(result.Value);
            }
        }
    }
}
