﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MaxPizzaProject.Models
{
    public class EFSizeRepository : ISizeRepository
    {
        private PizzeriaDbContext context;
        public EFSizeRepository (PizzeriaDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Size> GetAllExistedSizes()
        {
            return context.Sizes;
        }

        public Size GetSize (long sizeId)
        {
            return context.Sizes.Find(sizeId);
        }

        public Size GetSizeBySizeName(string sizeName)
        {
          
           return context.Sizes.Where(s => s.TheSize == sizeName).FirstOrDefault();
          
        }

        public void AddSize(Size size)
        {
            context.Sizes.Add(size);
            context.SaveChanges();
        }

        public void AddOrUpdateSize(Size size)
        {
            context.Sizes.Update(size);
            context.SaveChanges();
        }
    }
}
