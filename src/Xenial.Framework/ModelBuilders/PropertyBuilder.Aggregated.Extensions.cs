﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace Xenial.Framework.ModelBuilders
{
    /// <summary>
    /// Class AggregatedPropertyBuilderExtensions.
    /// </summary>
    /// <autogeneratedoc />
    public static class AggregatedPropertyBuilderExtensions
    {
        /// <summary>
        /// Excepts the specified builder.
        /// </summary>
        /// <typeparam name="TPropertyType">The type of the t property type.</typeparam>
        /// <typeparam name="TClassType">The type of the t class type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="propertyExpressions">The property expressions.</param>
        /// <returns>Xenial.Framework.ModelBuilders.IAggregatedPropertyBuilder&lt;TPropertyType, TClassType&gt;.</returns>
        /// <autogeneratedoc />
        public static IAggregatedPropertyBuilder<TPropertyType, TClassType> Except<TPropertyType, TClassType>(
            this IAggregatedPropertyBuilder<TPropertyType, TClassType> builder,
            params Expression<Func<TClassType, TPropertyType?>>[] propertyExpressions
        )
        {
            var propertyBuilders = builder.PropertyBuilders.ToList();
            foreach (var propertyExpression in propertyExpressions)
            {
                _ = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpressions));

                var propertyName = builder.ModelBuilder.ExpressionHelper.Property(propertyExpression);
                if (propertyName is not null)
                {
                    var memberInfo = builder.ModelBuilder.TypeInfo.FindMember(propertyName);
                    if (memberInfo is not null)
                    {
                        var propertyBuilder = propertyBuilders.FirstOrDefault(m => m.MemberInfo == memberInfo);
                        if (propertyBuilder is not null)
                        {
                            propertyBuilders.Remove(propertyBuilder);
                        }
                    }
                }
            }

            return new AggregatedPropertyBuilder<TPropertyType, TClassType>(builder.ModelBuilder, propertyBuilders);
        }
    }
}
