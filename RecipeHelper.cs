using Pianist.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Pianist
{
	public static class RecipeHelper
	{
		private static void MakeSimpleRecipe(Mod mod, int ingredient, int resultType, int ingredientStack = 1, int resultStack = 1, string reqTile = null)
		// notice the last parameters can be made optional by specifying a default value
		{
			ModRecipe recipe = new ModRecipe(mod); // make a new recipe for our mod
			//recipe.AddIngredient(null, ingredient, ingredientStack); // add the ingredient, passing null for the mod means it will use our mod, we could also pass mod from the arguments
			recipe.AddIngredient(ingredient, ingredientStack);
			if (reqTile != null)
			{ // when a required tile is specified
				recipe.AddTile(null, reqTile); // we add it 
			}

			recipe.SetResult(resultType, resultStack); // set the result to the specified type and with the specified stack.
			recipe.AddRecipe(); // finally, add the recipe
		}

		public static void AddRecipes(Mod mod)
		{
			MakeSimpleRecipe(mod, ItemID.Wood, ItemType<Drumstick>(), 2);
			MakeSimpleRecipe(mod, ItemID.Wood, ItemType<TiktokSquare>(), 4);
			MakeSimpleRecipe(mod, ItemID.Wood, ItemID.Wire, 1, 99);
			MakeSimpleRecipe(mod, ItemID.Wood, ItemID.Switch, 1, 1);
			MakeSimpleRecipe(mod, ItemID.Wood, ItemID.Wrench, 1, 1);
			MakeSimpleRecipe(mod, ItemID.Wood, ItemType<ExampleMech>(), 1, 1);
			MakeSimpleRecipe(mod, ItemID.Wood, ItemType<Tuner>(), 1, 1);
		}
	}
}