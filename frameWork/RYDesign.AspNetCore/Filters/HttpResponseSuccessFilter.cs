﻿using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RYDesignAspNetCore.Filter
{
    public class HttpResponseSuccessFilter : IAsyncActionFilter
    {

        private Stopwatch _stopwatch;

        public HttpResponseSuccessFilter()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ActionExecutedContext executedContext = await next.Invoke();

            if (executedContext.Result as FileContentResult != null)
            {
                executedContext.Result = executedContext.Result as FileContentResult;
                return;
            }
            var objectResult = executedContext.Result as ObjectResult;
            _stopwatch.Stop();
           
            if (objectResult != null && objectResult.Value != null)
            {
                executedContext.Result = new ObjectResult(new HttpResponseSuccess(HttpStatusCode.OK, objectResult.Value, "请求成功！", DateTime.Now, _stopwatch.ElapsedMilliseconds));
            }
            else
            {
                executedContext.Result = new ObjectResult(new HttpResponseSuccess(HttpStatusCode.NotFound, null, "未找到资源!", DateTime.Now, _stopwatch.ElapsedMilliseconds));
            }

        }


        #region 记录日志数据库

        //private readonly Serilog.ILogger _logger;
        //private Stopwatch _stopwatch;
        //public HttpResponseSuccessFilter(Serilog.ILogger logger)
        //{
        //    _logger = logger;
        //    _stopwatch = Stopwatch.StartNew();
        //}
        //public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    var request = context.HttpContext.Request;
        //    string method = request.Method;
        //    string requestQueryString = "";

        //    //获取post或者get的请求参数
        //    if (method == "POST")
        //        requestQueryString = JsonConvert.SerializeObject(context.ActionArguments.SingleOrDefault().Value);
        //    else
        //        requestQueryString = request.QueryString.ToString();

        //    ActionExecutedContext executedContext = await next.Invoke();

        //    if (executedContext.Result as FileContentResult != null)
        //    {
        //        executedContext.Result = executedContext.Result as FileContentResult;
        //        return;
        //    }
        //    var objectResult = executedContext.Result as ObjectResult;
        //    _stopwatch.Stop();
        //    int statusCode = 0;
        //    object data = null;
        //    if (objectResult != null && objectResult.Value != null)
        //    {
        //        statusCode = 200;
        //        data = objectResult.Value;
        //        executedContext.Result = new ObjectResult(new HttpResponseSuccess(HttpStatusCode.OK, objectResult.Value, "请求成功！", DateTime.Now, _stopwatch.ElapsedMilliseconds));
        //    }
        //    else
        //    {
        //        statusCode = 400;
        //        executedContext.Result = new ObjectResult(new HttpResponseSuccess(HttpStatusCode.NotFound, null, "未找到资源!", DateTime.Now, _stopwatch.ElapsedMilliseconds));
        //    }

        //    var response = executedContext.HttpContext.Response;
        //    string ExceptionString = "";
        //    if (executedContext.Exception != null)
        //    {
        //        statusCode = 500;
        //        ExceptionString = executedContext.Exception.Message;
        //    }
        //    //记录数据库日志中
        //    _logger
        //        .ForContext("Url", request.Path)//记录请求地址
        //        .ForContext("HttpMethod", method)// 记录请求方式
        //        .ForContext("RequestJson", requestQueryString)//请求字符串
        //        .ForContext("HttpStatusCode", statusCode)//状态码
        //        .ForContext("ExceptionMessage", ExceptionString)
        //        .ForContext("TotalMilliseconds", (_stopwatch.ElapsedMilliseconds / 1000d) + "s")
        //        .ForContext("ResponseJson", JsonConvert.SerializeObject(data))
        //        .Information("Request", request.Query); ;//响应数据json
        //}

        #endregion

    }

    /// <summary>
    /// 成功：返回数据格式
    /// </summary>
    /// <param name="code"></param>
    /// <param name="data"></param>
    /// <param name="msg"></param>
    /// <param name="ServiceTime"></param>
    /// <param name="TimeOut"></param>
    public record HttpResponseSuccess(HttpStatusCode code, object data, string msg, DateTime ServiceTime, long TimeOut);
}
