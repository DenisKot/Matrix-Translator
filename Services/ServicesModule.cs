namespace Services
{
    using Autofac;
    using CSV;
    using Matrix;

    public class ServicesModule
    {
        public static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<CsvReaderService>().As<ICsvReaderService>();
            builder.RegisterType<MatrixRotatorServiceFactory>().As<IMatrixRotatorServiceFactory>().SingleInstance();
        }
    }
}