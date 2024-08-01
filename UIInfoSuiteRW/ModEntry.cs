using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using UIInfoSuiteRW.Framework;
using UIInfoSuiteRW.Utils;

namespace UIInfoSuiteRW
{
  internal sealed class ModEntry : Mod
  {
    public static IMonitor MonitorObject = null!;
    public static IReflectionHelper Reflection { get; private set; } = null!;
    private static ModConfigManager ModConfigM = null!;
    private static FeatureManager FeatureM = null!;

    public override void Entry(IModHelper helper)
    {
      Reflection = helper.Reflection;
      MonitorObject = Monitor;

      I18n.Init(helper.Translation);

      // Registering events
      helper.Events.GameLoop.GameLaunched += OnGameLaunched;
      helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
      helper.Events.Input.ButtonPressed += OnButtonPressed;
      helper.Events.Display.Rendering += IconHandler.Handler.Reset;
    }

    private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
    {
      ModConfigM = new ModConfigManager(Helper, ModManifest);
      
      ModConfigM.GenericModConfigMenu?.OnFieldChanged(
        ModManifest, 
        (string id, object obj) => {
          if (obj is bool v)
          {
            ModConfigM.Config.FeatureConfig[id] = v;
            FeatureM.ToggleFeature(id, ModConfigM.Config);
          }
        }
      );

      FeatureM = new FeatureManager(Helper, ModConfigM.Config);
    }

    private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
    {
      if (!Context.IsWorldReady)
        return;
      
      if (Game1.activeClickableMenu == null)
      {
        if (ModConfigM.Config.OpenCalendarKeybind.JustPressed())
        {
          Game1.activeClickableMenu = new Billboard();
        }
        else if (ModConfigM.Config.OpenQuestBoardKeybind.JustPressed())
        {
          Game1.RefreshQuestOfTheDay();
          Game1.activeClickableMenu = new Billboard(true);
        }
      }
    }

    private void OnSaveLoaded(object? sender, SaveLoadedEventArgs e)
    {
      foreach(var feature in FeatureM.Features)
      {
        FeatureM.ToggleFeature(feature.Key, ModConfigM.Config);
      }
    }
  }
}
