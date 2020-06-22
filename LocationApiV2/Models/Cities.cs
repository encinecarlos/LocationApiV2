using System;
using System.Collections.Generic;

namespace LocationApiV2.Models
{
    public partial class Cities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
    }
}
