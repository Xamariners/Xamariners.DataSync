using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamariners.DataSync.Common.Infrastructure
{
    public static class BinarySerialiser
    {
        private static readonly object _lock = new object();

        public static void SerialiseObject<T>(string filePath, T objectToSerialize)
        {
            SerialiseObject<T>(filePath, objectToSerialize, null);
        }

        public static void SerialiseObject<T>(string filePath, T objectToSerialize, byte[] salt)
        {

        }

        public static byte[] SerialiseObject<T>(T objectToSerialize)
        {
            throw new NotImplementedException();
        }

        public static T DeSerializeObject<T>(string filePath) where T : class
        {
            return DeSerializeObject<T>(filePath, null);
        }

        public static T DeSerializeObject<T>(string filePath, byte[] salt) where T : class
        {
            throw new NotImplementedException();
        }

        public static T DeSerializeObject<T>(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public static object DeSerializeObject(byte[] bytes, Type type)
        {
            throw new NotImplementedException();
        }
    }
}