using System.ComponentModel.DataAnnotations.Schema;

namespace marioProgetto.Models
{
    [Table("VeichleFeatures")]
    public class VeichleFeature
    {
        public int VeichleId { get; set; }
        public int FeatureId { get; set; }
        public Veichle Veichle { get; set; }
        public Feature Feature { get; set; }

    }
}