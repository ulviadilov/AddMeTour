﻿using AddMeTour.Entity.ViewModels.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Service.Services.Abstractions
{
    public interface ITourService
    {
        Task<List<TourViewModel>> GetAllToursNonDeletedAsync();
        Task<TourViewModel> GetTourByGuidAsync(Guid id);
        Task CreateTourAsync(TourAddViewModel tourAddVM);
        Task<TourUpdateViewModel> UpdateTourByGuidAsync(Guid id);
        Task SafeDeleteTourAsync(Guid id);
        Task HardDeleteAsync(Guid id);
        Task RecoverTourAsync(Guid id);
        Task<List<TourViewModel>> GetAllPassiveTours();
    }
}
