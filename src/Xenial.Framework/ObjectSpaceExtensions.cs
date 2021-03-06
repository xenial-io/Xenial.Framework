﻿using System;
using System.Linq;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;

using Xenial.Framework.Base;
using Xenial.Framework.ModelBuilders;

namespace Xenial.Framework
{
    /// <summary>   Class ObjectSpaceExtensions. </summary>
    public static class ObjectSpaceExtensions
    {
        /// <summary>   Gets the singleton. </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <typeparam name="T">    . </typeparam>
        /// <param name="objectSpace">  The object space. </param>
        ///
        /// <returns>   T. </returns>

        public static T GetSingleton<T>(this IObjectSpace objectSpace)
        {
            _ = objectSpace ?? throw new ArgumentNullException(nameof(objectSpace));
            var obj = objectSpace.FindObject<T>(null, true);
            if (obj is T tObject)
            {
                return tObject;
            }

            obj = objectSpace.CreateObject<T>();

            var typeInfo = objectSpace.TypesInfo.FindTypeInfo<T>();
            if (typeInfo is not null
                && typeInfo.IsAttributeDefined<SingletonAttribute>(false)
                && typeInfo.FindAttribute<SingletonAttribute>()?.AutoCommit == true
            )
            {
                objectSpace.CommitChanges();
            }

            return obj;
        }

        /// <summary>   Gets the singleton. </summary>
        ///
        /// <exception cref="ArgumentNullException">    objectSpace. </exception>
        ///
        /// <param name="objectSpace">  The object space. </param>
        /// <param name="objectType">   Type of the object. </param>
        ///
        /// <returns>   T. </returns>

        public static object GetSingleton(this IObjectSpace objectSpace, Type objectType)
        {
            _ = objectSpace ?? throw new ArgumentNullException(nameof(objectSpace));
            var obj = objectSpace.FindObject(objectType, null, true);
            if (obj is not null)
            {
                return obj;
            }

            obj = objectSpace.CreateObject(objectType);

            var typeInfo = objectSpace.TypesInfo.FindTypeInfo(objectType);
            if (typeInfo is not null
                && typeInfo.IsAttributeDefined<SingletonAttribute>(false)
                && typeInfo.FindAttribute<SingletonAttribute>()?.AutoCommit == true
            )
            {
                objectSpace.CommitChanges();
            }

            return obj;
        }

        /// <summary>   Returns an <see cref="IObjectSpace" /> for the specified type. </summary>
        ///
        /// <exception cref="ArgumentNullException">    baseObject. </exception>
        ///
        /// <param name="baseObject">   The base object. </param>
        /// <param name="type">         The type. </param>
        ///
        /// <returns>   System.Nullable&lt;IObjectSpace&gt;. </returns>

        public static IObjectSpace? ObjectSpaceFor(this IObjectSpaceLink baseObject, Type type)
        {
            _ = baseObject ?? throw new ArgumentNullException(nameof(baseObject));

            if (baseObject.ObjectSpace is NonPersistentObjectSpace nonPersistentObjectSpace)
            {
                return nonPersistentObjectSpace.AdditionalObjectSpaces.FirstOrDefault(os => os.CanInstantiate(type));
            }
            return baseObject.ObjectSpace;
        }

        /// <summary>   Returns an <see cref="IObjectSpace"/> for the specified type. </summary>
        ///
        /// <typeparam name="T">    The type. </typeparam>
        /// <param name="baseObject">   The base object. </param>
        ///
        /// <returns>   An IObjectSpace? </returns>

        public static IObjectSpace? ObjectSpaceFor<T>(this IObjectSpaceLink baseObject)
            => baseObject.ObjectSpaceFor(typeof(T));
    }

    /// <summary>   Class ApplicationExtensions. </summary>
    public static class ApplicationExtensions
    {
        /// <summary>   Uses the non persistent singletons. </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="application">  The application. </param>
        ///
        /// <returns>   DevExpress.ExpressApp.XafApplication. </returns>

        public static XafApplication UseNonPersistentSingletons(this XafApplication application)
        {
            _ = application ?? throw new ArgumentNullException(nameof(application));

            application.ObjectSpaceCreated -= Application_ObjectSpaceCreated;
            application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
            application.Disposed -= Application_Disposed;
            application.Disposed += Application_Disposed;

            void Application_ObjectSpaceCreated(object? _, ObjectSpaceCreatedEventArgs e)
            {
                if (e.ObjectSpace is NonPersistentObjectSpace nos)
                {
                    nos.ObjectByKeyGetting -= Nos_ObjectByKeyGetting;
                    nos.ObjectByKeyGetting += Nos_ObjectByKeyGetting;
                    nos.Disposed -= Nos_Disposed;
                    nos.Disposed += Nos_Disposed;

                    void Nos_ObjectByKeyGetting(object? _, ObjectByKeyGettingEventArgs e)
                    {
                        if (e.Object != null)
                        {
                            return;
                        }

                        var typeInfo = application.TypesInfo.FindTypeInfo(e.ObjectType);

                        if (typeInfo.IsAttributeDefined<SingletonAttribute>(false))
                        {
                            e.Object = nos.GetSingleton(e.ObjectType);
                        }
                    }

                    void Nos_Disposed(object? _, EventArgs e)
                    {
                        nos.Disposed -= Nos_Disposed;
                        nos.ObjectByKeyGetting -= Nos_ObjectByKeyGetting;
                    }
                }
            }

            void Application_Disposed(object? _, EventArgs e)
            {
                application.ObjectSpaceCreated -= Application_ObjectSpaceCreated;
                application.Disposed -= Application_Disposed;
            }

            return application;
        }
    }
}
