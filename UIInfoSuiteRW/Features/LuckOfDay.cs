using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Menus;
using UIInfoSuiteRW.Features.HUDElements;
using UIInfoSuiteRW.Utils;

namespace UIInfoSuiteRW.Features
{
  internal class LuckOfDay : IFeature
  {
    #region Properties
    private readonly PerScreen<string> _hoverText = new(() => string.Empty);
    private readonly PerScreen<Color> _color = new(() => new Color(Color.White.ToVector4()));
    private int _sourceRectX = 0;
    private int _sourceRectY = 0;

    private readonly PerScreen<ClickableTextureComponent> _icon = new(
      () => HUDLuckIcon.Create()
    );

    private readonly IModHelper _helper;

    private static readonly Color Luck1Color = new(87, 255, 106, 255);
    private static readonly Color Luck2Color = new(148, 255, 210, 255);
    private static readonly Color Luck3Color = new(246, 255, 145, 255);
    private static readonly Color Luck4Color = new(255, 255, 255, 255);
    private static readonly Color Luck5Color = new(255, 155, 155, 255);
    private static readonly Color Luck6Color = new(165, 165, 165, 204);
    #endregion

    #region Lifecycle
    public LuckOfDay(IModHelper helper)
    {
      _helper = helper;
    }

    public void ToggleOption(bool toggle)
    {
      _helper.Events.Player.Warped -= OnWarped;
      _helper.Events.Display.RenderingHud -= OnRenderingHud;
      _helper.Events.Display.RenderedHud -= OnRenderedHud;
      _helper.Events.GameLoop.UpdateTicked -= OnUpdateTicked;

      if (toggle)
      {
        AdjustIconXToBlackBorder();
        _helper.Events.Player.Warped += OnWarped;
        _helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
        _helper.Events.Display.RenderingHud += OnRenderingHud;
        _helper.Events.Display.RenderedHud += OnRenderedHud;
      }
    }
    #endregion

    #region Event subscriptions
    private void OnUpdateTicked(object? sender, UpdateTickedEventArgs e)
    {
      CalculateLuck(e);
    }

    private void OnRenderedHud(object? sender, RenderedHudEventArgs e)
    {
      // Draw hover text
      if (_icon.Value.containsPoint(Game1.getMouseX(), Game1.getMouseY()))
      {
        IClickableMenu.drawHoverText(Game1.spriteBatch, _hoverText.Value, Game1.dialogueFont);
      }
    }

    private void OnRenderingHud(object? sender, RenderingHudEventArgs e)
    {
      // draw junimo icon
      if (UIElementUtils.IsRenderingNormally())
      {
        Point iconPosition = IconHandler.Handler.GetNewIconPosition();
        ClickableTextureComponent icon = _icon.Value;
        icon.bounds.X = iconPosition.X;
        icon.bounds.Y = iconPosition.Y;
        icon.sourceRect.X = _sourceRectX;
        icon.sourceRect.Y = _sourceRectY;
        _icon.Value = icon;
        _icon.Value.draw(Game1.spriteBatch, _color.Value, 1f);
      }
    }

    private void OnWarped(object? sender, WarpedEventArgs e)
    {
      // adjust icon X to black border
      if (e.IsLocalPlayer)
      {
        AdjustIconXToBlackBorder();
      }
    }
    #endregion

    #region Logic
    private void CalculateLuck(UpdateTickedEventArgs e)
    {
      if (e.IsMultipleOf(30)) // half second
      {
        switch (Game1.player.DailyLuck)
        {
          // Spirits are very happy (FeelingLucky)
          case > 0.07:
            _hoverText.Value = $"{I18n.LuckStatus1()}\n({Game1.player.DailyLuck * 1000}/125)";
            _color.Value = Luck1Color;
            _sourceRectX = 80;
            _sourceRectY = 80;
            break;
          // Spirits are in good humor (LuckyButNotTooLucky)
          case <= 0.07 and > 0.02:
            _hoverText.Value = $"{I18n.LuckStatus2()}\n({Game1.player.DailyLuck * 1000}/125)";
            _color.Value = Luck2Color;
            _sourceRectX = 64;
            _sourceRectY = 80;
            break;
          // The spirits feel absolutely neutral
          case 0:
            _hoverText.Value = $"{I18n.LuckStatus4()}\n({Game1.player.DailyLuck * 1000}/125)";
            _color.Value = Luck4Color;
            _sourceRectX = 48;
            _sourceRectY = 16;
            break;
          // The spirits feel neutral
          case <= 0.02 and >= -0.02:
            _hoverText.Value = $"{I18n.LuckStatus3()}\n({Game1.player.DailyLuck * 1000}/125)";
            _color.Value = Luck3Color;
            _sourceRectX = 0;
            _sourceRectY = 0;
            break;
          // The spirits are somewhat annoyed (NotFeelingLuckyAtAll)
          case >= -0.07 and < -0.02:
            _hoverText.Value = $"{I18n.LuckStatus5()}\n({Game1.player.DailyLuck * 1000}/125)";
            _color.Value = Luck5Color;
            _sourceRectX = 64;
            _sourceRectY = 16;
            break;
          // The spirits are very displeased (MaybeStayHome)
          case < -0.07:
            _hoverText.Value = $"{I18n.LuckStatus6()}\n({Game1.player.DailyLuck * 1000}/125)";
            _color.Value = Luck6Color;
            _sourceRectX = 112;
            _sourceRectY = 16;
            break;
        }
      }
    }

    private void AdjustIconXToBlackBorder()
    {
      _icon.Value = HUDLuckIcon.Create();
    }
    #endregion
  }
}
