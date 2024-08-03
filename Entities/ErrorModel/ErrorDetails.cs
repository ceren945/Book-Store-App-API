using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entities.ErrorModel
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this); //ErrorDetails sınıfı ayrıca ToString() yöntemini geçersiz kılar.
                                                   //Bu yöntem, ErrorDetails nesnesini JSON formatına dönüştürür.
                                                   //Bunun için System.Text.Json adlı bir sınıf kullanılır. Bu dönüşüm sayesinde,
                                                   //ErrorDetails nesnesi kolayca bir JSON dizesine dönüştürülebilir ve
                                                   //bu dize örneğin bir HTTP yanıtında döndürülebilir veya kaydedilebilir.
        }
    }
}
