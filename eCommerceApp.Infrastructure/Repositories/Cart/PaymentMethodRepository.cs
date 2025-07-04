using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces.Cart;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Infrastructure.Repositories.Cart {
    public class PaymentMethodRepository(AppDbContext context) : IPaymentMethod {
        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethods() {
            return await context.PaymentMethods.AsNoTracking().ToListAsync();
        }
    }
}
