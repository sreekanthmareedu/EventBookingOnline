
using BusinessEventsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BusinessEvents.DataAccess.Repository.IRepository
{
    public interface IBEventRepository : IRepository<BEvent>
    {
        public Task<BEvent> UpdateAsync(BEvent entity);
    }
}
