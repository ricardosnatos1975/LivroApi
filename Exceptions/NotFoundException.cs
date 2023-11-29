using System;

namespace LivroApi.Api.Exceptions
{
      public class NotFoundException : Exception
    {
        public string EntityName { get; }
        public object EntityId { get; }

        public NotFoundException() { }

        // Construtor que aceita o nome da entidade e o ID da entidade não encontrada
        public NotFoundException(string entityName, object entityId)
            : base($"Entidade '{entityName}' com ID '{entityId}' não encontrada.")
        {
            EntityName = entityName;
            EntityId = entityId;
        }

        // Construtor adicional para permitir mensagens personalizadas
        public NotFoundException(string message)
            : base(message) { }

        // Construtor adicional para permitir mensagens personalizadas e uma exceção interna
        public NotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }

}
