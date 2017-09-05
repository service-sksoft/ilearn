namespace ILearn.Entities
{
    using System;

    interface IAuditEntity: IBaseEntity
    {
        DateTime CreationTime { get; set; }

        DateTime? ModificationTime { get; set; }

        DateTime? ActivationTime { get; set; }

        bool IsActive { get; set; }
    }
}
