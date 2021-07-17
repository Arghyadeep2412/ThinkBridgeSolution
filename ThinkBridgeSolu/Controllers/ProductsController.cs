using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThinkBridgeSolu.BLL;
using ThinkBridgeSolu.Models.CodeModels;
using ThinkBridgeSolu.Models.DBModels;

namespace ThinkBridgeSolu.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : Controller
    {
        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<BaseResponse>> GetAllItems(int userId)
        {
            AuthenticateUser Authentication = new AuthenticateUser();
            if(!Authentication.CheckAdmin(userId))
            {
                BaseResponse resp = new BaseResponse(ResponseStatus.Fail);
                resp.Message = "User is not an admin";
                resp.ErrorCode = ErrorCode.USER_NOT_ADMIN;
                return resp;
            }
            Products x_Prods = new Products();
            return x_Prods.GetProductList();
        }

        [HttpGet]
        [Route("{userId}/{prodId}")]
        public async Task<ActionResult<BaseResponse>> GetProductById(int userId, int prodId)
        {
            AuthenticateUser Authentication = new AuthenticateUser();
            if (!Authentication.CheckAdmin(userId))
            {
                BaseResponse resp = new BaseResponse(ResponseStatus.Fail);
                resp.Message = "User is not an admin";
                resp.ErrorCode = ErrorCode.USER_NOT_ADMIN;
                return resp;
            }
            Products x_Prods = new Products();
            return x_Prods.GetProductById(prodId);
        }

        [HttpPut]
        [Route("{userId}")]
        public async Task<ActionResult<BaseResponse>> UpdateItem(int userId, [FromBody] Product newDetails)
        {
            AuthenticateUser Authentication = new AuthenticateUser();
            if (!Authentication.CheckAdmin(userId))
            {
                BaseResponse resp = new BaseResponse(ResponseStatus.Fail);
                resp.Message = "User is not an admin";
                resp.ErrorCode = ErrorCode.USER_NOT_ADMIN;
                return resp;
            }
            Products x_Prods = new Products();
            return x_Prods.UpdateProduct(newDetails);
        }

        [HttpDelete]
        [Route("{userId}/{prodId}")]
        public async Task<ActionResult<BaseResponse>> DeleteItem(int userId, int prodId)
        {
            AuthenticateUser Authentication = new AuthenticateUser();
            if (!Authentication.CheckAdmin(userId))
            {
                BaseResponse resp = new BaseResponse(ResponseStatus.Fail);
                resp.Message = "User is not an admin";
                resp.ErrorCode = ErrorCode.USER_NOT_ADMIN;
                return resp;
            }
            Products x_Prods = new Products();
            return x_Prods.DeleteProduct(prodId);
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task<ActionResult<BaseResponse>> AddNewItem(int userId, [FromBody] Product newDetails)
        {
            AuthenticateUser Authentication = new AuthenticateUser();
            if (!Authentication.CheckAdmin(userId))
            {
                BaseResponse resp = new BaseResponse(ResponseStatus.Fail);
                resp.Message = "User is not an admin";
                resp.ErrorCode = ErrorCode.USER_NOT_ADMIN;
                return resp;
            }
            Products x_Prods = new Products();
            return x_Prods.AddProduct(newDetails);
        }
    }
}
