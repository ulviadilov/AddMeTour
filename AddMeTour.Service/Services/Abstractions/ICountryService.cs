using AddMeTour.Entity.ViewModels.Tour.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Abstractions
{
    public interface ICountryService
    {
        Task<List<CountryViewModel>> GetAllCountriesNonDeletedAsync();
        Task<CountryViewModel> GetCountryByGuidAsync(Guid id);
        Task CreateCountryAsync(CountryAddViewModel countryAddVM);
        Task<CountryUpdateViewModel> UpdateCountryByGuidAsync(Guid id);
        Task UpdateCountryAsync(CountryUpdateViewModel countryUpdateVM);
        Task SoftDeleteCountryAsync(Guid id);
        Task HardDeleteCountryAsync(Guid id);
        Task RecoverCountryAsync(Guid id);
        Task<List<CountryViewModel>> GetAllPassiveCountries();
    }
}
