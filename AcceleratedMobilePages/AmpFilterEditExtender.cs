using System;
using CMS.SiteProvider;
using CMS;
using CMS.Base.Web.UI;
using CMS.PortalEngine.Web.UI;

[assembly: RegisterCustomClass("AmpFilterEditExtender", typeof(AcceleratedMobilePages.AmpFilterEditExtender))]

namespace AcceleratedMobilePages
{
    class AmpFilterEditExtender : ControlExtender<UIForm>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public override void OnInit()
        {
            Control.OnBeforeSave += Control_OnBeforeSave;
        }


        /// <summary>
        /// Sets the site ID before save
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void Control_OnBeforeSave(object sender, EventArgs e)
        {
            AmpFilterInfo elementInfo = (AmpFilterInfo) Control.EditedObject;
            elementInfo.SiteID = SiteContext.CurrentSiteID;
            if (elementInfo.UseDefaultStylesheet)
            {
                elementInfo.StylesheetID = 0;
            }
        }
    }
}
