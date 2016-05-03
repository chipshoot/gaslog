using System;

namespace NoteRepository.Common.Utility.Dal
{
    public interface IDataChangePublish
    {
        event EventHandler DataSourceChanged;
    }
}