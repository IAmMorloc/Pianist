using Pianist.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Pianist
{
	// In this class we separate recipe related code from our main class
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

		// Add recipes
		public static void AddRecipes(Mod mod)
		{
			// ExampleItem crafts into the following items
			// Check the method signature of MakeSimpleRecipes for the arguments, this is a method signature:
			// private static void MakeSimpleRecipe(Mod mod, string modIngredient, short resultType, int ingredientStack = 1, int resultStack = 1, string reqTile = null) 

			MakeSimpleRecipe(mod, ItemID.Wood, ModContent.ItemType<Drumstick>(), 1);

		}
	}
}