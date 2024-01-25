using ComunicationDataLayer.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Managers
{
    public interface IValidationManager
    {
        public IEnumerable<Result> Validate(string path);
    }
}
