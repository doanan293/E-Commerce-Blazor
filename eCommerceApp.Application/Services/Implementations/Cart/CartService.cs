using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Domain.Interfaces.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Services.Implementations.Cart {
    public class CartService(ICart cartInterface, IMapper mapper, IGeneric<Product> productInterface, IPaymentMethodService paymentMethodService, IPaymentService paymentService) : ICartService {
        public async Task<ServiceResponse> Checkout(Checkout checkout) {
            var (products, totalAmount) = await GetCartTotalAmount(checkout.Carts);
            var paymentMethods = await paymentMethodService.GetPaymentMethods();
            if (checkout.PaymentMethodId == paymentMethods.FirstOrDefault()!.Id ) {
                return await paymentService.Pay(totalAmount, products, checkout.Carts);
            }else {
                return new ServiceResponse { Success = false, Message = "Invalid payment method" };
            }
        }

        public async Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves) {
            var mappedData = mapper.Map<IEnumerable<Achieve>>(achieves);
            var result = await cartInterface.SaveCheckoutHistory(mappedData);
            return result > 0
                ? new ServiceResponse { Success = true, Message = "Checkout achieved" }
                : new ServiceResponse { Success = false, Message = "Error occured in saving" };
        }
        private async Task<(IEnumerable<Product>, decimal)> GetCartTotalAmount(IEnumerable<ProcessCart> carts) {
            if (!carts.Any()) {
                return (new List<Product>(), 0);
            }

            var products = await productInterface.GetAllAsync();
            if (!products.Any()) {
                return (new List<Product>(), 0);
            }

            var cartProducts = carts
                .Select(cartItem => products.FirstOrDefault(p => p.Id == cartItem.ProductId))
                .Where(product => product != null)
                .ToList();

            var totalAmount = carts
                .Where(cartItem => cartProducts.Any(p => p.Id == cartItem.ProductId))
                .Sum(cartItem => cartItem.Quantity * cartProducts.First(p => p.Id == cartItem.ProductId)!.Price);

            return (cartProducts!, totalAmount);
        }
    }
}
