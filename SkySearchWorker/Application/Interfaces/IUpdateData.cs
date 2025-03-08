using SkySearchWorker.Application.DTOs.Amadeus.FlightOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Application.Interfaces
{
    public interface IUpdateData
    {
        Task<bool> UpdateAirlines(Dictionary<string, string> carriers);
        Task<bool> UpdateAirports(Dictionary<string, DictionaryLocationDto> locations);
        Task<bool> UpdateFlights(List<DataDto> dataDtos);
    }
}
