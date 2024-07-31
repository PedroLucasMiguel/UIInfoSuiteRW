using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace UIInfoSuiteRW.Features
{
  public sealed class ExperienceGain : IFeature
  {
    private readonly PerScreen<Item> _previousItem = new();

    private readonly IModHelper _helper = null!;

    public ExperienceGain(IModHelper helper)
    {
      _helper = helper;
    }

    public void ToggleOption(bool toggle)
    {
      _helper.Events.GameLoop.UpdateTicked -= OnUpdateTicket;

      if (toggle)
      {
        _helper.Events.GameLoop.UpdateTicked += OnUpdateTicket;
      }
    }

    private void OnUpdateTicket(object? sender, UpdateTickedEventArgs e)
    {
      if (!e.IsMultipleOf(15)) // quarter second
      {
        return;
      }

      /*
      bool skillChanged = TryGetCurrentLevelIndexFromSkillChange(out int currentLevelIndex);
      bool itemChanged = Game1.player.CurrentItem != _previousItem.Value;

      if (itemChanged)
      {
        currentLevelIndex = GetCurrentLevelIndexFromItemChange(Game1.player.CurrentItem);
        _previousItem.Value = Game1.player.CurrentItem;
      }

      if (skillChanged || itemChanged)
      {
        UpdateExperience(currentLevelIndex, skillChanged);
      }
      */
    }
  }
}