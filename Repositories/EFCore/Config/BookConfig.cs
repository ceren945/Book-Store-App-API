using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
 
        public class BookConfig : IEntityTypeConfiguration<Book>
        {   // IEntityTypeConfiguration<Book> , bir varlık sınıfının (örneğin, Book) nasıl yapılandırılacağını tanımlayan bir arayüzdür.
            // Bu arayüzü uygulayarak, veritabanı tablosunun adını, sütunlarını, anahtarlarını ve diğer yapılandırma ayarlarını belirleyebilirsiniz.
            // BookConfig sınıfı, IEntityTypeConfiguration<Book> arayüzünü uygulayan bir sınıftır.
            // Bu sınıf, Book varlık sınıfının yapılandırmasını sağlar
            public void Configure(EntityTypeBuilder<Book> builder) //varlık sınıfının (Book) yapılandırmasını gerçekleştirmek için kullanılan bir yapılandırma sınıfıdır.
                                                                   //Bu sınıfın HasData metodu, Book tablosuna başlangıç verilerini eklemek için kullanılır.
            {

                builder.HasData(
                    new Book() { Id = 1, Title = "Nutuk", Price = 75 },
                    new Book() { Id = 2, Title = "Savaş ve Barış", Price = 85 },
                    new Book() { Id = 3, Title = "Serenad", Price = 95 });
            }
        }
    }

