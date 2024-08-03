using Entities.ErrorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Contracts;
using System.Net;
using System.Runtime.CompilerServices;

namespace WebApi.Extensions
{
    public static class ExceptionMiddlewearExtensions //ExceptionMiddlewearExtensions bir genişletme sınıfı 
    {
        public static void ConfigureExceptionHandler(this WebApplication app,  //ConfigureExceptionHandler adlı genişletme yöntemi, WebApplication nesnesine eklenir.
                                                                               //Bu yöntem, bir hata yönetimi ortamını yapılandırmak için kullanılır.
            ILoggerService logger)                                            
        {
            app.UseExceptionHandler(appErorr =>                                 //app.UseExceptionHandler yöntemini kullanarak bir hata işleyiciyi uygulamaya ekler.
                                                                                //Bu işleyici, istemci tarafında oluşan hataları ele alır ve uygun bir yanıt döndürür.
            {
                appErorr.Run(async context =>                                     //appErorr.Run yöntemi, bir HTTP isteği için hata işleyiciyi temsil eden bir RequestDelegate alır.
                                                                                  //Bu yöntem, hata işleme mantığını içeren bir fonksiyonu çalıştırır.
                {
                    
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();  //Bu özellik, hata bilgilerini elde etmek için kullanılır.

                    if (contextFeature != null)
                    {
                        context.Response.StatusCode= contextFeature.Error switch 
                        {
                            NotFoundExcepiton => StatusCodes.Status404NotFound, //Bizim belirlediğimiz hataysa bu kodla döner
                            _=>StatusCodes.Status500InternalServerError //Belirlediğimiz hatalardan değilse bu kodla döner
                        };


                        logger.LogError($"Something went wrong:{contextFeature.Error}");      //Bu, hataların bir günlüğe veya başka bir loglama mekanizmasına yazılmasını sağlar.
                        await context.Response.WriteAsync(new ErrorDetails()                  //Hata ayrıntılarını içeren bir ErrorDetails nesnesini JSON formatına dönüştürerek ve 
                                                                                              //HTTP yanıtına yazarak hatanın müşteriye döndürülmesini sağlar.
                                                                                              //İstemci tarafından oluşan hatalar yakalanır, hata ayrıntıları loglanır ve uygun bir hata yanıtı döndürülür.
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());                                                                    
                                                                                                        
                                                                                              
                        
                    }
                });

            });

        }
    }
}

