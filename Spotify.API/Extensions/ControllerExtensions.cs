using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Spotify.DTO.Response;
using System.Net;
using Spotify.Application.Exceptions.Auth;

namespace Spotify.API.Extensions
{
    /// <summary>
    /// Static class containing contoller extension methods used for returning custom Action Method responses
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Method used for returning a success response from controller Action Methods
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ObjectResult SuccessResponse<T>(this ControllerBase controller, T data)
        {
            return new ObjectResult(new ApiResponse<T>()
            {
                Message = "success",
                StatusCode = (int)HttpStatusCode.OK,
                Response = data
            });
        }

        /// <summary>
        /// Method used for returning a Not Found response from controller Action Methods
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static NotFoundObjectResult NotFoundResponse(this ControllerBase controller, string message = "Not Found")
        {
            return new NotFoundObjectResult(new ApiResponse<string>()
            {
                Message = "not_found",
                StatusCode = (int)HttpStatusCode.NotFound,
                Response = message
            });
        }

        /// <summary>
        /// Method used for returning a fail Unauthorized from controller Action Methods
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static UnauthorizedObjectResult UnauthorizedResponse(this ControllerBase controller, string response = "Unauthorized")
        {
            return new UnauthorizedObjectResult(new ApiResponse<string>()
            {
                Message = "unauthorized",
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Response = response
            });
        }

        /// <summary>
        /// Method used for returning a fail response from controller Action Methods
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static BadRequestObjectResult FailResponse(this ControllerBase controller, string response = "Bad Request")
        {
            return new BadRequestObjectResult(new ApiResponse<string>()
            {
                Message = "fail",
                StatusCode = (int)HttpStatusCode.BadRequest,
                Response = response
            });
        }

        /// <summary>
        /// Method used for returning a fail response from controller Action Methods
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ObjectResult FailResponse(this ControllerBase controller, Exception ex)
        {
            if (ex is AccessDeniedException)
            {
                return new UnauthorizedObjectResult(new ApiResponse<string>()
                {
                    Message = "forbidden",
                    StatusCode = (int)HttpStatusCode.Forbidden,
                    Response = ex.Message
                });
            }
            else
            {
                return new BadRequestObjectResult(new ApiResponse<string>()
                {
                    Message = "fail",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Response = ex.Message
                });
            }
        }

        /// <summary>
        /// Method used for returning a fail response from controller Action Methods
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static BadRequestObjectResult FailResponse(this ControllerBase controller, string message, string response = "Bad Request")
        {
            return new BadRequestObjectResult(new ApiResponse<string>()
            {
                Message = message,
                StatusCode = (int)HttpStatusCode.BadRequest,
                Response = response
            });
        }

        /// <summary>
        /// Method used for returning a validation error response from controller Action Methods
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static BadRequestObjectResult ValidationErrrorResponse(ModelStateDictionary modelState)
        {
            return new BadRequestObjectResult(new ApiResponse<string>()
            {
                Message = "validation_error",
                StatusCode = (int)HttpStatusCode.BadRequest,
                Response = modelState.Values.First().Errors.First().ErrorMessage
            });
        }

        /// <summary>
        /// Method used for returning an server error response from controller Action Methods
        /// Used in the ErrorHandlingMiddleware
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResponse<string> InternalServerErrorResponse(string message)
        {
            return new ApiResponse<string>()
            {
                Message = "server_error",
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Response = message
            };
        }
    }
}
