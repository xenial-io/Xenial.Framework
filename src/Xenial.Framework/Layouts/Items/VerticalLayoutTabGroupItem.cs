﻿
using DevExpress.ExpressApp.Layout;

#pragma warning disable CA1710 //Rename Type to end in Collection -> By Design
#pragma warning disable CA2227 //Collection fields should not have a setter: By Design

namespace Xenial.Framework.Layouts.Items
{
    /// <summary>
    /// 
    /// </summary>
    [XenialCheckLicence]
    public partial record VerticalLayoutTabGroupItem : LayoutTabGroupItem
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>Xenial.Framework.Layouts.Items.VerticalLayoutTabGroupItem.</returns>
        /// <autogeneratedoc />
        public static new VerticalLayoutTabGroupItem Create()
            => new();

        /// <summary>
        /// Creates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Xenial.Framework.Layouts.Items.VerticalLayoutTabGroupItem.</returns>
        /// <autogeneratedoc />
        public static new VerticalLayoutTabGroupItem Create(string id)
            => new(id);

        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalLayoutTabGroupItem"/> class.
        /// </summary>
        /// <autogeneratedoc />
        public VerticalLayoutTabGroupItem()
            => Direction = FlowDirection.Vertical;

        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalLayoutTabGroupItem"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <autogeneratedoc />
        public VerticalLayoutTabGroupItem(string id)
            : base(id, FlowDirection.Vertical) { }
    }
}
