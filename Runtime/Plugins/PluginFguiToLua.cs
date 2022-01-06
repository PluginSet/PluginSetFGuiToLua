using System;
using LuaInterface;
using PluginSet.Core;
using PluginSet.Lua;

namespace PluginSet.FGuiToLua
{
    [PluginRegister]
    public class PluginFguiToLua: PluginBase
    {
        public override string Name => "FguiToLua";
        
        protected override void Init(PluginSetConfig config)
        {
            AddEventListener(PluginToLuaConstants.TOLUA_ON_LUA_START, OnLuaStateStart);
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
        }

        private void OnDestroy()
        {
            RemoveEventListener(PluginToLuaConstants.TOLUA_ON_LUA_START, OnLuaStateStart);
        }
    }
}