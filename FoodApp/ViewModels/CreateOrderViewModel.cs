using FoodApp.DTOs;

namespace FoodApp.ViewModels
{
    public class CreateOrderViewModel
    {
        public List<OrderItemViewModel> OrderItems { get; set; }
        public AddressViewModel ShippingAddress { get; set; }
    }

}
