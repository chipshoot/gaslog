using NHibernate.Cfg;
using System;

namespace NoteRepository.Dal.NH.Infrastructure.SessionMan
{
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Occurs when before configure the NHibernate, add mapping class at this point.
        /// </summary>
        event EventHandler<ConfiguringEventArgs> BeforeConfigure;

        /// <summary>
        /// Occurs when after configure finished.
        /// </summary>
        event EventHandler<ConfiguredEventArgs> AfterConfigure;

        /// <summary>
        /// Configures this instance.
        /// </summary>
        /// <returns>The <see cref="Configuration"/></returns>
        Configuration Configure();
    }
}