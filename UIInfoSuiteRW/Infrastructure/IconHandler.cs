using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace UIInfoSuiteRW.Infrastructure;

public sealed class IconHandler
{
  private readonly PerScreen<int> _amountOfVisibleIcons = new();

  private IconHandler() { }

  public static IconHandler Handler { get; } = new();

  public bool IsQuestLogPermanent { get; set; } = false;

  public Point GetNewIconPosition()
  {
    //ModEntry.MonitorObject.Log($"{new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name}", LogLevel.Warn);
    int yPos = Game1.options.zoomButtons ? 290 : 260;
    int xPosition = Tools.GetWidthInPlayArea() - 70 - 48 * _amountOfVisibleIcons.Value;
    if (IsQuestLogPermanent || Game1.player.questLog.Any() || Game1.player.team.specialOrders.Any())
    {
      xPosition -= 65;
    }

    ++_amountOfVisibleIcons.Value;
    return new Point(xPosition, yPos);
  }

  public void Reset(object? sender, EventArgs e)
  {
    _amountOfVisibleIcons.Value = 0;
  }
}