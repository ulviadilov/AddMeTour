using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Data.UnitOfWorks.Concretes;
using AddMeTour.Entity.Entities.Home;
using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Features;
using AddMeTour.Entity.ViewModels.Tour;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Service.Helpers.Images;
using AddMeTour.Service.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Concretes
{
    public class TourService : ITourService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _env;

        public TourService(IMapper mapper, IUnitOfWork unitOfWork, IHostingEnvironment env)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _env = env;
        }
        
        public async Task<List<TourViewModel>> GetAllToursNonDeletedAsync()
        {
            var tours = await _unitOfWork.GetRepository<Tour>().GetAllAsync(x => x.IsActive == true);
            var map = _mapper.Map<List<TourViewModel>>(tours);
            return map;
        }

        public async Task<TourViewModel> GetTourByGuidAsync(Guid id)
        {
            var tour = await _unitOfWork.GetRepository<Tour>().GetByGuidAsync(id);
            var map = _mapper.Map<TourViewModel>(tour);
            return map;
        }

        public async Task CreateTourAsync(TourAddViewModel tourAddVM)
        {
            TourImage posterImage = new TourImage
            {
                IsActive = true,
                ImageUrl = tourAddVM.PosterImageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "tours")),
                IsPoster = true,
                TourId = tourAddVM.Id
            };
            await _unitOfWork.GetRepository<TourImage>().AddAsync(posterImage);

            Tour tour = new Tour
            {
                CreateTime = DateTime.Now,
                IsActive = true,
                DepartureDetails = tourAddVM.DepartureDetails,
                Duration = tourAddVM.Duration,
                GroupSize = tourAddVM.GroupSize,
                Overview = tourAddVM.Overview,
                Price = tourAddVM.Price,
                TourName = tourAddVM.TourName,
                PosterImageUrl = posterImage.ImageUrl
            };

            await _unitOfWork.GetRepository<Tour>().AddAsync(tour);

            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(x => x.IsActive == true && tourAddVM.CategoryIds.Contains(x.Id));
            foreach (var item in categories)
            {
                TourCategory tourCategory = new TourCategory
                {
                    CategoryId = item.Id,
                    TourId = tour.Id
                };
                await _unitOfWork.GetRepository<TourCategory>().AddAsync(tourCategory);
            }
            var countries = await _unitOfWork.GetRepository<Country>().GetAllAsync(x => x.IsActive == true && tourAddVM.CountryIds.Contains(x.Id));
            foreach (var country in countries)
            {
                TourCountry tourCountry = new TourCountry
                {
                    CountryId = country.Id,
                    TourId = tour.Id
                };
                await _unitOfWork.GetRepository<TourCountry>().AddAsync(tourCountry);
            }
            var inclusions = await _unitOfWork.GetRepository<Inclusion>().GetAllAsync(x => x.IsActive == true && tourAddVM.InclusionIds.Contains(x.Id));
            foreach (var inclusion in inclusions)
            {
                TourInclusion tourInclusion = new TourInclusion
                {
                    InclusionId = inclusion.Id,
                    TourId= tour.Id
                };
                await _unitOfWork.GetRepository<TourInclusion>().AddAsync(tourInclusion);
            }
            var exclusions = await _unitOfWork.GetRepository<Exclusion>().GetAllAsync(x => x.IsActive == true && tourAddVM.ExclusionIds.Contains(x.Id));
            foreach (var exclusion in exclusions)
            {
                TourExclusion tourExclusion = new TourExclusion
                {
                    ExclusionId = exclusion.Id,
                    TourId = tour.Id
                };
                await _unitOfWork.GetRepository<TourExclusion>().AddAsync(tourExclusion);
            }

            var languages = await _unitOfWork.GetRepository<Language>().GetAllAsync(x => x.IsActive == true && tourAddVM.LanguageIds.Contains(x.Id));
            foreach (var language in languages)
            {
                TourLanguage tourLanguage = new TourLanguage
                {
                    LanguageId = language.Id,
                    TourId = tour.Id
                };
                await _unitOfWork.GetRepository<TourLanguage>().AddAsync(tourLanguage);
            }

           

            foreach (IFormFile imageFile in tourAddVM.ImageFiles)
            {
                TourImage image = new TourImage
                {
                    IsActive = true,
                    ImageUrl = imageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "tours")),
                    IsPoster = false,
                    TourId = tourAddVM.Id
                };
                await _unitOfWork.GetRepository<TourImage>().AddAsync(image);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<TourUpdateViewModel> UpdateTourByGuidAsync(Guid id)
        {
            var tour = await _unitOfWork.GetRepository<Tour>().GetByGuidAsync(id);
            TourUpdateViewModel tourUpdateVM = new TourUpdateViewModel
            {
                GroupSize = tour.GroupSize,
                Duration = tour.Duration,
                IsActive = tour.IsActive,
                Overview = tour.Overview,
                Price = tour.Price,
                TourName = tour.TourName,
                TourId = tour.Id
            };
            foreach (TourInclusion inclusion in tour.TourInclusions)
            {
                tourUpdateVM.InclusionIds.Add(inclusion.InclusionId);
            }

            foreach (TourCategory tourCategory in tour.TourCategories)
            {
                tourUpdateVM.CategoryIds.Add(tourCategory.CategoryId);
            }

            foreach (TourCountry country in tour.TourCountries)
            {
                tourUpdateVM.CountryIds.Add(country.CountryId);
            }
            foreach (TourExclusion exclusion in tour.TourExclusions)
            {
                tourUpdateVM.ExclusionIds.Add(exclusion.ExclusionId);
            }
            foreach (TourLanguage language in tour.TourLanguages)
            {
                tourUpdateVM.LanguageIds.Add(language.LanguageId);
            }
            foreach(TourImage image in tour.TourImages)
            {
                tourUpdateVM.ImageIds.Add(image.Id);
            }

            return tourUpdateVM;
        }

        public async Task UpdateTourAsync(TourUpdateViewModel tourUpdateVM)
        {
            Tour existTour = await _unitOfWork.GetRepository<Tour>().GetAsync(x => x.IsActive == true && x.Id == tourUpdateVM.TourId);
            if (tourUpdateVM.PosterImageFile != null)
            {
                string deletePath = Path.Combine(_env.WebRootPath, "assets", "img", "tours", existTour.TourImages.FirstOrDefault(x => x.IsPoster == true).ImageUrl);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
                existTour.PosterImageUrl = tourUpdateVM.PosterImageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "tours"));
            }

            if (tourUpdateVM.ImageFiles is not null)
            {
                foreach (TourImage image in existTour.TourImages.Where(x => x.IsActive == true && x.IsPoster == false))
                {
                    string deletePath = Path.Combine(_env.WebRootPath, "assets", "img", "tours", image.ImageUrl);
                    if (System.IO.File.Exists(deletePath))
                    {
                        System.IO.File.Delete(deletePath);
                    }
                }

                foreach (IFormFile imageFile in tourUpdateVM.ImageFiles)
                {
                    TourImage image = new TourImage
                    {
                        IsActive = true,
                        ImageUrl = imageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "tours")),
                        IsPoster = false,
                        TourId = existTour.Id
                    };
                    await _unitOfWork.GetRepository<TourImage>().AddAsync(image);
                }
            }

            foreach(var item in existTour.TourInclusions)
            {
                await _unitOfWork.GetRepository<TourInclusion>().DeleteAsync(item);
            }

            var inclusions = await _unitOfWork.GetRepository<Inclusion>().GetAllAsync(x => tourUpdateVM.InclusionIds.Contains(x.Id));

            foreach (var item in inclusions)
            {
                await _unitOfWork.GetRepository<TourInclusion>().AddAsync(new TourInclusion { Tour = existTour, InclusionId = item.Id});
            }

            foreach (var item in existTour.TourExclusions)
            {
                await _unitOfWork.GetRepository<TourExclusion>().DeleteAsync(item);
            }

            var exclusions = await _unitOfWork.GetRepository<Exclusion>().GetAllAsync(x => tourUpdateVM.ExclusionIds.Contains(x.Id));

            foreach (var item in exclusions)
            {
                await _unitOfWork.GetRepository<TourExclusion>().AddAsync(new TourExclusion { Tour = existTour, ExclusionId = item.Id });
            }

            foreach (var item in existTour.TourLanguages)
            {
                await _unitOfWork.GetRepository<TourLanguage>().DeleteAsync(item);
            }

            var languages = await _unitOfWork.GetRepository<Language>().GetAllAsync(x => tourUpdateVM.LanguageIds.Contains(x.Id));

            foreach (var item in languages)
            {
                await _unitOfWork.GetRepository<TourLanguage>().AddAsync(new TourLanguage { Tour = existTour, LanguageId = item.Id });
            }

            foreach (var item in existTour.TourCategories)
            {
                await _unitOfWork.GetRepository<TourCategory>().DeleteAsync(item);
            }

            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(x => tourUpdateVM.CategoryIds.Contains(x.Id));

            foreach (var item in categories)
            {
                await _unitOfWork.GetRepository<TourCategory>().AddAsync(new TourCategory { Tour = existTour, CategoryId = item.Id });
            }

            foreach (var item in existTour.TourCountries)
            {
                await _unitOfWork.GetRepository<TourCountry>().DeleteAsync(item);
            }

            var countries = await _unitOfWork.GetRepository<Country>().GetAllAsync(x => tourUpdateVM.CountryIds.Contains(x.Id));

            foreach (var item in countries)
            {
                await _unitOfWork.GetRepository<TourCountry>().AddAsync(new TourCountry { Tour = existTour, CountryId = item.Id });
            }

            existTour.DepartureDetails = tourUpdateVM.DepartureDetails;
            existTour.Price = tourUpdateVM.Price;
            existTour.Duration = tourUpdateVM.Duration;
            existTour.GroupSize = tourUpdateVM.GroupSize;
            existTour.Overview = tourUpdateVM.Overview;
            existTour.IsActive = tourUpdateVM.IsActive;
            existTour.TourName = tourUpdateVM.TourName;
        }

        public async Task SafeDeleteTourAsync(Guid id)
        {
            Tour tour = await _unitOfWork.GetRepository<Tour>().GetByGuidAsync(id);
            tour.IsActive = false;
            await _unitOfWork.GetRepository<Tour>().UpdateAsync(tour);
            await _unitOfWork.SaveAsync();
        }


        public async Task HardDeleteAsync(Guid id)
        {
            Tour tour = await _unitOfWork.GetRepository<Tour>().GetByGuidAsync(id);
            
            foreach (var item in tour.TourImages)
            {
                string deletePath = Path.Combine(_env.WebRootPath,"assets", "img","tours", item.ImageUrl);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
            }
            await _unitOfWork.GetRepository<Tour>().DeleteAsync(tour);
            await _unitOfWork.SaveAsync();
        }

        public async Task RecoverTourAsync(Guid id)
        {
            Tour tour = await _unitOfWork.GetRepository<Tour>().GetByGuidAsync(id);
            tour.IsActive = true;
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<TourViewModel>> GetAllPassiveTours()
        {
            var tours = await _unitOfWork.GetRepository<Tour>().GetAllAsync(x => x.IsActive == false);
            var map = _mapper.Map<List<TourViewModel>>(tours);
            return map;
        }
    }
}
