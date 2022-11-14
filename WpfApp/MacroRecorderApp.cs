using System.Diagnostics;
using System.Windows.Forms;
using Linearstar.Windows.RawInput;


namespace ITDimkClicker.BL.Services
{
    public class MacroRecorderApp
    {
        private readonly RawInputReceiverWindow _receiver;

        public MacroRecorderApp(RawInputReceiverWindow receiver)
        {
            _receiver = receiver;
        }

        private void RegisterDevices()
        {
            /*
             It calls
             [DllImport("user32", SetLastError = true)]
             private static extern bool RegisterRawInputDevices(RawInputDeviceRegistration[] pRawInputDevices,
             uint uiNumDevices, uint cbSize);
             */
            RawInputDevice.RegisterDevice(HidUsageAndPage.Keyboard,
                RawInputDeviceFlags.InputSink | RawInputDeviceFlags.NoLegacy, _receiver.Handle);
            RawInputDevice.RegisterDevice(HidUsageAndPage.Mouse,
                RawInputDeviceFlags.InputSink | RawInputDeviceFlags.NoLegacy, _receiver.Handle);
        }

        private void UnregisterDevices()
        {
            RawInputDevice.UnregisterDevice(HidUsageAndPage.Keyboard);
            RawInputDevice.UnregisterDevice(HidUsageAndPage.Mouse);
        }

        public void Run()
        {
            _receiver.Input += (_, e) => { Debug.WriteLine(e); };

            RegisterDevices();
            Application.Run();
        }
    }
}