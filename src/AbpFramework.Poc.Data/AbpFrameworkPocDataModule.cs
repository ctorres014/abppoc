using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace AbpFramework.Poc.Data
{
    [DependsOn(typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpEntityFrameworkCoreModule)
        )]
    public class AbpFrameworkPocDataModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<AsidbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });
            context.Services.AddAbpDbContext<AsidbContext>();
            Configure<AbpDbContextOptions>(option =>
            {
                option.Configure(configureContext =>
                {
                    configureContext.UseSqlServer();
                });
            });
        }
    }
}
