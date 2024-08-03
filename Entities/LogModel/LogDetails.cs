using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entities.LogModel
{
    public class LogDetails
    {
        public Object? ModelName { get; set; } //Object? ModelName ifadesindeki soru işareti, ModelName özelliğinin nullable bir Object türü olduğunu ifade eder.
                                               //Yani, ModelName özelliği null değer alabilir.
        public Object? Controller { get; set; }
        public Object? Action { get; set; }
        public Object? Id { get; set; }
        public Object? CreateAt { get; set; }

        public LogDetails()                  // Bu yapıcı metot, sınıf örneği oluşturulduğunda çalışır ve CreateAt özelliğini geçerli tarih ve saat ile ayarlar.
                                             // CreateAt özelliği, DateTime.UtcNow kullanılarak Coordinated Universal Time (UTC) olarak ayarlanır.
        {
            CreateAt = DateTime.UtcNow;
        }

        public override string ToString() =>
            JsonSerializer.Serialize(this);

    }

}
}
