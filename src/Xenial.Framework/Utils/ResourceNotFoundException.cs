using System;
using System.Linq;
using System.Reflection;

#pragma warning disable CA1032 //By design
#pragma warning disable CA2237 //By design

namespace Xenial.Framework.Utils
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Exception" />
    public sealed class ResourceNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceNotFoundException"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="resourceName">Name of the resource.</param>
        public ResourceNotFoundException(Assembly assembly, string? resourceName)
            : base($"Resource '{resourceName}' was not found in Assembly '{assembly?.GetName()?.Name}'{Environment.NewLine}Possible Resources:{Environment.NewLine}{string.Join(Environment.NewLine, assembly?.GetManifestResourceNames().Select(r => "\t" + r))}")
        {
            Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
            ResourceName = resourceName;
        }

        /// <summary>
        /// Gets the assembly.
        /// </summary>
        /// <value>
        /// The assembly.
        /// </value>
        public Assembly Assembly { get; }

        /// <summary>
        /// Gets the name of the resource.
        /// </summary>
        /// <value>
        /// The name of the resource.
        /// </value>
        public string? ResourceName { get; }

        /// <summary>
        /// Gets the resource path.
        /// </summary>
        /// <value>
        /// The resource path.
        /// </value>
        public string ResourcePath => $"{Assembly.GetName().Name}.{ResourceName}";
    }
}
