using System;
using System.Runtime.Serialization;

namespace WebERP.Models.Exceptions
{
    [Serializable]
    internal class MudancaInvalidaDeStatusException : Exception
    {
        public MudancaInvalidaDeStatusException()
        {
        }

        public MudancaInvalidaDeStatusException(string message) : base(message)
        {
        }

        public MudancaInvalidaDeStatusException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MudancaInvalidaDeStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}