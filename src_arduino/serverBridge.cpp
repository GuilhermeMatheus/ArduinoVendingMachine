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

  static String sendData(const __FlashStringHelper *ifsh, const int timeout) {
    esp8266.print(ifsh);
    esp8266.print(F("\r\n"));
    return waitResponse(timeout);
  }

  static String sendData(const char *data, uint8_t len, const int timeout) {
    for(uint8_t i = 0; i < len; i++)
      esp8266.print(data[i]);

    esp8266.print(F("\r\n"));
    return waitResponse(timeout);
  }

  static void connectWifi() {
    sendData(F("AT+RST"), 2000);
    sendData(F("AT+CWMODE=1"), 2000);
    sendData(F("AT+CWJAP=\"Kibe\",\"86827012\""), 3000);
  }

  static void sendMessageToServer(const char *data, uint8_t len) {
    sendData(F("AT+CIPMUX=0"), 3000);
    sendData(F("AT+CIPSTART=\"TCP\",\"ESFIHA\",4444"), 2000);
    esp8266.print(F("AT+CIPSEND="));
    esp8266.print(len, DEC);
    esp8266.print(F("\r\n"));
    waitResponse(2000);
    sendData(data, len, 2000);
  }

  static void sendMachineStartup() {
    char hiId = gGlobals.gMachineId >> 8;
    char loId = gGlobals.gMachineId & 0xFF;
    char data[3] = {
      0x03, // Intention
      hiId,
      loId
    };
    sendMessageToServer(data, sizeof(data));
  }

  static void sendSale() {
    char hiId = gGlobals.gMachineId >> 8;
    char loId = gGlobals.gMachineId & 0xFF;

    union {
      float price;
      char bytes[sizeof(float)];
    } priceData; 

    priceData.price = gGlobals.gCurProd.price;

    char data[13] = {
      0x0, // Intention
      hiId,
      loId,
      gGlobals.gWallet.UID[0],
      gGlobals.gWallet.UID[1],
      gGlobals.gWallet.UID[2],
      gGlobals.gWallet.UID[3],
      1, // itemsCount
      gGlobals.gCurProd.id,
      priceData.bytes[0],
      priceData.bytes[1],
      priceData.bytes[2],
      priceData.bytes[3]
    };
    sendMessageToServer(data, sizeof(data));
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

void ServerBridge::sale() {
  sendSale();
}

void ServerBridge::verifyProductsUpdate() {
  
}