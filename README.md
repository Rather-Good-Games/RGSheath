# RGSheath

**Credits:** Special thanks to https://github.com/benhamlett aka TidyDev and his TidyPlayerSheath addon that helped make this project possible.

**Author:** RatherGood1

**Version**: 0.003

**Updated:** 3 Dec 21

**Compatibility:** Tested on Suriyun MMORPG Kit Version 1.72b2 and Unity 2021.1.23f

**Description:** 

**Disclaimer: This is NOT a finished project. In lieu of donations, please fork, modify and push back changes if you wish to improve this project! Minimal/limited support or special requests will be provided at my discression.** 

**Throughout the code you will find "TODO:" remarks of issues/improvemnts/bugs identified. Pick one and fix it!**

This is a modification/addon to https://github.com/suriyun-production/mmorpg-kit-docs sheath / un-sheath animations and model switching support.

Works with the ShooterPlayerController. Not tested with others. When sheathed, switches to "Adventure" mode and "Combat" mode when unsheathed. Plays the appropriate animation and switches the weapon models aat the appropriate time.

When sheathed, default animations are used and the appropriate weapon animations when un-sheathed.

Only bow and 1 Hand Sword are implemented in the example demo.

**Demo Video:**

[![RGSheath](media/RGSheathPic.png)](https://youtu.be/fDB8a7mWdaU)

**Other Dependencies:**

You need to provide your own animations. Example uses Explosive "RPG Character Mecanim Animation Pack" not included.  https://assetstore.unity.com/packages/3d/animations/rpg-character-mecanim-animation-pack-63772

**QUICK START:**

If you would like to test out RGSheath functionality. 

1. Start a fresh Unity 3D project.
2. From the unity asset store:     Download: MMOKIT 1.72b2 and "RPG Character Mecanim Animation Pack"
3. Import the RGSheathV0p003.unityPackage
4. Make the core modifications listed below.
5. Add this scene to the top of your build settings then open and run the scene: Assets\RatherGood\MMOKit\RGSheath\Scenes\00InitTestRGSheath
6. Equip a Bow001 and Sword001(single/dual) in the inventory weapon switch.
7. Press "Z" to sheath/un-sheath. "~" to change weapons.


NOTES:

* Look at the "Male_CC_RGSheath" Prefab and see how its set up.
* ShooterCharacterControllerRG should be the controller used on your GameInstance.
* Make sure the new weapons are in your database.
* Use the conveince scritable object "WeaponAnimationDataCopyRG" to setup and copy animations between your character or covert to PlayableCharacterModel form AnimatorCharacterModel (sp?).


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

in BaseCharacterEntity
 also
```csharp 
public virtual bool CanAttack()
```

**Instructions for use:**

Enable on GameInstance:

![RGSheath](media/GameInstanceRGSheath.png)

Create equipment containers to hold sheathed weapons and sheathed models. i.e. scabard can be added to back or hips when sheathed.

![RGSheath](media/PCMEquipmetcontainers.png)

Add animations for sheathing/unsheathing weapons right/left or dual or whatever. ***NOTE: Trigger duration will apply at 0.5 * animation time if no "trigger duration rate" is supplied. Only looks at first value supplied in array. Alternatively you can have weapons swap at specific time in animation by applying 0-1 value.***
![RGSheath](media/PCMWeaponAnimations.png)

Set the sheath models to the container and apply position/rotation offsets. 

![RGSheath](media/PCBBoo1_Item_RGSHEITHInfo.png)


**Done.**


[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=L7RYB7NRR78L6)