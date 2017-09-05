namespace ILearn.WebApi
{
    using Services.Common;
    using System.Web.Http;

    public class Bootstrapper
    {
        public static void Run()
        {
            // Configure Autofac
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
            //Configure AutoMapper
            //AutoMapperConfiguration.Configure();
         
        }
    }
}