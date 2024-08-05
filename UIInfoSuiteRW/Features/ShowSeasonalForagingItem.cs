using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using UIInfoSuiteRW.Features.HUDElements;
using UIInfoSuiteRW.Utils;

namespace UIInfoSuiteRW.Features
{
  internal class ShowSeasonalForagingItem : IFeature
  {
    #region Properties
    private Rectangle? _itemSpriteLocation;
    private float _spriteScale = 8 / 3f;
    private string _hoverText = null!;
    private ClickableTextureComponent _itemIcon = null!;

    private readonly IModHelper _helper;
    #endregion

    #region Lifecycle
    public ShowSeasonalForagingItem(IModHelper helper)
    {
      _helper = helper;
    }

    public void ToggleOption(bool toggle)
    {
      _itemSpriteLocation = null;
      _helper.Events.GameLoop.DayStarted -= OnDayStarted;
      _helper.Events.Display.RenderingHud -= OnRenderingHud;
      _helper.Events.Display.RenderedHud -= OnRenderedHud;

      if (toggle)
      {
        UpdateBerryForDay();

        _helper.Events.GameLoop.DayStarted += OnDayStarted;
        _helper.Events.Display.RenderingHud += OnRenderingHud;
        _helper.Events.Display.RenderedHud += OnRenderedHud;
      }
    }
    #endregion

    #region Event subscriptions
    private void OnDayStarted(object? sender, DayStartedEventArgs e)
    {
      UpdateBerryForDay();
    }

    private void OnRenderingHud(object? sender, RenderingHudEventArgs e)
    {
      // Draw icon
      if (!UIElementUtils.IsRenderingNormally() || !_itemSpriteLocation.HasValue)
      {
        return;
      }

      Point iconPosition = IconHandler.Handler.GetNewIconPosition();
      _itemIcon = HUDSeasonalForagingItemIcon.Create(iconPosition, _itemSpriteLocation, _spriteScale);
      _itemIcon.draw(Game1.spriteBatch);
    }

    private void OnRenderedHud(object? sender, RenderedHudEventArgs e)
    {
      // Show text on hover
      bool hasMouse = _itemIcon?.containsPoint(Game1.getMouseX(), Game1.getMouseY()) ?? false;
      bool hasText = !string.IsNullOrEmpty(_hoverText);
      if (_itemSpriteLocation.HasValue && hasMouse && hasText)
      {
        IClickableMenu.drawHoverText(Game1.spriteBatch, _hoverText, Game1.dialogueFont);
      }
    }
    #endregion

    #region Logic
    private void UpdateBerryForDay()
    {
      string? season = Game1.currentSeason;
      int day = Game1.dayOfMonth;

      if (season == "spring")
      {
        // Salmon Berry
        if (day >= 15 && day <= 18)
        {
          _itemSpriteLocation = new Rectangle(128, 193, 15, 15);
          _hoverText = I18n.CanFindSalmonberry();
          _spriteScale = 8 / 3f;
        }
        // Spring onion
        else
        {
          _itemSpriteLocation = new Rectangle(240, 256, 16, 16);
          _hoverText = I18n.CanFindSpringOnion();
          _spriteScale = 8 / 3f;
        }
      }
      else if (season == "summer")
      {
        // Fiddle Fern
        _itemSpriteLocation = new Rectangle(304, 160, 16, 16);
        _hoverText = I18n.CanFindFiddleheadFern();
        _spriteScale = 8 / 3f;
      }
      else if (season == "fall")
      {
        // Black Berry
        if (day >= 8 && day <= 11)
        {
          _itemSpriteLocation = new Rectangle(32, 272, 16, 16);
          _hoverText = I18n.CanFindBlackberry();
          _spriteScale = 5 / 2f;
        }
        // Hazelnut
        else if(day >= 15)
        {
          _itemSpriteLocation = new Rectangle(1, 274, 14, 14);
          _hoverText = I18n.CanFindHazelnut();
          _spriteScale = 20 / 7f;
        }
        else
        {
          _itemSpriteLocation = null;
        }
      }
      else
      {
        _itemSpriteLocation = null;
      }
    }
    #endregion
  }
}
