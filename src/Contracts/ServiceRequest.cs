using EventManagementSystem.Domain.ServiceEntity.ValueObjects;

namespace EventManagementSystem.Contracts
{
    public class ServiceRequest
    {
        public ServiceType ServiceType { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitCost { get; set; }
    }
}
