using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Entity.ViewModels.Tour.Country;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Concretes
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<List<CountryViewModel>> GetAllCountriesNonDeletedAsync()
        {
            var countries = await _unitOfWork.GetRepository<Country>().GetAllAsync(x => x.IsActive == true);
            var map = _mapper.Map<List<CountryViewModel>>(countries);
            return map;
        }

        public async Task<CountryViewModel> GetCountryByGuidAsync(Guid id)
        {
            var countries = await _unitOfWork.GetRepository<Country>().GetByGuidAsync(id);
            var map = _mapper.Map<CountryViewModel>(countries);
            return map;
        }

        public async Task CreateCountryAsync(CountryAddViewModel countryAddVM)
        {
            Country country = new Country
            {
                CountryName = countryAddVM.CountryName,
                Id = Guid.NewGuid(),
                IsActive = countryAddVM.IsActive
            };
            await _unitOfWork.GetRepository<Country>().AddAsync(country);
            await _unitOfWork.SaveAsync();
        }

        public async Task<CountryUpdateViewModel> UpdateCountryByGuidAsync(Guid id)
        {
            var country = await _unitOfWork.GetRepository<Country>().GetByGuidAsync(id);
            CountryUpdateViewModel countryUpdateVM = new CountryUpdateViewModel
            {
                Id = country.Id,
                CountryName = country.CountryName,
                IsActive = country.IsActive
            };
            return countryUpdateVM;
        }

        public async Task UpdateCountryAsync(CountryUpdateViewModel countryUpdateVM)
        {
            Country country = await _unitOfWork.GetRepository<Country>().GetAsync(x => x.IsActive == true && x.Id == countryUpdateVM.Id);
            country.IsActive = countryUpdateVM.IsActive;
            country.CountryName = countryUpdateVM.CountryName;
            await _unitOfWork.GetRepository<Country>().UpdateAsync(country);
            await _unitOfWork.SaveAsync();
        }

        public async Task SoftDeleteCountryAsync(Guid id)
        {
            Country country = await _unitOfWork.GetRepository<Country>().GetByGuidAsync(id);
            country.IsActive = false;
            await _unitOfWork.GetRepository<Country>().UpdateAsync(country);
            await _unitOfWork.SaveAsync();
        }

        public async Task HardDeleteCountryAsync(Guid id)
        {
            Country country = await _unitOfWork.GetRepository<Country>().GetByGuidAsync(id);
            await _unitOfWork.GetRepository<Country>().DeleteAsync(country);
            await _unitOfWork.SaveAsync();
        }

        public async Task RecoverCountryAsync(Guid id)
        {
            Country country = await _unitOfWork.GetRepository<Country>().GetByGuidAsync(id);
            country.IsActive = true;
            await _unitOfWork.GetRepository<Country>().UpdateAsync(country);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<CountryViewModel>> GetAllPassiveCountries()
        {
            List<Country> countries = await _unitOfWork.GetRepository<Country>().GetAllAsync(x => x.IsActive == false);
            var map = _mapper.Map<List<CountryViewModel>>(countries);
            return map;
        }
    }
}
