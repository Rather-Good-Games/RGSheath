# RGSheath

WORK IN PROGRESS

**Demo Video**

[![RGSheath](media/RGSheathPic.png)](https://youtu.be/BaNxLWO5S8A)

**Author:** RatherGood1

**Version**: 0.002

STILL Broken but much more interesting version using PlayableCharacterModel


**Updated:** 28 Nov 21

**Compatibility:** Tested on Suriyun MMORPG Kit Version 1.71f.



**Description:** 

Sheath / unsheath animations and model support.

**Other Dependencies:**

You need to provide your own animations. 

Example uses Exlosive "RPG Character Mecanim Animation Pack" not included.  https://assetstore.unity.com/packages/3d/animations/rpg-character-mecanim-animation-pack-63772


**Core MMORPG Kit modifications:**

Make partial:
```csharp 
public partial class PlayableCharacterModel : BaseCharacterModel
```

Make partial:
```csharp 
 public partial struct WeaponAnimations : IWeaponAnims
```

in BaseCharacterModel
```csharp 
protected readonly Dictionary<string, Dictionary<string, GameObject>> cacheModels = new Dictionary<string, Dictionary<string, GameObject>>();
```
also
```csharp 
protected DestroyCacheModel(string equipPosition)
```

**Instructions for use:**

TODO:

Enable on GameInstance:

![RGSheath](media/GameInstanceRGSheath.png)


Create equipment containers to hold sheathed weapons and sheathed models. i.e. scabard can be added to back or hips when sheathed.

![RGSheath](media/PCMEquipmetcontainers.png)

Add animations for sheathing/unsheathing weapons right/left or dual or whatever. ***NOTE: Trigger duration will apply at 0.5 * animation time if no "trigger duration rate" is supplied. Only looks at first value supplied in array. Alternaively ou can have weapons swap at specific time in animation by applying 0-1 value.***
![RGSheath](media/PCMWeaponAnimations.png)

Set the sheath models to the container and apply position/rotation offsets.  ***Set scale to 1,1,1*** it will default to 0,0,0
![RGSheath](media/PCBBoo1_Item_RGSHEITHInfo.png)


**Done.**


[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=L7RYB7NRR78L6)