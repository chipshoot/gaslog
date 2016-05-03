using System.Diagnostics;

namespace NoteRepository.Common.Utility.Dal.Pagination
{
    public interface IConverter<TSource, TTarget>
    {
        TTarget Create(TSource source);

        TSource Reverse(TTarget target);
    }
}