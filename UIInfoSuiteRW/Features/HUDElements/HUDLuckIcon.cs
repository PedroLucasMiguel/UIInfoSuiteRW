using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;
using UIInfoSuiteRW.Utils;

namespace UIInfoSuiteRW.Features.HUDElements
{
  public sealed class HUDLuckIcon
  {
    public static ClickableTextureComponent Create()
    {
      return new ClickableTextureComponent(
        "",
        new Rectangle(Tools.GetWidthInPlayArea() - 134, 290, 10 * Game1.pixelZoom, 10 * Game1.pixelZoom),
        "",
        "",
        Game1.content.Load<Texture2D>("Characters\\Junimo"),
        new Rectangle(0, 0, 16, 16),
        Game1.pixelZoom * 0.6f // Scaling to make junimo fit
      );
    }
  }
}