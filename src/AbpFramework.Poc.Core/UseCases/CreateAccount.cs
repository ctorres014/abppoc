using AbpFramework.Poc.Core.Dto;
using AbpFramework.Poc.Core.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace AbpFramework.Poc.Core.UseCases
{
    public class CreateAccount : ICreateAccount
    {
        private readonly IRepository<Customer> _customerRepository;

        public CreateAccount(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CreateAccountDto> CreateAsync(CreateAccountDto createAccountDto)
        {
            try
            {
                var newCustomer = Customer.Build(createAccountDto.Name, createAccountDto.Address);
                await _customerRepository.InsertAsync(newCustomer);
                return createAccountDto;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
           
        }
    }
}
