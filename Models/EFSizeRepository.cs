using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
    public class EFSizeRepository : ISizeRepository
    {
        private PizzeriaDbContext context;
        public EFSizeRepository (PizzeriaDbContext ctx)
        {
            context = ctx;
        }
        public Size GetSize (long sizeId)
        {
            return context.Sizes.Find(sizeId);
        }
    }
}
