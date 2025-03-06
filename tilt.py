import smbus
import time

#i2c address for Berry IMU
IMU_I2C_ADDRESS = 0x6A

#These are the addresses for the accelerometer
OUTX_L_XL = 0x28
OUTX_H_XL = 0x29
OUTY_L_XL = 0x2A
OUTY_H_XL = 0x2B
OUTZ_L_XL = 0x2C
OUTZ_H_XL = 0x2D

CTRL1_XL = 0x10  #control register for accelerometer

bus = smbus.SMBus(1)  

def write_register(register, value):
    bus.write_byte_data(IMU_I2C_ADDRESS, register, value)

def read_word(register_l, register_h):
    low = bus.read_byte_data(IMU_I2C_ADDRESS, register_l)
    high = bus.read_byte_data(IMU_I2C_ADDRESS, register_h)
    value = (high << 8) | low
    if value > 32767:
        value -= 65536
    return value

def read_accelerometer():
    x = read_word(OUTX_L_XL, OUTX_H_XL)
    y = read_word(OUTY_L_XL, OUTY_H_XL)
    z = read_word(OUTZ_L_XL, OUTZ_H_XL)
    return x, y, z

def initialize_accelerometer():
  
    write_register(CTRL1_XL, 0x40)

def detect_tilt():

    x, _, _ = read_accelerometer()


    x_g = x / 16384.0  

    if x_g > 0.45:  #You can make it more or less sensistive here
        return "Right" #RIGHT TRIGGER
    elif x_g < -0.45:
        return "Left" #LEFT TRIGGER
    else:
        return "Middle" #MIDDLE TRIGGER

if __name__ == "__main__":
    try:
        initialize_accelerometer()
        tilt_direction = detect_tilt()
        print(f"{tilt_direction}")
    except KeyboardInterrupt:
        print("\nExiting...")
