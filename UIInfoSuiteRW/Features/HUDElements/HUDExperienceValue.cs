using Microsoft.Xna.Framework;
using StardewValley;

namespace UIInfoSuiteRW.Features.HUDElements
{
  internal sealed class HUDExperienceValue
  {
    private readonly float _experiencePoints;

    private int _alpha = 100;
    private Vector2 _position;

    public HUDExperienceValue(float experiencePoints, Vector2 position)
    {
      _experiencePoints = experiencePoints;
      _position = position;
    }

    public bool IsInvisible => _alpha < 3;

    public void Draw()
    {
      _position.Y -= 0.5f;
      --_alpha;

      Game1.drawWithBorder(
        "XP " + _experiencePoints,
        Color.DarkSlateGray * (_alpha / 100f),
        Color.PaleTurquoise * (_alpha / 100f),
        Utility.ModifyCoordinatesForUIScale(new Vector2(_position.X - 28, _position.Y - 130)),
        0.0f,
        0.8f,
        0.0f
      );
    }
  }
}
