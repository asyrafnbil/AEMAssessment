using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AEMAssessment.Models
{
    public class PlatformWell
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string UniqueName { get; set; }
        [JsonProperty(Required = Required.Always)]
        public double Latitude { get; set; }
        [JsonProperty(Required = Required.Always)]
        public double Longitude { get; set; }
        [JsonProperty(Required = Required.Always)]
        public DateTimeOffset CreatedAt { get; set; }
        [JsonProperty(Required = Required.Always)]
        public DateTimeOffset UpdatedAt { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Well> Well { get; set; }
    }
}