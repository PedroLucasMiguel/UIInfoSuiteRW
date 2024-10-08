﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Tools;
using UIInfoSuiteRW.Utils;
using UIInfoSuiteRW.Utils.Helpers;
using Object = StardewValley.Object;

// Thanks sebbi08 (https://github.com/sebbi08) for the multiple bundles patch!
namespace UIInfoSuiteRW.Features
{
  #region Properties
  public record RequiredBundleItem(string Text, Color? Color);

  internal class ShowItemHoverInformation : IFeature
  {
    private readonly ClickableTextureComponent _bundleIcon = new(
      new Rectangle(0, 0, Game1.tileSize, Game1.tileSize),
      Game1.mouseCursors,
      new Rectangle(331, 374, 15, 14),
      3f
    );
    private readonly IModHelper _helper;

    private readonly Dictionary<int, Color?> _colorCache = new();

    private readonly int bundleInfoHeight = 36;

    private readonly PerScreen<Item?> _hoverItem = new();
    private readonly ClickableTextureComponent _museumIcon;
    private readonly ClickableTextureComponent _islandFieldOfficeIcon;

    private readonly ClickableTextureComponent _shippingBottomIcon = new(
      new Rectangle(0, 0, Game1.tileSize, Game1.tileSize),
      Game1.mouseCursors,
      new Rectangle(526, 218, 30, 22),
      1.2f
    );

    private readonly ClickableTextureComponent _shippingTopIcon = new(
      new Rectangle(0, 0, Game1.tileSize, Game1.tileSize),
      Game1.mouseCursors,
      new Rectangle(134, 236, 30, 15),
      1.2f
    );

    private LibraryMuseum _libraryMuseum = null!;
    private IslandFieldOffice _islandFieldOffice = null!;
    #endregion

    #region Lifecycle
    public ShowItemHoverInformation(IModHelper helper)
    {
      _helper = helper;
      
      // Just create both NPCs to show the indicator from day one
      NPC gunther = new() { Name = "Gunther", Age = 0, Sprite = new AnimatedSprite("Characters\\Gunther") };
      NPC professorSnail = new() { Name = "SafariGuy", Age = 0, Sprite = new AnimatedSprite("Characters\\SafariGuy") };

      _museumIcon = new ClickableTextureComponent(
        new Rectangle(0, 0, Game1.tileSize, Game1.tileSize),
        gunther.Sprite.Texture,
        HeadShotHelper.GetHeadShot(gunther),
        Game1.pixelZoom
      );

      _islandFieldOfficeIcon = new ClickableTextureComponent(
        new Rectangle(0, 0, Game1.tileSize, Game1.tileSize),
        professorSnail.Sprite.Texture,
        HeadShotHelper.GetHeadShot(professorSnail),
        Game1.pixelZoom
      );
    }
    
    public void ToggleOption(bool toggle)
    {
      _helper.Events.Display.RenderedActiveMenu -= OnRenderedActiveMenu;
      _helper.Events.Display.RenderedHud -= OnRenderedHud;
      _helper.Events.Display.Rendering -= OnRendering;

      if (toggle)
      {
        // Todo - Fix multiplayer?
        _libraryMuseum = Game1.RequireLocation<LibraryMuseum>("ArchaeologyHouse");
        _islandFieldOffice = Game1.RequireLocation<IslandFieldOffice>("IslandFieldOffice");

        _helper.Events.Display.RenderedActiveMenu += OnRenderedActiveMenu;
        _helper.Events.Display.RenderedHud += OnRenderedHud;
        _helper.Events.Display.Rendering += OnRendering;
      }
    }
    #endregion

    #region Event subscriptions
    private void OnRendering(object? sender, EventArgs e)
    {
      _hoverItem.Value = Tools.GetHoveredItem();
    }

    private void OnRenderedHud(object? sender, RenderedHudEventArgs e)
    {
      if (Game1.activeClickableMenu == null)
      {
        DrawAdvancedTooltip(e.SpriteBatch);
      }
    }

    [EventPriority(EventPriority.Low)]
    private void OnRenderedActiveMenu(object? sender, RenderedActiveMenuEventArgs e)
    {
      if (Game1.activeClickableMenu != null)
      {
        DrawAdvancedTooltip(e.SpriteBatch);
      }
    }
    #endregion

