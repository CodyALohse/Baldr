using System;

namespace Core
{
    public interface IBaseModel
    {
        Guid Id { get; set; }

        DateTimeOffset CreatedOn { get; set; }

        DateTimeOffset ModifiedOn { get; set; }
    }
}
