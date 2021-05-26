using Infra.CommonMessages;
using Infra.Constants;
using Infra.Localizer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Register.API.ViewModels;
using Register.Business.Hub;
using Register.Business.Models;
using System;

namespace Register.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IBusinessManager _business;
        private readonly IAuthenticationManager _authentication;
        private readonly ILocalizer _localizer;
        private const string ClaimName = "Name";

        public RegisterController(IBusinessManager business, IAuthenticationManager authentication, ILocalizer localizer)
        {
            _business = business;
            _authentication = authentication;
            _localizer = localizer;
        }

        /// <summary>
        /// User logins the system. 
        /// </summary>
        /// <returns>BaseResponse</returns>
        [HttpPost("Login")]
        public BaseResponse Login(RegisterRequest register)
        {
            var model = register.Adapt<RegisterDTORequest>();
            var isSuccess = _business.Login(model);
            var response = isSuccess ? BaseResponse.Success : BaseResponse.Fail;

            var token = _authentication.GenerateToken(Convert.ToInt32(PrebuiltVariables.JwtAccessTokenExpireDurationAsMinutes),
                Convert.ToInt32(PrebuiltVariables.JwtRefreshTokenExpireDurationAsMinutes), (ClaimName, register.Name));

            response = TypeAdapter.Adapt(token, response);

            return response;
        }

        /// <summary>
        /// User registers the system. 
        /// </summary>
        /// <returns>BaseResponse</returns>
        [HttpPost("Register")]
        public BaseResponse Register(RegisterRequest register)
        {
            var model = register.Adapt<RegisterDTORequest>();
            var registerResponse = _business.Register(model);
            
            BaseResponse response;
            switch (registerResponse)
            {
                case RegisterDTOResponse.Success:
                    response = BaseResponse.Success;
                    break;
                case RegisterDTOResponse.AlreadyExists:
                    return BaseResponse.Create("User already exists");
                default:
                    return BaseResponse.Fail;
            }

            var token = _authentication.GenerateToken(Convert.ToInt32(PrebuiltVariables.JwtAccessTokenExpireDurationAsMinutes),
                Convert.ToInt32(PrebuiltVariables.JwtRefreshTokenExpireDurationAsMinutes), (ClaimName, register.Name));
            
            response = TypeAdapter.Adapt(token, response);

            return response;
        }

        /// <summary>
        /// When access token expired, client will call that service and that service will produce new access token 
        /// with refresh token sent by header. Refresh token and its expiration date will remain same.
        /// </summary>
        /// <returns>BaseResponse</returns>
        [HttpGet("RefreshToken")]
        public BaseResponse RefreshToken()
        {
            var refreshToken = HttpContext.Request.Headers["RefreshToken"];
            var token = _authentication.RefreshToken(Convert.ToInt32(PrebuiltVariables.JwtAccessTokenExpireDurationAsMinutes),
                Convert.ToInt32(PrebuiltVariables.JwtRefreshTokenExpireDurationAsMinutes), refreshToken);

            var response = BaseResponse.Success;

            response = TypeAdapter.Adapt(token, response);

            return response;
        }
    }
}
