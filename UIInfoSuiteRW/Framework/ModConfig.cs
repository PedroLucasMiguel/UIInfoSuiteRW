using System.Collections.Generic;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace UIInfoSuiteRW.Framework
{
  internal class ModConfig
  {
    //public bool ShowOptionsTabInMenu { get; set; } = true;
    public KeybindList OpenCalendarKeybind { get; set; } = KeybindList.ForSingle(SButton.B);
    public KeybindList OpenQuestBoardKeybind { get; set; } = KeybindList.ForSingle(SButton.H);
    public bool XPBar { get; set; } = true;
    public bool XPBFadeout { get; set; } = true;
    public bool XPGain { get; set; } = true;
    public bool LvlUpAnimation { get; set; } = true;
    public bool NpcLocationOnMap { get; set; } = true;
    public bool ShowLuckIcon { get; set; } = true;
    public bool ShowHarvestPricesInShop { get; set; } = true;
    public bool HeartFill { get; set; } = true;
    public bool ShowBirthdayIcon { get; set; } = true;
    public bool DisplayCalendarAndBilboard { get; set; } = true;
    public bool ShowCropAndBarrelTooltip { get; set; } = true;
    public bool ShowItemEffectRanges { get; set; } = true;
    public bool ShowItemsRequiredForBundle { get; set; } = true;
    public bool ExtraItemInformation { get; set; } = true;
    public bool ShowNewRecipesIcon { get; set; } = true;
    public bool ShowRainyDayIcon { get; set; } = true;
    public bool ShowRobinBuildingStatusIcon { get; set; } = true;
    public bool ShowSeasonalBerryIcon { get; set; } = true;
    public bool ShowTodaysGiftsIcon { get; set; } = true;
    public bool ShowToolUpgradeStatusIcon { get; set; } = true;
    public bool ShowTravelerMerchantIcon { get; set; } = true;
    public bool ShowAnimalsNeedsPets { get; set; } = true;
    public Dictionary<string, bool> ShowLocationOfFriends { get; set; } = new();
  }
}
