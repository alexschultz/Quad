using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.IO.Ports;
using System.Text;
using NPWM = SecretLabs.NETMF.Hardware.PWM;
using WifiSample.I2C_Hardware;
using WifiSample.Sensor;

namespace WifiSample
{

   
    public class Program
    {


        private enum readyState
        {
            standby,
            communicating,
            letsFly
        }
        
        private static SerialPort _wifiPt;
        private static NPWM _led;
        private static InputPort _btn;
        private static string _buffer;
        // Initialize the Sensor
        private static MPU6050 mpu6050 = new MPU6050();
        // Creating Object for gyro and accelerometer
        private static AccelerationAndGyroData senorResult; 
   
        public static void Main()
        {

            Init();
            while (true)
            {
                        //string str =
                        //   "aX: " + (Int16)senorResult.Acceleration_X + "\t; " +
                        //   "aY: " + (Int16)senorResult.Acceleration_Y + "\t; " +
                        //   "aZ: " + (Int16)senorResult.Acceleration_Z + "\t; " +
                        //   "gX: " + (Int16)senorResult.Gyro_X + "\t; " +
                        //   "gY: " + (Int16)senorResult.Gyro_Y + "\t; " +
                        //   "gZ: " + (Int16)senorResult.Gyro_Z + "\t; " +
                        //   "T: " + (Byte)((Int16)senorResult.Temperatur / 340 + 36.53);
                        Write(mpu6050.GetSensorData().ToString());
                        //Thread.Sleep(100);
            }
        }

        private static void Init()
        {
            _led = new NPWM(Pins.ONBOARD_LED);
            _wifiPt = new SerialPort(SerialPorts.COM1, 115200, Parity.None, 8, StopBits.One);
            _wifiPt.DataReceived += new SerialDataReceivedEventHandler(rec_DataReceived);
            _wifiPt.Open();
            senorResult = mpu6050.GetSensorData();
        }

       

        #region Event Handlers
        private static void rec_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] bytes = new byte[_wifiPt.BytesToRead];
            _wifiPt.Read(bytes, 0, bytes.Length);

            char[] converted = new char[bytes.Length];
            for (int b = 0; b < bytes.Length; b++)
            {
                converted[b] = (char)bytes[b];
            }

            string str = new String(converted);
            if (str != null && str.Length > 0)
            {
                if (str.IndexOf("~") > -1)
                {
                    _buffer += str.Substring(0, str.IndexOf("~"));
                    ProcessReceivedString(_buffer);
                    _buffer = str.Substring(str.LastIndexOf("~") + 1);
                }
                else
                {
                    _buffer += str;
                }
            }
        }
        #endregion

        #region Private Methods
        private static void ProcessReceivedString(string _buffer)
        {
            if (_buffer == "ping")
            {
                Write(_buffer);
            }
            else
            {
                uint val = UInt32.Parse(_buffer);
                _led.SetDutyCycle(val);
            }
        }

        private static void Write(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes("~" + message + "~");
            _wifiPt.Write(bytes, 0, bytes.Length);
           
        }
        #endregion
    }
}
