# VREyeWatch
vr用眼睛注视选择物体
## 工程版本 ##
unity5.5.0f3 vs2017
## 用法说明 ##
- 在unity工程中导入本项目，新建场景，场景中可以随便添加一些物体用于测试
- 在MainCamera上添加WatchController.cs脚本，并拖入相应物体
- 在MainCamera上添加HightlightingRenderer.cs脚本
- 给需要注视的物体上添加WatchEvent.cs脚本，并注册相应的事件（就像UGUI的Button一样）
- 在Edit-Project Setting-Player中勾选Vitual Reality Supported

## 脚本说明 ##
### WatchController - 注视主控制器 ###
	可以挂在任意物体上
	Eye:		眼睛（一般是场景中的主摄像机）
	Point:		选择的点的样式，选择一个Prefab，可以是图片/UI/3D物体
	PointDefault:	默认未选中物体状态下点的位置（放在摄像机下）
	HighLightColor:	高光的颜色
	LaterMask:	选择屏蔽层
	WatchTime:	注视时间，单位秒
### WatchEvent - 注视事件 ###
	挂在需要注视事件的物体上
	OnWatchEnter:	注视点进入
	OnWatchExit:	注视点移出
	OnWatchUpdate:	注视点在物体上
	OnWatch:	注视N秒后（N指WatchController的WatchTime）
### WatchGameobject - 被注视物体 ###
	不用手动去挂，程序运行后会挂在被注视的物体上
	主要负责处理物体被注视后事件的注册
### TimerTool - 计时器工具 ###
	不是本项目重点，想用的自己拿去用，里面有详细的注释

## 备注 ##
案例场景在EyeWatch-Example-Scenes,可以自己看一下怎么用

注视事件除了拖拽，还可以用代码注册
`WatchEvent we;`
`we.onWacth.AddListener(delegate { Test(); });`
