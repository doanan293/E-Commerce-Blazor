using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Domain.Entities.Cart;

namespace eCommerceApp.Domain.Interfaces.Cart {
    public interface ICartService {
        Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves);
        Task<ServiceResponse> Checkout(Checkout checkouts);
    }
}
