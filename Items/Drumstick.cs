using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Pianist.NPCs;
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
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = 100;
            item.rare = 1;
            item.useTime = 1;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }
        public override void UseStyle(Player player)
        {
            GymLover.UpdateGymLover();
        }
    }
}
