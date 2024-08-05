using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Menus;

namespace UIInfoSuiteRW.Features.HUDElements
{
  public sealed class HUDSeasonalForagingItemIcon
  {
    public static ClickableTextureComponent Create(Point iconPosition, Rectangle? itemSpriteLocation, float spriteScale)
    {
      return new ClickableTextureComponent(
        new Rectangle(iconPosition.X, iconPosition.Y, 40, 40),
        Game1.objectSpriteSheet,
        itemSpriteLocation!.Value,
        spriteScale
      );
    }
  }
}