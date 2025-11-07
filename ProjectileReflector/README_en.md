# Projectile Reflector 刀反子弹 v1.1

[[中文](./README.md)] [English]  
[Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=3597305916)

Melee weapons can reflect bullets within a 180° sector in front. There are two reflection modes: Active and Passive.  
Active reflection triggers shortly after the player performs a melee attack, reflecting bullets without consuming stamina, (by default) featuring a larger reflection area and increased reflected bullet power;  
Passive reflection triggers when the player is hit while holding the weapon and has sufficient stamina, consuming stamina to deflect bullets, with a lower probability of reflecting bullets back towards the attacker.

## Configuration

The configuration file is located at `<SaveDirectory>/YukkuriC.ProjectileReflector.json` (typically `C:\Users\<Username>\AppData\LocalLow\TeamSoda\Duckov\Saves\YukkuriC.ProjectileReflector.json` for Windows OS); if installed with [ModConfig](https://steamcommunity.com/sharedfiles/filedetails/?id=3590674339) then the config can also be edited in game.  
All config entries are listed as follows:

<!-- table begin -->
Configuration Name|Type|Default Value|Description
--|--|--|--
ModVersion_1_1|bool|true|(Display only) Mod version: 1.1
ENABLE_ACTIVE_REFLECT|bool|true|Enables active reflection
ENABLE_PASSIVE_REFLECT|bool|true|Enables passive reflection
PASSIVE_REFLECT_BY_ADS|bool|false|Enables passive reflection only during ADS mode
PASSIVE_REFLECT_WHEN_RUNNING|bool|false|Whether passive reflection enables when running
PASSIVE_REFLECT_WHEN_DASHING|bool|false|Whether passive reflection enables when dashing
REFLECT_RANGE|float|2|Active reflection trigger range
REFLECT_RANGE_PASSIVE|float|1.5f|Passive reflection trigger range
TIME_PASSIVE_EXTEND|float|0.1f|State duration extension per passive reflection (seconds)
TIME_ACTIVE_EXTEND|float|0.2f|State duration extension per active reflection (seconds)
TIME_SWING_ACTIVE|float|0.3f|Active reflection state duration after a melee swing (seconds)
TIME_ADS_ACTIVE|float|0.3f|Active reflection state duration after entering ADS mode (seconds) (works only with "Enables passive during ADS")
CHANCE_BACK_ACTIVE|float|0.9f|Chance for active reflection to return bullet to shooter
CHANCE_BACK_PASSIVE|float|0.05f|Chance for passive reflection to return bullet to shooter
PASSIVE_STAMINA_COST|float|0.5f|Passive reflection stamina cost rate (based on bullet damage)
ACTIVE_STAMINA_GAIN|float|5|Stamina amount gain after each single active bullet reflection
DAMAGE_MULT_ACTIVE|float|1|Reflected bullet damage multiplier after active reflection
DAMAGE_MULT_PASSIVE|float|0.5f|Reflected bullet damage multiplier after passive reflection
DISTANCE_MULT_ACTIVE|float|5|Reflected bullet range multiplier after active reflection
DISTANCE_MULT_PASSIVE|float|1|Reflected bullet range multiplier after passive reflection
IGNORES_ANGLE|bool|false|Also reflects bullets from behind; might solve the issue that some high-speed bullets go through the barrier
ACTIVE_CRITICAL|bool|true|Whether actively reflected bullets are critical hits
ACTIVE_EXPLOSION|bool|false|Whether actively reflected bullets explode
ACTIVE_EXPLOSION_DAMAGE_FACTOR|float|1|Active reflection explosion extra damage multiplier (based on reflected bullet damage)
ACTIVE_EXPLOSION_RANGE|float|1|Active reflection explosion range
ENABLES_FLYING_BLADE|bool|false|Enables flying blade (?)
SFX_VOLUME|float|0.5f|Reflection sound effect volume
