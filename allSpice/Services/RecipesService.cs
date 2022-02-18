using System;
using System.Collections.Generic;
using allSpice.Models;

namespace allSpice.Services
{
    public class RecipesService
    {
        private readonly RecipesRepository _rr;
        public RecipesService(RecipesRepository rr)
        {
            _rr = rr;
        }

        internal List<Recipe> getAll()
        {
            return _rr.getAll();
        }

        internal Recipe getRecipeById(int id)
        {
            Recipe foundRecipe = _rr.getRecipeById(id);
            if (foundRecipe == null)
            {
                throw new Exception("Cannot Find Recipe");
            }
            return foundRecipe;
        }

        internal Recipe create(Recipe newRecipe)
        {
            return _rr.create(newRecipe);
        }

        internal void remove(int id, string userId)
        {
            Recipe recipeToDelete = getRecipeById(id);
            if (recipeToDelete.CreatorId != userId)
            {
                throw new Exception("Unauthorized");
            }
            _rr.remove(id);
        }

        internal Recipe edit(Recipe recipe, string userId)
        {
            Recipe foundRecipe = getRecipeById(recipe.Id);
            if (foundRecipe.CreatorId != userId)
            {
                throw new Exception("Unauthorized to edit");
            }
            foundRecipe.CreatorId = recipe.CreatorId;
            return _rr.edit(foundRecipe);
        }
    }
}