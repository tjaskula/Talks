using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReFuncToring.ObjectOriented
{
    public class BusinessLogic
    {
        private readonly IRepository _repository;

        public BusinessLogic(IRepository repository)
        {
            _repository = repository;
        }

        public int RunRules(string query)
        {
            var r = _repository.ReadData(query);
            return int.Parse(r);
        }
    }
}
