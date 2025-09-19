using System;
using System.Collections.Generic;
using PluginSet.FGUI;
using UnityEngine;

namespace PluginSet.FGuiToLua.Editor
{
#if FAIRYGUI_SPINE
    [ToLuaGenerateSetting("PluginSet.FGuiToLua.LuaWrap.SpineWrap", "Scripts/ToLuaWrapper/FguiSpine", "com.pluginset.fguitolua", -999999, "#if FAIRYGUI_TOLUA && FAIRYGUI_SPINE", "#endif")]
    public static class FGuiSpineToLuaExportSetting
    {
        //导出时强制做为静态类的类型(注意customTypeList 还要添加这个类型才能导出)
        //unity 有些类作为sealed class, 其实完全等价于静态类
        [ToLuaGenerateStatic]
        public static List<Type> staticClassTypes = new List<Type>
        {
            typeof(FairyGUIUtils),
            typeof(UIManager),
        };

        //附加导出委托类型(在导出委托时, customTypeList 中牵扯的委托类型都会导出， 无需写在这里)
        [ToLuaGenerateDrop]
        [ToLuaGenerateDelegate]
        public static List<Type> customDelegateList = new List<Type>()
        {        
        };

        //在这里添加你要导出注册到lua的类型列表
        [ToLuaGenerateDrop]
        [ToLuaGenerateClass]
        public static Type[] customTypeList =
        {                
            typeof(Spine.Unity.SkeletonDataAsset),
            typeof(Spine.Unity.SkeletonAnimation),
            typeof(Spine.Unity.SkeletonRenderer),
        };

        [ToLuaGenerateTempDrop]
        public static Type[] dropTypeList =
        {
	        typeof(Component),
	        typeof(MonoBehaviour),
	        typeof(Transform),
	        typeof(RectTransform),
        };

        [ToLuaGenerateBaseType]
        public static Dictionary<Type, Type> baseTypes = new Dictionary<Type, Type>()
        {
        };
        
        [ToLuaGenerateBlackList]
        public static Dictionary<Type, IEnumerable<string>> blackList = new Dictionary<Type, IEnumerable<string>>
        {
            {typeof(Spine.Unity.SkeletonRenderer), new[] {"Start", "EditorSkipSkinSync"}},
        };
    }
#endif
}
