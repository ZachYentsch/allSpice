using System;
using System.Collections.Generic;
using allSpice.Models;

namespace allSpice.Services
{
    public class IngrediantsService
    {
        private readonly IngrediantsRepository _ir;
        public IngrediantsService(IngrediantsRepository ir)
        {
            _ir = ir;
        }

        internal List<Ingrediant> getAll()
        {
            return _ir.getAll();
        }

        internal Ingrediant getById(int id)
        {
            Ingrediant foundIngrediant = _ir.getById(id);
            if (foundIngrediant == null)
            {
                throw new Exception("Cannot Find Ingrediant");
            }
            return foundIngrediant;
        }

        internal Ingrediant create(Ingrediant newIngrediant)
        {
            return _ir.create(newIngrediant);
        }

        internal void remove(int id, string userId)
        {
            Ingrediant ingrediantToDelete = getById(id);
            if (ingrediantToDelete.creatorId != userId)
            {
                throw new Exception("UnAuthorized")
            }
            _ir.remove(id);
        }

        internal Ingrediant edit(Ingrediant ingrediant, string userId)
        {
            Ingrediant foundIngrediant = getById(ingrediant.Id);
            if (foundIngrediant.CreatorId != userId)
            {
                throw new Exception("UnAuthorized");
            }
            foundIngrediant.CreatorId = ingrediant.CreatorId;
            return _ir.edit(foundIngrediant);
        }

    }
}