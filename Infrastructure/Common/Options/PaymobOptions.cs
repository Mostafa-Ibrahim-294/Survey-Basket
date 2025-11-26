using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Options
{
    public class PaymobOptions
    {
        [Required]
        public string ApiKey { get; set; } = string.Empty;
        [Required]
        public int IntegrationId { get; set; }
        [Required]
        public string Hmac { get; set; } = string.Empty;
        [Required]
        public int IframeId { get; set; }


    }
}
