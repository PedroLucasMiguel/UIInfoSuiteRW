using System.Collections.Generic;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;
using UIInfoSuiteRW.Features;

namespace UIInfoSuiteRW.Framework
{
  public sealed class ModConfig
  {
    //public bool ShowOptionsTabInMenu { get; set; } = true;
    public KeybindList OpenCalendarKeybind { get; set; } = KeybindList.ForSingle(SButton.B);
    public KeybindList OpenQuestBoardKeybind { get; set; } = KeybindList.ForSingle(SButton.H);

    public Dictionary<string, bool> FeatureConfig { get; set; } = new(){
      { FeatureIds.XP_BAR, true },
      { FeatureIds.XP_BAR_FADEOUT, true },
      { FeatureIds.XP_GAIN, true },
      { FeatureIds.NPC_LOCATION_ON_MAP, true },
      { FeatureIds.LVL_UP_ANIMATION, true },
      { FeatureIds.SHOW_LUCK_ICON, true },
      { FeatureIds.SHOW_HARVEST_PRICES_IN_SHOP, true },
      { FeatureIds.HEART_FILL, true },
      { FeatureIds.SHOW_BIRTHDAY_ICON, true },
      { FeatureIds.DISPLAY_CALENDAR_AND_BILBOARD, true },
      { FeatureIds.SHOW_CROP_AND_BARREL_TOOLTIP, true },
      { FeatureIds.SHOW_ITEM_EFFECT_RANGE, true },
      { FeatureIds.SHOW_ITEMS_REQUIRED_FOR_BUNDLES, true },
      { FeatureIds.EXTRA_ITEM_INFORMATION, true },
      { FeatureIds.SHOW_NEW_RECIPES_ICON, true },
      { FeatureIds.SHOW_RAINY_DAY_ICON, true },
      { FeatureIds.SHOW_ROBIN_BUILDING_STATUS, true },
      { FeatureIds.SHOW_SEASONAL_BERRY_ICON, true },
      { FeatureIds.SHOW_TODAYS_GIFT_ICON, true },
      { FeatureIds.SHOW_TOOL_UPGRADE_STATUS_ICON, true },
      { FeatureIds.SHOW_TRAVELER_MERCHANT_ICON, true },
      { FeatureIds.SHOW_ANIMALS_NEED_PETS, true },
    };

    public Dictionary<string, bool> ShowLocationOfFriends { get; set; } = new();
  }
}
