using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Application.Common.Exceptions
{
    [Serializable]
    public class ManyNotFoundException : Exception
    {
        #region Constructors

        public ManyNotFoundException()
        {
        }

        public ManyNotFoundException(string message)
            : base(message)
        {
        }

        public ManyNotFoundException(string message, Exception inner) 
            : base(message, inner)
        {
        }
        
        public ManyNotFoundException(string message, List<NotFoundException> exceptions)
            : base(message)
        {
            Exceptions = exceptions;
        }
        
        public ManyNotFoundException(List<NotFoundException> exceptions)
            : this(GetDefaultMessage(exceptions), exceptions)
        {
        }
        
        protected ManyNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        #endregion

        #region Properties

        public List<NotFoundException> Exceptions { get; set; }

        #endregion

        #region Methods

        private static string GetDefaultMessage(List<NotFoundException> exceptions)
        {
            IEnumerable<string> messages = exceptions.Select(exception => exception.Message);
            return string.Join("\n", messages);
        }

        #endregion
    }
}