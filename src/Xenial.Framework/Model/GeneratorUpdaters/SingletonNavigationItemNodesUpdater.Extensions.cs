﻿using System;

using Xenial.Framework.Model.GeneratorUpdaters;

namespace DevExpress.ExpressApp.Model.Core
{
    public static partial class ModelNodesGeneratorUpdatersExtentions
    {
        /// <summary>
        /// Uses the singleton navigation items.
        /// </summary>
        /// <param name="modelNodesGeneratorUpdaters">The model nodes generator updaters.</param>
        /// <returns>ModelNodesGeneratorUpdaters.</returns>
        /// <autogeneratedoc />
        public static ModelNodesGeneratorUpdaters UseSingletonNavigationItems(this ModelNodesGeneratorUpdaters modelNodesGeneratorUpdaters)
        {
            _ = modelNodesGeneratorUpdaters ?? throw new ArgumentNullException(nameof(modelNodesGeneratorUpdaters));
            modelNodesGeneratorUpdaters.Add(new SingletonNavigationItemNodesUpdater());
            return modelNodesGeneratorUpdaters;
        }
    }
}
