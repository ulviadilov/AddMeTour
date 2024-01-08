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
using System.Security.Cryptography.X509Certificates;
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
            var tours = await _unitOfWork.GetRepository<Tour>().GetAllAsync(x => x.IsActive == true, x => x.TourImages, x => x.TourLanguages, x => x.TourCategories, x => x.TourCountries);
            var map = _mapper.Map<List<TourViewModel>>(tours);
            return map;
        }

        public async Task<TourViewModel> GetTourByGuidAsync(Guid id)
        {
            var tour = await _unitOfWork.GetRepository<Tour>().GetByGuidAsync(id);
            var map = _mapper.Map<TourViewModel>(tour);
            return map;
        }

        public async Task<List<TourViewModel>> GetAllBestToursNonDeletedAsync()
        {
            TourCountry tourCountry = new TourCountry();
            var tours = await _unitOfWork.GetRepository<Tour>().GetAllAsync(x => x.IsActive == true && x.IsBest == true, x => x.TourCountries , x => x.TourImages);
            var map = _mapper.Map<List<TourViewModel>>(tours);
            return map;
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            var countries = await _unitOfWork.GetRepository<Country>().GetAllAsync(x => x.IsActive == true);
            var map = _mapper.Map<List<Country>>(countries);
            return map;
        }

        public async Task<List<TourImage>> GetAllTourImagesAsync()
        {
            var images = await _unitOfWork.GetRepository<TourImage>().GetAllAsync(x => x.IsActive == true);
            return images;
        }

        public async Task CreateTourAsync(TourAddViewModel tourAddVM)
        {
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
                IsBest = tourAddVM.IsBest
            };


            var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(x => x.IsActive == true && tourAddVM.CategoryIds.Contains(x.Id));
            foreach (var item in categories)
            {
                TourCategory tourCategory = new TourCategory
                {
                    CategoryId = item.Id,
                    Tour = tour,
                    IsActive = true

                };
                await _unitOfWork.GetRepository<TourCategory>().AddAsync(tourCategory);
            }
            var countries = await _unitOfWork.GetRepository<Country>().GetAllAsync(x => x.IsActive == true && tourAddVM.CountryIds.Contains(x.Id));
            foreach (var country in countries)
            {
                TourCountry tourCountry = new TourCountry
                {
                    CountryId = country.Id,
                    Tour = tour,
                    IsActive = true

                };
                await _unitOfWork.GetRepository<TourCountry>().AddAsync(tourCountry);
            }
            var inclusions = await _unitOfWork.GetRepository<Inclusion>().GetAllAsync(x => x.IsActive == true && tourAddVM.InclusionIds.Contains(x.Id));
            foreach (var inclusion in inclusions)
            {
                TourInclusion tourInclusion = new TourInclusion
                {
                    InclusionId = inclusion.Id,
                    Tour = tour,
                    IsActive = true

                };
                await _unitOfWork.GetRepository<TourInclusion>().AddAsync(tourInclusion);
            }
            var exclusions = await _unitOfWork.GetRepository<Exclusion>().GetAllAsync(x => x.IsActive == true && tourAddVM.ExclusionIds.Contains(x.Id));
            foreach (var exclusion in exclusions)
            {
                TourExclusion tourExclusion = new TourExclusion
                {
                    ExclusionId = exclusion.Id,
                    Tour = tour,
                    IsActive = true
                };
                await _unitOfWork.GetRepository<TourExclusion>().AddAsync(tourExclusion);
            }

            var languages = await _unitOfWork.GetRepository<Language>().GetAllAsync(x => x.IsActive == true && tourAddVM.LanguageIds.Contains(x.Id));
            foreach (var language in languages)
            {
                TourLanguage tourLanguage = new TourLanguage
                {
                    LanguageId = language.Id,
                    IsActive = true,
                    Tour = tour,
                };
                await _unitOfWork.GetRepository<TourLanguage>().AddAsync(tourLanguage);
            }

            TourImage posterImage = new TourImage
            {
                IsActive = true,
                ImageUrl = tourAddVM.PosterImageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "tours")),
                IsPoster = true,
                Tour = tour
            };
            await _unitOfWork.GetRepository<TourImage>().AddAsync(posterImage);

            foreach (IFormFile imageFile in tourAddVM.ImageFiles)
            {
                TourImage image = new TourImage
                {
                    IsActive = true,
                    ImageUrl = imageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "tours")),
                    IsPoster = false,
                    Tour = tour
                };
                await _unitOfWork.GetRepository<TourImage>().AddAsync(image);
            }
            tour.PosterImageUrl = posterImage.ImageUrl;
            await _unitOfWork.GetRepository<Tour>().AddAsync(tour);
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
                DepartureDetails = tour.DepartureDetails,
                TourName = tour.TourName,
                TourId = tour.Id,
                InclusionIds = new List<Guid>(),
                ExclusionIds = new List<Guid>(),
                CategoryIds = new List<Guid>(),
                CountryIds = new List<Guid>(),
                LanguageIds = new List<Guid>(),
                IsBest = tour.IsBest
            };
            foreach (TourInclusion inclusion in await _unitOfWork.GetRepository<TourInclusion>().GetAllAsync(x => x.TourId == tour.Id))
            {
                tourUpdateVM.InclusionIds?.Add(inclusion.InclusionId);
            }

            foreach (TourCategory tourCategory in await _unitOfWork.GetRepository<TourCategory>().GetAllAsync(x => x.TourId == tour.Id))
            {
                tourUpdateVM.CategoryIds?.Add(tourCategory.CategoryId);
            }

            foreach (TourCountry country in await _unitOfWork.GetRepository<TourCountry>().GetAllAsync(x => x.TourId == tour.Id))
            {
                tourUpdateVM.CountryIds?.Add(country.CountryId);
            }
            foreach (TourExclusion exclusion in await _unitOfWork.GetRepository<TourExclusion>().GetAllAsync(x => x.TourId == tour.Id))
            {
                tourUpdateVM.ExclusionIds?.Add(exclusion.ExclusionId);
            }
            foreach (TourLanguage language in await _unitOfWork.GetRepository<TourLanguage>().GetAllAsync(x => x.TourId == tour.Id))
            {
                tourUpdateVM.LanguageIds?.Add(language.LanguageId);
            }
            foreach (TourImage image in await _unitOfWork.GetRepository<TourImage>().GetAllAsync(x => x.TourId == tour.Id))
            {
                tourUpdateVM.ImageIds?.Add(image.Id);
            }

            return tourUpdateVM;
        }

        public async Task UpdateTourAsync(TourUpdateViewModel tourUpdateVM)
        {
            Tour existTour = await _unitOfWork.GetRepository<Tour>().GetByGuidAsync(tourUpdateVM.TourId);

            if (tourUpdateVM.PosterImageFile != null)
            {
                TourImage existImage = await _unitOfWork.GetRepository<TourImage>().GetAsync(x => x.IsPoster == true && x.TourId == existTour.Id);
                string deletePath = Path.Combine(_env.WebRootPath, "assets", "img", "tours",existImage?.ImageUrl);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
                existImage.ImageUrl = tourUpdateVM.PosterImageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "tours"));
                existTour.PosterImageUrl = tourUpdateVM.PosterImageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "tours"));
            }

            if (tourUpdateVM.ImageFiles is not null)
            {
                List<TourImage> tourImages = await _unitOfWork.GetRepository<TourImage>().GetAllAsync(x => x.IsPoster == false && x.TourId == existTour.Id);
                foreach (TourImage image in tourImages)
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

            if (tourUpdateVM.InclusionIds is not null)
            {
                var inclusions = await _unitOfWork.GetRepository<Inclusion>().GetAllAsync(x => tourUpdateVM.InclusionIds.Contains(x.Id));
                var existInclusions = await _unitOfWork.GetRepository<TourInclusion>().GetAllAsync(x => x.TourId == tourUpdateVM.TourId);
                foreach (TourInclusion item in existInclusions)
                {
                    await _unitOfWork.GetRepository<TourInclusion>().DeleteAsync(item);
                }
                foreach (var item in inclusions)
                {
                    await _unitOfWork.GetRepository<TourInclusion>().AddAsync(new TourInclusion { Tour = existTour, InclusionId = item.Id });
                }
            }
            else
            {
                var existInclusions = await _unitOfWork.GetRepository<TourInclusion>().GetAllAsync(x => x.TourId == tourUpdateVM.TourId);
                foreach (var item in existInclusions)
                {
                    await _unitOfWork.GetRepository<TourInclusion>().DeleteAsync(item);
                }
            }

            if (tourUpdateVM.ExclusionIds is not null)
            {
                var exclusions = await _unitOfWork.GetRepository<Exclusion>().GetAllAsync(x => tourUpdateVM.ExclusionIds.Contains(x.Id));
                var existExclusions = await _unitOfWork.GetRepository<TourExclusion>().GetAllAsync(x => x.TourId == tourUpdateVM.TourId);
                foreach (TourExclusion item in existExclusions)
                {
                    await _unitOfWork.GetRepository<TourExclusion>().DeleteAsync(item);
                }

                foreach (var item in exclusions)
                {
                    await _unitOfWork.GetRepository<TourExclusion>().AddAsync(new TourExclusion { Tour = existTour, ExclusionId = item.Id });
                }
            }
            else
            {
                var existExclusions = await _unitOfWork.GetRepository<TourExclusion>().GetAllAsync(x => x.TourId == tourUpdateVM.TourId);
                foreach (var item in existExclusions)
                {
                    await _unitOfWork.GetRepository<TourExclusion>().DeleteAsync(item);
                }
            }

            if (tourUpdateVM.LanguageIds is not null)
            {
                var languages = await _unitOfWork.GetRepository<Language>().GetAllAsync(x => tourUpdateVM.LanguageIds.Contains(x.Id));
                var existLanguages = await _unitOfWork.GetRepository<TourLanguage>().GetAllAsync(x => x.TourId == tourUpdateVM.TourId);

                foreach (var item in existLanguages)
                {
                    await _unitOfWork.GetRepository<TourLanguage>().DeleteAsync(item);
                }
                foreach (var item in languages)
                {
                    await _unitOfWork.GetRepository<TourLanguage>().AddAsync(new TourLanguage { Tour = existTour, LanguageId = item.Id });
                }
            }
            else
            {
                var existLanguages = await _unitOfWork.GetRepository<TourLanguage>().GetAllAsync(x => x.TourId == tourUpdateVM.TourId);
                foreach (var item in existLanguages)
                {
                    await _unitOfWork.GetRepository<TourLanguage>().DeleteAsync(item);
                }
            }

            if (tourUpdateVM.CategoryIds is not null)
            {
                var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(x => tourUpdateVM.CategoryIds.Contains(x.Id));
                var existCategories = await _unitOfWork.GetRepository<TourCategory>().GetAllAsync(x => x.TourId == tourUpdateVM.TourId);

                foreach (var item in existCategories)
                {
                    await _unitOfWork.GetRepository<TourCategory>().DeleteAsync(item);
                }
                foreach (var item in categories)
                {
                    await _unitOfWork.GetRepository<TourCategory>().AddAsync(new TourCategory { Tour = existTour, CategoryId = item.Id });
                }
            }
            else
            {
                var existCategories = await _unitOfWork.GetRepository<TourCategory>().GetAllAsync(x => x.TourId == tourUpdateVM.TourId);
                foreach (var item in existCategories)
                {
                    await _unitOfWork.GetRepository<TourCategory>().DeleteAsync(item);
                }
            }

            if (tourUpdateVM.CountryIds is not null)
            {
                var countries = await _unitOfWork.GetRepository<Country>().GetAllAsync(x => tourUpdateVM.CountryIds.Contains(x.Id));
                var existCountries = await _unitOfWork.GetRepository<TourCountry>().GetAllAsync(x => x.TourId == tourUpdateVM.TourId);
                foreach (var item in existCountries)
                {
                    await _unitOfWork.GetRepository<TourCountry>().DeleteAsync(item);
                }
                foreach (var item in countries)
                {
                    await _unitOfWork.GetRepository<TourCountry>().AddAsync(new TourCountry { Tour = existTour, CountryId = item.Id });
                }
            }
            else
            {
                var existCountries = await _unitOfWork.GetRepository<TourCountry>().GetAllAsync(x => x.TourId == tourUpdateVM.TourId);
                foreach (var item in existCountries)
                {
                    await _unitOfWork.GetRepository<TourCountry>().DeleteAsync(item);
                }
            }

            existTour.DepartureDetails = tourUpdateVM.DepartureDetails;
            existTour.Price = tourUpdateVM.Price;
            existTour.Duration = tourUpdateVM.Duration;
            existTour.GroupSize = tourUpdateVM.GroupSize;
            existTour.Overview = tourUpdateVM.Overview;
            existTour.IsActive = tourUpdateVM.IsActive;
            existTour.TourName = tourUpdateVM.TourName;
            existTour.IsBest = tourUpdateVM.IsBest;
            await _unitOfWork.SaveAsync();
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
                string deletePath = Path.Combine(_env.WebRootPath, "assets", "img", "tours", item.ImageUrl);
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
