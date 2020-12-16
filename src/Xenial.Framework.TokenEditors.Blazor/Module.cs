﻿using System;
using System.Collections.Generic;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;

namespace Xenial.Framework.TokenEditors.Blazor
{
    /// <summary>
    /// Class XenialTokenEditorsBlazorModule.
    /// Implements the <see cref="Xenial.Framework.XenialModuleBase" />
    /// </summary>
    /// <seealso cref="Xenial.Framework.XenialModuleBase" />
    /// <autogeneratedoc />
    public class XenialTokenEditorsBlazorModule : XenialModuleBase
    {
        /// <summary>
        /// Adds the DevExpress.ExpressApp.SystemModule.SystemModule to the collection
        /// </summary>
        /// <returns>ModuleTypeList.</returns>
        /// <autogeneratedoc />
        protected override ModuleTypeList GetRequiredModuleTypesCore()
            => base.GetRequiredModuleTypesCore()
                .AndModuleTypes(typeof(XenialTokenEditorsModule));

        /// <summary>
        /// Registers the editor descriptors.
        /// </summary>
        /// <param name="editorDescriptorsFactory">The editor descriptors factory.</param>
        /// <autogeneratedoc />
        protected override void RegisterEditorDescriptors(EditorDescriptorsFactory editorDescriptorsFactory)
        {
            base.RegisterEditorDescriptors(editorDescriptorsFactory);
            editorDescriptorsFactory.UseTokenStringPropertyEditorsBlazor();
        }
    }
}
