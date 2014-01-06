using Microsoft.SPOT;
using System.Threading;
using WifiSample.I2C_Hardware;

namespace WifiSample.Sensor
{
    public class MPU6050
    {
        /// <summary>
        /// Classes for the connection via the I ² C bus
        /// </summary>
        private I2C_Connector _I2C;
        /// <summary>
        /// Class with the initializing Entsprechendn addresses
        /// </summary>
        public MPU6050()
        {
            Debug.Print("Initialize the accelerometer and gyro sensor MPU-6050");
            _I2C = new I2C_Connector(0x68, 400);

            Initialize();

            // Test Connection
            Debug.Print("Test Connection:");
            ErrorStatus(_I2C.Read(new byte[] { MPU6050_Registers.WHO_AM_I }));
            Debug.Print("-----------------------------------------------------------------------");
        }
        /// <summary>
        /// Initialize the sensor with the default address
        /// </summary>
        public void Initialize()
        {
            ErrorStatus(_I2C.Write(new byte[] { 0x6B, 0x80 }));
            Thread.Sleep(10);
            ErrorStatus(_I2C.Write(new byte[] { 0x6B, 0x00 }));
            ErrorStatus(_I2C.Write(new byte[] { 0x1A, 0x06 }));
        }
        /// <summary>
        /// Returns an error status via Debug
        /// </summary>
        /// <param name="error"></param>
        private void ErrorStatus(object error)
        {
            if (error != null)
            {
                    if ((int)error == 0) { Debug.Print("Status: Error"); }
                    else { Debug.Print("Status: OK"); }
            }
        }
        /// <summary>
        /// Retrieves the data from the sensor from
        /// </summary>
        public AccelerationAndGyroData GetSensorData()
        {
            byte[] registerList = new byte[14];

            registerList[0] = MPU6050_Registers.ACCEL_XOUT_H;
            registerList[1] = MPU6050_Registers.ACCEL_XOUT_L;
            registerList[2] = MPU6050_Registers.ACCEL_YOUT_H;
            registerList[3] = MPU6050_Registers.ACCEL_YOUT_L;
            registerList[4] = MPU6050_Registers.ACCEL_ZOUT_H;
            registerList[5] = MPU6050_Registers.ACCEL_ZOUT_L;
            registerList[6] = MPU6050_Registers.TEMP_OUT_H;
            registerList[7] = MPU6050_Registers.TEMP_OUT_L;
            registerList[8] = MPU6050_Registers.GYRO_XOUT_H;
            registerList[9] = MPU6050_Registers.GYRO_XOUT_L;
            registerList[10] = MPU6050_Registers.GYRO_YOUT_H;
            registerList[11] = MPU6050_Registers.GYRO_YOUT_L;
            registerList[12] = MPU6050_Registers.GYRO_ZOUT_H;
            registerList[13] = MPU6050_Registers.GYRO_ZOUT_L;
            _I2C.Write(new byte[] { MPU6050_Registers.ACCEL_XOUT_H });
            _I2C.Read(registerList);
            return new AccelerationAndGyroData(registerList);
        }
    }
}
