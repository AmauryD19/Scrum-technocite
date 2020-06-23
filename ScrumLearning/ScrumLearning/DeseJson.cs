using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumLearning
{
    class DeseJson
    {
        //[JsonProperty(PropertyName = "Current")]
        public Current current { get; set; }
    }

    class Current
    {
        public int temperature { get; set; }

    }

}
