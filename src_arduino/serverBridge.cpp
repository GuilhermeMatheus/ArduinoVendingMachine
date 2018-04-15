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
  static String waitResponse(const int timeout) {
    String response = "";
    long int time = millis();
    
    while ((time + timeout) > millis())
    while (esp8266.available()) {
      response += (char)esp8266.read();
    }
    
#ifdef DEBUG
    Serial.print(response);
#endif
    
    return response;
  }

  // TODO: Do we rly rly want string type dependency???
  static String sendData(const __FlashStringHelper *ifsh, const int timeout) {
    esp8266.print(ifsh);
    esp8266.print(F("\r\n"));
    return waitResponse(timeout);
  }

  static String sendData(const char *data, uint8_t len, const int timeout) {
    esp8266.print(data);
    esp8266.print(F("\r\n"));
    return waitResponse(timeout);
  }

  static void connectWifi() {
    sendData(F("AT+RST"), 2000);
    sendData(F("AT+CWMODE=1"), 2000);
    sendData(F("AT+CWJAP=\"Kibe\",\"86827012\""), 9000);
  }

  static void sendMachineStartup() {
    char data[2] = {
      0x03, // Intention
      0x12, // Machine ID
    };

    sendData(F("AT+CIPMUX=0"), 9000);
    sendData(F("AT+CIPSTART=\"TCP\",\"ESFIHA\",4444"), 2000);
    sendData(F("AT+CIPSEND=2"), 2000); // sizeof(data)
	  
	  sendData(data, sizeof(data), 2000);
  }
}

ServerBridge::ServerBridge() {
};

void ServerBridge::begin() {
  Helpers::lcdWrite(1, 0, F("Iniciando rede"));
  Helpers::lcdWrite(1, 1, F("ESP8266"));
  esp8266.begin(19200);
  connectWifi();
  Helpers::lcdWrite(1, 1, F("sendMachineStartup"));
  sendMachineStartup();
}