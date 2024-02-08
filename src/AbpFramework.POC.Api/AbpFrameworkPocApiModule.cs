using AbpFramework.Poc.Core;
using AbpFramework.Poc.Data;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace AbpFramework.POC.Api
{
    [DependsOn(typeof(AbpFrameworkPocCoreModule))]
    [DependsOn(typeof(AbpAspNetCoreMvcModule))]
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(AbpEntityFrameworkCoreSqlServerModule))]
    [DependsOn(typeof(AbpSwashbuckleModule))]
    [DependsOn(typeof(AbpFrameworkPocDataModule))]
    //[DependsOn(typeof(AccountNumberSupplierCoreModule))]
    //[DependsOn(typeof(AccountNumberSupplierDiagnosticsModule))]
    //[DependsOn(typeof(AbpDistributedLockingModule))]
    public class AbpFrameworkPocApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureSwagger(context.Services);
            ConfigureEfCore(context);
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SolutionArchitecture.Host API", Version = "v1", Description = "Architecture POC" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.DocInclusionPredicate((docName, description) => true);
                c.CustomSchemaIds(type => type.FullName);
                c.DocumentFilter<CustomSwaggerFilter>();

            });
               
        }

        private class CustomSwaggerFilter : IDocumentFilter
        {
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                //remove paths those start with /api/abp prefix
                swaggerDoc.Paths
                    .Where(x => x.Key.ToLowerInvariant().StartsWith("/api/abp"))
                    .ToList()
                    .ForEach(x => swaggerDoc.Paths.Remove(x.Key));

                //remove component schema starting with volo. prefix
                swaggerDoc.Components.Schemas
                    .Where(x => x.Key.ToLowerInvariant().StartsWith("volo."))
                    .ToList()
                    .ForEach(x => swaggerDoc.Components.Schemas.Remove(x.Key));
            }
        }

        private void ConfigureEfCore(ServiceConfigurationContext context)
        {
            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Asidb;";
            });
            Configure<AbpDbContextOptions>(options =>
            {
                options.Configure(configurationContext => { configurationContext.UseSqlServer(); });
            });
            context.Services.AddAbpDbContext<AsidbContext>(opt =>
            {
                opt.AddDefaultRepositories(includeAllEntities: true);

            });

            //context.Services.AddAbpDbContext<PubDatabaseContext>(opt =>
            //{
            //    opt.AddDefaultRepositories(includeAllEntities: true);


            //});
        }
    }
}
