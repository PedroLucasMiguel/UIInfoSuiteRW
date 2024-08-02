using System.Collections.Generic;
using StardewModdingAPI;
using UIInfoSuiteRW.Features;

namespace UIInfoSuiteRW.Framework
{
    internal class FeatureManager
    {
      public Dictionary<string, IFeature> Features = null!;

      public FeatureManager(IModHelper helper, ModConfig config)
      {
        Features = new(){
          //{ "TESTING", new Testing(helper) },
          { FeatureIds.XP_BAR, new EXPIndicators(helper, config) },
          { FeatureIds.NPC_LOCATION_ON_MAP, new LocationOfNPCs(helper, config) },
          { FeatureIds.SHOW_LUCK_ICON, new LuckOfDay(helper) },
          { FeatureIds.SHOW_HARVEST_PRICES_IN_SHOP, new ShopHarvestPrices(helper) },
          { FeatureIds.HEART_FILL, new ShowAccurateHearts(helper.Events) },
          { FeatureIds.SHOW_BIRTHDAY_ICON, new ShowBirthdayIcon(helper, config) },
          { FeatureIds.DISPLAY_CALENDAR_AND_BILBOARD, new ShowCalendarAndBillboardOnGameMenuButton(helper) },
          { FeatureIds.SHOW_CROP_AND_BUILDING_TOOLTIP, new ShowCropAndBuildingTooltip(helper) },
          { FeatureIds.SHOW_ITEM_EFFECT_RANGE, new ShowItemEffectRanges(helper, config) },
          { FeatureIds.EXTRA_ITEM_INFORMATION, new ShowItemHoverInformation(helper) },
          { FeatureIds.SHOW_NEW_RECIPES_ICON, new ShowQueenOfSauceIcon(helper) },
          { FeatureIds.SHOW_RAINY_DAY_ICON, new ShowRainyDayIcon(helper) },
          { FeatureIds.SHOW_ROBIN_BUILDING_STATUS, new ShowRobinBuildingStatusIcon(helper) },
          { FeatureIds.SHOW_SEASONAL_BERRY_ICON, new ShowSeasonalBerry(helper) },
          { FeatureIds.SHOW_TODAYS_GIFT_ICON, new ShowTodaysGifts(helper) },
          { FeatureIds.SHOW_TOOL_UPGRADE_STATUS_ICON, new ShowToolUpgradeStatus(helper) },
          { FeatureIds.SHOW_TRAVELER_MERCHANT_ICON, new ShowTravelingMerchant(helper) },
          { FeatureIds.SHOW_ANIMAL_INDICATORS, new ShowAnimalIndicators(helper, config) }
        };
      }

      public void ToggleFeature(string featureId, ModConfig config)
      {
        switch (featureId)
        {
          /*
          case "TESTING":
           Features[featureId].ToggleOption(true);
            break;*/
          case FeatureIds.XP_BAR:
           Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.XP_BAR]);
            break;
          case FeatureIds.NPC_LOCATION_ON_MAP:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.NPC_LOCATION_ON_MAP]
            );
            break;
          case FeatureIds.SHOW_LUCK_ICON:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.SHOW_LUCK_ICON]
            );
            break;
          case FeatureIds.SHOW_HARVEST_PRICES_IN_SHOP:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.SHOW_HARVEST_PRICES_IN_SHOP]
            );
            break;
          case FeatureIds.HEART_FILL:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.HEART_FILL]
            );
            break;
          case FeatureIds.SHOW_BIRTHDAY_ICON:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.SHOW_BIRTHDAY_ICON]
            );
            break;
          case FeatureIds.DISPLAY_CALENDAR_AND_BILBOARD:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.DISPLAY_CALENDAR_AND_BILBOARD]
            );
            break;
          case FeatureIds.SHOW_CROP_AND_BUILDING_TOOLTIP:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.SHOW_CROP_AND_BUILDING_TOOLTIP]
            );
            break;
          case FeatureIds.SHOW_ITEM_EFFECT_RANGE:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.SHOW_ITEM_EFFECT_RANGE]
            );
            break;
          case FeatureIds.EXTRA_ITEM_INFORMATION:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.EXTRA_ITEM_INFORMATION]
            );
            break;
          case FeatureIds.SHOW_NEW_RECIPES_ICON:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.SHOW_NEW_RECIPES_ICON]
            );
            break;
          case FeatureIds.SHOW_RAINY_DAY_ICON:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.SHOW_RAINY_DAY_ICON]
            );
            break;
          case FeatureIds.SHOW_ROBIN_BUILDING_STATUS:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.SHOW_ROBIN_BUILDING_STATUS]
            );
            break;
          case FeatureIds.SHOW_SEASONAL_BERRY_ICON:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.SHOW_SEASONAL_BERRY_ICON]
            );
            break;
          case FeatureIds.SHOW_TODAYS_GIFT_ICON:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.SHOW_TODAYS_GIFT_ICON]
            );
            break;
          case FeatureIds.SHOW_TOOL_UPGRADE_STATUS_ICON:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.SHOW_TOOL_UPGRADE_STATUS_ICON]
            );
            break;
          case FeatureIds.SHOW_TRAVELER_MERCHANT_ICON:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.SHOW_TRAVELER_MERCHANT_ICON]
            );
            break;
          case FeatureIds.SHOW_ANIMAL_INDICATORS:
            Features[featureId].ToggleOption(
              config.FeatureConfig[FeatureIds.SHOW_ANIMAL_INDICATORS]
            );
            break;
        }
      }
    }
}