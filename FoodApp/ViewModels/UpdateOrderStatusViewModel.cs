using FoodApp.Data.Entities;

namespace FoodApp.ViewModels;

public record UpdateOrderStatusViewModel(int OrderId, OrderStatus NewStatus);
