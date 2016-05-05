using System;
using System.Linq;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using NoteRepository.Common.Utility.LogService;

namespace NoteRepository.Dal.NH.Infrastructure.SessionMan
{
    public static class MappingFilterExtension
    {
        /// <summary>
        /// Adds class map for fluent NHibernate from assembly and filter the class in namespace.
        /// </summary>
        /// <typeparam name="T">The type of class contains in assembly, whose mapping class will be added to NHibenate</typeparam>
        /// <param name="mappings">The mappings.</param>
        /// <param name="where">The namespace where all class need to be add.</param>
        /// <returns></returns>
        public static FluentMappingsContainer AddFromAssemblyOf<T>(this FluentMappingsContainer mappings, Predicate<Type> where, ILogService logger)
        {
            if (where == null)
            {
                return mappings.AddFromAssemblyOf<T>();
            }

            var mappingClasses = typeof(T).Assembly.GetExportedTypes()
                .Where(x => typeof(IMappingProvider).IsAssignableFrom(x) && where(x));

            foreach (var type in mappingClasses)
            {
                logger?.DebugFormat("add type {0}", type.FullName);
                mappings.Add(type);
            }

            return mappings;
        }
    }
}