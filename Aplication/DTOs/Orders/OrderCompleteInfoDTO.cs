using Aplication.DTOs.Users;
using Aplication.Entities;

namespace Aplication.DTOs.Orders
{
    public class OrderCompleteInfoDTO : OrderBasicDTO
    {

        public DeliveryBasicInfoDTO DeliveryInfo { get; set; } = null!;

        public UserWithAddressDTO UserInfo { get; set; } = null!;

        public ICollection<OrderProductCompleteInfoDTO> OrderProducts { get; } = new List<OrderProductCompleteInfoDTO>();
    }
}
