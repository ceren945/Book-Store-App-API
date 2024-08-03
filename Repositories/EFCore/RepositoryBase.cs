using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
     where T : class


    {
        protected readonly RepositoryContext _context;
        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }


        public void Create(T entity) => _context.Set<T>().Add(entity);  //T entity ifadesi, generic bir parametre kullanarak herhangi bir türden (class) bir nesneyi temsil etmek için kullanılır.
                                                                        //Burada T yerine geçecek olan tür, RepositoryBase sınıfını miras alan sınıflar tarafından belirlenir.
                                                                        //_context.Set<Product>() ifadesiyle, _context üzerindeki varlık tablosunu temsil eden bir DbSet<T> nesnesi elde edilir. 

        public void Delete(T entity) => _context.Set<T>().Remove(entity);//Diğer fonksiyonlar (Create, Delete, Update) veritabanı üzerinde değişiklik yaparken void dönüş tipini kullanıyorlar,
                                                                         //çünkü bu fonksiyonlar veri ekleme, silme veya güncelleme gibi işlemleri gerçekleştiriyorlar,
                                                                         //ancak geriye bir sonuç döndürmüyorlar.
                                                                         //Bu nedenle, IQueryable<T> türünü kullanmak, sorgu yapılabilen ve
                                                                         //sonuç döndürebilen bir fonksiyon için uygun bir seçimdir,
                                                                         //ancak veritabanı üzerinde değişiklik yapan fonksiyonlar için gerekli değildir.

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?                                               //trackChanges değerinin tersi eğer true ise ğişiklikleri izlemeyen bir sorgu yapısı döndürür.    
            _context.Set<T>().AsNoTracking() :                            //False ise  Bu ifade, _context üzerindeki T türündeki veritabanı tablosunu temsil eden bir DbSet nesnesini döndürür.
                                                                          //Bu durumda, değişiklikler takip edilecektir.
            _context.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
            bool trackChanges) =>
            !trackChanges ?
            _context.Set<T>().Where(expression).AsNoTracking() :
            _context.Set<T>().Where(expression);
        public void Update(T entity) => _context.Set<T>().Update(entity);
    }
}
///RepositoryBase<T> sınıfı abstract bir sınıf olduğu için doğrudan örneklenemez,başka sınıflar tarafından türetilerek kullanılır.
//Türetilen sınıflar, RepositoryBase<T> sınıfının özelliklerini ve işlevlerini devralır.
////RepositoryContext, veritabanı bağlantısını ve Entity Framework Core özelliklerini sağlayan bir sınıftır.
//RepositoryBase<T> sınıfı, veritabanı işlemlerini gerçekleştirmek için RepositoryContext nesnesine ihtiyaç duyar.
//Bu nesneyi her seferinde yeniden oluşturmak yerine, dışarıdan enjekte edilerek kullanılması daha esnek bir yapı sağlar.
//Bu nedenle, RepositoryBase<T> sınıfının bir kurucu metodu tanımlanır ve RepositoryContext tipinden bir bağımlılık enjekte edilir.
//Enjeksiyonun temel mantığı, RepositoryBase<T> sınıfının RepositoryContext nesnesini kendisi oluşturmaması,
//RepositoryContext tipinden bir bağımlılık enjekte eder ve _context alanına atar.
//_context alanı, RepositoryBase sınıfının türetilen sınıflarda veritabanı işlemleri yapabilmesini sağlar.S
//protected ve readonly anahtar kelimelerinin kullanımı, _context alanının sadece tanımlandığı sınıf ve
//türetilen sınıflar tarafından erişilebilir ve değeri sadece bir kez atanabilir hale getirir.
//Bu, _context'in güvenli bir şekilde kullanılmasını ve beklenmedik durumların önüne geçilmesini sağlar.




