using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kovács_Dominik_backend.Models
{
    public partial class Versenyzo
    {
        public int Id { get; set; }
        public string Nev { get; set; } = null!;
        public string Szakmaid { get; set; } = null!;
        public string Orszagid { get; set; } = null!;
        public int Pont { get; set; }
        [JsonIgnore]
        public virtual Orszag? Orszag { get; set; } = null!;
        [JsonIgnore]
        public virtual Szakma? Szakma { get; set; } = null!;
    }
}
