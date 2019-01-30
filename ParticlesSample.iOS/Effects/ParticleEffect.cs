using System;
using System.Linq;
using System.Threading.Tasks;
using CoreAnimation;
using Foundation;
using ParticlesSample.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("CrossGeeks")]
[assembly: ExportEffect(typeof(ParticleEffect), "ParticleEffect")]
namespace ParticlesSample.iOS.Effects
{
    public class ParticleEffect : PlatformEffect
    {
        readonly UITapGestureRecognizer tapDetector;

        public ParticleEffect()
        {
            tapDetector = new UITapGestureRecognizer(() =>
            {
                var control = Control ?? Container;
                var tapPoint = tapDetector.LocationInView(control);

                var effect = (ParticlesSample.Effects.ParticleEffect)Element.Effects.FirstOrDefault(p => p is ParticlesSample.Effects.ParticleEffect);

                var mode = effect?.Mode ?? ParticlesSample.Effects.EmitMode.OneShot;
                var lifeTime = effect?.LifeTime??1.5f;
                var numberOfItems = effect?.NumberOfParticles ?? 4000;
                var scale = effect?.Scale ?? 1.0f;
                var speed = effect?.Speed*1000 ?? 100.0f;
                var image = effect?.Image ?? "Icon";

                var emitterLayer = new CAEmitterLayer();


                emitterLayer.Position = tapPoint;
                emitterLayer.Shape = CAEmitterLayer.ShapeCircle;
            

                var cell = new CAEmitterCell();

                cell.Name = "pEmitter";

                cell.BirthRate = numberOfItems;
                cell.Scale =0f;
                cell.ScaleRange = scale;
                cell.Velocity = speed;
                cell.LifeTime =(float)lifeTime;

                cell.EmissionRange = (nfloat)Math.PI * 2.0f;
                cell.Contents = UIImage.FromBundle(image).CGImage;

                emitterLayer.Cells = new CAEmitterCell[] { cell };

               
                control.Layer.AddSublayer(emitterLayer);
                    Task.Delay(1).ContinueWith(t => {

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            emitterLayer.SetValueForKeyPath(NSNumber.FromInt32(0), new NSString("emitterCells.pEmitter.birthRate"));
                          
                        });

                    });
            }); 
        }
        protected override void OnAttached()
        {
            var control = Control ?? Container;

            control.AddGestureRecognizer(tapDetector);
            tapDetector.Enabled = true;


        }



        protected override void OnDetached()
        {
            var control = Control ?? Container;
            control.RemoveGestureRecognizer(tapDetector);
            tapDetector.Enabled = false;
        }

    }
}
