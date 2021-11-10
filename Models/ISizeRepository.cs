using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaxPizzaProject.Models
{
    public interface ISizeRepository
    {
        public Size GetSize(long sizeId);

        IEnumerable<Size> GetAllExistedSizes();

        Size GetSizeBySizeName(string sizeName);
    }
}
