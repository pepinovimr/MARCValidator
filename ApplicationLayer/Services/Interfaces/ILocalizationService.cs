using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.Interfaces
{
    public interface ILocalizationService
    {
        public string this[string name] { get; }

        public void SetCultureInfo(string culture);
    }
}
