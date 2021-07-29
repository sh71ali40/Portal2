using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Portal.DataLayer.Model;
using Portal.DataLayer.Model.Entities;

namespace Portal.Infrustructure.Service
{
    public interface IErrorLogService
    {
        int Add(Exception error);
        int Add(Exception exception, string userMessage);
    }

    public class ErrorLogService : IErrorLogService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<ErrorLog> _errorLogDbSet;
        private readonly IConfiguration _config;

        public ErrorLogService(IHttpContextAccessor httpContextAccessor , IUnitOfWork uow, IConfiguration config)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = uow;
            _errorLogDbSet = uow.Set<ErrorLog>();
            _config = config;
        }

        private int InsertToDb(Exception error, string userMessage = null)
        {

            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)
                ?.Value;
            var stackTrace = error.StackTrace;

            // var exceptionHandlerPathFeature = _httpContextAccessor.HttpContext.Features);//.Get<IExceptionHandlerPathFeature>();
            //var originalFeature = _httpContextAccessor.HttpContext.Features.Get<IStatusCodeReExecuteFeature>()?.Path;

            if (error.InnerException != null)
            {
                stackTrace += "--- End of stack trace from previous location where exception was thrown ---" +
                              error.InnerException;
            }

            var referer = _httpContextAccessor.HttpContext.Request.Headers.
                FirstOrDefault(x => x.Key.ToLower() == "referer").Value.FirstOrDefault();
            var requestPath = _httpContextAccessor.HttpContext.Request.Path;
            var errorUrl = requestPath == "/error" && referer!=null ? referer : requestPath.ToString();

            var errorObj = new ErrorLog
            {
                LogDateTime = DateTime.Now,
                Message = error.Message,
                Exception = stackTrace,
                Url = errorUrl,
                Ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                UserId = userId != null ? int.Parse(userId) : (int?)null,
                UserMessage = userMessage

            };
 

            _errorLogDbSet.Add(errorObj);
            _unitOfWork.SaveAllChanges();
            return errorObj.EventId;

        }



        /// <summary>
        /// Add Exceptions with user message to ErrorLog Table
        /// </summary>
        /// <param name="exception">thrown exception</param>
        /// <param name="customMessage">programmer custom message</param>
        /// <returns>EventId</returns>
        public int Add(Exception exception, string customMessage)
        {
            try
            {
               var eventId = InsertToDb(exception, customMessage);
               return eventId;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Add Exceptions to ErrorLog Table
        /// </summary>
        /// <param name="exception">thrown exception</param>
        /// <returns>EventId</returns>
        public int Add(Exception exception)
        {
            try
            {
                var eventId = InsertToDb(exception);
                return eventId;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

