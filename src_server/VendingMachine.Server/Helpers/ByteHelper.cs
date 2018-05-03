using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Infrastructure.Helpers
{
    internal static class ByteHelper
    {
        public static short GetMachineId(ReadOnlySpan<byte> bytes, int startByte)
        {
            return (short)
                ((bytes[startByte] << 8) + (bytes[startByte + 1]));
        }

        public static IPAddress GetMachineAccessPointIP(ReadOnlySpan<byte> bytes)
        {
            var apip = "";
            foreach (char b in bytes)
            {
                if (b == '"') break;
                apip += b;
            }

            return IPAddress.Parse(apip);
        }

        public static long GetMifareId(ReadOnlySpan<byte> bytes, int startByte)
        {
            // A representação do  ID conforme o protocolo MifareID
            // é dada em bytes. 
            // No banco de dados,  estaremos armazenando isto  como
            // um tipo numérico. Não  queremos  nos  preocupar  com
            // sinal de números.
            uint clientId =
                (uint)(bytes[startByte + 0] << (8 * 3)) +
                (uint)(bytes[startByte + 1] << (8 * 2)) +
                (uint)(bytes[startByte + 2] << (8 * 1)) +
                (uint)(bytes[startByte + 3] << (8 * 0));

            // De qualquer maneira, o tipo uint não é cls-compliant,
            // vamos limitar seu uso em funções internas e não expor
            // este tipo publicamente.
            long longClientId = clientId;

            return longClientId;
        }

        public static string ByteArrayToString(byte[] bytes) =>
            BitConverter.ToString(bytes);

    }
}
