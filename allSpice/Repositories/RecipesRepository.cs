using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using allSpice.Models;

namespace allSpice.Repositories
{
    public class RecipesRepository
    {
        private readonly IDbConnection _db;
        public RecipesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Recipe> getAll()
        {
            string sql = @"
            SELECT
            r.*,
            a.*
            FROM recipes r
            JOIN accounts a ON a.id = r.creatorId;";
            return _db.Query<Recipe, Profile, Recipe>(sql, (r, p) =>
            {
                r.Chef = p;
                return r;
            }).ToList();
        }

        internal Recipe getRecipeById(int id)
        {
            string sql = @"
            SELECT
            r.*,
            a.*
            FROM recipes r
            JOIN accounts a ON a.id = r.creatorId
            WHERE t.id = @Id;";
            return _db.Query<Recipe, Profile, Recipe>(sql, (r, p) =>
           {
               r.Chef = p;
               return r;
           }, new { id }).FirstOrDefault();
        }

        internal Recipe create(Recipe newRecipe)
        {
            string sql = @"
            INSERT INTO recipes(title, subtitle, category, creatorId)
            VALUES(@Title, @Subtitle, @Category, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newRecipe);
            newRecipe.Id = id;
            return newRecipe;
        }

        internal void remove(int Id)
        {
            string sql = "DELETE FROM recipes WHERE id = @Id LIMIT 1;";
            var delorted = _db.Execute(sql, new { Id });
            if (delorted == 0)
            {
                throw new System.Exception("Unable to Delort");
            }
        }

        internal Recipe edit(Recipe updatedRecipe)
        {
            string sql = @"
            UPDATE recipes
            SET
            title = @title,
            subtitle = @subtitle,
            category = @category
            WHERE id = @id;";
            var updated = _db.Execute(sql, updatedRecipe);
            if (updated == 0)
            {
                throw new System.Exception("Cannot Edit");
            }
            return updatedRecipe;
        }
    }
}