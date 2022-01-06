using System.IO;
using PluginSet.Core;
using PluginSet.Core.Editor;
using PluginSet.Lua.Editor;
using UnityEditor;

namespace PluginSet.FGuiToLua.Editor
{
    [BuildTools]
    public static class FGuiPluginTools
    {
        [OnFrameworkInit]
        public static void OnFrameInit(BuildProcessorContext context)
        {
            Global.AddPlatformSymbol(BuildTargetGroup.iOS, "FAIRYGUI_TOLUA");
            Global.AddPlatformSymbol(BuildTargetGroup.Android, "FAIRYGUI_TOLUA");
            Global.AddPlatformSymbol(BuildTargetGroup.Standalone, "FAIRYGUI_TOLUA");
        }
        
        [OnSyncEditorSetting]
        public static void OnSyncEditorSetting(BuildProcessorContext context)
        {
            context.Symbols.Add("FAIRYGUI_TOLUA");
            context.AddLinkAssembly("PluginSet.FGuiToLua");
        }
        

        [OnCopyLuaFiles]
        public static void CopyLuaFiles(string targetPath)
        {
            var path = Global.GetPackageFullPath("com.pluginset.fguitolua");
            if (string.IsNullOrEmpty(path))
                return;
            if (!Directory.Exists(path))
                return;

            var luaPath = Path.Combine(path, "Resources", "Lua");
            Global.CopyFiles(luaPath, targetPath, "*.lua");
            Global.CopyFiles(luaPath, targetPath, "*.lua.txt");
        }
    }
}