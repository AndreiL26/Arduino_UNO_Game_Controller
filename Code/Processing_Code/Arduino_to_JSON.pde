import processing.serial.*;
Serial serialPort;
JSONObject json;

void setup() {
  String portName = Serial.list()[0];
  serialPort = new Serial(this, portName, 9600);
  serialPort.bufferUntil('\n'); 
  json = new JSONObject();
}

void draw() {
  while (serialPort.available() > 0) {
    String receivedString = serialPort.readStringUntil('\n');

    if (receivedString != null) {
      processSerialInput(receivedString);
    }
  }
}

void processSerialInput(String receivedString) {
  
  float [] nums = float(split(receivedString, ' '));
 
  // Saving data in a JSON format
  json.setFloat ("temperature", nums[0]);
  json.setFloat ("lightUnits", nums[1]);
  json.setFloat ("tilt", nums[2]);
  json.setFloat ("turnLeft", nums[3]);
  json.setFloat ("turnRight", nums[4]);
  
  saveJSONObject(json, "../Assets/ExternalData/data.json");
  
}
