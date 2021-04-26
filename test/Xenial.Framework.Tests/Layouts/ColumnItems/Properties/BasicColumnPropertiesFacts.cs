using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

using DevExpress.Data;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;

using Xenial.Framework.Layouts.ColumnItems;
using Xenial.Framework.Tests.Assertions;

using static Xenial.Framework.Tests.Layouts.TestModelApplicationFactory;
using static Xenial.Tasty;

namespace Xenial.Framework.Tests.Layouts.ColumnItems.Properties
{
    public static class BasicColumnPropertiesFacts
    {
        public static void ColumnPropertiesTests() => Describe(nameof(Column), () =>
        {
            var faker = new Faker();
            Describe("Properties", () =>
            {
                It($"{nameof(IModelColumn)}", () =>
                {
                    var width = faker.Random.Number(100);
                    var sortOrder = faker.Random.Enum<ColumnSortOrder>();
                    var groupInterval = faker.Random.Enum<GroupInterval>();
                    var sortIndex = faker.Random.Number(100);
                    var groupIndex = faker.Random.Number(100);

                    var listView = CreateListViewWithColumns(b => new()
                    {
                        b.Column(m => m.StringProperty) with
                        {
                            Width = width,
                            SortOrder = sortOrder,
                            SortIndex = sortIndex,
                            GroupIndex = groupIndex,
                            GroupInterval = groupInterval
                        },
                    });

                    listView.AssertColumnProperties<IModelColumn, IModelColumn>((e) => new()
                    {
                        [e.Property(m => m.Width)] = width,
                        [e.Property(m => m.SortOrder)] = sortOrder,
                        [e.Property(m => m.SortIndex)] = sortIndex,
                        [e.Property(m => m.GroupIndex)] = groupIndex,
                        [e.Property(m => m.GroupInterval)] = groupInterval
                    });
                });

                It($"{nameof(IModelMemberViewItem)}", () =>
                {
                    var dataSourceProperty = faker.Random.String2(100);
                    var dataSourceCriteriaProperty = faker.Random.String2(100);
                    var propertyName = faker.Random.String2(100);
                    var maxLength = faker.Random.Number(100);
                    var imageEditorCustomHeight = faker.Random.Number(100);
                    var imageEditorMode = faker.Random.Enum<ImageEditorMode>();
                    var imageEditorFixedWidth = faker.Random.Number(100);
                    var imageEditorFixedHeight = faker.Random.Number(100);

                    var listView = CreateListViewWithColumns(b => new()
                    {
                        b.Column(m => m.StringProperty) with
                        {
                            DataSourceProperty = dataSourceProperty,
                            DataSourceCriteriaProperty = dataSourceCriteriaProperty,
                            PropertyName = propertyName,
                            MaxLength = maxLength,
                            ImageEditorCustomHeight = imageEditorCustomHeight,
                            ImageEditorMode = imageEditorMode,
                            ImageEditorFixedWidth = imageEditorFixedWidth,
                            ImageEditorFixedHeight = imageEditorFixedHeight
                        },
                    });

                    listView.AssertColumnProperties<IModelColumn, IModelMemberViewItem>((e) => new()
                    {
                        [e.Property(m => m.DataSourceProperty)] = dataSourceProperty,
                        [e.Property(m => m.DataSourceCriteriaProperty)] = dataSourceCriteriaProperty,
                        [e.Property(m => m.PropertyName)] = propertyName,
                        [e.Property(m => m.MaxLength)] = maxLength,
                        [e.Property(m => m.ImageEditorCustomHeight)] = imageEditorCustomHeight,
                        [e.Property(m => m.ImageEditorMode)] = imageEditorMode,
                        [e.Property(m => m.ImageEditorFixedWidth)] = imageEditorFixedWidth,
                        [e.Property(m => m.ImageEditorFixedHeight)] = imageEditorFixedHeight
                    });
                });

                It($"{nameof(IModelLayoutElement)}", () =>
                {
                    var id = faker.Random.String2(100);
                    var index = faker.Random.Int(100);

                    var listView = CreateListViewWithColumns(b => new()
                    {
                        b.Column(m => m.StringProperty) with
                        {
                            Id = id,
                            Index = index
                        },
                    });

                    listView.AssertColumnProperties<IModelColumn, IModelLayoutElement>((e) => new()
                    {
                        [e.Property(m => m.Id)] = id,
                        [e.Property(m => m.Index)] = index,
                    });
                });

                It($"{nameof(IModelToolTip)}", () =>
                {
                    var tooltip = faker.Random.String2(100);

                    var listView = CreateListViewWithColumns(b => new()
                    {
                        b.Column(m => m.StringProperty) with
                        {
                            ToolTip = tooltip
                        },
                    });

                    listView.AssertColumnProperties<IModelColumn, IModelToolTip>((e) => new()
                    {
                        [e.Property(m => m.ToolTip)] = tooltip,
                    });
                });

                It($"{nameof(IModelCommonMemberViewItem)}", () =>
                {
                    var editMask = faker.Random.String2(100);
                    var propertyEditorType = faker.PickRandom(typeof(BasicColumnPropertiesFacts).Assembly.GetTypes());
                    var immediatePostData = faker.Random.Bool();
                    var lookupEditorMode = faker.Random.Enum<LookupEditorMode>();
                    var predefinedValues = faker.Random.String2(100);
                    var imageSizeMode = faker.Random.Enum<ImageSizeMode>();
                    var imageForFalse = faker.Random.String2(100);
                    var imageForTrue = faker.Random.String2(100);
                    var captionForFalse = faker.Random.String2(100);
                    var captionForTrue = faker.Random.String2(100);
                    var allowClear = faker.Random.Bool();
                    var dataSourcePropertyIsNullCriteria = faker.Random.String2(100);
                    var dataSourcePropertyIsNullMode = faker.Random.Enum<DataSourcePropertyIsNullMode>();
                    var lookupProperty = faker.Random.String2(100);
                    var allowEdit = faker.Random.Bool();
                    var rowCount = faker.Random.Int(100);
                    var caption = faker.Random.String2(100);
                    var displayFormat = faker.Random.String2(100);
                    var isPassword = faker.Random.Bool();
                    var editMaskType = faker.Random.Enum<EditMaskType>();
                    var dataSourceCriteria = faker.Random.String2(100);
                    var nullText = faker.Random.String2(100);

                    var listView = CreateListViewWithColumns(b => new()
                    {
                        b.Column(m => m.StringProperty) with
                        {
                            EditMask = editMask,
                            PropertyEditorType = propertyEditorType,
                            ImmediatePostData = immediatePostData,
                            LookupEditorMode = lookupEditorMode,
                            PredefinedValues = predefinedValues,
                            ImageSizeMode = imageSizeMode,
                            ImageForFalse = imageForFalse,
                            ImageForTrue = imageForTrue,
                            CaptionForFalse = captionForFalse,
                            CaptionForTrue = captionForTrue,
                            AllowClear = allowClear,
                            DataSourcePropertyIsNullCriteria = dataSourcePropertyIsNullCriteria,
                            DataSourcePropertyIsNullMode = dataSourcePropertyIsNullMode,
                            LookupProperty = lookupProperty,
                            AllowEdit = allowEdit,
                            RowCount = rowCount,
                            Caption = caption,
                            DisplayFormat = displayFormat,
                            IsPassword = isPassword,
                            EditMaskType = editMaskType,
                            DataSourceCriteria = dataSourceCriteria,
                            NullText = nullText,
                        },
                    });

                    listView.AssertColumnProperties<IModelColumn, IModelCommonMemberViewItem>((e) => new()
                    {
                        [e.Property(m => m.EditMask)] = editMask,
                        [e.Property(m => m.PropertyEditorType)] = propertyEditorType,
                        [e.Property(m => m.ImmediatePostData)] = immediatePostData,
                        [e.Property(m => m.LookupEditorMode)] = lookupEditorMode,
                        [e.Property(m => m.PredefinedValues)] = predefinedValues,
                        [e.Property(m => m.ImageSizeMode)] = imageSizeMode,
                        [e.Property(m => m.ImageForFalse)] = imageForFalse,
                        [e.Property(m => m.ImageForTrue)] = imageForTrue,
                        [e.Property(m => m.CaptionForFalse)] = captionForFalse,
                        [e.Property(m => m.CaptionForTrue)] = captionForTrue,
                        [e.Property(m => m.AllowClear)] = allowClear,
                        [e.Property(m => m.DataSourcePropertyIsNullCriteria)] = dataSourcePropertyIsNullCriteria,
                        [e.Property(m => m.DataSourcePropertyIsNullMode)] = dataSourcePropertyIsNullMode,
                        [e.Property(m => m.LookupProperty)] = lookupProperty,
                        [e.Property(m => m.AllowEdit)] = allowEdit,
                        [e.Property(m => m.RowCount)] = rowCount,
                        [e.Property(m => m.Caption)] = caption,
                        [e.Property(m => m.DisplayFormat)] = displayFormat,
                        [e.Property(m => m.IsPassword)] = isPassword,
                        [e.Property(m => m.EditMaskType)] = editMaskType,
                        [e.Property(m => m.DataSourceCriteria)] = dataSourceCriteria,
                        [e.Property(m => m.NullText)] = nullText,
                    });
                });
            });
        });
    }
}
