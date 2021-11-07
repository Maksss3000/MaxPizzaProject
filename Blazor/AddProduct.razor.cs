using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace MaxPizzaProject.Blazor
{
    public  partial class AddProduct
    {



        [Parameter]
        public string ProductName { get; set; }

    }
}
