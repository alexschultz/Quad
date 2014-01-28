
namespace QuadCopter.Sensor
{
    /// <summary>
    /// Adressen alle abgeleitet vom Beispiel Sketch.
    /// Siehe Seite http://arduino.cc/playground/Main/MPU-6050
    /// </summary>
    public static class MPU6050_Registers
    {
        /// <summary>
        /// Standardadresse zum initialisieren.
        /// </summary>
        public static byte I2C_ADDRESS = 0x68;
        /// <summary>
        /// 0x1A
        /// </summary>
        public static byte MPU6050_CONFIG = 0x1A;
        /// <summary>
        /// 0x1B
        /// </summary>
        public static byte GYRO_CONFIG = 0x1B;    // R/W
        /// <summary>
        ///  0x1C
        /// </summary>
        public static byte ACCEL_CONFIG = 0x1C;   // R/W
        /// <summary>
        /// 0x3B
        /// </summary>
        public static byte ACCEL_XOUT_H = 0x3B;       // R  
        /// <summary>
        /// 0x3C
        /// </summary>
        public static byte ACCEL_XOUT_L = 0x3C;       // R  
        /// <summary>
        /// 0x3D
        /// </summary>
        public static byte ACCEL_YOUT_H = 0x3D;       // R  
        /// <summary>
        /// 0x3E
        /// </summary>
        public static byte ACCEL_YOUT_L = 0x3E;       // R  
        /// <summary>
        /// 0x3F
        /// </summary>
        public static byte ACCEL_ZOUT_H = 0x3F;       // R  
        /// <summary>
        /// 0x40
        /// </summary>
        public static byte ACCEL_ZOUT_L = 0x40;       // R 
        /// <summary>
        /// 0x41
        /// </summary>
        public static byte TEMP_OUT_H = 0x41;         // R  
        /// <summary>
        /// 0x42
        /// </summary>
        public static byte TEMP_OUT_L = 0x42;         // R  
        /// <summary>
        /// 0x43
        /// </summary>
        public static byte GYRO_XOUT_H = 0x43;        // R  
        /// <summary>
        /// 0x44
        /// </summary>
        public static byte GYRO_XOUT_L = 0x44;        // R  
        /// <summary>
        /// 0x45
        /// </summary>
        public static byte GYRO_YOUT_H = 0x45;        // R  
        /// <summary>
        /// 0x46
        /// </summary>
        public static byte GYRO_YOUT_L = 0x46;        // R  
        /// <summary>
        /// 0x47
        /// </summary>
        public static byte GYRO_ZOUT_H = 0x47;        // R  
        /// <summary>
        /// 0x48
        /// </summary>
        public static byte GYRO_ZOUT_L = 0x48;        // R  
        /// <summary>
        /// 0x6B
        /// </summary>
        public static byte PWR_MGMT_1 = 0x6B;         // R/W
        /// <summary>
        /// 0x6C
        /// </summary>
        public static byte PWR_MGMT_2 = 0x6C;         // R/W
        /// <summary>
        /// 0x75
        /// </summary>
        public static byte WHO_AM_I = 0x75;           // R
    }
}
