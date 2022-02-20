using System.Collections.Generic;
using System.Data;
using System.Linq;
using allSpice.Models;
using Dapper;

namespace allSpice.Repositories
{
    public class IngrediantsRepository
    {
        private readonly IDbConnection _db;
        public IngrediantsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Ingrediant> getAll()
        {
            string sql = "SELECT * FROM ingrediants;";
            List<Ingrediant> ingrediants = _db.Query<Ingrediant>(sql).ToList();
            return ingrediants;
        }

        internal Ingrediant getById(int id)
        {
            string sql = "SELECT * FROM ingrediants WHERE id = @id;";
            Ingrediant ingrediant = _db.QueryFirstOrDefault<Ingrediant>(sql, new { id });
            return ingrediant;
        }

        internal Ingrediant create(Ingrediant newIngrediant)
        {
            string sql = @"
            INSERT INTO ingrediants
            (name, quantity, recipeId)
            VALUES
            (@Name, @Quantity, @RecipeId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newIngrediant);
            newIngrediant.Id = id;
            return newIngrediant;
        }

        internal void edit(Ingrediant original)
        {
            string sql = @"
            Update ingrediants
            SET
            name = @Name,
            quantity = @Quantity
            WHERE id = @Id;";
            _db.Execute(sql, original);
        }

        internal void remove(int id)
        {
            string sql = "DELETE FROM ingrediants WHERE id = @id LIMIT 1";
            int changed = _db.Execute(sql, new { id });
            if (changed == 0)
            {
                throw new System.Exception("ERROR, not deleted");
            }
        }
    }
}