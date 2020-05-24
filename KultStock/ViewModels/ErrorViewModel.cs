using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KultStock.ViewModels
{

        public class ErrorViewModel
        {
            public string RequestId { get; set; }
            public string Summary { get; set; }
            public string Description { get; set; }

            public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        }
    }

