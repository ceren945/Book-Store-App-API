namespace Entities.DataTransferObjects
{
    /*[Serializable]*/
    public record BookDto
    {
        public int Id { get; init; }  //Bu sınıf dto sınıfı old için veri dönüşümü kolay olmaz
                                      //Bu şekilde satır satır tanımlamak daha derli toplu bir çıktı almamızı sağlar.
                                      
        public string Title { get; init; }                             
        public decimal Price { get; init; }
    }
}
