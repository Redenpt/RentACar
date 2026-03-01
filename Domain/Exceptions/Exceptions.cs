using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string entityName, string value)
            : base($"Já existe uma {entityName} com o valor '{value}'.") {}
    }

    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName)
            : base($"{entityName} não foi encontrado/a.") { }
    }

    public class EntityInactiveException : Exception
    {
        public EntityInactiveException(string entityName)
            : base($"{entityName} não está ativo/a.") { }
    }

    public class EntityValidationException : Exception
    {
        public EntityValidationException(string entityName, string message)
            : base($"{entityName}' é inválido/a: {message}") { }
    }

    public class LicensePlateRequiredException : Exception
    {
        public LicensePlateRequiredException()
            : base("A matrícula do veículo é obrigatória.") { }
    }

    public class VehicleAlreadyRentedException : Exception
    {
        public VehicleAlreadyRentedException()
            : base($"O veículo selecionado já tem um contrato de aluguer para o periodo selecionado."){ }
    }

    public class EntityHasActiveRentals : Exception
    {
        public EntityHasActiveRentals(string entityName)
            : base($"O {entityName} selecionado não pode ser eliminado porque tem contratos de aluguer ativos ou pendentes") { }
    }
}
