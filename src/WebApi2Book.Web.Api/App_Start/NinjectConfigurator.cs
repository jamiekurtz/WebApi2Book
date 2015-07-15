// NinjectConfigurator.cs
// Copyright Jamie Kurtz, Brian Wortman 2015.

using EFCommonContext;
using log4net.Config;
using Ninject;
using Ninject.Activation;
using Ninject.Web.Common;
using WebApi2Book.Common;
using WebApi2Book.Common.Logging;
using WebApi2Book.Common.Security;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Data.SqlServer.Mapping;
using WebApi2Book.Data.SqlServer.QueryProcessors;
using WebApi2Book.Web.Api.AutoMappingConfiguration;
using WebApi2Book.Web.Api.Controllers.V1;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.LegacyProcessing;
using WebApi2Book.Web.Api.LegacyProcessing.ProcessingStrategies;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Api.Security;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Security;

namespace WebApi2Book.Web.Api
{
    /// <summary>
    ///     Class used to set up the Ninject DI container.
    /// </summary>
    public class NinjectConfigurator
    {
        /// <summary>
        ///     Entry method used by caller to configure the given
        ///     container with all of this application's
        ///     dependencies.
        /// </summary>
        public void Configure(IKernel container)
        {
            AddBindings(container);
        }

        private void AddBindings(IKernel container)
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            // IMPORTANT NOTE! *Never* configure a type as singleton if it depends upon ISession!!! This is because
            // ISession is disposed of at the end of every request. For the same reason, make sure that types
            // dependent upon such types aren't configured as singleton.
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ConfigureLog4net(container);
            ConfigureUserSession(container);
            ConfigureEntityFramework(container);
            ConfigureDependenciesOnlyUsedForLegacyProcessing(container);
            ConfigureAutoMapper(container);

            container.Bind<IBasicSecurityService>().To<BasicSecurityService>().InSingletonScope();
            container.Bind<IDateTime>().To<DateTimeAdapter>().InSingletonScope();

            container.Bind<ICommonLinkService>().To<CommonLinkService>().InRequestScope();
            container.Bind<IPagedDataRequestFactory>().To<PagedDataRequestFactory>().InSingletonScope();

            container.Bind<IAllStatusesInquiryProcessor>().To<AllStatusesInquiryProcessor>().InRequestScope();
            container.Bind<IAllStatusesQueryProcessor>().To<AllStatusesQueryProcessor>().InRequestScope();
            container.Bind<IStatusLinkService>().To<StatusLinkService>().InRequestScope();

            container.Bind<IAllUsersInquiryProcessor>().To<AllUsersInquiryProcessor>().InRequestScope();
            container.Bind<IAllUsersQueryProcessor>().To<AllUsersQueryProcessor>().InRequestScope();
            container.Bind<IUserByIdInquiryProcessor>().To<UserByIdInquiryProcessor>().InRequestScope();
            container.Bind<IUserByIdQueryProcessor>().To<UserByIdQueryProcessor>().InRequestScope();
            container.Bind<IUserLinkService>().To<UserLinkService>().InRequestScope();

            container.Bind<ITasksControllerDependencyBlock>().To<TasksControllerDependencyBlock>().InRequestScope();
            container.Bind<IAddTaskMaintenanceProcessor>().To<AddTaskMaintenanceProcessor>().InRequestScope();
            container.Bind<IAddTaskQueryProcessor>().To<AddTaskQueryProcessor>().InRequestScope();
            container.Bind<IUpdateTaskMaintenanceProcessor>().To<UpdateTaskMaintenanceProcessor>().InRequestScope();
            container.Bind<IUpdateablePropertyDetector>().To<JObjectUpdateablePropertyDetector>().InSingletonScope();
            container.Bind<IUpdateTaskQueryProcessor>().To<UpdateTaskQueryProcessor>().InRequestScope();
            container.Bind<IDeleteTaskQueryProcessor>().To<DeleteTaskQueryProcessor>().InRequestScope();
            container.Bind<IStartTaskWorkflowProcessor>().To<StartTaskWorkflowProcessor>().InRequestScope();
            container.Bind<ICompleteTaskWorkflowProcessor>().To<CompleteTaskWorkflowProcessor>().InRequestScope();
            container.Bind<IReactivateTaskWorkflowProcessor>().To<ReactivateTaskWorkflowProcessor>().InRequestScope();
            container.Bind<IUpdateTaskStatusQueryProcessor>().To<UpdateTaskStatusQueryProcessor>().InRequestScope();
            container.Bind<IAllTasksInquiryProcessor>().To<AllTasksInquiryProcessor>().InRequestScope();
            container.Bind<IAllTasksQueryProcessor>().To<AllTasksQueryProcessor>().InRequestScope();
            container.Bind<ITaskByIdInquiryProcessor>().To<TaskByIdInquiryProcessor>().InRequestScope();
            container.Bind<ITaskByIdQueryProcessor>().To<TaskByIdQueryProcessor>().InRequestScope();
            container.Bind<ITaskLinkService>().To<TaskLinkService>().InRequestScope();

