using Shared.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SharedLibraryTest : Page
    {
        public SharedLibraryTest()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        #region Network Helper Test
        
        private void network_check_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(NetworkHelper.Current.IsWifiConnectionAvailable)
            {
                this.wifi_status.Text = "Available";
            }
            else
            {
                this.wifi_status.Text = "Unavailable";
            }
        }

        #endregion

        #region DataLoader Test

        private void loadData_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string dataURL = "http://wudixiaop.github.io/toolset/data/testdata.json";
            string dataURL2 = "http://wudixiaop.github.io/toolset/data/data.json";
            string listDataURL = "http://wudixiaop.github.io/toolset/data/ListData.json";

            GenericDataLoader<Data> gd = new GenericDataLoader<Data>();
            gd.Load(dataURL, true, "Test", "testdata.json", (data) =>
                {
                    this.loaderData.Text = data.ID.ToString(); 
                });

            DataLoader<DataObject> dl = new DataLoader<DataObject>();
            dl.Load(dataURL2, true, "Test", "testdata2.json", (data) =>
                {
                    this.loaderData2.Text = data.Data.ID.ToString();
                });

            ListDataLoader<Data> ldl = new ListDataLoader<Data>();
            ldl.Load(listDataURL, true, "test", "listdata.json", (data) =>
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in data)
                    {
                        sb.AppendLine(item.ID.ToString());
                    }

                    this.loaderdata3.Text = sb.ToString();
                });
        }

        [DataContract]
        class Data
        {
            [DataMember(Name="id")]
            public int ID { get; set; }
        }

        [DataContract]
        class DataObject
        {
            [DataMember]
            public Data Data { get; set; }
        }

        #endregion

        #region Image Helper

        private async void downloadImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ImageHelper helper = new ImageHelper();
            await helper.Download("https://github.com/wudixiaop/ShaderlabVS/raw/master/img/Highlighting.PNG", "images", "1.png");
            var bitmapImage = await helper.ReadImage("images", "1.png");
            this.image.Source = bitmapImage;
        }

        #endregion
    }
}
