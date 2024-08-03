using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilters
{
    public class ValidationFilterAttribute : ActionFilterAttribute //Abstract bir clas
    {
        public override void OnActionExecuting(ActionExecutingContext context)    //Eylem çağrıldığında ve eylem yöntemi başlamadan önce işlemler gerçekleştirilir.
        {
            var controller = context.RouteData.Values["controller"];            //İlgili bağlam bilgilerini kullanarak, controller ve action değerleri RouteData üzerinden alınır.
                                                                                //Bu, hangi kontrolcü ve eylemin çalıştığını belirlemek için kullanılır.
            var action = context.RouteData.Values["action"];

            // Dto
            var param = context.ActionArguments                                   //Ardından, eyleme geçirilen parametreleri kontrol etmek için ActionArguments özelliği kullanılır.
                                                                                  //Burada, parametreler arasında "Dto" kelimesini içeren bir parametre aranır.
                .SingleOrDefault(p => p.Value.ToString().Contains("Dto")).Value;

            if (param is null)
            {
                context.Result = new BadRequestObjectResult($"Object is null. " + //Yanıtta, hata mesajı olarak "Object is null", kontrolcü adı ve eylem adı yer alır.
                    $"Controller : {controller} " +
                    $"Action :  {action}");
                return; // 400 
            }

            if (!context.ModelState.IsValid)                                          //doğrulama işleminin başarısız olduğu durumu belirlemek için kullanılır.
                                                                                      //Dto gereklilikler,ne uymuyorsa eğer.
                context.Result = new UnprocessableEntityObjectResult(context.ModelState); // 422 
        }
    }
}
