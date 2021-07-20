using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThinkBridgeSolu.Models.DBModels;
using ThinkBridgeSolu.Models.CodeModels;

namespace ThinkBridgeSolu.BLL
{
    public class ProductsBLL
    {
        private sakilaContext dbContext = new sakilaContext();

        private bool CheckIsCategoryIdPresent(int categoryId)
        {
            ProductCategory catDetails = dbContext.ProductCategories.AsQueryable().Where(cat => cat.CategoryId == categoryId).FirstOrDefault();
            if(catDetails == null)
            {
                return false;
            }
            return true;
        }
        private void UpdateProdItem(ref Product source, ref Product dest)
        {
            dest.UpdatedAt = DateTime.UtcNow;
            if (!String.IsNullOrEmpty(source.ProductName))
            {
                dest.ProductName = source.ProductName;
            }
            if (!String.IsNullOrEmpty(source.ProductDescription))
            {
                dest.ProductDescription = source.ProductDescription;
            }
            if (!String.IsNullOrEmpty(source.PriceCurrency))
            {
                dest.PriceCurrency = source.PriceCurrency;
            }
            if (source.ProductPrice.HasValue)
            {
                dest.ProductPrice = source.ProductPrice;
            }
            if(source.CategoryId > 0)
            {   
                if(CheckIsCategoryIdPresent(source.CategoryId))
                {
                    dest.CategoryId = source.CategoryId;
                }
                else
                {
                    dest.CategoryId = Globals.DefaultProductCategoryId;
                }
            }
        }
        public BaseResponse GetProductList()
        {
            BaseResponse resp = new BaseResponse(ResponseStatus.Fail);
            try
            {
                var productsQuery = dbContext.Products.AsQueryable().Where(item => item.ProductId > 0);
                List<Product> allProds = productsQuery.Where(item => true).ToList();
                resp.Data = new
                {
                    Products = allProds
                };
                resp.Message = "All went fine";
                resp.Status = ResponseStatus.Success;
            }
            catch(Exception ex)
            {
                resp.Message = ex.Message;
                resp.Status = ResponseStatus.Error;
            }
            return resp;
        }
        public BaseResponse GetProductById(int itemId)
        {
            BaseResponse resp = new BaseResponse(ResponseStatus.Fail);
            if(itemId <= 0)
            {
                resp.Status = ResponseStatus.Error;
                resp.ErrorCode = ErrorCode.ITEM_DOES_NOT_EXISTS;
                return resp;
            }
            try
            {
                Product thisProd = dbContext.Products.AsQueryable().Where(item => item.ProductId == itemId).FirstOrDefault();
                if(thisProd == null)
                {
                    resp.Status = ResponseStatus.Error;
                    resp.ErrorCode = ErrorCode.ITEM_DOES_NOT_EXISTS;
                    return resp;
                }
                resp.Data = new
                {
                    Product = thisProd
                };
                resp.Message = "All went fine";
                resp.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.Status = ResponseStatus.Error;
            }
            return resp;
        }
        public BaseResponse UpdateProduct(Product newDetails)
        {
            BaseResponse resp = new BaseResponse(ResponseStatus.Fail);
            if(newDetails.ProductId <= 0)
            {
                resp.Status = ResponseStatus.Error;
                resp.ErrorCode = ErrorCode.ITEM_DOES_NOT_EXISTS;
                return resp;
            }
            try
            {
                Product item = dbContext.Products.AsQueryable().Where(item => item.ProductId == newDetails.ProductId).FirstOrDefault();
                if (item == null)
                {
                    resp.Status = ResponseStatus.Error;
                    resp.ErrorCode = ErrorCode.ITEM_DOES_NOT_EXISTS;
                    return resp;
                }
                UpdateProdItem(ref newDetails, ref item);
                dbContext.SaveChanges();
                resp.Status = ResponseStatus.Success;
            }
            catch(Exception ex)
            {
                resp.Message = ex.Message;
                resp.Status = ResponseStatus.Error;
            }
            return resp;
        }
        public BaseResponse DeleteProduct(int itemId)
        {
            BaseResponse resp = new BaseResponse(ResponseStatus.Fail);
            try
            {
                Product item = dbContext.Products.AsQueryable().Where(item => item.ProductId == itemId).FirstOrDefault();
                if (item != null)
                {
                    dbContext.Products.Remove(item);
                    dbContext.SaveChanges();
                }
                resp.Status = ResponseStatus.Success;
            }
            catch(Exception ex)
            {
                resp.Message = ex.Message;
                resp.Status = ResponseStatus.Error;
            }
            return resp;
        }
        public BaseResponse AddProduct(Product newDetails)
        {
            BaseResponse resp = new BaseResponse(ResponseStatus.Fail);
            if(newDetails == null)
            {
                resp.Message = "Nothing to add";
                resp.Status = ResponseStatus.Error;
                return resp;
            }
            if(String.IsNullOrEmpty(newDetails.ProductName))
            {
                resp.Message = "Name is mandatory to add a new item";
                resp.Status = ResponseStatus.Error;
                return resp;
            }
            try
            {
                newDetails.UpdatedAt = DateTime.UtcNow;
                if(newDetails.ProductPrice.HasValue)
                {
                    if(String.IsNullOrEmpty(newDetails.PriceCurrency))
                    {
                        newDetails.PriceCurrency = Globals.DefaultPriceCurrency;
                    }
                }
                dbContext.Products.Add(newDetails);
                dbContext.SaveChanges();
                resp.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.Status = ResponseStatus.Error;
            }
            return resp;
        }
    }
}
