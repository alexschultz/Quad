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
using QuadCopter.I2C_Hardware;
using QuadCopter.Sensor;
using QuadCopter.pid;

namespace QuadCopter
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
        private static readyState commStatus;
        private static pid.PIDLibrary.PID _pidController;
        private static NPWM motor1;
        private static NPWM motor2;
        private static NPWM motor3;
        private static NPWM motor4;
        private static uint powerLevel = 0;
   
        public static void Main()
        {
            Init();
            while (true)
            {
                updateMotors(powerLevel);
                Write(mpu6050.GetSensorData().ToString());
                //Thread.Sleep(100);
            }
        }

        private static void Init()
        {
            commStatus = readyState.standby;
            _led = new NPWM(Pins.ONBOARD_LED);
            powerLevel = 0;
            _wifiPt = new SerialPort(SerialPorts.COM1, 115200, Parity.None, 8, StopBits.One);
            _wifiPt.DataReceived += new SerialDataReceivedEventHandler(rec_DataReceived);
            _wifiPt.Open();
            //_pidController = new pid.PIDLibrary.PID(0,0,0,0,0,0,0,0,0,0);

            motor1 = new NPWM(Pins.GPIO_PIN_D5);
            motor2 = new NPWM(Pins.GPIO_PIN_D6);
            motor3 = new NPWM(Pins.GPIO_PIN_D9);
            motor4 = new NPWM(Pins.GPIO_PIN_D10);
            Thread.Sleep(2000);
            Write("Alex");
            ConnectToBaseStation();
        }

        private static void ConnectToBaseStation()
        {
            //while (commStatus == readyState.standby)
            //{
            //    Write("Hello Alex!");
 
            //}
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
            try
            {
                ParseControllerInputs(_buffer);
                Debug.Print(_buffer);
            }
            catch (Exception ex)
            {
                Debug.Print("Error reading data on quad Data: " + _buffer);

            }
        }

        private static void ParseControllerInputs(String inpts)
        {   
            String[] mpuArray = inpts.Split('|');
            powerLevel = Convert.ToUInt32(mpuArray[0]);
        }

        private static void updateMotors(uint val)
        {
            try
            {

                motor1.SetPulse(20000, 1000 + val);
                motor2.SetPulse(20000, 1000 + val);
                motor3.SetPulse(20000, 1000 + val);
                motor4.SetPulse(20000, 1000 + val);
                
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        private static void Write(string message)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes("~" + message + "~");
                _wifiPt.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
           
        }

        
        #endregion
    }
}
