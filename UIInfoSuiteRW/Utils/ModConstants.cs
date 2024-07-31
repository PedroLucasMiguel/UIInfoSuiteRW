using System.Collections.Generic;

namespace UIInfoSuiteRW.Utils;

/// <summary>The constant values used by the mod.</summary>
public static class ModConstants
{

  public static readonly Dictionary<string, int> NpcHeadShotSize = new()
  {
    { "Piere", 9 },
    { "Sebastian", 7 },
    { "Evelyn", 5 },
    { "Penny", 6 },
    { "Jas", 6 },
    { "Caroline", 5 },
    { "Dwarf", 5 },
    { "Sam", 9 },
    { "Maru", 6 },
    { "Wizard", 9 },
    { "Jodi", 7 },
    { "Krobus", 7 },
    { "Alex", 8 },
    { "Kent", 10 },
    { "Linus", 4 },
    { "Harvey", 9 },
    { "Shane", 8 },
    { "Haley", 6 },
    { "Robin", 7 },
    { "Marlon", 2 },
    { "Emily", 8 },
    { "Marnie", 5 },
    { "Abigail", 7 },
    { "Leah", 6 },
    { "George", 5 },
    { "Elliott", 9 },
    { "Gus", 7 },
    { "Lewis", 8 },
    { "Demetrius", 11 },
    { "Pam", 5 },
    { "Vincent", 6 },
    { "Sandy", 7 },
    { "Clint", 10 },
    { "Willy", 10 }
  };

  /*********
  ** Accessors
  *********/
  /// <summary>The network message IDs used by the mod.</summary>
  public static class MessageIds
  {
    /// <summary>A message from the host containing NPC location data.</summary>
    public const string TownFolksMultiplayerSync = "TownFolksMultiplayerSync";
  }
}