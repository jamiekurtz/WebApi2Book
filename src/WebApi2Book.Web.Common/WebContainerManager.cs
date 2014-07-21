// WebContainerManager.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace WebApi2Book.Web.Common
{
    /// <summary>
    ///     Provides access to dependencies managed by the <see cref="IDependencyResolver" />. Useful where
    ///     access to the resolver is not convenient/possible.
    /// </summary>
    public static class WebContainerManager
    {
        /// <summary>
        ///     Provides access to the dependency resolver.
        /// </summary>
        public static IDependencyResolver GetDependencyResolver()
        {
            var dependencyResolver = GlobalConfiguration.Configuration.DependencyResolver;
            if (dependencyResolver != null)
            {
                return dependencyResolver;
            }

            throw new InvalidOperationException("The dependency resolver has not been set.");
        }

        /// <summary>
        ///     Provides access to a specific type of dependency managed by the <see cref="IDependencyResolver" />. Use only
        ///     where access to the resolver is not convenient/possible.
        /// </summary>
        public static T Get<T>()
        {
            var service = GetDependencyResolver().GetService(typeof (T));

            if (service == null)
                throw new NullReferenceException(string.Format("Requested service of type {0}, but null was found.",
                    typeof (T).FullName));

            return (T) service;
        }

        /// <summary>
        ///     Provides access to a specific type of dependency managed by the <see cref="IDependencyResolver" />. Use only
        ///     where access to the resolver is not convenient/possible.
        /// </summary>
        public static IEnumerable<T> GetAll<T>()
        {
            var services = GetDependencyResolver().GetServices(typeof (T)).ToList();

            if (!services.Any())
                throw new NullReferenceException(string.Format("Requested services of type {0}, but none were found.",
                    typeof (T).FullName));

            return services.Cast<T>();
        }
    }
}