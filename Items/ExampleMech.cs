using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Pianist.Items
{
    class ExampleMech : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Example Mech");
		}

		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 12;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = TileType<Tiles.ExampleMech>();
			item.placeStyle = 0;
		}
	}
}
