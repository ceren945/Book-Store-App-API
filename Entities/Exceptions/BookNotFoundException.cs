using System.Reflection.Metadata;

namespace Entities.Exceptions
{
    public sealed class BookNotFoundException : NotFoundExcepiton
    {
        public BookNotFoundException(int id) : base($"The book with id :{id} could not found.")
        {

        }

        //Bu kod parçası ise, BookNotFound adlı bir alt sınıfı tanımlar.
        //Bu sınıf, NotFound sınıfından türetilir ve kitap bulunamaması durumunda fırlatılan özel bir hata türünü temsil eder.
        //BookNotFound sınıfı sealed anahtar kelimesiyle işaretlenmiştir, bu da başka bir sınıfın bundan türetilmesini engeller.

        //BookNotFound sınıfı, int id parametresini alır ve bu parametre kullanılarak hata mesajı oluşturulur.
        //Örneğin, BookNotFound hatası bir kitabın belirli bir kimliğiyle bulunamaması durumunda oluşabilir.Hata mesajı, kitabın kimliğini içeren bir mesajdır.
    }
}
