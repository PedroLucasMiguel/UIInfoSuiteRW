using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using UIInfoSuiteRW.Framework;
using UIInfoSuiteRW.Infrastructure;

namespace UIInfoSuiteRW
{
  internal sealed class ModEntry : Mod
  {
    //private static SkipIntro _skipIntro; // Needed so GC won't throw away object with subscriptions
    public static ModConfigManager ModCFG = null!;
    public static IMonitor MonitorObject = null!;
    public static IReflectionHelper Reflection { get; private set; } = null!;
    private static FeatureManager FMGR = null!;

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
      // Initializing configuration
      ModCFG = new ModConfigManager(Helper, ModManifest);
      FMGR = new FeatureManager(Helper);
    }

    private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
    {
      if (!Context.IsWorldReady)
        return;

      if (ModCFG.Settings.OpenCalendarKeybind.JustPressed())
      {
        Game1.activeClickableMenu = new Billboard();
      }
      else if (ModCFG.Settings.OpenQuestBoardKeybind.JustPressed())
      {
        Game1.RefreshQuestOfTheDay();
        Game1.activeClickableMenu = new Billboard(true);
      }
    }

    private void OnSaveLoaded(object? sender, SaveLoadedEventArgs e)
    {
      foreach(var featureConfig in ModCFG.Settings.FeaturesConfig)
      {
        FMGR.ToggleFeature(featureConfig.Key);
      }
    }
  }
}
