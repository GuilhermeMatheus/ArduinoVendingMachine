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

#define IDX_IPD_BEGIN 4

namespace {
  static SoftwareSerial esp8266(SOFTWARE_SERIAL_RX, SOFTWARE_SERIAL_TX);
  static const char *IPD = "+IPD,";

  static void waitResponse(const int timeout, char *response, uint8_t lenResponse) {
    long int time = millis();
    
    uint8_t idx_ipd = 0;
    uint8_t idx_response = 0;
    bool serverMessageStarted = false;

    while ((time + timeout) > millis())
    while (esp8266.available()) {
      uint8_t curByte = esp8266.read();

      // Waiting for "+IDP,"
      if(curByte == IPD[idx_ipd] && idx_ipd != IDX_IPD_BEGIN) {
        idx_ipd++;
      } else if (idx_ipd == IDX_IPD_BEGIN) {
        if(serverMessageStarted && idx_response < lenResponse) {
          response[idx_response] = (char)curByte;
          idx_response++;
#ifdef DEBUG
          Serial.print(curByte, HEX); Serial.print('-');
#endif
        }
        // Waiting for "+IDP,NN:"
        serverMessageStarted = serverMessageStarted || (!serverMessageStarted && curByte == ':');
      } else {
        idx_ipd = 0;
      }
    }
    
#ifdef DEBUG
    if(serverMessageStarted) Serial.print(F("\r\n"));
#endif
  }

  static void sendData(const __FlashStringHelper *ifsh, const int timeout) {
    esp8266.print(ifsh);
    esp8266.print(F("\r\n"));
    delay(timeout);
  }

  static void sendData(const char *data, uint8_t len, const int timeout) {
    for(uint8_t i = 0; i < len; i++)
      esp8266.print(data[i]);

    esp8266.print(F("\r\n"));
    delay(timeout);
  }

  static void sendMessageToServer(const char *data, uint8_t len, char *response, uint8_t lenResponse) {
    sendData(F("AT+CIPSTART=\"TCP\",\"ESFIHA\",4444"), 2000);
    esp8266.print(F("AT+CIPSEND="));
    esp8266.print(len, DEC);
    esp8266.print(F("\r\n"));
    delay(2000);
    sendData(data, len, 0);
    waitResponse(4000, response, lenResponse);
    //sendData(F("+IPD"), 2000);
    //sendData(F("AT+CIPCLOSE"), 2000);
  }
  
  static void sendSale(void *responseDest) {
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
      (char)(gGlobals.gWallet.UID[0]),
      (char)(gGlobals.gWallet.UID[1]),
      (char)(gGlobals.gWallet.UID[2]),
      (char)(gGlobals.gWallet.UID[3]),
      1, // itemsCount
      (char)gGlobals.gCurProd.id,
      (char)(priceData.bytes[0]),
      (char)(priceData.bytes[1]),
      (char)(priceData.bytes[2]),
      (char)(priceData.bytes[3])
    };

    char responseData[7];
    sendMessageToServer(data, sizeof(data), responseData, sizeof(responseData));
    memcpy(responseDest, responseData, sizeof(responseData));
  }

  static void connectWifi() {
    sendData(F("AT+RST"), 2000);
    sendData(F("AT+CWMODE=1"), 2000);
    sendData(F("AT+CWJAP=\"Kibe\",\"86827012\""), 3000);
    sendData(F("AT+CIPMUX=0"), 3000);
  }

  static void sendMachineStartup() {
    char hiId = gGlobals.gMachineId >> 8;
    char loId = gGlobals.gMachineId & 0xFF;
    char data[3] = {
      0x03, // Intention
      hiId,
      loId
    };
    //START_OK
    char response[1];
    sendMessageToServer(data, sizeof(data), response, sizeof(response));
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

void ServerBridge::sale(SaleResponse &response) {
  sendSale(&response);
}

void ServerBridge::verifyProductsUpdate() {
  
}