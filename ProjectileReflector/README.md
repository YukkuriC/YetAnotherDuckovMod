# Projectile Reflector 刀反子弹

[中文] [[English](./README_en.md)]  
[Steam创意工坊](https://steamcommunity.com/sharedfiles/filedetails/?id=3597305916)

近战武器可以反弹面前 180° 扇形内的子弹。有两种反射方式：主动与被动。  
主动反射在玩家近战攻击后短时间内触发，无需消耗体力即可反弹，（默认配置下）拥有更大的反射范围与反射子弹威力；  
被动反射在玩家手持武器受击，且体力充足时触发，消耗体力弹开子弹，反射回开火敌人的概率较小。

## 配置

配置文件位于`<存档目录>/YukkuriC.ProjectileReflector.json`（Windows 系统下通常为`C:\Users\<用户名>\AppData\LocalLow\TeamSoda\Duckov\Saves\YukkuriC.ProjectileReflector.json`），其中内容如下：

| 配置名                         | 类型  | 默认值 | 描述                                           |
| ------------------------------ | ----- | ------ | ---------------------------------------------- |
| REFLECT_RANGE                  | float | 2      | 主动反射触发范围                               |
| REFLECT_RANGE_PASSIVE          | float | 1.5f   | 被动反射触发范围                               |
| TIME_PASSIVE_EXTEND            | float | 0.1f   | 每次被动反射延续状态时长（秒）                 |
| TIME_ACTIVE_EXTEND             | float | 0.2f   | 每次主动反射延续状态时长（秒）                 |
| TIME_SWING_ACTIVE              | float | 0.3f   | 挥刀后主动反射状态持续时长（秒）               |
| CHANCE_BACK_ACTIVE             | float | 0.9f   | 主动反射回弹子弹概率                           |
| CHANCE_BACK_PASSIVE            | float | 0.05f  | 被动反射回弹子弹概率                           |
| PASSIVE_STAMINA_COST           | float | 0.5f   | 被动反射体力消耗率（基于子弹伤害）             |
| DAMAGE_MULT_ACTIVE             | float | 1      | 主动反射后子弹伤害乘数                         |
| DAMAGE_MULT_PASSIVE            | float | 0.5f   | 被动反射后子弹伤害乘数                         |
| DISTANCE_MULT_ACTIVE           | float | 5      | 主动反射后子弹射程乘数                         |
| DISTANCE_MULT_PASSIVE          | float | 1      | 被动反射后子弹射程乘数                         |
| ACTIVE_CRITICAL                | bool  | true   | 主动反射子弹是否暴击                           |
| ACTIVE_EXPLOSION               | bool  | false  | 主动反射子弹是否爆炸                           |
| ACTIVE_EXPLOSION_DAMAGE_FACTOR | float | 1      | 主动反射爆炸额外伤害乘数（基于反射后子弹伤害） |
| ACTIVE_EXPLOSION_RANGE         | float | 1      | 主动反射爆炸范围                               |
| SFX_VOLUME                     | float | 0.5f   | 反射音效强度                                   |
