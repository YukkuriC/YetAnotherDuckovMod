# Projectile Reflector 刀反子弹 v1.3

[[中文](./README.md)] [English]  
[Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=3597305916)

Melee weapons can reflect bullets within a 180° sector in front. There are two reflection modes: Active and Passive.  
Active reflection triggers shortly after the player performs a melee attack, reflecting bullets without consuming stamina, (by default) featuring a larger reflection area and increased reflected bullet power;  
Passive reflection triggers when the player is hit while holding the weapon and has sufficient stamina, consuming stamina to deflect bullets, with a lower probability of reflecting bullets back towards the attacker.

## Configuration

The configuration file is located at `<SaveDirectory>/YukkuriC.ProjectileReflector.json` (typically `C:\Users\<Username>\AppData\LocalLow\TeamSoda\Duckov\Saves\YukkuriC.ProjectileReflector.json` for Windows OS); if installed with [ModConfig](https://steamcommunity.com/sharedfiles/filedetails/?id=3590674339) or [ModSetting](https://steamcommunity.com/sharedfiles/filedetails/?id=3595729494) then the config can also be edited in game.  
All config entries are listed as follows:

### Group：Version 1.3
Configuration Name|Type|Default Value|Description
--|--|--|--
ModVersion_1_3|bool|true|（仅展示）Mod版本：1.3
### Group：Functions
Configuration Name|Type|Default Value|Description
--|--|--|--
ENABLE_ACTIVE_REFLECT|bool|true|启用主动反射
ENABLE_PASSIVE_REFLECT|bool|true|启用被动反射
PASSIVE_REFLECT_BY_ADS|bool|false|仅在机瞄状态下启用被动反射
PASSIVE_REFLECT_WHEN_RUNNING|bool|false|是否在跑动中被动反射
PASSIVE_REFLECT_WHEN_DASHING|bool|false|是否在翻滚中被动反射
AUTO_SCALE_MELEE_RANGE|bool|true|根据持有武器攻击范围自动放缩反射范围（以1.5为分母）
### Group：Parameters
Configuration Name|Type|Default Value|Description
--|--|--|--
REFLECT_RANGE|float|2|主动反射触发范围
REFLECT_RANGE_PASSIVE|float|1.5f|被动反射触发范围
TIME_PASSIVE_EXTEND|float|0.1f|每次被动反射延续状态时长（秒）
TIME_ACTIVE_EXTEND|float|0.2f|每次主动反射延续状态时长（秒）
TIME_SWING_ACTIVE|float|0.3f|挥刀后主动反射状态持续时长（秒）
TIME_ADS_ACTIVE|float|0.3f|机瞄后主动反射状态持续时长（秒）（需开启“仅机瞄被动反射”）
CHANCE_BACK_ACTIVE|float|0.9f|主动反射回弹子弹概率
CHANCE_BACK_PASSIVE|float|0.05f|被动反射回弹子弹概率
PASSIVE_STAMINA_COST|float|0.5f|被动反射体力消耗率（基于子弹伤害）
ACTIVE_STAMINA_GAIN|float|5|主动反射单颗子弹恢复体力量
DAMAGE_MULT_ACTIVE|float|1|主动反射后子弹伤害乘数
DAMAGE_MULT_PASSIVE|float|0.5f|被动反射后子弹伤害乘数
DISTANCE_MULT_ACTIVE|float|5|主动反射后子弹射程乘数
DISTANCE_MULT_PASSIVE|float|1|被动反射后子弹射程乘数
### Group：Grenade Reflection
Configuration Name|Type|Default Value|Description
--|--|--|--
ENABLE_GRENADE_REFLECT|bool|true|启用手雷反射
AIM_GRENADE_OWNER|bool|true|反射方向瞄准投掷者（如果非自己）
GRENADE_REFLECT_HORIZONTAL_SPEED|float|10|手雷反射横向速度
GRENADE_REFLECT_VERTICAL_SPEED|float|3|手雷反射纵向速度
### Group：Misc
Configuration Name|Type|Default Value|Description
--|--|--|--
IGNORES_ANGLE|bool|false|后方子弹也可反射；或许可解决部分高速子弹穿透防御问题
ACTIVE_CRITICAL|bool|true|主动反射子弹是否暴击
ACTIVE_EXPLOSION|bool|false|主动反射子弹是否爆炸
ACTIVE_EXPLOSION_DAMAGE_FACTOR|float|1|主动反射爆炸额外伤害乘数（基于反射后子弹伤害）
ACTIVE_EXPLOSION_RANGE|float|1|主动反射爆炸范围
ENABLES_FLYING_BLADE|bool|false|启用飞刃（？）
FLYING_BLADE_STRENGTH|float|0.5f|飞刃伤害乘数
FLYING_BLADE_VAMPIRISM|float|0.5f|飞刃造成伤害吸血比例
### Group：Sound
Configuration Name|Type|Default Value|Description
--|--|--|--
SFX_VOLUME|float|0.5f|反射音效强度