            container.Bind<ITaskUsersInquiryProcessor>().To<TaskUsersInquiryProcessor>().InRequestScope();
            container.Bind<ITaskUsersMaintenanceProcessor>().To<TaskUsersMaintenanceProcessor>().InRequestScope();
            container.Bind<ITaskUsersLinkService>().To<TaskUsersLinkService>().InRequestScope();

            container.Bind<IAddTaskMaintenanceProcessorV2>().To<AddTaskMaintenanceProcessorV2>().InRequestScope();
        }

        private void ConfigureAutoMapper(IKernel container)
        {
            container.Bind<IAutoMapper>().To<AutoMapperAdapter>().InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
                .To<StatusEntityToStatusAutoMapperTypeConfigurator>()
                .InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
                .To<StatusToStatusEntityAutoMapperTypeConfigurator>()
                .InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
                .To<UserEntityToUserAutoMapperTypeConfigurator>()
                .InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
                .To<UserToUserEntityAutoMapperTypeConfigurator>()
                .InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
                .To<NewTaskToTaskEntityAutoMapperTypeConfigurator>()
                .InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
                .To<TaskEntityToTaskAutoMapperTypeConfigurator>()
                .InSingletonScope();
            container.Bind<IAutoMapperTypeConfigurator>()
                .To<TaskToTaskEntityAutoMapperTypeConfigurator>()
                .InSingletonScope();

            container.Bind<IAutoMapperTypeConfigurator>()
                .To<NewTaskV2ToTaskEntityAutoMapperTypeConfigurator>()
                .InRequestScope();
        }

        private void ConfigureUserSession(IKernel container)
        {
            var userSession = new UserSession();
            container.Bind<IUserSession>().ToConstant(userSession).InSingletonScope();
            container.Bind<IWebUserSession>().ToConstant(userSession).InSingletonScope();
        }

        private void ConfigureEntityFramework(IKernel container)
        {
            var contextFactory = WebContextFactory.BuildFactory("WebApi2BookDb", typeof(TaskMap).Assembly, "System.Data.SqlClient", "2012");
            container.Bind<IWebContextFactory>().ToConstant(contextFactory);

            container.Bind<IDbContext>().ToMethod(context => context.Kernel.Get<IWebContextFactory>().GetNewOrCurrentContext());
            container.Bind<IActionTransactionHelper>().To<ActionTransactionHelper>().InRequestScope();
        }

        private void ConfigureDependenciesOnlyUsedForLegacyProcessing(IKernel container)
        {
            container.Bind<ILegacyMessageProcessor>().To<LegacyMessageProcessor>().InRequestScope();
            container.Bind<ILegacyMessageParser>().To<LegacyMessageParser>().InSingletonScope();
            container.Bind<ILegacyMessageTypeFormatter>().To<LegacyMessageTypeFormatter>().InSingletonScope();

            container.Bind<ILegacyMessageProcessingStrategy>()
                .To<GetTasksMessageProcessingStrategy>()
                .InRequestScope();
            container.Bind<ILegacyMessageProcessingStrategy>()
                .To<GetTaskByIdMessageProcessingStrategy>()
                .InRequestScope();
        }

        private void ConfigureLog4net(IKernel container)
        {
            XmlConfigurator.Configure();

            var logManager = new LogManagerAdapter();
            container.Bind<ILogManager>().ToConstant(logManager);
        }
    }
}