#include <math.h>

#define lightSensor  A0
#define temperatureSensor  A1
#define tiltSensor 4
#define turnRight 6
#define turnLeft 7

double temperature;
int input, lightUnits, tilt, leftState, rightState;

void setup() {
  Serial.begin (9600);
}

double getTemperature(int rawADC) {
 rawADC -= 200; // Modify the input value to calibrate the temperature.
 double temp;
 temp = log(((10240000/rawADC) - 10000));
 temp = 1 / (0.001129148 +
 (0.000234125 + (0.0000000876741 * temp * temp ))* temp );
 temp = floor(temp*100)/100;
 return temp - 273.15; // Convert Kelvin to Celsius
} 

void readData () {
  input = analogRead (temperatureSensor);
  temperature = getTemperature (input);
  lightUnits = analogRead (lightSensor);
  tilt = digitalRead (tiltSensor);
  leftState = digitalRead (turnLeft);
  rightState = digitalRead (turnRight);
}

void writeData () {
  Serial.println ((String)temperature + ' ' + lightUnits + ' ' + tilt + ' ' + leftState + ' ' + rightState);
}

void loop () {
  readData ();
  writeData (); 
  delay (200);
 
}
