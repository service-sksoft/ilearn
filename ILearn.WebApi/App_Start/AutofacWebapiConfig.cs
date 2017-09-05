namespace ILearn.WebApi
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Http;

    using Autofac;
    using Autofac.Integration.WebApi;
    using ILearn.Data;
    using ILearn.Data.Infrastructure;
    using ILearn.Data.Repositories;
    using Services.Topic;
    using Services.QuesList;
    using Services.ExternalResource;
    using Services.MultipleChoice;
    using Services.TutorialTitle;
    using Services.TutorialSubtitle;
    using Services.TutorialContent;

    public class AutofacWebapiConfig
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
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // EF ILearnContext
            builder.RegisterType<ILearnContext>().As<DbContext>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterGeneric(typeof(EntityBaseRepository<>)).As(typeof(IEntityBaseRepository<>)).InstancePerRequest();

            // Services
            builder.RegisterType<TopicService>().As<ITopicService>().InstancePerRequest();
            builder.RegisterType<QuesListService>().As<IQuesListService>().InstancePerRequest();
            builder.RegisterType<ExternalResourceService>().As<IExternalResourceService>().InstancePerRequest();
            builder.RegisterType<MultipleChoiceService>().As<IMultipleChoiceService>().InstancePerRequest();
            builder.RegisterType<TutorialTitleService>().As<ITutorialTitleService>().InstancePerRequest();
            builder.RegisterType<TutorialSubtitleService>().As<ITutorialSubtitleService>().InstancePerRequest();
            builder.RegisterType<TutorialContentService>().As<ITutorialContentService>().InstancePerRequest();

            Container = builder.Build();

            return Container;
        }
    }
}