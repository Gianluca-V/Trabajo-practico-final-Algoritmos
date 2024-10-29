using EventManagementSystem.Domain.ServiceEntity.Exceptions;
using EventManagementSystem.Domain.ServiceEntity.ValueObjects;
using EventManagementSystem.Domain.Shared.ValueObjects;

namespace EventManagementSystem.Domain.ServiceEntity
{
    public class Service
    {
        private ServiceType serviceType;
        private string description;
        private int quantity;
        private Money unitCost;

        public ServiceType ServiceType => serviceType;
        public string Description => description;
        public int Quantity => quantity;
        public Money UnitCost => unitCost;

        public Money TotalCost => unitCost * quantity;

        private Service(ServiceType serviceType, string description, int quantity, Money unitCost)
        {
            this.serviceType = serviceType;
            this.description = description;
            this.quantity = quantity;
            this.unitCost = unitCost;
        }

        public static Service Make(ServiceType serviceType, string description, int quantity, double unitCost)
        {
            if (quantity <= 0)
                throw new InvalidServiceQuantityException("Quantity must be greater than zero");

            return new Service(serviceType, description, quantity, Money.Make(unitCost));
        }

        public override string ToString()
        {
            return $"  - {ServiceType}: {Description} (x{Quantity}) | subtotal: {TotalCost.ToString()}";
        }
    }
}
