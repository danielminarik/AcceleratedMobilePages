﻿using System;
using System.Data;
using CMS.SiteProvider;
using CMS.DocumentEngine;
using CMS.Localization;
using CMS;
using CMS.Base.Web.UI;
using CMS.Helpers;
using CMS.UIControls;

[assembly: RegisterCustomClass("AmpFilterListExtender", typeof(AcceleratedMobilePages.AmpFilterListExtender))]

namespace AcceleratedMobilePages
{
    class AmpFilterListExtender : ControlExtender<UniGrid>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public override void OnInit()
        {
            Control.OnExternalDataBound += Control_OnExternalDatabound;
            Control.OnAfterRetrieveData += Control_OnAfterRetrieveData;
            Control.ShowObjectMenu = false;
        }


        /// <summary>
        /// Returns data only for current site context
        /// </summary>
        /// <param name="data">Data set</param>
        private DataSet Control_OnAfterRetrieveData(DataSet data)
        {
            return AmpFilterInfoProvider.GetAmpFilters().WhereEquals("SiteID", SiteContext.CurrentSiteID).Result;
        }


        /// <summary>
        /// Handles the Unigrid control's OnExternalDataBound event.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="sourceName">Source name</param>
        /// <param name="parameter">Parameter object</param>
        private object Control_OnExternalDatabound(object sender, string sourceName, object parameter)
        {
            // Initializing variables
            string nodeGuid;
            TreeNode tn;

            switch (sourceName)
            {
                case "pagename":
                    // Gets the nodeGUID of the of the page
                    nodeGuid = ValidationHelper.GetString(parameter, "");
                    tn = GetTreeNodeForGuid(nodeGuid);
                    return tn != null ? tn.DocumentName : nodeGuid;

                case "pagenamepath":
                    // Gets the nodeGUID of the of the page
                    nodeGuid = ValidationHelper.GetString(parameter, "");
                    tn = GetTreeNodeForGuid(nodeGuid);
                    return tn != null ? tn.DocumentNamePath : nodeGuid;

                case "pagecss":
                    string cssName = "";
                    // Gets the stylesheetID of the style assigned to current page
                    int stylesheetID = ValidationHelper.GetInteger(parameter, 0);
                    var cssInfo = CMS.PortalEngine.CssStylesheetInfoProvider.GetCssStylesheetInfo(stylesheetID);
                    if (cssInfo != null)
                    {
                        cssName = cssInfo.StylesheetDisplayName;
                    }
                    return (stylesheetID == 0) ? "" : cssName;

                case "usedefault":
                    // Gets the nodeGUID of the of the page
                    bool useDefault = ValidationHelper.GetBoolean(parameter, false);
                    return useDefault ? ResHelper.GetString("AcceleratedMobilePages.GridYes") : ResHelper.GetString("AcceleratedMobilePages.GridNo");
            }
            return parameter;
        }


        /// <summary>
        /// Helper function.
        /// Returns treenode for requested node GUID
        /// </summary>
        /// <param name="nodeGuid">Node GUID of requested object</param>
        private TreeNode GetTreeNodeForGuid(String nodeGuid)
        {
            TreeProvider tp = new TreeProvider();
            TreeNode tn = tp.SelectSingleNode(new Guid(nodeGuid), LocalizationContext.CurrentCulture.CultureCode, SiteContext.CurrentSiteName);
            return tn;
        }
    }
}
