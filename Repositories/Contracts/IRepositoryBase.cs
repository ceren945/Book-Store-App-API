using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{                                        //LINQ sorguları, IQueryable<T> arayüzü kullanılarak
                                         //veri kaynağı üzerinde filtreleme, sıralama, gruplama, birleştirme gibi çeşitli işlemleri gerçekleştirebilir.
    public interface IRepositoryBase<T> // tipik bir veritabanı işlemleri arabirimi olarak kullanılabilir. T'nin yerine bir varlık sınıfının türünü yerleştirerek,
                                        // o varlık türüyle ilgili temel veritabanı işlemlerini gerçekleştirebilirsiniz.
    {
                                        // CRUD  bir nesne koleksiyonunu işlemek için genel(Oluşturma, Okuma, Güncelleme, Silme) işlemlerini destekler.
        IQueryable<T> FindAll(bool trackChanges); //Bu yöntem, koleksiyonun tüm varlıklarını sorgulamak için kullanılır.
                                                  //trackChanges parametresi, varlıkların izlenip izlenmediğini belirtir.
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges); 
        //Bu yöntem, belirli bir koşula göre varlıkları sorgulamak için kullanılır. expression parametresi, varlık nesnelerindeki koşulu belirtir.
                               
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
