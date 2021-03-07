using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHA_2
{





    class Algorithm
    {
        public struct Digest { 
        
            public uint H0, H1, H2, H3, H4, H5, H6, H7;
        }


        public Digest digest;
        
        
        private  byte zeroByte = 0;
        private readonly uint[] k = new uint[] {
        0x428a2f98, 0x71374491, 0xb5c0fbcf,0xe9b5dba5,0x3956c25b,0x59f111f1,0x923f82a4,0xab1c5ed5,
        0xd807aa98, 0x12835b01, 0x243185be,0x550c7dc3,0x72be5d74,0x80deb1fe,0x9bdc06a7,0xc19bf174,
        0xe49b69c1, 0xefbe4786, 0x0fc19dc6,0x240ca1cc,0x2de92c6f,0x4a7484aa,0x5cb0a9dc,0x76f988da,
        0x983e5152, 0xa831c66d, 0xb00327c8,0xbf597fc7,0xc6e00bf3,0xd5a79147,0x06ca6351,0x14292967,
        0x27b70a85, 0x2e1b2138, 0x4d2c6dfc,0x53380d13,0x650a7354,0x766a0abb,0x81c2c92e,0x92722c85,
        0xa2bfe8a1, 0xa81a664b, 0xc24b8b70,0xc76c51a3,0xd192e819,0xd6990624,0xf40e3585,0x106aa070,
        0x19a4c116, 0x1e376c08, 0x2748774c,0x34b0bcb5,0x391c0cb3,0x4ed8aa4a,0x5b9cca4f,0x682e6ff3,
        0x748f82ee, 0x78a5636f, 0x84c87814,0x8cc70208,0x90befffa,0xa4506ceb,0xbef9a3f7,0xc67178f2,};



        private void initHs()
        {
            /* SHA-256 initial hash value
            * The first 32 bits of the fractional parts of the square roots
            * of the first eight prime numbers
            */
            digest.H0 = 0x6a09e667;
            digest.H1 = 0xbb67ae85;
            digest.H2 = 0x3c6ef372;
            digest.H3 = 0xa54ff53a;
            digest.H4 = 0x510e527f;
            digest.H5 = 0x9b05688c;
            digest.H6 = 0x1f83d9ab;
            digest.H7 = 0x5be0cd19;
        }



        public void Start(byte[] inputSequnce, long size)
        {
            initHs();
            /*List<byte[]> binarySequence = GetBinaryCode(inputSequnce, size);*/
            var convertedSequence = GetWorlds(GetBinaryCode(inputSequnce, size));

        }



        private List<byte[]> GetBinaryCode(byte[] array, long size)
        {
            List<byte[]> result = new List<byte[]>();
            
            for(long i = 0; i < size / 64; i++)
            {
                result.Add(new byte[64]);
            }
            for(int i = 0; i < result.Count; i++)
            {

                for(int j = 0; j < 64; j++)
                {
                    result[i][j] = array[i * 64 + j];
                }
            }
            result.Add(new byte[512]);
            for (int j = 0; j < 64; j++)
            {
                byte temp = (j > size % 64) ? zeroByte : array[size - size % 64 + j];
                result.Last()[j] = temp;
            }
            
            if (size % 64 > 55)
            {             
                result.Add(new byte[512]);
            }
            result.Last()[(size % 64)] = 128;
            byte[] arrayForSize = new byte[8];
            long tempSize = size;
            for(int i = 0; i < 8; i++)
            {
                byte partOfSize = 0;
                for(int j = 0; j < 8; j++)
                {
                    partOfSize +=(byte)(Math.Pow(2,j)*(tempSize / 2));
                    tempSize /= 2;
                }
                arrayForSize[7-i] = partOfSize;
                if (tempSize == 0)
                {
                    break;
                }
            }
            for(int i = 56; i < 64; i++)
            {
                result.Last()[i] = arrayForSize[7 - i];
            }
            return result;
        }


        private List<uint[]> GetWorlds(List<byte[]> inputSequence)
        {
            List<uint[]> result = new List<uint[]>(inputSequence.Count);
            foreach(var elem in inputSequence)
            {
                result.Add(ConvertToUint32(elem));
            }
            return result;
        } 


        private uint[] ConvertToUint32(byte[] input)
        {
            uint[] result = new uint[64];
            for(int i=0; i < 16; i++)
            {
                uint temp = 0;
                for(int j = 0; j < 4; j++)
                {
                    temp |= (uint)(input[i * 4 + j] << (3 - j)*8);
                }
            }
            return result;
        }



    }
}
