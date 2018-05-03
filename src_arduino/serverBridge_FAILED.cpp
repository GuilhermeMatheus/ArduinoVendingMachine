// #include "server/serverBridge.h"
// #include <Arduino.h>
// #include <SoftwareSerial.h>
// #include "globals.h"
// #include "hardware/helpers.h"
// 
// #define CON_WIFI_SSID "Kibe"
// #define CON_WIFI_PSWD "86827012"
// 
// #define SOFTWARE_SERIAL_RX 8
// #define SOFTWARE_SERIAL_TX A0
// 
// #define READ_DELAY 100
// 
// #define IDX_IPD_BEGIN 4
// 
// #define DEBUG
// 
// namespace {
//   static SoftwareSerial esp8266(SOFTWARE_SERIAL_RX, SOFTWARE_SERIAL_TX);
//   static const char *IPD = "+IPD,";
//   static bool inServerMode = false;
// 
//   static void waitAndIgnoreResponse(const int timeout) {
//     long int time = millis();
// 
// #ifdef DEBUG
//     String debugResponse = "";
// #endif
// 
// 
//     while ((time + timeout) > millis())
//     while (esp8266.available()) {
//       delay(READ_DELAY);
//       if(esp8266.find("OK"))
//         goto end_loop;
//       uint8_t curByte = esp8266.read();
// 
// #ifdef DEBUG
//       debugResponse += (char)curByte;
// #endif
//     }
// 
// end_loop:
// 
// #ifdef DEBUG
//     Serial.print(debugResponse);
//     Serial.print(F("\r\n"));
// #endif
//   }
// 
//   static void waitResponse(const int timeout, char *response, uint8_t lenResponse) {
//     long int time = millis();
//     
//     uint8_t idx_ipd = 0;
//     uint8_t idx_response = 0;
//     bool serverMessageStarted = false;
// 
// #ifdef DEBUG
//     String debugResponse = "";
// #endif
// 
// 
//     while ((time + timeout) > millis())
//     while (esp8266.available()) {
//       uint8_t curByte = esp8266.read();
// 
// #ifdef DEBUG
//       debugResponse += (char)curByte;
// #endif
// 
//       // Waiting for "+IDP,"
//       // Writed when Gui doesnt knows that Stream base class has a Find method :P
//       if(curByte == IPD[idx_ipd] && idx_ipd != IDX_IPD_BEGIN) {
//         idx_ipd++;
//       } else if (idx_ipd == IDX_IPD_BEGIN) {
//         if(serverMessageStarted && idx_response < lenResponse) {
//           response[idx_response] = (char)curByte;
//           idx_response++;
// #ifdef DEBUG
//           Serial.print(curByte, HEX); Serial.print('-');
// #endif
//         }
//         // Waiting for "+IDP,NN:"
//         serverMessageStarted = serverMessageStarted || (!serverMessageStarted && curByte == ':');
//       } else {
//         idx_ipd = 0;
//       }
//     }
//     
// #ifdef DEBUG
//     Serial.print(debugResponse);
//     if(serverMessageStarted)
//       Serial.print(F("\r\n"));
// #endif
//   }
// 
//   static void sendData_impl(const __FlashStringHelper *ifsh) {
//     esp8266.print(ifsh);
// #ifdef DEBUG
//     Serial.print(F("\r\nSending:\r\n    "));
//     Serial.print(ifsh);
// #endif
//   }
// 
//   static void sendData_impl(unsigned long n, int base) {
//     esp8266.print(n, base);
// #ifdef DEBUG
//     Serial.print(F("\r\nSending:\r\n    "));
//     Serial.print(n, base);
// #endif
//   }
//   static void sendData_impl(char c) {
//     esp8266.print(c);
// #ifdef DEBUG
//     Serial.print(F("\r\nSending:\r\n    "));
//     Serial.print(c);
// #endif
//   }
// 
//   static void sendData(const __FlashStringHelper *ifsh, const int timeout) {
//     sendData_impl(ifsh);
//     sendData_impl(F("\r\n"));
//     waitAndIgnoreResponse(timeout);
//   }
// 
//   static void sendData(const char *data, uint8_t len, const int timeout) {
//     for(uint8_t i = 0; i < len; i++)
//       sendData_impl(data[i]);
// 
//     sendData_impl(F("\r\n"));
//     waitAndIgnoreResponse(timeout);
//   }
// 
//   static void connectWifi() {
//     sendData(F("AT+RST"), 2000);
//     sendData(F("AT+CWJAP=\"" CON_WIFI_SSID "\",\"" CON_WIFI_PSWD "\""), 5000);
//     sendData(F("AT+CWMODE=3"), 2000);
//     sendData(F("AT+CIPMUX=0"), 5000);
//   }
// 
//   static void setServerMode_impl(bool value) {
//     if(inServerMode == value)
//       return;
//     
//     if(value) {
//       sendData(F("AT+CIPMUX=1"), 5000);
//       sendData(F("AT+CIPSERVERMAXCONN=1"), 4000);
//       sendData(F("AT+CIPSERVER=1,879"), 4000);
//       sendData(F("AT+CIPSTO=2000"), 4000);
//     } else {
//       sendData(F("AT+CIPSERVER=0"), 4000);
//       connectWifi();
//     }
// 
//     inServerMode = value;
//   }
// 
//   static void sendMessageToServer(const char *data, uint8_t len, char *response, uint8_t lenResponse) {
//     setServerMode_impl(false);
//     sendData(F("AT+CIPMUX=0"), 5000);
// 
//     sendData(F("AT+CIPSTART=\"TCP\",\"ESFIHA\",4444"), 2000);
//     sendData_impl(F("AT+CIPSEND="));
//     sendData_impl(len, DEC);
//     sendData_impl(F("\r\n"));
//     waitAndIgnoreResponse(2000);
//     sendData(data, len, 0);
//     waitResponse(4000, response, lenResponse);
// 
//     sendData(F("AT+CIPMUX=1"), 5000);
//   }
//   
//   static void sendSale(void *responseDest) {
//     char hiId = gGlobals.gMachineId >> 8;
//     char loId = gGlobals.gMachineId & 0xFF;
// 
//     union {
//       float price;
//       char bytes[sizeof(float)];
//     } priceData; 
// 
//     priceData.price = gGlobals.gCurProd.price;
// 
//     char data[13] = {
//       0x0, // Intention
//       hiId,
//       loId,
//       (char)(gGlobals.gWallet.UID[0]),
//       (char)(gGlobals.gWallet.UID[1]),
//       (char)(gGlobals.gWallet.UID[2]),
//       (char)(gGlobals.gWallet.UID[3]),
//       1, // itemsCount
//       (char)gGlobals.gCurProd.id,
//       (char)(priceData.bytes[0]),
//       (char)(priceData.bytes[1]),
//       (char)(priceData.bytes[2]),
//       (char)(priceData.bytes[3])
//     };
// 
//     char responseData[7];
//     sendMessageToServer(data, sizeof(data), responseData, sizeof(responseData));
//     memcpy(responseDest, responseData, sizeof(responseData));
//   }
// 
//   static void waitResponseFromCIFSR(const int timeout, char *response, uint8_t lenResponse) {
// #ifdef DEBUG
//     String debugResponse = "";
// #endif
//     long int time = millis();
//     while ((time + timeout) > millis())
//     while (esp8266.available()) {
//       if (esp8266.find("+CIFSR:APIP,\"")) {
//         char c;
//         uint8_t idx = 0;
//         while( idx < lenResponse) {
//           delay(READ_DELAY);
//           c = esp8266.read();
//           response[idx] = c;
//           debugResponse += c;
//           idx++;
//         }
//       } else {
//         esp8266.read();
//       }
//     }
// #ifdef DEBUG
//     Serial.print(debugResponse);
//     Serial.print(F("\r\n"));
// #endif
//   }
// 
//   static void getAccessPointIp(char *ip) {
//     sendData_impl(F("AT+CIFSR"));
//     sendData_impl(F("\r\n"));
//     waitResponseFromCIFSR(3000, ip, 17);
//   }
// 
// 
//   static void sendMachineStartup() {
//     //"192.168.192.192"
//     //"192.168.4.1"----
//     char stationIp[17];
//     getAccessPointIp(stationIp);
// 
//     char hiId = gGlobals.gMachineId >> 8;
//     char loId = gGlobals.gMachineId & 0xFF;
//     char data[20] = {
//       0x03, // Intention
//       hiId,
//       loId
//     };
// 
//     memcpy(&data[3], stationIp, sizeof(stationIp));
// 
//     //START_OK
//     char response[1];
//     sendMessageToServer(data, sizeof(data), response, sizeof(response));
//   }
// }
// 
// ServerBridge::ServerBridge() {
// };
// 
// void ServerBridge::begin() {
//   Helpers::lcdWrite(1, 0, F("Iniciando rede"));
//   Helpers::lcdWrite(1, 1, F("ESP8266"));
//   esp8266.begin(19200);
//   connectWifi();
//   Helpers::lcdWrite(1, 1, F("sendMachineStartup"));
//   sendMachineStartup();
//   setServerMode();
// }
// 
// void ServerBridge::sale(SaleResponse &response) {
//   sendSale(&response);
// }
// 
// void ServerBridge::setServerMode() {
//   setServerMode_impl(true);
// }
// 
// #define MAX_UPDATE_LEN (2+(27*16))
// 
// bool ServerBridge::verifyProductsUpdate(uint16_t timeout, void (*f_handler)(char *)) {
//   long int time = millis();
//   uint16_t idx_data = 0;
//   char productData[MAX_UPDATE_LEN];
// 
//   while ((time + timeout) > millis() && idx_data < MAX_UPDATE_LEN) {
//     if (esp8266.available()) {
//       timeout = 16000;
//       //if (esp8266.find("+IPD,")) {
//         //uint8_t connectionId = esp8266.read() - 48;
//       {
//         while(esp8266.available() && idx_data < MAX_UPDATE_LEN) {
//           char c = esp8266.read();
//           Serial.print(c);
//           //Serial.print('-');
//           //productData[idx_data] = c;
//           idx_data++;
//         }
// 
// #ifdef DEBUG
//         /*for(uint16_t i = 0; i < MAX_UPDATE_LEN; i++) {
//           Serial.print((uint8_t)productData[i], HEX);
//           Serial.print('-');
//         }*/
// #endif
//       }
//     }
//   }
// #ifdef DEBUG
//   Serial.print(".\n");
// #endif
// }