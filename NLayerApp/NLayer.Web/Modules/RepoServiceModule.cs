using Autofac;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace NLayer.Web.Modules
{
    public class RepoServiceModule:Module
    { // bu moduleyi de program.cs de dahil edeceğiz. şöyle : builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            // katmanları dinamik olarak getireceğiz, istersek isimlerini yazarak da getirebiliriz ama olabildiğince tip güvenli gitmek için içinden herhangi bir class getiriyoruz


            var apiAssembly = Assembly.GetExecutingAssembly(); //bu classın bulunduğu API assemblysini aldım yani api katmanı

            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext)); // repositoryde bulunan herhangi bir class yani repository katmanı

            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile)); // service katmanı

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope(); // burada classslardan sonu Repository ile bitenleri al, sonu repository ile biten interfacelerle eşleştir dedik

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
            //InstancePerLifetimeScope => Scope
            //InstancePerDependency => transient

            


        }
    }
}
