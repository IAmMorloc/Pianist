﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace Pianist.Items
{
    class Drumstick : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Drumstick v1.0");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 0, 10, 0); //售价
            item.useStyle = ItemUseStyleID.SwingThrow; //使用方式 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
            item.useTurn = true; //使用时转身
            item.autoReuse = false; //按住时连续使用
            item.holdStyle = 1; //手持方式 1 for holding out (torches and glowsticks), 2 for holding up (Breathing Reed), 3 for a different version of holding out (Magical Harp)
            item.useAnimation = 20; //使用间隔，以帧为单位，默认为100
            item.useTime = 20; //使用时长，以帧为单位，默认为100。当useAnimation > useTime时，一次使用会产生多次效果
            item.reuseDelay = 0; //硬直时长，以帧为单位，默认为0
            item.consumable = false; //是否为消耗品
            item.rare = ItemRarityID.Blue; //稀有度 from -1 to 13
            item.maxStack = 1; //最大堆叠
            item.width = 20; //物品宽度
            item.height = 40; //物品高度
            item.scale = 1f; //使用时的放大倍数
            //item.UseSound = SoundID.Item1; //使用时播放的soundID
            item.noMelee = true; //是否没有近战伤害，默认为false有近战伤害。
        }

        public override bool UseItem(Player player)
        {
            Terraria.Audio.LegacySoundStyle hitSound = SoundID.Item1;
            Vector2 position = GetLightPosition(player);

            //Vector2 location = hitbox.Location.ToVector2();
            Point tileLocation = position.ToTileCoordinates();
            ModTile tile = TileLoader.GetTile(Main.tile[tileLocation.X, tileLocation.Y].type);
            if (tile == null)
            {
                return false;
            }
            switch (tile.soundType & 3)
            {
                case 0:
                    hitSound = SoundID.Item11; break;
                case 1:
                    hitSound = SoundID.Item12; break;
                case 2:
                    hitSound = SoundID.Item13; break;
                case 3:
                    hitSound = SoundID.Item14; break;
            }
            Main.PlaySound(hitSound, position);
            return true;
        }

        private Vector2 GetLightPosition(Player player)
        {
            Vector2 position = Main.screenPosition;
            position.X += Main.mouseX;
            position.Y += player.gravDir == 1 ? Main.mouseY : Main.screenHeight - Main.mouseY;
            return position;
        }
    }

}
