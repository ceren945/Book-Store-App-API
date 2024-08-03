using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects

//Bu kod parçası, veri transferi sırasında kitap verilerinin doğrulama işlemlerini gerçekleştirmek için System.ComponentModel.
//DataAnnotations isim alanındaki doğrulama niteliklerini (attributes) kullanmaktadır.
//Bu nitelikler, veri alanlarının geçerlilik kontrollerini sağlamak ve hata mesajlarını belirlemek için kullanılmaktadır.
//Böylece, kitap verileri bu doğrulama kurallarına uymadığında hata mesajları döndürülebilir.
{
    public abstract record  BookDtoForManipulations
    {
        [Required(ErrorMessage ="Title is a required fieald")]
        [MinLength(2, ErrorMessage = "Title is must consist of at least two character")] 
        [MaxLength(50, ErrorMessage = "Title is must consist of at most fifty character")]
        public String Title { get; init; }


        [Required(ErrorMessage = "PRice is a required fieald")]
        [Range(10, 1000)]
        public decimal Price { get; init; }
    }
}
