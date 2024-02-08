using Volo.Abp.Domain.Entities;

namespace AbpFramework.Poc.Core.Entities
{
    public class Customer: Entity<int>
    {
        //public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Address { get; private set; } = string.Empty;

        protected Customer(): base() { }

        private Customer(int id, string name, string address)
            : base(id)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public static Customer Build(string name, string address)
        {
            Random rnd = new Random();
            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(name), nameof(address));

            return new Customer(rnd.Next(), name, address);
        }
    }
}
