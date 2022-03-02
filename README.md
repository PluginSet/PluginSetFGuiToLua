# PluginSet.FairyGUI在ToLua中的封装

## 一、设计目的
ToLua下使用FairyGUI，可以利用脚本可热更的特性，更灵活地开发界面逻辑。
自定义FairyGUI文件脚本导出，配合UI框架可以减少代码编写量。

## 二、设计思路
FairyGUI中可以编写插件导出自定义lua脚本，脚本中可以收集组件的所有依赖项，包括依赖包与依赖组件，在加载包之后创建组件之前，绑定所有组件的扩展创建方法，从而将组件对象映射到lua中，使lua脚本中可以很方便的调用组件lua扩展方法。

lua使用可变参数更灵活。在lua中重新实现```UIWindow```和```UIModal```可以更好的利用可变参数。

## 三、主要实现

### ```PluginFguiToLua``` 运行插件
该插件只在ToLua虚拟机启动时，尝试加载"FairyInit"脚本，并调用全局方法"InitFairy"，项目中若有需求，可以在lua脚本根目录中重写"FairyInit"脚本。

### ```PackageLuaLoader``` lua层的```IPackageLoader```实现
该类为lua层提供了```IPackageLoader```的实现方式，可以在lua层设置```UIPackage```的加载方法。

### Resources中的lua脚本
#### ```FairyGUI.FairyUtils```
该脚本根参考FairyGUI提供的ToLua绑定脚本，结合自定义脚本导出结构稍做了改造，让继承```FairyGUI.GObject```的类可以直接使用ToLua的```class```方法来完成继承。

```fguiUtils.Class```能继承C#导出类，并提供```Bind```接口绑定组件扩展，将指定url对应的组件生成为返回的类的实例。

```fguiUtils.Component```在```fguiUtils.Class```的基础上，对导出脚本进行封装。组件在初始化时会调用导出的```InitMembers```方法获取组件中的子节点并作为成员变量保存

#### ```FairyGUI.FairyBase```
该脚本对FairyGUI中的常用组件类型进行了统一封装，添加```UI```前缀作为全局变量，方便直接继承或使用这些类型。

#### ```FairyGUI.UIWindowBase```
在```PluginSet.FairyGUI.UIWindowBase```的实现基础上，```FairyGUI.UIWindowBase```重写了生命周期函数，并在界面创建后会将界面实例记录起来，在界面移除时删除记录，这样在lua层可以获取已打开的指定名称的界面实例。
另外```FairyGUI.UIWindowBase```还封装了增加/移除组件点击事件```OnClick```和```OffClick```接口，以及延时关闭界面的```DelayHide```接口。

#### ```FairyGUI.UIWindow```
```FairyGUIL.UIWindow```与C#层的```PluginSet.FairyGUI.UIWindow```效果相同，是继承lua层的```UIWindowBase```的实现，不过生命周期函数与C#层有所差别。

1. ```OnUIInit``` 界面创建成功后初始化
2. ```SetData``` 界面初始化后/界面再次进入时，设置界面数据
3. ```OnShown``` 界面创建初始化并设置数据后，显示到舞台
4. ```OnHide``` 界面隐藏退出舞台
5. ```OnDestroy``` 界面销毁，与C#不同，在lua层```UIWindow```重载了```OnDispose```做了一些销毁处理

### ```FairyGUI.UIModal```
```FairyGUI.UIModal```的实现与C#层的```PluginSet.FairyGUI.UIModal```相同，是继承lua层的```UIWindow```的实现，生命周期与上述```UIWindow```相同。与```PluginSet.FairyGUI.UIModal```一样，在使用前，需要先设置```UIModal.ModalContentUrl```指定模态窗口容器组件，创建的组件会被容器加载，**在界面关闭时，该组件不会缓存到对象池，会直接被销毁**

### ```FairyGUI.UIManager``` 
```FairyGUI.UIManager```是C#层```PluginSet.FairyGUI.UIManager```的再封装，与其接口一致。提供了注册界面的接口```UIManager.RegisterWindow```，在使用界面之前，可以用该接口将界面先注册到管理器中，声名界面是否为单例及其tag，在调用显示界面接口创建界面时，会根据传入的参数作相应的处理：
 * 是否为单例 如果界面为单例模式，在打开重复调用打开界面时，如果发现已有打开的同名界面，则会将已有界面提到最上层，并重新设置新值；如果非单例模式，则会创建新的同名界面
 * tag 该tag将设置到```FairyGUI.Window.data```参数中，```UIManager```提供了根据tag关闭界面的接口

lua层的```UIManager```中，```ShowModalWait```方法还接受一个锁定键值，只有当调用```HideModalWait```且传入了相同键值时，才会真正关闭等待界面。