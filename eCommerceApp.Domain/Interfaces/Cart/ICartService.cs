using eCommerceApp.Domain.Entities.Cart;

namespace eCommerceApp.Domain.Interfaces.Cart {
    public interface ICartService {
        Task<int> SaveCheckoutHistory(IEnumerable<CreateAchieve> checkouts);
    }
}
