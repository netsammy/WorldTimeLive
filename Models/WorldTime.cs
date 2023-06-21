using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTime
{
    public class TimeZoneItem
    {
        public string DisplayName { get; set; }
        public string CurrentTime { get; set; }

        public string Id { get; internal set; }
        public bool IsSelected { get; set; }
    }
}
