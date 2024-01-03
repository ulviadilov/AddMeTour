﻿using AddMeTour.Data.UnitOfWorks.Abstractions;
using AddMeTour.Data.UnitOfWorks.Concretes;
using AddMeTour.Entity.Entities.Tour;
using AddMeTour.Entity.ViewModels.Tour;
using AddMeTour.Entity.ViewModels.Tour.Category;
using AddMeTour.Service.Helpers.Images;
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
    public class TourService
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
            Tour tour = new Tour
            {
                CreateTime = DateTime.Now,
                IsActive = true,
                DepartureDetails = tourAddVM.DepartureDetails,
                Duration = tourAddVM.Duration,
                GroupSize = tourAddVM.GroupSize,
                Overview = tourAddVM.Overview,
                Price = tourAddVM.Price,
                TourName = tourAddVM.TourName
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

            TourImage posterImage = new TourImage
            {
                IsActive = true,
                ImageUrl = tourAddVM.PosterImageFile.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img", "tours")),
                IsPoster = true,
                TourId = tourAddVM.Id
            };
            await _unitOfWork.GetRepository<TourImage>().AddAsync(posterImage);

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
        }

    }
}
