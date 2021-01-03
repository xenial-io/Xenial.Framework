﻿using System;
using System.Collections.Generic;
using System.Linq;

using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;

using Xenial.Framework.Layouts;
using Xenial.Framework.Layouts.Items;
using Xenial.Framework.Layouts.Items.Base;
using Xenial.Framework.Layouts.Items.LeafNodes;
using Xenial.Framework.Layouts.Items.PubTernal;

namespace Xenial.Framework.Model.GeneratorUpdaters
{
    /// <summary>
    /// Class ModelDetailViewLayoutNodesGeneratorUpdater.
    /// Implements the <see cref="DevExpress.ExpressApp.Model.ModelNodesGeneratorUpdater{DevExpress.ExpressApp.Model.NodeGenerators.ModelDetailViewLayoutNodesGenerator}" />
    /// </summary>
    /// <seealso cref="DevExpress.ExpressApp.Model.ModelNodesGeneratorUpdater{DevExpress.ExpressApp.Model.NodeGenerators.ModelDetailViewLayoutNodesGenerator}" />
    /// <autogeneratedoc />
    public class ModelDetailViewLayoutNodesGeneratorUpdater : ModelNodesGeneratorUpdater<ModelDetailViewLayoutNodesGenerator>
    {
        /// <summary>
        /// Updates the Application Model node content generated by the Nodes Generator, specified by the <see cref="T:DevExpress.ExpressApp.Model.ModelNodesGeneratorUpdater`1" /> class' type parameter.
        /// </summary>
        /// <param name="modelNode">A ModelNode Application Model node to be updated.</param>
        /// <exception cref="NotImplementedException"></exception>
        /// <autogeneratedoc />
#pragma warning disable CA1725 //match identitfier of base class -> would conflict with nodes
        public override void UpdateNode(ModelNode modelNode)
#pragma warning restore CA1725 //match identitfier of base class -> would conflict with nodes
        {
            if (modelNode is IModelViewLayout modelViewLayout)
            {
                if (modelViewLayout.Parent is IModelDetailView modelDetailView)
                {
                    //TODO: check IModelObjectGeneratedView

                    if (modelDetailView.Equals(modelDetailView.ModelClass.DefaultDetailView))
                    {
                        //TODO: multiple views and attributes
                        var attribute = modelDetailView.ModelClass.TypeInfo.FindAttribute<DetailViewLayoutBuilderAttribute>();
                        //TODO: Factory
                        if (attribute.BuildLayoutDelegate is not null)
                        {
                            var builder = attribute.BuildLayoutDelegate;
                            var layout = builder.Invoke()
                                ?? throw new InvalidOperationException($"LayoutBuilder on Type '{modelDetailView.ModelClass.TypeInfo.Type}' for View '{modelDetailView.Id}' must return an object of Type '{typeof(Layout)}'");

                            modelViewLayout.ClearNodes();

                            var modelMainNode = modelViewLayout
                                .AddNode<IModelLayoutGroup>(ModelDetailViewLayoutNodesGenerator.MainLayoutGroupName)
                                ?? throw new InvalidOperationException($"Cannot generate 'Main' node on Type '{modelDetailView.ModelClass.TypeInfo.Type}' for View '{modelDetailView.Id}'");

                            foreach (var groupItemNode in VisitNodes<LayoutGroupItem>(layout))
                            {
                                var modelLayoutGroup = modelMainNode.AddNode<IModelLayoutGroup>(groupItemNode.Id);

                                if (modelLayoutGroup is IModelNode genericModelNode)
                                {
                                    MapModelNode(genericModelNode, groupItemNode);
                                }

                                if (modelLayoutGroup is IModelViewLayoutElement modelViewLayoutElement)
                                {
                                    MapModelViewLayoutElement(modelViewLayoutElement, groupItemNode);
                                }

                                if (modelLayoutGroup is IModelLayoutElementWithCaptionOptions modelLayoutElementWithCaptionOptions)
                                {
                                    MapLayoutElementWithCaptionOptions(modelLayoutElementWithCaptionOptions, groupItemNode);
                                }

                                if (modelLayoutGroup is IModelLayoutElementWithCaption modelLayoutElementWithCaption)
                                {
                                    MapCaption(modelLayoutElementWithCaption, groupItemNode);
                                }

                                if (modelLayoutGroup is ISupportControlAlignment modelSupportControlAlignment)
                                {
                                    MapSupportControlAlignment(modelSupportControlAlignment, groupItemNode);
                                }

                                if (modelLayoutGroup is IModelToolTip modelToolTip)
                                {
                                    MapModelToolTip(modelToolTip, groupItemNode);
                                }

                                if (modelLayoutGroup is IModelToolTipOptions modelToolTipOptions)
                                {
                                    MapModelToolTipOptions(modelToolTipOptions, groupItemNode);
                                }

                                MapLayoutGroup(modelLayoutGroup, groupItemNode);

                                if (groupItemNode.LayoutGroupOptions is not null)
                                {
                                    groupItemNode.LayoutGroupOptions(modelLayoutGroup);
                                }
                            }

                            foreach (var tabGroupItemNode in VisitNodes<LayoutTabGroupItem>(layout))
                            {
                                var modelLayoutViewItem = modelMainNode.AddNode<IModelLayoutGroup>(tabGroupItemNode.Id);

                                if (modelLayoutViewItem is IModelNode genericModelNode)
                                {
                                    MapModelNode(genericModelNode, tabGroupItemNode);
                                }

                                if (modelLayoutViewItem is IModelViewLayoutElement modelViewLayoutElement)
                                {
                                    MapModelViewLayoutElement(modelViewLayoutElement, tabGroupItemNode);
                                }

                                if (modelLayoutViewItem is IModelLayoutElementWithCaptionOptions modelLayoutElementWithCaptionOptions)
                                {
                                    MapLayoutElementWithCaptionOptions(modelLayoutElementWithCaptionOptions, tabGroupItemNode);
                                }

                                if (modelLayoutViewItem is IModelLayoutElementWithCaption modelLayoutElementWithCaption)
                                {
                                    MapCaption(modelLayoutElementWithCaption, tabGroupItemNode);
                                }

                                if (modelLayoutViewItem is ISupportControlAlignment modelSupportControlAlignment)
                                {
                                    MapSupportControlAlignment(modelSupportControlAlignment, tabGroupItemNode);
                                }

                                if (modelLayoutViewItem is IModelToolTip modelToolTip)
                                {
                                    MapModelToolTip(modelToolTip, tabGroupItemNode);
                                }

                                if (modelLayoutViewItem is IModelToolTipOptions modelToolTipOptions)
                                {
                                    MapModelToolTipOptions(modelToolTipOptions, tabGroupItemNode);
                                }

                                MapLayoutGroup(modelLayoutViewItem, tabGroupItemNode);

                                if (tabGroupItemNode.LayoutGroupOptions is not null)
                                {
                                    tabGroupItemNode.LayoutGroupOptions(modelLayoutViewItem);
                                }
                            }

                            foreach (var tabbedGroupItemNode in VisitNodes<LayoutTabbedGroupItem>(layout))
                            {
                                var modelTabbedGroup = modelMainNode.AddNode<IModelTabbedGroup>(tabbedGroupItemNode.Id);

                                if (modelTabbedGroup is IModelNode genericModelNode)
                                {
                                    MapModelNode(genericModelNode, tabbedGroupItemNode);
                                }

                                if (modelTabbedGroup is IModelViewLayoutElement modelViewLayoutElement)
                                {
                                    MapModelViewLayoutElement(modelViewLayoutElement, tabbedGroupItemNode);
                                }

                                if (modelTabbedGroup is IModelLayoutElementWithCaptionOptions modelLayoutElementWithCaptionOptions)
                                {
                                    MapLayoutElementWithCaptionOptions(modelLayoutElementWithCaptionOptions, tabbedGroupItemNode);
                                }

                                if (modelTabbedGroup is IModelLayoutElementWithCaption modelLayoutElementWithCaption)
                                {
                                    MapCaption(modelLayoutElementWithCaption, tabbedGroupItemNode);
                                }

                                MapTabbedLayoutGroup(modelTabbedGroup, tabbedGroupItemNode);

                                if (tabbedGroupItemNode.TabbedGroupOptions is not null)
                                {
                                    tabbedGroupItemNode.TabbedGroupOptions(modelTabbedGroup);
                                }
                            }

                            foreach (var emptySpaceItemNode in VisitNodes<LayoutEmptySpaceItem>(layout))
                            {
                                var modelLayoutViewItem = modelMainNode.AddNode<IModelLayoutViewItem>(emptySpaceItemNode.Id);
                                if (modelLayoutViewItem is IModelNode genericModelNode)
                                {
                                    MapModelNode(genericModelNode, emptySpaceItemNode);
                                }

                                if (modelLayoutViewItem is IModelLayoutItem modelLayoutItem)
                                {
                                    MapModelLayoutItem(modelLayoutItem, emptySpaceItemNode);
                                }

                                if (modelLayoutViewItem is IModelViewLayoutElement modelViewLayoutElement)
                                {
                                    MapModelViewLayoutElement(modelViewLayoutElement, emptySpaceItemNode);
                                }

                                if (modelLayoutViewItem is ISupportControlAlignment modelSupportControlAlignment)
                                {
                                    MapSupportControlAlignment(modelSupportControlAlignment, emptySpaceItemNode);
                                }
                            }

                            foreach (var layoutViewItemNode in VisitNodes<LayoutViewItem>(layout))
                            {
                                var modelLayoutViewItem = modelMainNode.AddNode<IModelLayoutViewItem>(layoutViewItemNode.Id);
                                modelLayoutViewItem.ViewItem = modelDetailView.Items.OfType<IModelViewItem>().FirstOrDefault(m => m.Id == layoutViewItemNode.ViewItemId);

                                if (modelLayoutViewItem is IModelNode genericModelNode)
                                {
                                    MapModelNode(genericModelNode, layoutViewItemNode);
                                }

                                if (modelLayoutViewItem is IModelLayoutItem modelLayoutItem)
                                {
                                    MapModelLayoutItem(modelLayoutItem, layoutViewItemNode);
                                }

                                if (modelLayoutViewItem is IModelViewLayoutElement modelViewLayoutElement)
                                {
                                    MapModelViewLayoutElement(modelViewLayoutElement, layoutViewItemNode);
                                }

                                if (modelLayoutViewItem is ISupportControlAlignment modelSupportControlAlignment)
                                {
                                    MapSupportControlAlignment(modelSupportControlAlignment, layoutViewItemNode);
                                }

                                if (modelLayoutViewItem is IModelToolTip modelToolTip)
                                {
                                    MapModelToolTip(modelToolTip, layoutViewItemNode);
                                }

                                if (modelLayoutViewItem is IModelToolTipOptions modelToolTipOptions)
                                {
                                    MapModelToolTipOptions(modelToolTipOptions, layoutViewItemNode);
                                }

                                if (modelLayoutViewItem is IModelLayoutElementWithCaptionOptions modelLayoutElementWithCaptionOptions)
                                {
                                    MapLayoutElementWithCaptionOptions(modelLayoutElementWithCaptionOptions, layoutViewItemNode);
                                }
                                else if (modelLayoutViewItem.ViewItem is IModelLayoutElementWithCaptionOptions modelLayoutElementWithCaptionOptions2)
                                {
                                    MapLayoutElementWithCaptionOptions(modelLayoutElementWithCaptionOptions2, layoutViewItemNode);
                                }

                                if (modelLayoutViewItem is IModelLayoutElementWithCaption modelLayoutElementWithCaption)
                                {
                                    MapCaption(modelLayoutElementWithCaption, layoutViewItemNode);
                                }
                                else if (modelLayoutViewItem.ViewItem is IModelLayoutElementWithCaption modelLayoutElementWithCaption2)
                                {
                                    MapCaption(modelLayoutElementWithCaption2, layoutViewItemNode);
                                }
                                else if (modelLayoutViewItem.ViewItem is not null)
                                {
                                    if (layoutViewItemNode.Caption is not null)
                                    {
                                        modelLayoutViewItem.ViewItem.Caption = layoutViewItemNode.Caption;
                                    }
                                }

                                if (layoutViewItemNode.ViewItemOptions is not null)
                                {
                                    var modelViewItem = modelDetailView
                                        .Items[layoutViewItemNode.ViewItemId];

                                    if (modelViewItem is not null)
                                    {
                                        layoutViewItemNode.ViewItemOptions(modelViewItem);
                                    }
                                }

                                if (layoutViewItemNode is LayoutPropertyEditorItem layoutPropertyEditorItem
                                    && layoutPropertyEditorItem.PropertyEditorOptions is not null)
                                {
                                    var modelViewItem = modelDetailView
                                        .Items[layoutPropertyEditorItem.PropertyEditorId];

                                    if (modelViewItem is IModelPropertyEditor modelPropertyEditor)
                                    {
                                        layoutPropertyEditorItem.PropertyEditorOptions(modelPropertyEditor);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {

                    }
                }
            }

            static IEnumerable<TItem> VisitNodes<TItem>(LayoutItemNode node)
                where TItem : LayoutItemNode
            {
                if (node is TItem targetNode)
                {
                    yield return targetNode;
                }

                if (node is IEnumerable<LayoutItemNode> items)
                {
                    foreach (var item in items)
                    {
                        foreach (var nestedItem in VisitNodes<TItem>(item))
                        {
                            yield return nestedItem;
                        }
                    }
                }
            }

            static void MapCaption(
                IModelLayoutElementWithCaption modelLayoutElementWithCaption,
                ILayoutElementWithCaption layoutViewItemNode
            )
            {
                if (layoutViewItemNode.Caption is not null)
                {
                    modelLayoutElementWithCaption.Caption =
                        layoutViewItemNode.Caption ?? modelLayoutElementWithCaption.Caption;
                }
            }

            static void MapModelViewLayoutElement(
                IModelViewLayoutElement modelModelViewLayoutElement,
                LayoutItemNode layoutViewItemNode
            )
            {
                if (layoutViewItemNode.Id is not null)
                {
                    modelModelViewLayoutElement.Id =
                        layoutViewItemNode.Id ?? modelModelViewLayoutElement.Id;
                }

                if (layoutViewItemNode.RelativeSize is not null)
                {
                    modelModelViewLayoutElement.RelativeSize =
                        layoutViewItemNode.RelativeSize ?? modelModelViewLayoutElement.RelativeSize;
                }
            }

            static void MapLayoutElementWithCaptionOptions(
                IModelLayoutElementWithCaptionOptions modelLayoutElementWithCaption,
                ILayoutElementWithCaptionOptions layoutViewItemNode
            )
            {
                if (layoutViewItemNode.ShowCaption is not null)
                {
                    modelLayoutElementWithCaption.ShowCaption =
                        layoutViewItemNode.ShowCaption ?? modelLayoutElementWithCaption.ShowCaption;
                }

                if (layoutViewItemNode.CaptionLocation is not null)
                {
                    modelLayoutElementWithCaption.CaptionLocation =
                        layoutViewItemNode.CaptionLocation ?? modelLayoutElementWithCaption.CaptionLocation;
                }

                if (layoutViewItemNode.CaptionHorizontalAlignment is not null)
                {
                    modelLayoutElementWithCaption.CaptionHorizontalAlignment =
                        layoutViewItemNode.CaptionHorizontalAlignment ?? modelLayoutElementWithCaption.CaptionHorizontalAlignment;
                }

                if (layoutViewItemNode.CaptionVerticalAlignment is not null)
                {
                    modelLayoutElementWithCaption.CaptionVerticalAlignment =
                        layoutViewItemNode.CaptionVerticalAlignment ?? modelLayoutElementWithCaption.CaptionVerticalAlignment;
                }

                if (layoutViewItemNode.CaptionWordWrap is not null)
                {
                    modelLayoutElementWithCaption.CaptionWordWrap =
                        layoutViewItemNode.CaptionWordWrap ?? modelLayoutElementWithCaption.CaptionWordWrap;
                }
            }

            static void MapModelNode(
                IModelNode genericModelNode,
                LayoutItemNode genericLayoutItemNode
            )
            {
                if (genericLayoutItemNode.Index is not null)
                {
                    genericModelNode.Index =
                        genericLayoutItemNode.Index ?? genericModelNode.Index;
                }
            }

            static void MapSupportControlAlignment(
                ISupportControlAlignment modelSupportControlAlignment,
                ILayoutItemNodeWithAlign layoutItemNodeWithAlign
            )
            {
                if (layoutItemNodeWithAlign.HorizontalAlign is not null)
                {
                    modelSupportControlAlignment.HorizontalAlign =
                        layoutItemNodeWithAlign.HorizontalAlign ?? modelSupportControlAlignment.HorizontalAlign;
                }

                if (layoutItemNodeWithAlign.VerticalAlign is not null)
                {
                    modelSupportControlAlignment.VerticalAlign =
                        layoutItemNodeWithAlign.VerticalAlign ?? modelSupportControlAlignment.VerticalAlign;
                }
            }

            static void MapModelLayoutItem(
                IModelLayoutItem modelLayoutItem,
                LayoutItemLeaf layoutItemLeaf
            )
            {
                if (layoutItemLeaf.SizeConstraintsType is not null)
                {
                    modelLayoutItem.SizeConstraintsType =
                        layoutItemLeaf.SizeConstraintsType ?? modelLayoutItem.SizeConstraintsType;
                }

                if (layoutItemLeaf.MinSize is not null)
                {
                    modelLayoutItem.MinSize =
                        layoutItemLeaf.MinSize ?? modelLayoutItem.MinSize;
                }

                if (layoutItemLeaf.MaxSize is not null)
                {
                    modelLayoutItem.MaxSize =
                        layoutItemLeaf.MaxSize ?? modelLayoutItem.MaxSize;
                }
            }

            static void MapModelToolTip(
                IModelToolTip modelToolTip,
                ILayoutToolTip layoutViewItemNode
            )
            {
                if (layoutViewItemNode.ToolTip is not null)
                {
                    modelToolTip.ToolTip =
                        layoutViewItemNode.ToolTip ?? modelToolTip.ToolTip;
                }
            }

            static void MapModelToolTipOptions(
                IModelToolTipOptions modelToolTipOptions,
                ILayoutToolTipOptions layoutViewItemNode
            )
            {
                if (layoutViewItemNode.ToolTipTitle is not null)
                {
                    modelToolTipOptions.ToolTipTitle =
                        layoutViewItemNode.ToolTipTitle ?? modelToolTipOptions.ToolTipTitle;
                }

                if (layoutViewItemNode.ToolTipIconType is not null)
                {
                    modelToolTipOptions.ToolTipIconType =
                        layoutViewItemNode.ToolTipIconType ?? modelToolTipOptions.ToolTipIconType;
                }
            }

            void MapLayoutGroup(IModelLayoutGroup modelLayoutGroup, ILayoutGroupItem groupItemNode)
            {
                modelLayoutGroup.Direction = groupItemNode.Direction;

                if (groupItemNode.ImageName is not null)
                {
                    modelLayoutGroup.ImageName =
                        groupItemNode.ImageName ?? modelLayoutGroup.ImageName;
                }

                if (groupItemNode.IsCollapsibleGroup is not null)
                {
                    modelLayoutGroup.IsCollapsibleGroup =
                        groupItemNode.IsCollapsibleGroup ?? modelLayoutGroup.IsCollapsibleGroup;
                }
            }

            void MapTabbedLayoutGroup(IModelTabbedGroup modelTabbedGroup, LayoutTabbedGroupItem tabbedGroupItemNode)
            {
                modelTabbedGroup.Direction = tabbedGroupItemNode.Direction;

                if (tabbedGroupItemNode.MultiLine is not null)
                {
                    modelTabbedGroup.MultiLine =
                        tabbedGroupItemNode.MultiLine ?? modelTabbedGroup.MultiLine;
                }
            }
        }

    }
}
