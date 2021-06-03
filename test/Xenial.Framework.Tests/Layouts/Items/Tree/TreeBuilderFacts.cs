using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xenial.Framework.Tests.Assertions;
using Xenial.Framework.Utils;

using static Xenial.Framework.Tests.Layouts.TestModelApplicationFactory;
using static Xenial.Tasty;

namespace Xenial.Framework.Tests.Layouts.Items
{
    public static class TreeBuilderFacts
    {
        public static void TreeBuilderTests() => Describe("layout tree", () =>
        {

            FIt("generates code", () =>
            {
                var xml = ResourceUtil.GetResourceString(typeof(TreeBuilderFacts),
                    "Layouts/Items/Tree/SimpleDetailView.txt"
                );
            });

            It("builds simple tree structure with record syntax", () =>
            {
                var detailView = CreateComplexDetailViewWithLayout(l => new()
                {
                    l.VerticalGroup() with
                    {
                        Children = new()
                        {
                            l.PropertyEditor(p => p.StringProperty),
                            l.PropertyEditor(p => p.IntegerProperty),
                            l.HorizontalGroup() with
                            {
                                Children = new()
                                {
                                    l.PropertyEditor(p => p.BoolProperty),
                                    l.TabbedGroup() with
                                    {
                                        Children = new()
                                        {
                                            l.Tab() with
                                            {
                                                Children = new()
                                                {
                                                    l.PropertyEditor(p => p.ObjectProperty),
                                                    l.PropertyEditor(p => p.NullableIntegerProperty),
                                                }
                                            }
                                        }
                                    },
                                    l.PropertyEditor(p => p.NullableBoolProperty),
                                }
                            }
                        }
                    }
                });

                var _ = detailView?.Layout?.FirstOrDefault(); //We need to access the layout node cause it's lazy evaluated

                //detailView.VisualizeModelNode();
            });
        });
    }
}
