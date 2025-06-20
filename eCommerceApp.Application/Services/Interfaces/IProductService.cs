using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.Application.DTOs.Product;

namespace eCommerceApp.Application.Services.Interfaces {
    public interface IProductService {
        Task<IEnumerable<CreateProduct>> GetAllAsync();
        //Task<TEntity?> GetByIdAsync(Guid id);
        //Task<int> AddAsync(TEntity entity);
        //Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(Guid id);

    }
}
