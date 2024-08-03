using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public abstract class NotFoundExcepiton:Exception
    {

        protected NotFoundExcepiton(string message) : base(message)  //Bu kod parçası, NotFound adında soyut bir sınıf tanımlar.
                                                            //Bu sınıf, Exception sınıfından türetilir ve özel hata tipleri için bir temel sınıf olarak hizmet verir.
                                                            //NotFound sınıfı soyut olduğu için doğrudan örneklenemez,
                                                            //yalnızca türetilen alt sınıflar aracılığıyla kullanılabilir.
        {
            
        }

    }
}
