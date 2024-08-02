using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Menus;

namespace UIInfoSuiteRW.Features.HUDElements
{
  public sealed class HudQueenOfSauceIcon
  {
    public static ClickableTextureComponent Create(Point iconPosition)
    {
      return new ClickableTextureComponent(
        new Rectangle(iconPosition.X, iconPosition.Y, 40, 40),
        Game1.mouseCursors,
        new Rectangle(609, 361, 28, 28),
        1.3f
      );
    }
  }
}