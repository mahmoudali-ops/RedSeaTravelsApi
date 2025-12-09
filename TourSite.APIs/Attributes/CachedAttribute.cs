using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using TourSite.Core.Servicies.Contract;

namespace TourSite.APIs.Attributes
{
    public class CachedAttribute: Attribute, IAsyncActionFilter
    {
        private readonly int expireTime;

        public CachedAttribute(int expireTime)
        {
            this.expireTime = expireTime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cachedService= context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var GeneratedKey = GenerateRequestKey(context.HttpContext.Request);
            var CacheData = await cachedService.GetCacheKeyAsync(GeneratedKey);

            if (!string.IsNullOrEmpty(CacheData)) 
            {
              var contentResult =new ContentResult() 
              {
                  Content= CacheData,
                  ContentType="application/json",
                  StatusCode=StatusCodes.Status200OK
              };
                context.Result = contentResult;
                return;
            }
            
            var GettenDataAfterEndpoint= await next();

            if (GettenDataAfterEndpoint.Result is OkObjectResult response)
            {
                await cachedService.SetCacheKeyAsync(GeneratedKey, response.Value, TimeSpan.FromDays(expireTime));
            }
        }

        public string GenerateRequestKey(HttpRequest request)
        {
            var cachedKey = new StringBuilder();
            cachedKey.Append($"|{request.Path}");
            foreach (var (key,value) in request.Query.OrderBy(a=>a.Key)) 
            {
                cachedKey.Append($"|{key}-{value}");
            }

            return cachedKey.ToString();
        }
    
    }
}
