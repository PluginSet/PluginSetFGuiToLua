using System;
using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using PluginSet.FGUI;
using UnityEngine;

namespace PluginSet.FGuiToLua.Editor
{
    [ToLuaGenerateSetting("PluginSet.FGuiToLua.LuaWrap", "FguiWraps", "com.pluginset.fguitolua", -999999, "#if FAIRYGUI_TOLUA", "#endif")]
    public static class FGuiToLuaExportSetting
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
            typeof(UISource.LoadFunc),
            
			typeof(FairyGUI.EventCallback1),
			typeof(FairyGUI.EventCallback0),
			typeof(FairyGUI.ListItemRenderer),
			typeof(FairyGUI.ListItemProvider),
			typeof(FairyGUI.PlayCompleteCallback),
			typeof(FairyGUI.TransitionHook),
			typeof(System.Action<FairyGUI.PackageItem>),
			typeof(FairyGUI.UIPackage.LoadResource),
			typeof(FairyGUI.UIPackage.LoadResourceAsync),
			typeof(FairyGUI.UIPackage.CreateObjectCallback),
			typeof(FairyGUI.GObjectPool.InitCallbackDelegate),
			typeof(FairyGUI.TimerCallback),
			typeof(FairyGUI.GTweenCallback),
			typeof(FairyGUI.GTweenCallback1),
			typeof(FairyGUI.UIObjectFactory.GComponentCreator),
			typeof(FairyGUI.UIObjectFactory.GLoaderCreator),
			typeof(FairyGUI.UIObjectFactory.GLoader3DCreator),
			typeof(FairyGUI.UILoadCallback),
			typeof(System.Action<System.Action>),
			
			typeof(PackageLuaLoader.PreparePackageFunc),
			typeof(PackageLuaLoader.LoadPackageFunc),
			typeof(PackageLuaLoader.ReloadPackageFunc),
			typeof(PackageLuaLoader.ReleasePackageFunc),
        };

        //在这里添加你要导出注册到lua的类型列表
        [ToLuaGenerateDrop]
        [ToLuaGenerateClass]
        public static Type[] customTypeList =
        {                
            // fairy gui
            typeof(EventContext),
            typeof(EventDispatcher),
            typeof(EventListener),
            typeof(InputEvent),
            typeof(DisplayObject),
            typeof(Container),
            typeof(Stage),
            typeof(Controller),
            typeof(GObject),
            typeof(GGraph),
            typeof(GGroup),
            typeof(GImage),
            typeof(GLoader),
            typeof(GMovieClip),
            typeof(TextFormat),
            typeof(GTextField),
            typeof(GRichTextField),
            typeof(GTextInput),
            typeof(GComponent),
            typeof(GList),
            typeof(GRoot),
            typeof(GLabel),
            typeof(GButton),
            typeof(GComboBox),
            typeof(GProgressBar),
            typeof(GSlider),
            typeof(PopupMenu),
            typeof(ScrollPane),
            typeof(Transition),
            typeof(UIPackage),
            typeof(Window),
            typeof(GObjectPool),
            typeof(Relations),
            typeof(RelationType),
            typeof(Timers),
            typeof(GTween),
            typeof(GTweener),
            typeof(EaseType),
            typeof(TweenValue),
            typeof(UIObjectFactory),
            typeof(ByteBuffer),
            typeof(ToolSet),
            
            // plugin set fgui
            typeof(RenderImage),
            typeof(UILayer),
            typeof(UIObjectPool),
            typeof(UISource),
            typeof(UIWindowBase),
            typeof(FairyGUIUtils),
            typeof(UIManager),
            typeof(UnionPackage),
            typeof(PackageLuaLoader),
            typeof(UIToast),
            typeof(GCustomLoader),
            typeof(GCustomList),
            typeof(GLoader3D),
            typeof(GCustomLoader3D),
            typeof(CustomListDelegate),
            typeof(FanListDelegate),
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
	        {typeof(PackageLuaLoader), null},
        };
    }
}