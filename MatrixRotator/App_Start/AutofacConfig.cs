namespace MatrixRotator
{
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;
    using Services;

    public static class AutofacConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            ServicesModule.ConfigureContainer(builder);

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }

    }

    //public static void ConfigureContainer()
    //{
    //    // получаем экземпляр контейнера
    //    var builder = new ContainerBuilder();

    //    // регистрируем контроллер в текущей сборке
    //    builder.RegisterControllers(typeof(MvcApplication).Assembly);

    //    // регистрируем споставление типов
    //    //builder.RegisterType<BookRepository>().As<IRepository>();
    //    ServicesModule.ConfigureContainer(builder);

    //    // создаем новый контейнер с теми зависимостями, которые определены выше
    //    var container = builder.Build();

    //    // установка сопоставителя зависимостей
    //    DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        //}

}