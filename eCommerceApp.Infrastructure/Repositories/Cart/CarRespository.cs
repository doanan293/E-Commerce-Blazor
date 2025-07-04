using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces.Cart;
using eCommerceApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Infrastructure.Repositories.Cart {
    public class CarRespository(AppDbContext context) : ICart {
        public async Task<int> SaveCheckoutHistory(IEnumerable<Achieve> checkouts) {
            return await context.SaveChangesAsync();
        }
    }
}
