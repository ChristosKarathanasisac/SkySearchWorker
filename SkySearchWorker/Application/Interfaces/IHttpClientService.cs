﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySearchWorker.Application.Interfaces
{
    public interface IHttpClientService
    {
        Task<T?> GetAsync<T>(string url, string client);
    }
}
