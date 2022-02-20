using System.Collections.Generic;
using System.Data;
using System.Linq;
using allSpice.Models;
using Dapper;

namespace allSpice.Repositories
{
    public class StepsRepository
    {
        private readonly IDbConnection _db;
        public StepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Step> getAll()
        {
            string sql = "SELECT * FROM steps;";
            List<Step> steps = _db.Query<Step>(sql).ToList();
            return steps;
        }

        internal Step getById(int id,)
        {
            string sql = "SELECT * FROM steps WHERE id = @id;";
            Step step = _db.QueryFirstOrDefault<Step>(sql, new { id });
            return step;
        }

        internal Step create(Step newStep)
        {
            string sql = @"
            INSET INTO steps
            (numberedSteps, body, recipeId, creatorId)
            VALUES
            (@NumberedSteps, @Body, @RecipeId, @CreatorId)
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newStep);
            newStep.Id = id;
            return newStep;
        }

        internal void edit(Step original)
        {
            string sql = @"
            UPDATE steps
            SET
            numberedSteps = @numberedSteps,
            body = @body
            WHERE id = @Id;";
            _db.Execute(sql, original);
        }

        internal void remove(int id)
        {
            string sql = "DELETE FROM steps WHERE id = @id LIMIT 1";
            int changed = _db.Execute(sql, new { id });
            if (changed == 0)
            {
                throw new System.Exception("ERROR not deleted");
            }
        }
    }
}