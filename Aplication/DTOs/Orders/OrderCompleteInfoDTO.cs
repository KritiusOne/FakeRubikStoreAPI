using Aplication.DTOs.Users;
using Aplication.Entities;

namespace Aplication.DTOs.Orders
{
    public class OrderCompleteInfoDTO : OrderBasicDTO
    {

        public DeliveryBasicInfoDTO DeliveryInfo { get; set; } = null!;

        public UserMinimalDTO UserInfo { get; set; } = null!;

        public ICollection<OrderProductCompleteInfoDTO> OrderProducts { get; set; } = new List<OrderProductCompleteInfoDTO>();
    }
}
