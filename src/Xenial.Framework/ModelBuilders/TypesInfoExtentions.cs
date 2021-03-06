﻿using System;
using System.Linq;

namespace DevExpress.ExpressApp.DC
{
    /// <summary>   Class TypesInfoExtentions. </summary>
    public static partial class TypesInfoExtentions
    {
        /// <summary>   Creates the model builder. </summary>
        ///
        /// <exception cref="ArgumentNullException">        Thrown when one or more required arguments
        ///                                                 are null. </exception>
        /// <exception cref="InvalidOperationException">    Cannot create ModelBuilder of Type
        ///                                                 '{typeof(TModelBuilder)}' because the base
        ///                                                 generic type can not be found. </exception>
        ///
        /// <typeparam name="TModelBuilder">    The type of the t model builder. </typeparam>
        /// <param name="typesInfo">    The types information. </param>
        ///
        /// <returns>   TModelBuilder. </returns>

        public static TModelBuilder CreateModelBuilder<TModelBuilder>(this ITypesInfo typesInfo)
        {
            _ = typesInfo ?? throw new ArgumentNullException(nameof(typesInfo));

            var modelTypeForBuilder = typeof(TModelBuilder).BaseType?.GenericTypeArguments.FirstOrDefault();
            if (modelTypeForBuilder == null)
            {
                throw new InvalidOperationException($"Cannot create ModelBuilder of Type '{typeof(TModelBuilder)}' because the base generic type can not be found");
            }

            var typeInfo = typesInfo.FindTypeInfo(modelTypeForBuilder);
            if (typeInfo == null)
            {
                throw new InvalidOperationException($"Cannot create ModelBuilder of Type '{typeof(TModelBuilder)}' because no TypeInfo for Type '{modelTypeForBuilder}' could be found");
            }

            var builder = Activator.CreateInstance(typeof(TModelBuilder), typeInfo);
            if (builder is TModelBuilder tModelBuilder)
            {
                return tModelBuilder;
            }

            throw new InvalidOperationException($"Created builder type '{builder!.GetType()}' is not of expected type {typeof(TModelBuilder)}");
        }
    }
}

