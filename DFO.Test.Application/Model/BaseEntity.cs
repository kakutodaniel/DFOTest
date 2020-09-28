using System;

namespace DFO.Test.Application.Model
{
    public abstract class BaseEntity
    {
        public int Id { get; private set; }

        public string HashId { get; private set; } = Guid.NewGuid().ToString();

        public DateTime CreationDate { get; private set; } = DateTime.Now;

        public DateTime? UpdateDate { get; set; }

    }
}
