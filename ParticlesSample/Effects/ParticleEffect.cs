using Xamarin.Forms;

namespace ParticlesSample.Effects
{
    public enum EmitMode
    {
        OneShot,
        Infinite
    }
   
    public class ParticleEffect : RoutingEffect
    {
        public EmitMode Mode { get; set; } = EmitMode.OneShot;
        public int NumberOfParticles { get; set; } = 4000;
        public double LifeTime { get; set; } = 1.5f;
        public float Speed { get; set; } =0.1f;
        public float Scale { get; set; } = 1.0f;
        public string Image { get; set; } = "particle";


        public ParticleEffect(): base("CrossGeeks.ParticleEffect")
        {

        }
    }

        
}
