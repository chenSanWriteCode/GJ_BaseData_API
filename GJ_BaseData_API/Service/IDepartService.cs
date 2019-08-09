using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GJ_BaseData_API.Entity;
using GJ_BaseData_API.Infrastructure;

namespace GJ_BaseData_API.Service
{
    public interface IDepartService
    {
        Task<Result<List<Depart>>> getDepart();

        Task<Result<Depart>> getDepartById(int id);
    }
}
