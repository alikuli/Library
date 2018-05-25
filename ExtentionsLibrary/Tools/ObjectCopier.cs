using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliKuli.Tools
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Reference Article http://www.codeproject.com/KB/tips/SerializedObjectCloner.aspx
    /// Provides a method for performing a deep copy of an object.
    /// Binary Serialization is used to perform the copy.
    /// </summary>
    public static class ObjectCopier
    {
        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                string name = typeof(T).Name;
                throw new ArgumentException(string.Format("The type '{0}' must be serializable.",name));
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Usage
        /// var testObject = new Dictionary<int, object>();
        ///result = testObject.GetType().Implements(typeof(IDictionary<int, object>)); // true!
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <param name="type"></param>
        /// <param name="interface"></param>
        /// <returns></returns>
        public static bool Implements<I>(this Type type, I interfaceType) where I : class
        {
            if (((interfaceType as Type) == null) || !(interfaceType as Type).IsInterface)
                throw new ArgumentException("Only interfaces can be 'implemented'.");

            return (interfaceType as Type).IsAssignableFrom(type);
        }
    }
}
