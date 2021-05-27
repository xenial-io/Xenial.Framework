using System;
using System.Collections.Generic;
using System.Linq;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Templates.ActionContainers;
using DevExpress.ExpressApp.Updating;

using Xenial.FeatureCenter.Module.BusinessObjects;
using Xenial.FeatureCenter.Module.BusinessObjects.Badges;
using Xenial.FeatureCenter.Module.BusinessObjects.Editors;
using Xenial.FeatureCenter.Module.BusinessObjects.ModelBuilders;
using Xenial.FeatureCenter.Module.Model.GeneratorUpdaters;
using Xenial.FeatureCenter.Module.Updaters;

using Xenial.Framework;
using Xenial.Framework.Badges;
using Xenial.Framework.StepProgressEditors;
using Xenial.Framework.TokenEditors;
using Xenial.Framework.WebView;

namespace Xenial.FeatureCenter.Module
{
    public sealed class FeatureCenterModule : XenialModuleBase
    {
        public static AvailablePlatform? CurrentPlatform { get; set; }

        protected override ModuleTypeList GetRequiredModuleTypesCore()
            => base.GetRequiredModuleTypesCore()
                .AndModuleTypes(
                    typeof(ConditionalAppearanceModule),
                    typeof(XenialWebViewModule),
                    typeof(XenialTokenEditorsModule),
                    typeof(XenialStepProgressEditorsModule),
                    typeof(XenialBadgesModule)
                );

        protected override IEnumerable<Type> GetDeclaredExportedTypes()
            => base.GetDeclaredExportedTypes()
            .Concat(ModelTypeList.NonPersistentTypes)
            .Concat(ModelTypeList.PersistentTypes);

        protected override IEnumerable<Type> GetDeclaredControllerTypes()
            => base.GetDeclaredControllerTypes()
                .UseXenialSingletonControllers().Concat(new[]
                {
                    typeof(BadgesFeatureController)
                });

        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
            => base.GetModuleUpdaters(objectSpace, versionFromDB)
                .Concat(new ModuleUpdater[]
                {
                    new SeedModuleUpdater(objectSpace, versionFromDB)
                });

        public override void AddGeneratorUpdaters(ModelNodesGeneratorUpdaters updaters)
        {
            base.AddGeneratorUpdaters(updaters);

            updaters.Add(new FeatureCenterNavigationItemNodesUpdater());

            updaters
                .UseSingletonNavigationItems()
                .UseNoViewsGeneratorUpdater()
                .UseDetailViewLayoutBuilders()
                .UseNavigationOptions(o => o with
                {
                    NavigationStyle = NavigationStyle.Accordion
                })
                .UseAppOptions(o => o with
                {
                    Title = "Xenial.FeatureCenter",
                    Company = "Fa. Manuel Grundner, xenial.io",
                    Logo = "xenial",
                    Copyright = $"© Fa. Manuel Grundner, xenial.io 2018-{DateTime.Today.Year}<br>{Chipmunkify()}"
                });
        }

        private static string Chipmunkify(int size = 20)
        {
            var colors = new[]
            {
                "red",
                "orange",
                "yellow",
                "green",
                "indigo",
                "violet",
            };
            return string.Join(string.Empty, colors.Select(color => $"<size={size}><color={color}>🐿</color></size>"));
        }

        public override void Setup(XafApplication application)
        {
            application.UseNonPersistentSingletons();

            base.Setup(application);
        }

        public override void CustomizeTypesInfo(ITypesInfo typesInfo)
        {
            base.CustomizeTypesInfo(typesInfo);

            typesInfo.RemoveXafViewsFromApplicationModel();
            typesInfo.RemoveXpoViewsFromApplicationModel();

            typesInfo
                .CreateModelBuilder<FeatureCenterEditorsBaseObjectModelBuilder>()
                .Build();

            typesInfo
                .CreateModelBuilder<FeatureCenterModelBuildersBaseObjectModelBuilder>()
                .Build();

            #region Editors

            typesInfo
                .CreateModelBuilder<StepProgressBarEnumEditorDemoModelBuilder>()
                .Build();

            typesInfo
                .CreateModelBuilder<TokenStringEditorDemoModelBuilder>()
                .Build();

            typesInfo
                .CreateModelBuilder<TokenObjectsEditorDemoModelBuilder>()
                .Build();

            typesInfo
                .CreateModelBuilder<WebViewUriEditorDemoModelBuilder>()
                .Build();

            typesInfo
                .CreateModelBuilder<WebViewHtmlStringEditorDemoModelBuilder>()
                .Build();

            #endregion

            #region ModelBuilders

            typesInfo
                .CreateModelBuilder<ModelBuilderIntroductionDemoBuilderInfra>()
                .Build();

            typesInfo
                .CreateModelBuilder<ModelBuilderIntroductionDemoBuilder>()
                .Build();

            #endregion


            #region Badges

            typesInfo
                .CreateModelBuilder<FeatureCenterBadgesBaseObjectModelBuilder>()
                .Build();

            typesInfo
                .CreateModelBuilder<BadgesIntroductionDemoModelBuilder>()
                .Build();

            #endregion
        }

        public static string[] VersionInformation = new[]
        {
            $"Xenial: {XenialVersion.Version}/{XenialVersion.Branch}",
            $"Demo: {XenialVersion.Version}/{XenialVersion.Branch}",
            $"DxVersion: {XenialVersion.DxVersion}"
        };
    }
}
