using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid ID { get; set; }

        // Data e hora em que o registo foi criado
        public DateTime CreatedAt { get; set; }

        // Data e hora da última vez em que o registo foi alterado
        // Fica a null enquanto o registo não for alterado
        public DateTime? UpdatedAt { get; set; }

        // Data e hora em que o registo foi marcado como eliminado (soft delete)
        // Fica a null enquanto o registo estiver ativo
        public DateTime? DeletedAt { get; set; }

        // Indica se o registo está ativo no sistema
        // false significa que foi desativado (soft delete)
        public bool IsActive { get; set; }


        protected BaseEntity()
        {
            ID = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }


        public void SoftDelete()
        {
            IsActive = false;
            DeletedAt = DateTime.UtcNow;
        }

        public void LastUpdate()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public void Restore()
        {
            IsActive = true;
            DeletedAt = null;
        }
    }
}
