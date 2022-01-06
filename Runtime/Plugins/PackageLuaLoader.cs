using FairyGUI;
using LuaInterface;
using PluginSet.Core;
using PluginSet.FGUI;

namespace PluginSet.FGuiToLua
{
    public class PackageLuaLoader: IPackageLoader
    {
        public delegate AsyncOperationHandle PreparePackageFunc(string packageName);
        public delegate UIPackage LoadPackageFunc(string packageName);
        public delegate void ReloadPackageFunc(UIPackage package);
        public delegate void ReleasePackageFunc(UIPackage package);

        private PreparePackageFunc _preparePackageFunc;
        private LoadPackageFunc _loadPackageFunc;
        private ReloadPackageFunc _reloadPackageFunc;
        private ReleasePackageFunc _releasePackageFunc;
        
        public void SetPreparePackageFunc(LuaFunction func)
        {
            _preparePackageFunc = (PreparePackageFunc) DelegateTraits<PreparePackageFunc>.Create(func);
        }

        public void SetLoadPackageFunc(LuaFunction func)
        {
            _loadPackageFunc = (LoadPackageFunc) DelegateTraits<LoadPackageFunc>.Create(func);
        }

        public void SetReloadPackageFunc(LuaFunction func)
        {
            _reloadPackageFunc = (ReloadPackageFunc) DelegateTraits<ReloadPackageFunc>.Create(func);
        }

        public void SetReleasePackageFunc(LuaFunction func)
        {
            _releasePackageFunc = (ReleasePackageFunc) DelegateTraits<ReleasePackageFunc>.Create(func);
        }
        
        [NoToLua]
        public AsyncOperationHandle PreparePackage(string packageName)
        {
            return _preparePackageFunc?.Invoke(packageName) ?? AsyncNonResultHandle<object>.Default;
        }

        [NoToLua]
        public UIPackage LoadPackage(string packageName)
        {
            return _loadPackageFunc?.Invoke(packageName);
        }

        [NoToLua]
        public void ReloadPackage(UIPackage package)
        {
            _reloadPackageFunc?.Invoke(package);
        }

        [NoToLua]
        public void ReleasePackage(UIPackage package)
        {
            _releasePackageFunc?.Invoke(package);
        }
    }
}