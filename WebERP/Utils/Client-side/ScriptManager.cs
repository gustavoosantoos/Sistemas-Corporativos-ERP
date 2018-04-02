using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebERP.Utils
{
    public static class ScriptManager
    {
        public static void SetStartupScript(ITempDataDictionary tempData, string script, bool mustSurroundWithScriptTag = false)
        {
            string scriptStart = "<script>";
            string scriptEnd = "</script>";

            if (mustSurroundWithScriptTag)
                tempData["StartupScript"] = scriptStart + script + scriptEnd;
            else
                tempData["StartupScript"] = script;
        }
    }
}
