using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class LoadingCircle : UserControl
    {

        public static readonly DependencyProperty CircleDiameterProperty = DependencyProperty.Register("CircleDiameter", typeof(double), typeof(LoadingCircle), new PropertyMetadata(56));

        public double CircleDiameter
        {
            get { return (double)GetValue(CircleDiameterProperty); }
            set { SetValue(CircleDiameterProperty, value); }
        }

        private Storyboard loadingAni;

        public LoadingCircle()
        {
            this.InitializeComponent();
            loadingAni = this.Resources["loadingAnimation"] as Storyboard;
            this.root.DataContext = this;
        }

        public void StartLoading()
        {
            if (loadingAni != null)
            {
                loadingAni.Begin();
            }
        }

        public void StopLoading()
        {
            if (loadingAni != null)
            {
                loadingAni.Stop();
            }
        }
    }
}
