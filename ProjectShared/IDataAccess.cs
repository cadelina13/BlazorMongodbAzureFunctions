using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectShared.Models;
using Refit;

namespace ProjectShared
{
    public interface IDataAccess
    {
        [Post("/AddRecords")]
        Task<PersonModel> AddRecords(PersonModel person);

        [Get("/GetRecords")]
        Task<List<PersonModel>> GetRecords();
    }
}
