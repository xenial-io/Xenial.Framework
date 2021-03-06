﻿using System;
using System.Collections.Generic;

using DevExpress.Xpo;

using static Xenial.FeatureCenter.Module.HtmlBuilders.HtmlBuilder;

namespace Xenial.FeatureCenter.Module.BusinessObjects.Badges
{
    public abstract class FeatureCenterBadgesBaseObject : FeatureCenterBaseObjectId
    {
        public FeatureCenterBadgesBaseObject(Session session) : base(session) { }

        public string Introduction => BuildHtml("Introduction", BuildIntroductionHtml());

        protected virtual string BuildIntroductionHtml()
            => MarkDownBlock.FromResourceString("BusinessObjects/Badges/BadgesIntroductionDemo.Introduction.md").ToString();

        public string Installation => BuildHtml("Installation", BuildInstallationHtml());

        protected virtual string BuildInstallationHtml()
            => NugetInstallSection(GetRequiredModules()).ToString();

        protected virtual IEnumerable<RequiredNuget> GetRequiredModules() => new[]
        {
            new RequiredNuget("Badges"),
            new RequiredNuget("Badges", AvailablePlatform.Win)
        };
    }
}
