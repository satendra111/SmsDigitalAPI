using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommonEntity
{
    public class CorrelationIdOptions
    {
        private const string DefaultHeader = "X-Correlation-ID";
        public string Header { get; set; } = DefaultHeader;
        public bool IncludeInResponse { get; set; } = true;
    }
}
