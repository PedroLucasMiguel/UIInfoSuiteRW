using System.Linq;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;

namespace UIInfoSuiteRW.Features
{
  public sealed class Testing : IFeature
  { 

    private readonly IModHelper _helper;
    private CommunityCenter? _communityCenter = null;

    public Testing(IModHelper helper)
    {
      _helper = helper;
    }

    public void ToggleOption(bool toggle)
    {
      _helper.Events.GameLoop.OneSecondUpdateTicked -= OnOneSecondUpdateTicked;

      if (toggle)
      {
        _helper.Events.GameLoop.OneSecondUpdateTicked += OnOneSecondUpdateTicked;
      }
    }

    public void OnOneSecondUpdateTicked(object? sender, OneSecondUpdateTickedEventArgs e)
    {
      if (_communityCenter == null)
      {
        _communityCenter = Game1.RequireLocation<CommunityCenter>("CommunityCenter");
        _communityCenter.refreshBundlesIngredientsInfo();
      }

      Toolbar? toolbar = (Toolbar)Game1.onScreenMenus.First(tb => tb is Toolbar);
      
      if (toolbar != null && toolbar.hoverItem is Object o)
      {
        ModEntry.MonitorObject.Log($"{toolbar.hoverItem.Name}", LogLevel.Warn);
        if(_communityCenter.couldThisIngredienteBeUsedInABundle(o))
        {  
          ModEntry.MonitorObject.Log("BUNDLE!", LogLevel.Warn);
        }
      }
      /*
      InventoryMenu
      if (Context toolbar.hoverItem != null)
      {
        if(Game1.RequireLocation<CommunityCenter>("CommunityCenter").couldThisIngredienteBeUsedInABundle((Object)toolbar.hoverItem))
        {  
          ModEntry.MonitorObject.Log("BUNDLE!", LogLevel.Warn);
        }
      }*/
      if (GameMenu.bundleItemHovered)
      {
        ModEntry.MonitorObject.Log("BUNDLE!", LogLevel.Warn);
      }
    }
  }
}