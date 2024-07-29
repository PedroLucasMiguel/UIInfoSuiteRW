using StardewModdingAPI;
using UIInfoSuiteRW.Features;

namespace UIInfoSuiteRW.Framework
{

  internal class ModConfigManager
  {
    private const string GENERIC_MOD_MENU_CONFIG_UID = "spacechase0.GenericModConfigMenu";
    public ModConfig Settings { get; set; } = new();
    public IGenericModConfigMenuApi? Gmcm { get; set; }
    private IManifest Manifest { get; set; } = null!;
    private IModHelper Helper { get; set; } = null!;

    public ModConfigManager(IModHelper helper, IManifest manifest)
    {
      Manifest = manifest;
      Helper = helper;
      Gmcm = helper.ModRegistry.GetApi<IGenericModConfigMenuApi>(GENERIC_MOD_MENU_CONFIG_UID);

      if (Gmcm == null)
        return;

      CreateMenu();
    }

    private void CreateMenu()
    {
      Gmcm!.Register(Manifest, () => Settings = new ModConfig(), () => Helper.WriteConfig(Settings));

      // Keybinds
      Gmcm.AddSectionTitle(
        Manifest,
        text: () => I18n.ConfigMenu_Keybinds_SectionTitle(),
        tooltip: () => I18n.ConfigMenu_Keybinds_SectionTooltip()
      );
      Gmcm.AddKeybindList(
        Manifest,
        name: () => I18n.ConfigMenu_Keybinds_OpenCalendar(),
        tooltip: () => I18n.ConfigMenu_Keybinds_OpenCalendarTooltip(),
        getValue: () => Settings.OpenCalendarKeybind,
        setValue: value => Settings.OpenCalendarKeybind = value
      );
      Gmcm.AddKeybindList(
        Manifest,
        name: () => I18n.ConfigMenu_Keybinds_OpenQuestBoard(),
        tooltip: () => I18n.ConfigMenu_Keybinds_OpenQuestBoard(),
        getValue: () => Settings.OpenQuestBoardKeybind,
        setValue: value => Settings.OpenQuestBoardKeybind = value
      );

      // Tweaks
      Gmcm.AddSectionTitle(
        Manifest,
        text: () => I18n.ConfigMenu_Features_SectionTitle(),
        tooltip: () => I18n.ConfigMenu_Features_SectionTooltip()
      );
      // XP Bar show
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_XpBar_Show(),
        tooltip: () => I18n.ConfigMenu_Features_XpBar_Show_Tooltip(),
        getValue: () => Settings.XPBar,
        setValue: value => Settings.XPBar = value,
        fieldId: FeatureIds.XP_BAR
      );
      // XP Bar fadeout
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_XpBar_Fadeout(),
        tooltip: () => I18n.ConfigMenu_Features_XpBar_Fadeout_Tooltip(),
        getValue: () => Settings.XPBFadeout,
        setValue: value => Settings.XPBFadeout = value,
        fieldId: FeatureIds.XP_BAR_FADEOUT
      );
      // Xp Gain
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_XpGain_Show(),
        tooltip: () => I18n.ConfigMenu_Features_XpBar_Show_Tooltip(),
        getValue: () => Settings.XPGain,
        setValue: value => Settings.XPGain = value,
        fieldId: FeatureIds.XP_GAIN
      );
      // Level up animation
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_LevelUpAnimation_Show(),
        tooltip: () => I18n.ConfigMenu_Features_LevelUpAnimation_Show_Tooltip(),
        getValue: () => Settings.LvlUpAnimation,
        setValue: value => Settings.LvlUpAnimation = value,
        fieldId: FeatureIds.LVL_UP_ANIMATION
      );
      // Heart Fill
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_HeartFills_Show(),
        tooltip: () => I18n.ConfigMenu_Features_HeartFills_Show_Tooltip(),
        getValue: () => Settings.HeartFill,
        setValue: value => Settings.HeartFill = value,
        fieldId: FeatureIds.HEART_FILL
      );
      // Extra item information
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_ExtraItemInfo_Show(),
        tooltip: () => I18n.ConfigMenu_Features_ExtraItemInfo_Show_Tooltip(),
        getValue: () => Settings.ExtraItemInformation,
        setValue: value => Settings.ExtraItemInformation = value,
        fieldId: FeatureIds.EXTRA_ITEM_INFORMATION
      );
      // Show NPC location on map
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_NpcOnMap_Show(),
        tooltip: () => I18n.ConfigMenu_Features_NpcOnMap_Show_Tooltip(),
        getValue: () => Settings.NpcLocationOnMap,
        setValue: value => Settings.NpcLocationOnMap = value,
        fieldId: FeatureIds.NPC_LOCATION_ON_MAP
      );
      // Show luck icon
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_LuckIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_LuckIcon_Show_Tooltip(),
        getValue: () => Settings.ShowLuckIcon,
        setValue: value => Settings.ShowLuckIcon = value,
        fieldId: FeatureIds.SHOW_LUCK_ICON
      );
      // Show traveler merchant icon
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_TravelerMerchantIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_TravelerMerchantIcon_Show_Tooltip(),
        getValue: () => Settings.ShowTravelerMerchantIcon,
        setValue: value => Settings.ShowTravelerMerchantIcon = value,
        fieldId: FeatureIds.SHOW_TRAVELER_MERCHANT_ICON
      );
      // Show rainy day icon
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_RayniDayIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_RayniDayIcon_Show_Tooltip(),
        getValue: () => Settings.ShowRainyDayIcon,
        setValue: value => Settings.ShowRainyDayIcon = value,
        fieldId: FeatureIds.SHOW_RAINY_DAY_ICON
      );
      // Show birthday icon
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_BirthdayIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_BirthdayIcon_Show_Tooltip(),
        getValue: () => Settings.ShowBirthdayIcon,
        setValue: value => Settings.ShowBirthdayIcon = value,
        fieldId: FeatureIds.SHOW_BIRTHDAY_ICON
      );
      // Show today gift icon
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_TodaysGiftIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_TodaysGiftIcon_Show_Tooltip(),
        getValue: () => Settings.ShowTodaysGiftsIcon,
        setValue: value => Settings.ShowTodaysGiftsIcon = value,
        fieldId: FeatureIds.SHOW_TODAYS_GIFT_ICON
      );
      // Show when new recipes are available icon
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_NewRecipeIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_NewRecipeIcon_Show_Tooltip(),
        getValue: () => Settings.ShowNewRecipesIcon,
        setValue: value => Settings.ShowNewRecipesIcon = value,
        fieldId: FeatureIds.SHOW_NEW_RECIPES_ICON
      );
      // Show tool upgrade status icon
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_ToolUpgradeIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_ToolUpgradeIcon_Show_Tooltip(),
        getValue: () => Settings.ShowToolUpgradeStatusIcon,
        setValue: value => Settings.ShowToolUpgradeStatusIcon = value,
        fieldId: FeatureIds.SHOW_TOOL_UPGRADE_STATUS_ICON
      );
      // Show Robin building status icon
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_RobinIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_RobinIcon_Show_Tooltip(),
        getValue: () => Settings.ShowRobinBuildingStatusIcon,
        setValue: value => Settings.ShowRobinBuildingStatusIcon = value,
        fieldId: FeatureIds.SHOW_ROBIN_BUILDING_STATUS
      );
      //Show seasonal berry icon
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_SeasonalBerryIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_SeasonalBerryIcon_Show_Tooltip(),
        getValue: () => Settings.ShowSeasonalBerryIcon,
        setValue: value => Settings.ShowSeasonalBerryIcon = value,
        fieldId: FeatureIds.SHOW_SEASONAL_BERRY_ICON
      );
      // Show crop/barrel tooltip
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_CropBarrelTooltip_Show(),
        tooltip: () => I18n.ConfigMenu_Features_CropBarrelTooltip_Show_Tooltip(),
        getValue: () => Settings.ShowCropAndBarrelTooltip,
        setValue: value => Settings.ShowCropAndBarrelTooltip = value,
        fieldId: FeatureIds.SHOW_CROP_AND_BARREL_TOOLTIP
      );
      // Show animals need pets
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_AnimalPets_Show(),
        tooltip: () => I18n.ConfigMenu_Features_AnimalPets_Show_Tooltip(),
        getValue: () => Settings.ShowAnimalsNeedsPets,
        setValue: value => Settings.ShowAnimalsNeedsPets = value,
        fieldId: FeatureIds.SHOW_ANIMALS_NEED_PETS
      );
      // Show item effect ranges
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_ItemEffectRange_Show(),
        tooltip: () => I18n.ConfigMenu_Features_ItemEffectRange_Show_Tooltip(),
        getValue: () => Settings.ShowItemEffectRanges,
        setValue: value => Settings.ShowItemEffectRanges = value,
        fieldId: FeatureIds.SHOW_ITEM_EFFECT_RANGE
      );
      // Show items required for bundles
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_ItemBundle_Show(),
        tooltip: () => I18n.ConfigMenu_Features_ItemBundle_Show_Tooltip(),
        getValue: () => Settings.ShowItemsRequiredForBundle,
        setValue: value => Settings.ShowItemsRequiredForBundle = value,
        fieldId: FeatureIds.SHOW_ITEMS_REQUIRED_FOR_BUNDLES
      );
      // Show harvest prices in shop
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_HarvestPriceInShop_Show(),
        tooltip: () => I18n.ConfigMenu_Features_HarvestPriceInShop_Show_Tooltip(),
        getValue: () => Settings.ShowHarvestPricesInShop,
        setValue: value => Settings.ShowHarvestPricesInShop = value,
        fieldId: FeatureIds.SHOW_HARVEST_PRICES_IN_SHOP
      );
      // Display calendar and billboard
      Gmcm.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_CalendarBilboard_Show(),
        tooltip: () => I18n.ConfigMenu_Features_CalendarBilboard_Show_Tooltip(),
        getValue: () => Settings.DisplayCalendarAndBilboard,
        setValue: value => Settings.DisplayCalendarAndBilboard = value,
        fieldId: FeatureIds.DISPLAY_CALENDAR_AND_BILBOARD
      );
    }
  }
}