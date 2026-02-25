白驿寻名

<p align="center">
  <img src="https://img.shields.io/badge/引擎-Unity-000000?style=flat&logo=unity&logoColor=white" alt="引擎">
  <img src="https://img.shields.io/badge/类型-2D文化叙事解谜-008080?style=flat" alt="类型">
  <img src="https://img.shields.io/badge/状态-开发中-yellow?style=flat" alt="状态">
  <br/>
  <strong>一段为文明“正名”的寻回之旅。</strong>
</p>

概述

《白驿寻名》是一款以华夏文化传承为核心的2D叙事解谜游戏。玩家将扮演忘却了自身使命的上古神兽白泽，在最后一个年俗循环之地“澈谷”苏醒，以失忆考古学者白驿的身份，手持可记录万物的《考古日志》，踏上对抗“大遗忘”、修复文明锚点的旅程。

你的武器不是刀剑，而是“理解”与“正名”。直面因被曲解而堕落为“文化病灶”的四大凶兽，探寻其被遗忘的本真意义，令其重归文明谱系中的正确位置，从而逐步取回自己的力量与记忆。

核心特色

- 正名之战，非诛灭之战

• 独特的战斗/解谜系统：对抗凶兽的方式并非消灭它们，而是通过探索、对话与逻辑推理，结合《考古日志》中收集的文化碎片，在关键时刻为被扭曲的概念“正名”。

• 例如：面对象征“贪婪暴食”的饕餮，你需要还原其“敬食惜福”的原始文化内涵，完成“正名仪式”。

- 手持万卷《考古日志》

• 你的核心工具与伙伴。它可以：

    ◦ 记录万物：点击场景中的文化元素（器物、图案、习俗遗存）进行记录，丰富数据库。

    ◦ 线索拼图：记录的信息会相互关联，自动生成推理线索，帮助你解开“正名”之谜。

    ◦ 记忆回响：随着游戏推进，日志中将逐渐浮现白泽古老的记忆碎片。

- 穿梭于文明残响之间

• 起始之地·澈谷：一个因执念而时间循环的除夕村落，保留着最后的完整年俗，是故事起点也是心灵锚点。

• 凶兽领域：四个风格迥异、代表不同“文化病灶”的关卡世界。从饕餮的“无尽盛宴”到混沌的“失序迷宫”，每个领域的美术与谜题设计都与其核心概念紧密相连。

• 文化废墟：在领域之间探索那些即将被遗忘彻底吞噬的文明遗迹，抢在它们消失前点亮“传承火种”。

- 国风手绘，意境叙事

• 采用融合传统绘画元素与现代设计感的2D手绘风格，营造出既古朴又奇幻的视觉体验。

• 音乐与音效注重氛围营造，从澈谷循环的缱绻民乐到凶兽领域的压迫之音，层层递进。

运行方法

1.  环境要求
    ◦ 确保已安装 Unity 2021.3 LTS 或更高版本。

    ◦ 项目使用 Unity 2D URP 管线。

2.  克隆与打开
    git clone https://github.com/onedasein/UnityProject_X
    
    ◦ 用Unity Hub打开克隆下来的项目文件夹。

3.  试玩
    ◦ 在Unity编辑器中，打开 Assets/Scenes 目录下的初始场景（如 StartMenu 或 CheValley）。

    ◦ 点击运行按钮即可体验当前开发版本。

项目结构
Assets/Scripts/
├──  Core
│   ├── GameManager.cs          # 游戏总控制器
│   ├── AudioManager.cs         # 音频管理器
│   ├── UIManager.cs            # UI总管理器（**可负责管理DialogUIManager等UI组件的启用/禁用**）
│   ├── SceneLoader.cs          # 场景加载器
│   └── ManualManager.cs        # 文本手册管理器
│
├──  Systems
│   ├── InputSystem.cs          # 输入控制封装
│   ├── SaveSystem.cs           # 存档系统
│   ├── EventSystem.cs          # 事件系统
│   └── PoolSystem.cs           # 对象池系统
│
├──  GameTextSystem
│   ├──  Base
│   │   └── GameText.cs         # 文本基类
│   ├──  Derived
│   │   └── ... (文本派生类)
│   └──  Data
│       └── TextDataArray.cs
│
├──  Player
│   ├── PlayerController.cs         # 玩家移动控制
│   ├── PlayerStats.cs              # 玩家属性
│   ├── PlayerAnimation.cs          # 玩家动画控制
│   ├── PlayerCombat.cs             # 玩家战斗系统
│   └── PlayerInteractionHandler.cs # 玩家交互控制
│
├──  Entities
│   ├── EnemyController.cs      # 敌人AI基类
│   ├── EnemyMelee.cs           # 近战敌人
│   ├── EnemyRanged.cs          # 远程敌人
│   └── NPC.cs                  # 继承自 Interactable 的具体实体类
│
├──  Gameplay
│   ├── HealthSystem.cs         # 通用生命值系统
│   ├── DamageDealer.cs         # 伤害系统
│   ├── Collectible.cs          # 可收集物品
│   ├── Checkpoint.cs           # 存档点
│   └── Interactable.cs         # 作为所有可交互物体的抽象基类，属于核心玩法机制
│
├──  UI
│   ├── HUDController.cs        # 游戏内HUD
│   ├── MenuController.cs       # 菜单控制
│   ├── DialogUIManager.cs      # 对话界面的核心控制器
│   ├── DialogueUI.cs           # (可选)可保留作为对话界面更底层的UI元素类，或与DialogUIManager合并
│   ├── InventoryUI.cs          # 背包UI
│   └── ManualUI.cs             # 图鉴/手册UI
│
└──  Utilities
    ├── CameraFollow.cs         # 相机跟随
    ├── ParallaxBackground.cs   # 视差背景
    ├── Timer.cs                # 计时器工具
    └── Extensions.cs           # 扩展方法

开发团队

• [你的名字]： 项目经理 & 主程序

• [同学A名字]： 主策划 & 叙事设计

• [同学B名字]： 主美术 & 动画

• [同学C名字]： 程序 & 系统设计

（请在此处填写实际成员与分工）

开源协议

本项目采用 LICENSE 开源协议。

致谢

• 灵感来源于博大精深的中华神话与传统文化。

• 感谢所有在开发过程中提供测试与反馈的朋友们。

文明在记忆中延续，在理解中重生。欢迎踏上这段寻回与正名之旅。
