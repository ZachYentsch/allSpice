using System;
using System.Collections.Generic;
using allSpice.Models;
using allSpice.Repositories;

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
            Ingrediant ingrediant = _ir.create(newIngrediant);
            return ingrediant;
        }

        internal void remove(int id, string userId)
        {
            Ingrediant ingrediantToDelete = getById(id);
            if (ingrediantToDelete.creatorId.ToString() != userId)
            {
                throw new Exception("UnAuthorized");
            }
            _ir.remove(id);
        }

        internal Ingrediant edit(Ingrediant updated, string userId)
        {
            Ingrediant foundIngrediant = getById(updated.Id);
            if (foundIngrediant.creatorId.ToString() != userId)
            {
                throw new Exception("UnAuthorized");
            }
            foundIngrediant.Name = updated.Name != null ? updated.Name : foundIngrediant.Name;
            foundIngrediant.Quantity = updated.Quantity != 0 ? updated.Quantity : foundIngrediant.Quantity;
            foundIngrediant.creatorId = updated.creatorId;
            _ir.edit(updated);
            return foundIngrediant;
        }

    }
}