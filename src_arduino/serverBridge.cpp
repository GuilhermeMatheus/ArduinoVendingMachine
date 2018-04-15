#include "server/serverBridge.h"
#include <Arduino.h>
#include <SoftwareSerial.h>
#include "globals.h"
#include "hardware/helpers.h"

#define DEBUG

#define CON_WIFI_SSID "Kibe"
#define CON_WIFI_PSWD "86827012"

#define SOFTWARE_SERIAL_RX 8
#define SOFTWARE_SERIAL_TX A0

namespace {
  static SoftwareSerial esp8266(SOFTWARE_SERIAL_RX, SOFTWARE_SERIAL_TX);

  // TODO: Do we rly rly want string type dependency???
  static String sendData(String command, const int timeout)
  {
    String response = "";
    esp8266.print(command);
    esp8266.print(F("\r\n"));
    long int time = millis();
    
    while ( (time + timeout) > millis())
    {
      while (esp8266.available())
      {
        char c = esp8266.read();
        response += c;
      }
    }
    
#ifdef DEBUG
    Serial.print(response);
#endif
    
    return response;
  }

  static void connectWifi() {
    sendData("AT+RST", 2000);
    sendData("AT+GMR", 2000);
    sendData("AT+CWMODE=1", 2000);
    sendData("AT+CWJAP=\"Kibe\",\"86827012\"", 9000);
    sendData("AT+CIPMUX=0", 9000);
  }

  static void sendMachineStartup() {
    sendData("AT+CIPSTART=\"TCP\",\"ESFIHA\"\,80", 2000, DEBUG);  
    sendData("AT+CIPSEND=18", 2000, DEBUG);  
    sendData("GET / HTTP/1.1", 2000, DEBUG);  
    sendData("", 2000, DEBUG);


    sendData("AT+RST", 2000);
    sendData("AT+GMR", 2000);
    sendData("AT+CWMODE=1", 2000);
    sendData("AT+CWJAP=\"Kibe\",\"86827012\"", 9000);
    sendData("AT+CIPMUX=0", 9000);
  }
}

ServerBridge::ServerBridge() {
};

void ServerBridge::begin() {
  Helpers::lcdWrite(0, 2, F("Iniciando rede"));

  esp8266.begin(19200);
  connectWifi();

}