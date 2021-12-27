using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MaxPizzaProject.Models
{
    public class EFAdminRepository : IAdminRepository
    {
        private PizzeriaDbContext context;

        public EFAdminRepository(PizzeriaDbContext ctx)
        {
            context = ctx;
        }
      
    }
}
