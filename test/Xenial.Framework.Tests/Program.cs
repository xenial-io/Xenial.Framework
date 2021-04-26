using System;
using System.Threading.Tasks;

using Xenial.Framework.Tests.Generators;
using Xenial.Framework.Tests.Layouts.ColumnItems;
using Xenial.Framework.Tests.Layouts.ColumnItems.Properties;
using Xenial.Framework.Tests.Layouts.Items;
using Xenial.Framework.Tests.Model.Core;
using Xenial.Framework.Tests.Model.GeneratorUpdaters;
using Xenial.Framework.Tests.ModelBuilders;
using Xenial.Framework.Tests.Utils;
using Xenial.Framework.Tests.Utils.Slugger;
using Xenial.Utils.Tests;

using static Xenial.Tasty;

namespace Xenial.Framework.Tests
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            Describe(nameof(Xenial), () =>
            {
                SourceGeneratorFacts.SourceGeneratorTests();

                NullDiffsStoreFacts.NullDiffsStoreTests();
                ModelOptionsNodesGeneratorUpdaterFacts.ModelOptionsNodesGeneratorUpdaterTests();
                ModuleTypeListExtentionsFacts.ModuleTypeListExtentionsTests();
                ExpressionHelperFacts.ExpressionHelperTests();

                ModelBuilderFacts.ModelBuilderTests();
                ModelBuilderExtensionFacts.ModelBuilderExtensionTests();
                PropertyBuilderFacts.PropertyBuilderTests();
                PropertyBuilderExtensionsFacts.PropertyBuilderExtensionsTests();

                BuilderManagerFacts.BuilderManagerTests();
                XafBuilderManagerFacts.XafBuilderManagerTests();

                SlugerFacts.SluggerTests();
                ResourceUtilFacts.ResourceExtentionsTests();

                Describe("Layouts", () =>
                {
                    GeneralLayoutFacts.GeneralLayoutTests();

                    BasicLayoutFacts.BasicLayoutTests();
                    LayoutPropertyEditorItemFacts.LayoutPropertyEditorItemTests();
                    LayoutEmptySpaceItemFacts.LayoutEmptySpaceItemTests();

                    LayoutGroupItemFacts.LayoutGroupItemTests();
                    LayoutGroupItemFacts.LayoutGroupItemChildrenTests();
                    LayoutTabGroupItemFacts.LayoutTabGroupItemTests();
                    LayoutTabbedGroupItemFacts.LayoutTabbedGroupItemTests();
                    LayoutTabbedGroupItemFacts.LayoutTabbedGroupItemChildTests();

                    TreeBuilderFacts.TreeBuilderTests();

                    LayoutIntegrationFacts.LayoutIntegrationTests();
                });

                Describe("Columns", () =>
                {
                    BasicColumnsFacts.BasicColumnsTests();
                    ColumnsIntegrationFacts.ColumnsIntegrationTests();
                    BasicColumnPropertiesFacts.ColumnPropertiesTests();
                });
            });

            return await Run(args);
        }
    }
}
