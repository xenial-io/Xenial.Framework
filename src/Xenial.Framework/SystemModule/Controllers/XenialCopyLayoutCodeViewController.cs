using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Model;

namespace Xenial.Framework.SystemModule
{
    public sealed class XenialCopyLayoutCodeViewController : ViewController<DetailView>
    {
        private const string actionCategory = "Diagnostic";
        public SimpleAction CopyLayoutCodeSimpleAction { get; }
        public XenialCopyLayoutCodeViewController()
        {
            CopyLayoutCodeSimpleAction = new SimpleAction(this, nameof(CopyLayoutCodeSimpleAction), actionCategory)
            {
                Caption = "Copy Layout Code"
            };

            CopyLayoutCodeSimpleAction.Execute += CopyLayoutCodeSimpleAction_Execute;
        }

        private void CopyLayoutCodeSimpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var writer = new ModelXmlWriter();
            var xml = writer.WriteToString(View.Model, 0);
            var document = XDocument.Parse(xml);


        }
    }
}
