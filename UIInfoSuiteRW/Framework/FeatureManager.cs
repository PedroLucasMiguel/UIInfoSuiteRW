using System.Collections.Generic;
using StardewModdingAPI;
using UIInfoSuiteRW.Features;

namespace UIInfoSuiteRW.Framework
{
    internal class FeatureManager
    {
      public Dictionary<string, object> Features = null!;

      public FeatureManager(IModHelper helper)
      {
        Features = new(){
          { FeatureIds.XP_BAR, new ExperienceBar(helper) },
          { FeatureIds.NPC_LOCATION_ON_MAP, new LocationOfTownsfolk(helper, ModEntry.ModCFMGR.Settings) },
          { FeatureIds.SHOW_LUCK_ICON, new LuckOfDay(helper) },
          { FeatureIds.SHOW_HARVEST_PRICES_IN_SHOP, new ShopHarvestPrices(helper) },
          { FeatureIds.HEART_FILL, new ShowAccurateHearts(helper.Events) },
          { FeatureIds.SHOW_BIRTHDAY_ICON, new ShowBirthdayIcon(helper) },
          { FeatureIds.DISPLAY_CALENDAR_AND_BILBOARD, new ShowCalendarAndBillboardOnGameMenuButton(helper) },
          { FeatureIds.SHOW_CROP_AND_BARREL_TOOLTIP, new ShowCropAndBarrelTime(helper) },
          { FeatureIds.SHOW_ITEM_EFFECT_RANGE, new ShowItemEffectRanges(helper) },
          { FeatureIds.EXTRA_ITEM_INFORMATION, new ShowItemHoverInformation(helper) },
          { FeatureIds.SHOW_NEW_RECIPES_ICON, new ShowQueenOfSauceIcon(helper) },
          { FeatureIds.SHOW_RAINY_DAY_ICON, new ShowRainyDayIcon(helper) },
          { FeatureIds.SHOW_ROBIN_BUILDING_STATUS, new ShowRobinBuildingStatusIcon(helper) },
          { FeatureIds.SHOW_SEASONAL_BERRY_ICON, new ShowSeasonalBerry(helper) },
          { FeatureIds.SHOW_TODAYS_GIFT_ICON, new ShowTodaysGifts(helper) },
          { FeatureIds.SHOW_TOOL_UPGRADE_STATUS_ICON, new ShowToolUpgradeStatus(helper) },
          { FeatureIds.SHOW_TRAVELER_MERCHANT_ICON, new ShowTravelingMerchant(helper) },
          { FeatureIds.SHOW_ANIMALS_NEED_PETS, new ShowWhenAnimalNeedsPet(helper) }
        };
      }

      public void ToggleFeature(string featureId)
      {
        switch (featureId)
        {
          case FeatureIds.XP_BAR:
           ((ExperienceBar)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.XPBar,
              ModEntry.ModCFMGR.Settings.XPBFadeout,
              ModEntry.ModCFMGR.Settings.XPGain,
              ModEntry.ModCFMGR.Settings.LvlUpAnimation
            );
            break;
          case FeatureIds.NPC_LOCATION_ON_MAP:
            ((LocationOfTownsfolk)Features[featureId]).ToggleShowNPCLocationsOnMap(
              ModEntry.ModCFMGR.Settings.NpcLocationOnMap
            );
            break;
          case FeatureIds.SHOW_LUCK_ICON:
            ((LuckOfDay)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.ShowLuckIcon
            );
            break;
          case FeatureIds.SHOW_HARVEST_PRICES_IN_SHOP:
            ((ShopHarvestPrices)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.ShowHarvestPricesInShop
            );
            break;
          case FeatureIds.HEART_FILL:
            ((ShowAccurateHearts)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.HeartFill
            );
            break;
          case FeatureIds.SHOW_BIRTHDAY_ICON:
            ((ShowBirthdayIcon)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.ShowBirthdayIcon
            );
            break;
          case FeatureIds.DISPLAY_CALENDAR_AND_BILBOARD:
            ((ShowCalendarAndBillboardOnGameMenuButton)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.DisplayCalendarAndBilboard
            );
            break;
          case FeatureIds.SHOW_CROP_AND_BARREL_TOOLTIP:
            ((ShowCropAndBarrelTime)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.ShowCropAndBarrelTooltip
            );
            break;
          case FeatureIds.SHOW_ITEM_EFFECT_RANGE:
            ((ShowItemEffectRanges)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.ShowItemEffectRanges
            );
            break;
          case FeatureIds.EXTRA_ITEM_INFORMATION:
            ((ShowItemHoverInformation)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.ExtraItemInformation
            );
            break;
          case FeatureIds.SHOW_NEW_RECIPES_ICON:
            ((ShowQueenOfSauceIcon)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.ShowNewRecipesIcon
            );
            break;
          case FeatureIds.SHOW_RAINY_DAY_ICON:
            ((ShowRainyDayIcon)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.ShowRainyDayIcon
            );
            break;
          case FeatureIds.SHOW_ROBIN_BUILDING_STATUS:
            ((ShowRobinBuildingStatusIcon)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.ShowRobinBuildingStatusIcon
            );
            break;
          case FeatureIds.SHOW_SEASONAL_BERRY_ICON:
            ((ShowSeasonalBerry)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.ShowSeasonalBerryIcon
            );
            break;
          case FeatureIds.SHOW_TODAYS_GIFT_ICON:
            ((ShowTodaysGifts)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.ShowTodaysGiftsIcon
            );
            break;
          case FeatureIds.SHOW_TOOL_UPGRADE_STATUS_ICON:
            ((ShowToolUpgradeStatus)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.ShowToolUpgradeStatusIcon
            );
            break;
          case FeatureIds.SHOW_TRAVELER_MERCHANT_ICON:
            ((ShowTravelingMerchant)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.ShowTravelerMerchantIcon
            );
            break;
          case FeatureIds.SHOW_ANIMALS_NEED_PETS:
            ((ShowWhenAnimalNeedsPet)Features[featureId]).ToggleOption(
              ModEntry.ModCFMGR.Settings.ShowAnimalsNeedsPets
            );
            break;
        }
      }
    }
}