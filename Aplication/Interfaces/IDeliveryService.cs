namespace Aplication.Interfaces
{
    public interface IDeliveryService
    {
        Task UpdateState(int newState, int IdDelivery);
        bool existState(int possibleState);
    }
}
