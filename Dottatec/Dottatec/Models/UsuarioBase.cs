using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace Dottatec.Models
{
    public class UsuarioBase : IRegra
    {
        [Version]
        public string Version { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("deleted")]
        public bool Deletado { get; set; } 
    }
}
