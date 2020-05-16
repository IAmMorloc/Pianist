using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

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
        }
    }
}
