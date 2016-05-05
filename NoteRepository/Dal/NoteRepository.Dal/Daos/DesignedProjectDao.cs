using System;
using System.Collections.Generic;

namespace Hadrian.ProjectCenter.Dal.Daos
{
    public class DesignedProjectDao
    {
        public virtual Guid Id { get; set; }

        public virtual int ProjectId { get; set; }

        public virtual string Name { get; set; }

        public virtual string AssignedTo { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual bool IsArchived { get; set; }

        public virtual string Comment { get; set; }

        public virtual int Status { get; set; }

        public virtual IList<StakeDao> Stakes { get; set; }
    }
}