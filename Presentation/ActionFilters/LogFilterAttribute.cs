using Entities.LogModel;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Services.Contracts;

namespace Presentation.ActionFilters
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        private readonly ILoggerService _logger;

        public LogFilterAttribute(ILoggerService logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInfo(Log("OnActionExecuting", context.RouteData));  // ifadesi kullanılarak loglama işlemi gerçekleştirilir. 
        }

        private string Log(string modelName, RouteData routeData) //Log metodu, modelName ve routeData parametrelerini alır. Bu metot, LogDetails sınıfından bir örneği oluşturur ve bu örneği doldurur.
                                                                  //ModelName özelliği modelName değerini alırken, Controller ve Action özellikleri routeData.Values üzerinden alınır.
                                                                  //Ayrıca, routeData.Values koleksiyonunda en az 3 öğe varsa, Id özelliği de doldurulu
        {
            var logDetails = new LogDetails()
            {
                ModelName = modelName,
                Controller = routeData.Values["controller"],
                Action = routeData.Values["Id"]
            };

            if (routeData.Values.Count >= 3)
                logDetails.Id = routeData.Values["Id"];

            return logDetails.ToString();
        }
    }
}