using AbpFramework.Poc.Core.Dto;

namespace AbpFramework.Poc.Core.UseCases
{
    public interface ICreateAccount
    {
        Task<CreateAccountDto> CreateAsync(CreateAccountDto createAccountDto);
    }
}