    #region Logic
    private void DrawAdvancedTooltip(SpriteBatch spriteBatch)
    {
      // Checking if the item have sell value.
      // Scythes and fishing rods does not have sell value
      if (_hoverItem.Value != null &&
          !(_hoverItem.Value is MeleeWeapon weapon && weapon.isScythe()) &&
          _hoverItem.Value is not FishingRod)
      {
        var hoveredObject = _hoverItem.Value as Object;

        int itemPrice = Tools.GetSellToStorePrice(_hoverItem.Value);

        var stackPrice = 0;
        if (itemPrice > 0 && _hoverItem.Value.Stack > 1)
        {
          stackPrice = itemPrice * _hoverItem.Value.Stack;
        }

        // Getting crop price
        int cropPrice = Tools.GetHarvestPrice(_hoverItem.Value);

        // Checking if item can be donated to the museoum
        bool notDonatedYet = _libraryMuseum.isItemSuitableForDonation(_hoverItem.Value);

        int fieldOfficeDonationSlot = -1;
        // Checking if item can be donated to the field office
        // Checking for Snake Vertebrae
        if (_hoverItem.Value.ItemId == "826")
        {
          if (_islandFieldOffice.piecesDonated[6] && 
              !_islandFieldOffice.piecesDonated[7])
          {
            fieldOfficeDonationSlot = 7;
          }
          else if (_islandFieldOffice.piecesDonated[7] &&
                  !_islandFieldOffice.piecesDonated[6])
          {
            fieldOfficeDonationSlot = 6;
          }
        }
        // Checking for Fossilized Leg
        else if (_hoverItem.Value.ItemId == "823")
        {
          if (_islandFieldOffice.piecesDonated[0] && 
            !_islandFieldOffice.piecesDonated[2])
          {
            fieldOfficeDonationSlot = 2;
          }
          else if (_islandFieldOffice.piecesDonated[2] && 
                  !_islandFieldOffice.piecesDonated[0])
          {
            fieldOfficeDonationSlot = 0;
          }
        }
        else
        {
          fieldOfficeDonationSlot = FieldOfficeMenu.getPieceIndexForDonationItem($"(O){_hoverItem.Value.ItemId}");
        }

        // Checking if the item was not shipped yet
        bool notShippedYet = hoveredObject != null &&
                            hoveredObject.countsForShippedCollection() &&
                            !Game1.player.basicShipped.ContainsKey(hoveredObject.ItemId) &&
                            hoveredObject.Type != "Fish" &&
                            hoveredObject.Category != Object.skillBooksCategory;

        List<RequiredBundleItem>? requiredBundleItems = new();

        if (hoveredObject != null)
        {
          List<BundleRequiredItem>? bundleDisplayData = BundleHelper.GetBundleItemIfNotDonated(hoveredObject);

          if (bundleDisplayData != null)
          {
            bundleDisplayData.ForEach(item =>
            {
              _colorCache.TryGetValue(item.Id, out Color? bundleColor);
              if (bundleColor == null)
              {
                Color? uncachedColor = BundleHelper.GetRealColorFromIndex(item.Id);
                if(uncachedColor != null)
                {
                  bundleColor = ColorHelper.Desaturate((Color)uncachedColor, 0.35f);
                  _colorCache[item.Id] = bundleColor;
                }
              }
              requiredBundleItems.Add(new RequiredBundleItem(item.Name + " : " + item.Count, bundleColor));
            });
          }
        }

        var drawPositionOffset = new Vector2();
        int windowWidth, windowHeight;

        var bundleHeaderWidth = 0;
        if (requiredBundleItems.Count > 0)
        {
          // bundleHeaderWidth = ((bundleIcon.Width * 3 = 45) - 7 = 38) + 3 + bundleTextSize.X + 3 + ((shippingBin.Width * 1.2 = 36) - 12 = 24)
          int maxWidth = requiredBundleItems.Max(item => (int)Game1.dialogueFont.MeasureString(item.Text).X);

          bundleHeaderWidth = 68 + maxWidth;
        }

        var itemTextWidth = (int)Game1.smallFont.MeasureString(itemPrice.ToString()).X;
        var stackTextWidth = (int)Game1.smallFont.MeasureString(stackPrice.ToString()).X;
        var cropTextWidth = (int)Game1.smallFont.MeasureString(cropPrice.ToString()).X;
        var minTextWidth = (int)Game1.smallFont.MeasureString("000").X;
        // largestTextWidth = 12 + 4 + (icon.Width = 32) + 4 + max(textSize.X) + 8 + 16
        int largestTextWidth =
          76 + Math.Max(minTextWidth, Math.Max(stackTextWidth, Math.Max(itemTextWidth, cropTextWidth)));
        windowWidth = Math.Max(bundleHeaderWidth, largestTextWidth);

        windowHeight = 20 + 16;
        if (itemPrice > 0)
        {
          windowHeight += 40;
        }

        if (stackPrice > 0)
        {
          windowHeight += 40;
        }

        if (cropPrice > 0)
        {
          windowHeight += 40;
        }

        if (requiredBundleItems.Count > 0)
        {
          windowHeight += 4 + bundleInfoHeight * (requiredBundleItems.Count - 1);
          drawPositionOffset.Y += 4;
        }

        // Minimal window dimensions
        windowHeight = Math.Max(windowHeight, 40);
        windowWidth = Math.Max(windowWidth, 40);

        int windowY = Game1.getMouseY() + 20;
        int windowX = Game1.getMouseX() - 25 - windowWidth;

        // Adjust the tooltip's position when it overflows
        Rectangle safeArea = Utility.getSafeArea();

        if (windowY + windowHeight > safeArea.Bottom)
        {
          windowY = safeArea.Bottom - windowHeight;
        }

        if (Game1.getMouseX() + 300 > safeArea.Right)
        {
          windowX = safeArea.Right - 350 - windowWidth;
        }
        else if (windowX < safeArea.Left)
        {
          windowX = Game1.getMouseX() + 350;
        }

        var windowPos = new Vector2(windowX, windowY);

        if(requiredBundleItems.Count > 1)
        {
          windowPos.Y += bundleInfoHeight * (requiredBundleItems.Count - 1);
        }

        Vector2 drawPosition = windowPos + new Vector2(16, 20) + drawPositionOffset;

        if(requiredBundleItems.Count > 1)
        {
          windowPos.Y -= bundleInfoHeight * (requiredBundleItems.Count - 1);
        }

        // Icons are drawn in 32x40 cells. The small font has a cap height of 18 and an offset of (2, 6)
        var rowHeight = 40;
        var iconCenterOffset = new Vector2(16, 20);
        var textOffset = new Vector2(32 + 4, (rowHeight - 18) / 2 - 6);

        if (itemPrice > 0 ||
            stackPrice > 0 ||
            cropPrice > 0 ||
            requiredBundleItems.Count > 0 ||
            notDonatedYet ||
            notShippedYet ||
            fieldOfficeDonationSlot >= 0)
        {
          IClickableMenu.drawTextureBox(
            spriteBatch,
            Game1.menuTexture,
            new Rectangle(0, 256, 60, 60),
            (int)windowPos.X,
            (int)windowPos.Y,
            windowWidth,
            windowHeight,
            Color.White
          );
        }

        if (itemPrice > 0)
        {
          spriteBatch.Draw(
            Game1.debrisSpriteSheet,
            drawPosition + iconCenterOffset,
            Game1.getSourceRectForStandardTileSheet(Game1.debrisSpriteSheet, 8, 16, 16),
            Color.White,
            0,
            new Vector2(8, 8),
            Game1.pixelZoom,
            SpriteEffects.None,
            0.95f
          );

          DrawSmallTextWithShadow(spriteBatch, itemPrice.ToString(), drawPosition + textOffset);

          drawPosition.Y += rowHeight;
        }

        if (stackPrice > 0)
        {
          var overlapOffset = new Vector2(0, 10);
          spriteBatch.Draw(
            Game1.debrisSpriteSheet,
            drawPosition + iconCenterOffset - overlapOffset / 2,
            Game1.getSourceRectForStandardTileSheet(Game1.debrisSpriteSheet, 8, 16, 16),
            Color.White,
            0,
            new Vector2(8, 8),
            Game1.pixelZoom,
            SpriteEffects.None,
            0.95f
          );
          spriteBatch.Draw(
            Game1.debrisSpriteSheet,
            drawPosition + iconCenterOffset + overlapOffset / 2,
            Game1.getSourceRectForStandardTileSheet(Game1.debrisSpriteSheet, 8, 16, 16),
            Color.White,
            0,
            new Vector2(8, 8),
            Game1.pixelZoom,
            SpriteEffects.None,
            0.95f
          );

          DrawSmallTextWithShadow(spriteBatch, stackPrice.ToString(), drawPosition + textOffset);

          drawPosition.Y += rowHeight;
        }

        if (cropPrice > 0)
        {
          spriteBatch.Draw(
            Game1.mouseCursors,
            drawPosition + iconCenterOffset,
            new Rectangle(60, 428, 10, 10),
            Color.White,
            0.0f,
            new Vector2(5, 5),
            Game1.pixelZoom * 0.75f,
            SpriteEffects.None,
            0.85f
          );

          DrawSmallTextWithShadow(spriteBatch, cropPrice.ToString(), drawPosition + textOffset);
        }

        if (notDonatedYet)
        {
          spriteBatch.Draw(
            _museumIcon.texture,
            windowPos + new Vector2(2, windowHeight + 8),
            _museumIcon.sourceRect,
            Color.White,
            0f,
            new Vector2(_museumIcon.sourceRect.Width / 2, _museumIcon.sourceRect.Height),
            2,
            SpriteEffects.None,
            0.86f
          );
        }

        if (fieldOfficeDonationSlot >= 0)
        {
          if(_islandFieldOffice.piecesDonated[fieldOfficeDonationSlot] == false)
          {
            spriteBatch.Draw(
              _islandFieldOfficeIcon.texture,
              windowPos + new Vector2(2, windowHeight + 8),
              _islandFieldOfficeIcon.sourceRect,
              Color.White,
              0f,
              new Vector2(_islandFieldOfficeIcon.sourceRect.Width / 2, _islandFieldOfficeIcon.sourceRect.Height),
              2,
              SpriteEffects.None,
              0.86f
            );
          }
        }

        if (requiredBundleItems.Count > 0)
        {
          // Draws a 30x42 bundle icon offset by (-7, -13) from the top-left corner of the window
          // and the 36px high banner with the bundle name
          for (int i = 0; i < requiredBundleItems.Count; i++)
          {
            var item = requiredBundleItems[i];
            DrawBundleBanner(spriteBatch, item.Text, windowPos + new Vector2(-7, -13) + new Vector2(0, bundleInfoHeight * i), windowWidth, item.Color);
          }
        }

        if (notShippedYet)
        {
          // Draws a 36x28 shipping bin offset by (-24, -6) from the top-right corner of the window
          var shippingBinDims = new Vector2(30, 24);
          DrawShippingBin(spriteBatch, windowPos + new Vector2(windowWidth - 6, 8), shippingBinDims / 2);
        }
      }
    }

