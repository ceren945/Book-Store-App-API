using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {

        //Bu arayüz, servislerin yönetildiği bir yöneticinin (manager) özelliklerini içerir.
        //Diğer servis arayüzleri de benzer şekilde IServiceManager arayüzüne eklenerek,
        //tüm servislerin merkezi bir noktadan yönetilebileceği bir yapı oluşturulabilir
        IBookService BookService { get; }

        // IServiceManager arayüzü IBookService özelliği ile bir arayüz referansı sağlıyor,
        // ancak bu referansın somut bir uygulaması arayüz içinde bulunmuyor.
    }
}
