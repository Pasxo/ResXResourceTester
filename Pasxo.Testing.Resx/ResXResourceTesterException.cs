namespace Pasxo.Testing.Resx
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// Represents exceptions that occur while using the ResXResourceTester class.
    /// </summary>
    [Serializable]
    public class ResXResourceTesterException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResXResourceTesterException"/> class.
        /// </summary>
        public ResXResourceTesterException()
        {
            this.ExceptionType = ResXResourceTesterExceptionType.Unknown;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResXResourceTesterException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ResXResourceTesterException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResXResourceTesterException"/> class.
        /// </summary>
        /// <param name="type">The exception type.</param>
        /// <param name="message">The message.</param>
        public ResXResourceTesterException(ResXResourceTesterExceptionType type, string message)
            : base(message)
        {
            this.ExceptionType = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResXResourceTesterException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ResXResourceTesterException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResXResourceTesterException"/> class.
        /// </summary>
        /// <param name="type">The exception type.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ResXResourceTesterException(ResXResourceTesterExceptionType type, string message, Exception innerException)
            : base(message, innerException)
        {
            this.ExceptionType = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResXResourceTesterException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ResXResourceTesterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.ExceptionType = (ResXResourceTesterExceptionType)info.GetInt32("ResXResourceTesterExceptionType");
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The exception type.
        /// </value>
        public ResXResourceTesterExceptionType ExceptionType { get; private set; }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="System.ArgumentNullException">Argument Null Exception if info is null.</exception>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
        /// </PermissionSet>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            base.GetObjectData(info, context);

            info.AddValue("ResXResourceTesterExceptionType", this.ExceptionType);
        }
    }
}
