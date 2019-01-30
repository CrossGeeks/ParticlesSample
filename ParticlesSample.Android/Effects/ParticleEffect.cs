using System.Linq;
using DroidView = Android.Views;
using Com.Plattysoft.Leonids;
using ParticlesSample.Effects;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ParticleEffect = ParticlesSample.Droid.Effects.ParticleEffect;
using Android.Support.V4.Content;

[assembly: ResolutionGroupName("CrossGeeks")]
[assembly: ExportEffect(typeof(ParticleEffect), "ParticleEffect")]
namespace ParticlesSample.Droid.Effects
{
    public class ParticleEffect : PlatformEffect
    {
        ParticleSystem particleSystem;

        protected override void OnAttached()
        {
            var control = Control ?? Container;
            control.Touch += OnControlTouch;
        }

        protected override void OnDetached()
        {
            var control = Control ?? Container;
            control.Touch -= OnControlTouch;
        }


        void OnControlTouch(object sender, Android.Views.View.TouchEventArgs e)
        {
         
                var effect = (ParticlesSample.Effects.ParticleEffect)Element.Effects.FirstOrDefault(p => p is ParticlesSample.Effects.ParticleEffect);

                var mode = effect?.Mode ?? ParticlesSample.Effects.EmitMode.OneShot;
                var lifeTime = (long)(effect?.LifeTime * 1000 ?? (long)1500);
                var numberOfItems = effect?.NumberOfParticles ?? 4000;
                var scale = effect?.Scale ?? 1.0f;
                var speed = effect?.Speed ?? 0.1f;
                var image = effect?.Image ?? "ic_launcher";

                switch (e.Event.Action)
                {
                    case DroidView.MotionEventActions.Down:

                        var drawableImage = ContextCompat.GetDrawable(CrossCurrentActivity.Current.Activity, CrossCurrentActivity.Current.Activity.Resources.GetIdentifier(image, "drawable", CrossCurrentActivity.Current.Activity.PackageName));
                        particleSystem = new ParticleSystem(CrossCurrentActivity.Current.Activity, numberOfItems, drawableImage, lifeTime);
                        particleSystem
                          .SetSpeedRange(0f, speed)
                          .SetScaleRange(0, scale)
                          .Emit((int)e.Event.GetX(), (int)e.Event.GetY(), numberOfItems);
                        
                        break;
                    case DroidView.MotionEventActions.Move:
                  
                        break;
                    case DroidView.MotionEventActions.Up:
                        if (mode == EmitMode.OneShot)
                        {
                            particleSystem?.StopEmitting();
                        }

                       break;
                }

        }


    }
}
