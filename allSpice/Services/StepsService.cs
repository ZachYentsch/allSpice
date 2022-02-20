using System;
using System.Collections.Generic;
using allSpice.Models;
using allSpice.Repositories;

namespace allSpice.Services
{
    public class StepsService
    {
        private readonly StepsRepository _sr;
        public StepsService(StepsRepository sr)
        {
            _sr = sr;
        }

        internal List<Step> getAll()
        {
            return _sr.getAll();
        }

        internal Step getById(int id)
        {
            Step foundStep = _sr.getById(id);
            if (foundStep == null)
            {
                throw new Exception("CANNOT find step");
            }
            return foundStep;
        }

        internal Step create(Step newStep)
        {
            Step step = _sr.create(newStep);
            return step;
        }

        internal void remove(int id, string userId)
        {
            Step stepToDelete = getById(id);
            if (stepToDelete.creatorId.ToString() != userId)
            {
                throw new Exception("UNauthorized");
            }
            _sr.remove(id);
        }

        internal Step edit(Step updated, string userId)
        {
            Step foundStep = getById(updated.Id);
            if (foundStep.creatorId.ToString() != userId)
            {
                throw new Exception("UNAuthorized");
            }
            foundStep.Body = updated.Body != null ? updated.Body : foundStep.Body;
            foundStep.NumberedSteps = updated.NumberedSteps != 0 ? updated.NumberedSteps : foundStep.NumberedSteps;
            _sr.edit(updated);
            return foundStep;
        }
    }
}