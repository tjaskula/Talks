using System;
using Domain;

namespace Infrastructure
{
    public class ClassRepository
    {
        public Class GetById(int id)
        {
            return new Class(id, DateTime.Now, DateTime.Now.AddMonths(6));
        }
    }
}