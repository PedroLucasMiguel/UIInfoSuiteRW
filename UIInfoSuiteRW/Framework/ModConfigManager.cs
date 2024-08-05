using StardewModdingAPI;
using UIInfoSuiteRW.Features;

namespace UIInfoSuiteRW.Framework
{
  public sealed class ModConfigManager
  {
    private const string GENERIC_MOD_MENU_CONFIG_UID = "spacechase0.GenericModConfigMenu";
    public ModConfig Config { get; set; } = null!;
    public IGenericModConfigMenuApi? GenericModConfigMenu { get; set; }
    private IManifest Manifest { get; set; } = null!;
    private IModHelper Helper { get; set; } = null!;

    public ModConfigManager(IModHelper helper, IManifest manifest)
    {
      Manifest = manifest;
      Helper = helper;
      Config = helper.ReadConfig<ModConfig>();
      GenericModConfigMenu = helper.ModRegistry.GetApi<IGenericModConfigMenuApi>(GENERIC_MOD_MENU_CONFIG_UID);

      if (GenericModConfigMenu == null)
        return;

      CreateMenu();
    }

    private void CreateMenu()
    {
      GenericModConfigMenu!.Register(Manifest, () => Config = new ModConfig(), () => Helper.WriteConfig(Config));

      // Keybinds
      GenericModConfigMenu.AddSectionTitle(
        Manifest,
        text: () => I18n.ConfigMenu_Keybinds_SectionTitle(),
        tooltip: () => I18n.ConfigMenu_Keybinds_SectionTooltip()
      );
      GenericModConfigMenu.AddKeybindList(
        Manifest,
        name: () => I18n.ConfigMenu_Keybinds_OpenCalendar(),
        tooltip: () => I18n.ConfigMenu_Keybinds_OpenCalendarTooltip(),
        getValue: () => Config.OpenCalendarKeybind,
        setValue: value => Config.OpenCalendarKeybind = value
      );
      GenericModConfigMenu.AddKeybindList(
        Manifest,
        name: () => I18n.ConfigMenu_Keybinds_OpenQuestBoard(),
        tooltip: () => I18n.ConfigMenu_Keybinds_OpenQuestBoardTooltip(),
        getValue: () => Config.OpenQuestBoardKeybind,
        setValue: value => Config.OpenQuestBoardKeybind = value
      );
      GenericModConfigMenu.AddKeybindList(
        Manifest,
        name: () => I18n.ConfigMenu_Keybinds_ShowPlacedObjectRange(),
        tooltip: () => I18n.ConfigMenu_Keybinds_ShowPlacedObjectRangeTooltip(),
        getValue: () => Config.ShowPlacedObjectRange,
        setValue: value => Config.ShowPlacedObjectRange = value
      );

      // Tweaks
      GenericModConfigMenu.AddSectionTitle(
        Manifest,
        text: () => I18n.ConfigMenu_Features_SectionTitle(),
        tooltip: () => I18n.ConfigMenu_Features_SectionTooltip()
      );
      // XP Bar show
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_XpBar_Show(),
        tooltip: () => I18n.ConfigMenu_Features_XpBar_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.XP_BAR],
        setValue: value => Config.FeatureConfig[FeatureIds.XP_BAR] = value,
        fieldId: FeatureIds.XP_BAR
      );
      // XP Bar fadeout
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_XpBar_Fadeout(),
        tooltip: () => I18n.ConfigMenu_Features_XpBar_Fadeout_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.XP_BAR_FADEOUT],
        setValue: value => Config.FeatureConfig[FeatureIds.XP_BAR_FADEOUT] = value,
        fieldId: FeatureIds.XP_BAR_FADEOUT
      );
      // Xp Gain
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_XpGain_Show(),
        tooltip: () => I18n.ConfigMenu_Features_XpBar_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.XP_GAIN],
        setValue: value => Config.FeatureConfig[FeatureIds.XP_GAIN] = value,
        fieldId: FeatureIds.XP_GAIN
      );
      // Level up animation
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_LevelUpAnimation_Show(),
        tooltip: () => I18n.ConfigMenu_Features_LevelUpAnimation_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.LVL_UP_ANIMATION],
        setValue: value => Config.FeatureConfig[FeatureIds.LVL_UP_ANIMATION] = value,
        fieldId: FeatureIds.LVL_UP_ANIMATION
      );
      // Heart Fill
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_HeartFills_Show(),
        tooltip: () => I18n.ConfigMenu_Features_HeartFills_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.HEART_FILL],
        setValue: value => Config.FeatureConfig[FeatureIds.HEART_FILL] = value,
        fieldId: FeatureIds.HEART_FILL
      );
      // Extra item information
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_ExtraItemInfo_Show(),
        tooltip: () => I18n.ConfigMenu_Features_ExtraItemInfo_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.EXTRA_ITEM_INFORMATION],
        setValue: value => Config.FeatureConfig[FeatureIds.EXTRA_ITEM_INFORMATION] = value,
        fieldId: FeatureIds.EXTRA_ITEM_INFORMATION
      );
      // Show NPC location on map
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_NpcOnMap_Show(),
        tooltip: () => I18n.ConfigMenu_Features_NpcOnMap_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.NPC_LOCATION_ON_MAP],
        setValue: value => Config.FeatureConfig[FeatureIds.NPC_LOCATION_ON_MAP] = value,
        fieldId: FeatureIds.NPC_LOCATION_ON_MAP
      );
      // Show luck icon
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_LuckIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_LuckIcon_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.SHOW_LUCK_ICON],
        setValue: value => Config.FeatureConfig[FeatureIds.SHOW_LUCK_ICON] = value,
        fieldId: FeatureIds.SHOW_LUCK_ICON
      );
      // Show traveler merchant icon
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_TravelerMerchantIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_TravelerMerchantIcon_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.SHOW_TRAVELER_MERCHANT_ICON],
        setValue: value => Config.FeatureConfig[FeatureIds.SHOW_TRAVELER_MERCHANT_ICON] = value,
        fieldId: FeatureIds.SHOW_TRAVELER_MERCHANT_ICON
      );
      // Show rainy day icon
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_RayniDayIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_RayniDayIcon_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.SHOW_RAINY_DAY_ICON],
        setValue: value => Config.FeatureConfig[FeatureIds.SHOW_RAINY_DAY_ICON] = value,
        fieldId: FeatureIds.SHOW_RAINY_DAY_ICON
      );
      // Show birthday icon
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_BirthdayIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_BirthdayIcon_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.SHOW_BIRTHDAY_ICON],
        setValue: value => Config.FeatureConfig[FeatureIds.SHOW_BIRTHDAY_ICON] = value,
        fieldId: FeatureIds.SHOW_BIRTHDAY_ICON
      );
      // Show today gift icon
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_TodaysGiftIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_TodaysGiftIcon_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.SHOW_TODAYS_GIFT_ICON],
        setValue: value => Config.FeatureConfig[FeatureIds.SHOW_TODAYS_GIFT_ICON] = value,
        fieldId: FeatureIds.SHOW_TODAYS_GIFT_ICON
      );
      // Show when new recipes are available icon
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_NewRecipeIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_NewRecipeIcon_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.SHOW_NEW_RECIPES_ICON],
        setValue: value => Config.FeatureConfig[FeatureIds.SHOW_NEW_RECIPES_ICON] = value,
        fieldId: FeatureIds.SHOW_NEW_RECIPES_ICON
      );
      // Show tool upgrade status icon
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_ToolUpgradeIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_ToolUpgradeIcon_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.SHOW_TOOL_UPGRADE_STATUS_ICON],
        setValue: value => Config.FeatureConfig[FeatureIds.SHOW_TOOL_UPGRADE_STATUS_ICON] = value,
        fieldId: FeatureIds.SHOW_TOOL_UPGRADE_STATUS_ICON
      );
      // Show Robin building status icon
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_RobinIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_RobinIcon_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.SHOW_ROBIN_BUILDING_STATUS],
        setValue: value => Config.FeatureConfig[FeatureIds.SHOW_ROBIN_BUILDING_STATUS] = value,
        fieldId: FeatureIds.SHOW_ROBIN_BUILDING_STATUS
      );
      //Show seasonal berry icon
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_SeasonalForagingIcon_Show(),
        tooltip: () => I18n.ConfigMenu_Features_SeasonalForagingIcon_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.SHOW_SEASONAL_FORAGING_ICON],
        setValue: value => Config.FeatureConfig[FeatureIds.SHOW_SEASONAL_FORAGING_ICON] = value,
        fieldId: FeatureIds.SHOW_SEASONAL_FORAGING_ICON
      );
      // Show crop/barrel tooltip
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_CropBuildingTooltip_Show(),
        tooltip: () => I18n.ConfigMenu_Features_CropBuildingTooltip_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.SHOW_CROP_AND_BUILDING_TOOLTIP],
        setValue: value => Config.FeatureConfig[FeatureIds.SHOW_CROP_AND_BUILDING_TOOLTIP] = value,
        fieldId: FeatureIds.SHOW_CROP_AND_BUILDING_TOOLTIP
      );
      // Show animals need pets
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_AnimalIndicators_Show(),
        tooltip: () => I18n.ConfigMenu_Features_AnimalIndicators_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.SHOW_ANIMAL_INDICATORS],
        setValue: value => Config.FeatureConfig[FeatureIds.SHOW_ANIMAL_INDICATORS] = value,
        fieldId: FeatureIds.SHOW_ANIMAL_INDICATORS
      );
      // Show animals need pets
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_HideIndicatorsMaxFriendship_Show(),
        tooltip: () => I18n.ConfigMenu_Features_HideIndicatorsMaxFriendship_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.HIDE_ON_MAX_FRIENDSHIP],
        setValue: value => Config.FeatureConfig[FeatureIds.HIDE_ON_MAX_FRIENDSHIP] = value,
        fieldId: FeatureIds.HIDE_ON_MAX_FRIENDSHIP
      );
      // Show item effect ranges
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_ItemEffectRange_Show(),
        tooltip: () => I18n.ConfigMenu_Features_ItemEffectRange_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.SHOW_ITEM_EFFECT_RANGE],
        setValue: value => Config.FeatureConfig[FeatureIds.SHOW_ITEM_EFFECT_RANGE] = value,
        fieldId: FeatureIds.SHOW_ITEM_EFFECT_RANGE
      );
      // Show items required for bundles
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_ItemBundle_Show(),
        tooltip: () => I18n.ConfigMenu_Features_ItemBundle_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.SHOW_ITEMS_REQUIRED_FOR_BUNDLES],
        setValue: value => Config.FeatureConfig[FeatureIds.SHOW_ITEMS_REQUIRED_FOR_BUNDLES] = value,
        fieldId: FeatureIds.SHOW_ITEMS_REQUIRED_FOR_BUNDLES
      );
      // Show harvest prices in shop
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_HarvestPriceInShop_Show(),
        tooltip: () => I18n.ConfigMenu_Features_HarvestPriceInShop_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.SHOW_HARVEST_PRICES_IN_SHOP],
        setValue: value => Config.FeatureConfig[FeatureIds.SHOW_HARVEST_PRICES_IN_SHOP] = value,
        fieldId: FeatureIds.SHOW_HARVEST_PRICES_IN_SHOP
      );
      // Display calendar and billboard
      GenericModConfigMenu.AddBoolOption(
        Manifest,
        name: () => I18n.ConfigMenu_Features_CalendarBilboard_Show(),
        tooltip: () => I18n.ConfigMenu_Features_CalendarBilboard_Show_Tooltip(),
        getValue: () => Config.FeatureConfig[FeatureIds.DISPLAY_CALENDAR_AND_BILBOARD],
        setValue: value => Config.FeatureConfig[FeatureIds.DISPLAY_CALENDAR_AND_BILBOARD] = value,
        fieldId: FeatureIds.DISPLAY_CALENDAR_AND_BILBOARD
      );
    }
  }
}