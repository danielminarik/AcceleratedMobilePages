using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.Helpers;
using CMS.MacroEngine;
using CMS.SiteProvider;

namespace AcceleratedMobilePages
{
    public class CustomMacroMethods : MacroMethodContainer
    {
        [MacroMethod(typeof(bool), "Returns true if the current page is AMP page.", 1)]
        public static object IsAmpPage(EvaluationContext context, params object[] parameters)
        {
            return (AcceleratedMobilePages.CheckStateHelper.CheckFilterState() == Constants.ENABLED_AND_ACTIVE);
        }
    }
}
