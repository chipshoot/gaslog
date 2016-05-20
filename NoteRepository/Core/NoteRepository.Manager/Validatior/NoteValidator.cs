using System.Linq;
using NoteRepository.Common.Utility.Dal;
using NoteRepository.Common.Utility.Validation;
using NoteRepository.Core.DomainEntity;

namespace NoteRepository.Core.Manager.Validatior
{
    public class NoteValidator : ValidatorBase<Note>
    {
        #region constructor

        public NoteValidator(ILookupRepository lookupRepo) : base(lookupRepo)
        {
        }

        #endregion constructor

        #region implementation of ValidatorBase

        public override bool IsValid(Note entity, bool isNewEntity)
        {
            if (entity == null)
            {
                ValidationError = "Note is null.";
                return false;
            }

            if (entity.Author == null)
            {
                ValidationError = "Note's author is null.";
                return false;

            }

            if (!LookupRepo.FindEntities<User>().Any(u => u.Id == entity.Author.Id))
            {
                ValidationError = "Note's author does not exists in data source.";
                return false;
            }

            if (entity.Catalog == null)
            {
                ValidationError = "Note's catalog is null.";
                return false;
            }

            if (!LookupRepo.FindEntities<NoteCatalog>().Any(c => c.Id == entity.Catalog.Id))
            {
                ValidationError = "Note's catalog does not exists in data source.";
                return false;
            }
            return true;
        }

        #endregion implementation of ValidatorBase
    }
}