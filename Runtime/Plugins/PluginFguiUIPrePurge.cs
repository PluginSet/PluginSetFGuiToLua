using System.Collections;
using FairyGUI;
using PluginSet.Core;
using PluginSet.FGUI;

namespace PluginSet.FGuiToLua
{
    [PluginRegister]
    public class PluginFguiUIPrePurge: PluginBase, IStartPlugin
    {
        public override string Name => "FguiPrePurge";

        public int StartOrder => PluginsStartOrder.VirtualMachine + 1;
        
        public bool IsRunning { get; private set; }

        protected override void Init(PluginSetConfig config)
        {
        }

        public IEnumerator StartPlugin()
        {
            IsRunning = true;
            yield break;
        }

        public void DisposePlugin(bool isAppQuit)
        {
            if (isAppQuit) return;

            UIManager.PrePurge();
            UIManager.CloseAll();
            UIManager.Purge();
            UIWindow.ClearInstances();
            TweenManager.KillAllTweens();
            UIObjectPool.ClearAll();
            UIObjectFactory.Clear();
            IsRunning = false;
        }
    }
}