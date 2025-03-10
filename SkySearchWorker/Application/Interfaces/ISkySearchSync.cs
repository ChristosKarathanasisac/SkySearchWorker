using SkySearchWorker.Application.DTOs.Amadeus.FlightOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Application.Interfaces
{
    public interface ISkySearchSync
    {
        Task<bool> Sync();
        Task<List<FlightOfferDto>> GetFlightOffersAsync();
    }
}
