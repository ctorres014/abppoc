using AbpFramework.Poc.Core.UseCases;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace AbpFramework.Poc.Core
{
    [DependsOn(typeof(AbpDddApplicationModule)
        )]
    public class AbpFrameworkPocCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<ICreateAccount, CreateAccount>();
        }
    }
}
