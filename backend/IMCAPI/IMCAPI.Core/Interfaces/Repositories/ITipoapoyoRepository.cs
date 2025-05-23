﻿using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Repositories
{
    public interface ITipoapoyoRepository
    {
        Task<IEnumerable<Tipoapoyo>> GetTipoapoyosAsync();
        Task<Tipoapoyo?> GetTipoapoyoByIdAsync(int id);
        Task AddTipoapoyoAsync(Tipoapoyo tipoapoyo);
        Task UpdateTipoapoyoAsync(Tipoapoyo tipoapoyo);
        Task DeleteTipoapoyoAsync(int id);
    }
}
