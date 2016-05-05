using System;

namespace Hadrian.ProjectCenter.Dal.Daos
{
    public class StakeDao
    {
        /// <summary>
        /// Primary Key id of the stake entity
        /// </summary>
        public virtual Guid Id { get; set; }

        public virtual Guid ProjectId { get; set; }

        public virtual int StakeId { get; set; }

        public virtual int MinorId { get; set; }

        public virtual DateTime CreateDate { get; set; }

        public virtual int Status { get; set; }

        public virtual string ProjectName { get; set; }

        public virtual string AssignedTo { get; set; }

        public virtual string DrawnBy { get; set; }

        public virtual byte[] BinDesignedProject { get; set; }

        public virtual byte[] BinBom { get; set; }

        public virtual string PONumber { get; set; }

        public virtual float Price { get; set; }

        public virtual int StatusOutstanding { get; set; }

        public virtual string ZipCode { get; set; }

        public virtual string Attention { get; set; }

        public virtual float Freight { get; set; }

        public virtual int NumberOfSkids { get; set; }

        public virtual float SpecialCharge { get; set; }

        public virtual float SubTotal { get; set; }

        public virtual float TaxTotal { get; set; }

        public virtual float Margin { get; set; }

        public virtual string JobType { get; set; }

        public virtual DateTime DateRequired { get; set; }

        public virtual string Architect { get; set; }

        public virtual string Contractor { get; set; }

        public virtual bool JdeStarted { get; set; }

        public virtual bool JdeFinished { get; set; }

        public virtual string JdeOrderNos { get; set; }

        public virtual int BomSchemaVersion { get; set; }

        public virtual Guid SessionUid { get; set; }

        public virtual string ContactInformation { get; set; }

        public virtual int FileFormat { get; set; }

        public virtual bool IsVerified { get; set; }

        public virtual DateTime DateVerified { get; set; }

        public virtual Guid PersonId { get; set; }

        public virtual string ContactEmail { get; set; }

        public virtual Guid ContactUid { get; set; }

        public virtual string Comment { get; set; }
    }
}