using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Pianist.Dusts
{
	class MusicalNote : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.frame = new Rectangle(Main.rand.Next(3) * 20, 0, 20, 20);
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.scale -= 0.01f;
			if (dust.scale < 0.75f)
			{
				dust.active = false;
			}
			return false;
		}
	}
}