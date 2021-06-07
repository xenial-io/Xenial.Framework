using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xenial.Framework.Layouts.Items.Base;
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

                var method = new CodeMemberMethod()
                {
                    Name = "BuildLayout",
                    Attributes = MemberAttributes.Public | MemberAttributes.Final | MemberAttributes.Static,
                    ReturnType = new CodeTypeReference(typeof(Layout).FullName)
                };

                var @ref = new CodeObjectCreateExpression(new CodeTypeReference(typeof(Layout).FullName));

                var layout = new CodeMethodReturnStatement(@ref);

                method.Statements.Add(layout);

                var provider = new Microsoft.CSharp.CSharpCodeProvider();
                if (true)
                {
                    var writer = new StringWriter();
                    provider.GenerateCodeFromMember(method, writer, new CodeGeneratorOptions
                    {
                        BracingStyle = "C" //New line style
                    });

                    throw new Exception(writer.ToString());
                }
                return false;
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
