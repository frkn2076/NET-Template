using Infra.CommonMessages;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Register.Business.Hub;
using Register.Business.Models;
using Register.Service.ViewModels;

namespace RegisterService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IBusinessManager _business;
        private readonly IAuthenticationManager _authentication;
        private const int _accessTokenExpireIn = 30;
        private const int _refreshTokenExpireIn = 300;
        private const string ClaimName = "Name";

        public RegisterController(IBusinessManager business, IAuthenticationManager authentication)
        {
            _business = business;
            _authentication = authentication;
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

            var token = _authentication.GenerateToken(_accessTokenExpireIn, _refreshTokenExpireIn, (ClaimName, register.Name));

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

            var token = _authentication.GenerateToken(_accessTokenExpireIn, _refreshTokenExpireIn, (ClaimName, register.Name));
            
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
            var token = _authentication.RefreshToken(_accessTokenExpireIn, _refreshTokenExpireIn, refreshToken);

            var response = BaseResponse.Success;

            response = TypeAdapter.Adapt(token, response);

            return response;
        }
    }
}
