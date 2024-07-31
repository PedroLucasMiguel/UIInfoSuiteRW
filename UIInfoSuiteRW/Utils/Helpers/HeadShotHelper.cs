using Microsoft.Xna.Framework;
using StardewValley;

namespace UIInfoSuiteRW.Utils.Helpers
{
  public static class HeadShotHelper
  {
    public static Rectangle GetHeadShot(NPC npc)
    {
      int size;

      if (!ModConstants.NpcHeadShotSize.TryGetValue(npc.Name, out size))
      {
        size = 4;
      }

      Rectangle mugShotSourceRect = npc.getMugShotSourceRect();
      mugShotSourceRect.Height -= size / 2;
      mugShotSourceRect.Y -= size / 2;
      return mugShotSourceRect;
    }
  }
}
