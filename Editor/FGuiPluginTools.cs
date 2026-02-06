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
            Global.CopyFiles(luaPath, targetPath, "*.lua.txt", SearchOption.AllDirectories, (src, dst) =>
            {
                if (dst.EndsWith("lua.txt"))
                    dst = dst.Substring(0, dst.Length - 4);
                Global.CheckAndCopyFile(src, dst);
            });
        }
    }
}