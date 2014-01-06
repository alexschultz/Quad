
using System.Text;
namespace WifiSample.Sensor
{
    /// <summary>
    /// Object to evaluate the results and provide the measured values.
    /// </summary>
    public struct AccelerationAndGyroData
    {
        /// <summary>
        /// X Achse des Beschleunigungssensors
        /// </summary>
        public int Acceleration_X;
        /// <summary>
        /// Y Achse des Beschleunigungssensors
        /// </summary>
        public int Acceleration_Y;
        /// <summary>
        /// Z  Achse des Beschleunigungssensors
        /// </summary>
        public int Acceleration_Z;
        /// <summary>
        /// Temperatur Wert
        /// </summary>
        public int Temperatur;
        /// <summary>
        /// X Achse des Gyroskop
        /// </summary>
        public int Gyro_X;
        /// <summary>
        /// Y Achse des Gyroskop
        /// </summary>
        public int Gyro_Y;
        /// <summary>
        /// Z Achse des Gyroskop
        /// </summary>
        public int Gyro_Z;
        /// <summary>
        /// Creates the object with the results
        /// </summary>
        /// <param name="results"></param>
        public AccelerationAndGyroData(byte[] results)
        {
            // Result for the acceleration sensor put together by bit shifting
            Acceleration_X = (((int)results[0]) << 8) | results[1];
            Acceleration_Y = (((int)results[2]) << 8) | results[3];
            Acceleration_Z = (((int)results[4]) << 8) | results[5];

            // Results for temperature (date not yet tested)
            Temperatur = (((int)results[6]) << 8) | results[7];

            // Result for the gyroscope sensor put together by bit shifting
            Gyro_X = (((int)results[8]) << 8) | results[9];
            Gyro_Y = (((int)results[10]) << 8) | results[11];
            Gyro_Z = (((int)results[12]) << 8) | results[13];
        }
        /// <summary>
        /// Override the ToString () method
        /// Are all values ​​in a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            /*
             *The MPU data will be transmitted as small as possile 
             *The format will be ~Acceleration[X]|Acceleration[Y]| Acceleration[Z]|Temp|Gyro[X]|Gyro[Y]|Gyro[Z]~
             *i.e. ~2|4|72|95|100|100|100~
             */
            StringBuilder rtnStr = new StringBuilder("m");
            rtnStr.Append(Acceleration_X);
            rtnStr.Append("|");
            rtnStr.Append(Acceleration_Y);
            rtnStr.Append("|");
            rtnStr.Append(Acceleration_Z);
            rtnStr.Append("|");
            rtnStr.Append(Temperatur);
            rtnStr.Append("|");
            rtnStr.Append(Gyro_X);
            rtnStr.Append("|");
            rtnStr.Append(Gyro_Y);
            rtnStr.Append("|");
            rtnStr.Append(Gyro_Z);
            return rtnStr.ToString();
        }
        /// <summary>
        /// Returns the value as a percentage value in order to keep the number small.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetProzent(int value)
        {
            double d = (double)value;
            //double result = System.Math.Round((d / 65535) * 100);
            return d.ToString();
        }
    }
}