    private void DrawSmallTextWithShadow(SpriteBatch b, string text, Vector2 position)
    {
      b.DrawString(Game1.smallFont, text, position + new Vector2(2, 2), Game1.textShadowColor);
      b.DrawString(Game1.smallFont, text, position, Game1.textColor);
    }

    private void DrawBundleBanner(
      SpriteBatch spriteBatch,
      string bundleName,
      Vector2 position,
      int windowWidth,
      Color? color = null
    )
    {
      // NB The dialogue font has a cap height of 30 and an offset of (3, 6)

      Color drawColor = color ?? Color.Crimson;

      var bundleBannerX = (int)position.X;
      int bundleBannerY = (int)position.Y + 3;
      var cellCount = 36;
      var solidCells = 8;
      int cellWidth = windowWidth / cellCount;
      for (var cell = 0; cell < cellCount; ++cell)
      {
        float fadeAmount = 0.97f - (cell < solidCells ? 0 : 1.0f * (cell - solidCells) / (cellCount - solidCells));
        spriteBatch.Draw(
          Game1.staminaRect,
          new Rectangle(bundleBannerX + cell * cellWidth, bundleBannerY, cellWidth, 36),
          drawColor * fadeAmount
        );
      }

      spriteBatch.Draw(
        Game1.mouseCursors,
        position,
        _bundleIcon.sourceRect,
        Color.White,
        0f,
        Vector2.Zero,
        _bundleIcon.scale,
        SpriteEffects.None,
        0.86f
      );

      Utility.drawTextWithColoredShadow(
        spriteBatch,
        bundleName,
        Game1.dialogueFont,
        position + new Vector2(_bundleIcon.sourceRect.Width * _bundleIcon.scale + 3, 0),
        Color.Ivory,
        Color.DarkSlateGray,
        horizontalShadowOffset: 2,
        verticalShadowOffset: 2,
        numShadows: 3
      );
    }

    private void DrawShippingBin(SpriteBatch b, Vector2 position, Vector2 origin)
    {
      var shippingBinOffset = new Vector2(0, 2);
      // var shippingBinLidOffset = Vector2.Zero;

      // NB This is not the texture used to draw the shipping bin on the farm map.
      //    The one for the farm is located in "Buildings\Shipping Bin".
      b.Draw(
        _shippingBottomIcon.texture,
        position,
        _shippingBottomIcon.sourceRect,
        Color.White,
        0f,
        origin - shippingBinOffset,
        _shippingBottomIcon.scale,
        SpriteEffects.None,
        0.86f
      );
      b.Draw(
        _shippingTopIcon.texture,
        position,
        _shippingTopIcon.sourceRect,
        Color.White,
        0f,
        origin,
        _shippingTopIcon.scale,
        SpriteEffects.None,
        0.86f
      );
    }
  }
  #endregion
}