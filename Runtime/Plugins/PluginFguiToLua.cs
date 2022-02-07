using System.Collections;
using LuaInterface;
using PluginSet.Core;
using PluginSet.FGUI;
using PluginSet.Lua;

namespace PluginSet.FGuiToLua
{
    [PluginRegister]
    public class PluginFguiToLua: PluginBase, IStartPlugin
    {
        public override string Name => "FguiToLua";

        public int StartOrder => 0;
        
        public bool IsRunning { get; private set; }

        private LuaFunction _luaOnPackageLoaded = null;
        private LuaFunction _luaOnPackageUnload = null;
        
        protected override void Init(PluginSetConfig config)
        {
            AddEventListener(PluginToLuaConstants.NOTIFY_ON_LUA_START, OnLuaStateStart);
            
            FairyGUIUtils.OnPackageLoadedHandle += OnPackageLoaded;
            FairyGUIUtils.OnPackageUnloadHandle += OnPackageUnload;
        }

        private void OnLuaStateStart(PluginsEventContext context)
        {
            var luaState = (LuaState) context.Data;
            luaState.Require("FairyInit");
            
            var init = luaState.GetFunction("InitFairy");
            if (init == null)
                return;
            
            init.Call();
            init.Dispose();
            IsRunning = true;

            _luaOnPackageLoaded = luaState.GetFunction("OnFairyPackageLoaded");
            _luaOnPackageUnload = luaState.GetFunction("OnFairyPackageUnload");
        }

        private void OnPackageLoaded(string packageName)
        {
            if (!IsRunning) return;
            if (_luaOnPackageLoaded == null) return;
            
            if (_luaOnPackageLoaded.IsAlive)
                _luaOnPackageLoaded.Call(packageName);
        }
        
        private void OnPackageUnload(string packageName)
        {
            if (!IsRunning) return;
            if (_luaOnPackageUnload == null) return;
            
            if (_luaOnPackageUnload.IsAlive)
                _luaOnPackageUnload.Call(packageName);
        }
        
        public IEnumerator StartPlugin()
        {
            yield break;
        }

        public void DisposePlugin(bool isAppQuit)
        {
            _luaOnPackageLoaded?.Dispose();
            _luaOnPackageUnload?.Dispose();
            IsRunning = false;
        }
    }
}