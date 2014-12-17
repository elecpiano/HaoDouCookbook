using System;
using System.Threading.Tasks;
using Windows.Devices.Sensors;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.System.Profile;

namespace HaoDouCookBook.Utility
{
    public class DeviceHelper
    {
        public static string GetUniqueDeviceID()
        {
            HardwareToken token = HardwareIdentification.GetPackageSpecificToken(null);
            IBuffer hardwareId = token.Id;

            HashAlgorithmProvider hasher = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer hashed = hasher.HashData(hardwareId);

            string hashedString = CryptographicBuffer.EncodeToHexString(hashed);
            return hashedString;
        }

#if WINDOWS_PHONE_APP 
        public static void PhoneCall(string phoneNumber)
        {
            Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(phoneNumber, "");
        }
#endif
        public async static Task OpenHttpURL(string url)
        {
            if (!url.StartsWith("http"))
            {
                url = "http://" + url;
            }

            await OpenUrl(url);
            
        }

        public async static Task OpenHttpsUrl(string url)
        {
            if (!url.StartsWith("https"))
            {
                url = "https://" + url;
            }

            await OpenUrl(url);
        }

        private async static Task OpenUrl(string url)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(url));
        }
    
    }

    public class Vector3
    {
        public static Vector3 MinValue = new Vector3() { X = double.MinValue, Y = double.MinValue, Z = double.MaxValue };
        public static Vector3 MaxValue = new Vector3() { X = double.MaxValue, Y = double.MaxValue, Z = double.MaxValue };

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double Distance(Vector3 other)
        {
            return Math.Sqrt((other.X - X) * (other.X - X) + (other.Y - Y) * (other.Y - Y) + (other.Z - Z) * (other.Z - Z));
        }

        public static bool operator ==(Vector3 one, Vector3 two)
        {
            return (one.X == two.X) && (one.Y == two.Y) && (one.Z == two.Z);
        }

        public static bool operator !=(Vector3 one, Vector3 two)
        {
            return (one.X != two.X) || (one.Y != two.Y) || (one.Z != two.Z);
        }

        public override bool Equals(object obj)
        {
            Vector3 other = obj as Vector3;
            if (other == null)
            {
                return false;
            }

            return other == this;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Vector3()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

    }

    public class ShakeGesture
    {
        private static double _distance = 3;
        private static Vector3 lastPos = Vector3.MaxValue;
        private static Accelerometer acc = null;
        private static Action _callback;

        private static void SetMovementDistance(double distance)
        {
            _distance = distance;
        }

        public static void StartListenning(Action callback)
        {
            acc = Accelerometer.GetDefault();
            if (acc != null)
            {
                acc.ReportInterval = 200;
                acc.ReadingChanged += acc_ReadingChanged;
                _callback = callback;
            }
        }

        public static void StopListenning()
        {
            if (acc != null)
            {
                acc.ReadingChanged -= acc_ReadingChanged;
                acc = null;
                _callback = null;
                lastPos = Vector3.MaxValue;
            }
        }

        static void acc_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            if (lastPos == Vector3.MaxValue)
            {
                lastPos = new Vector3()
                {
                    X = args.Reading.AccelerationX,
                    Y = args.Reading.AccelerationY,
                    Z = args.Reading.AccelerationZ
                };
                return;
            }

            Vector3 pos = new Vector3()
            {
                X = args.Reading.AccelerationX,
                Y = args.Reading.AccelerationY,
                Z = args.Reading.AccelerationZ
            };

            if (pos.Distance(lastPos) >= _distance)
            {

                if (_callback != null)
                {
                    _callback.Invoke();

                    // Sometimes, the Accelerometer will stop working.
                    // This this a workaroud to ensure Accelerometer works.
                    //
                    try
                    {
                        acc.ReadingChanged -= acc_ReadingChanged;
                    }
                    catch (Exception)
                    {
                    }
                    acc = Accelerometer.GetDefault();
                    acc.ReadingChanged += acc_ReadingChanged;
                }
            }

            lastPos = pos;
        }
    }
}
