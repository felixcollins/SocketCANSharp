#region License
/* 
BSD 3-Clause License

Copyright (c) 2021, Derek Will
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.

3. Neither the name of the copyright holder nor the names of its
   contributors may be used to endorse or promote products derived from
   this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
*/
#endregion

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using SocketCANSharp;
using SocketCANSharp.Network;

namespace CanBusSniffer
{
    /// <summary>
    /// Really basic CAN bus sniffer to show how to use SocketCAN# for RAW CAN.
    /// </summary>
    class Program
    {
        private static readonly BlockingCollection<CanFrame> concurrentQueue = new BlockingCollection<CanFrame>();

		//private static SafeFileDescriptorHandle socketHandle;
		private static RawCanSocket socket;

		// Replace this string with the if you want to use.
		// $ifconfig to see what it is called - you may need to configure interfaces for your platform first
		// vcan0 would be another likely option
		const string CanIfToSniff = "can0";

        static void Main(string[] args)
        {
			//////////////////////////////////////////////////////
			/// NB: Code converted from using the native calls to using the RawCanSocket class
			/// I've left the original interop calls as comments
			//////////////////////////////////////////////////////
			///
			//socketHandle = LibcNativeMethods.Socket(SocketCanConstants.PF_CAN, SocketType.Raw, SocketCanProtocolType.CAN_RAW);
			socket = new RawCanSocket();

			//using (socketHandle)
			using (socket)
            {
                //if (socketHandle.IsInvalid)
				if(socket.SafeHandle.IsInvalid)
                {
                    Console.WriteLine("Failed to create socket.");
                    return;
                }

				//var ifr = new Ifreq(CanIfToSniff);

				//            //int ioctlResult = LibcNativeMethods.Ioctl(socketHandle, SocketCanConstants.SIOCGIFINDEX, ifr);
				//int ioctlResult = LibcNativeMethods.Ioctl(socket.SafeHandle, SocketCanConstants.SIOCGIFINDEX, ifr);
				//if (ioctlResult == -1)
				//            {
				//                Console.WriteLine("Failed to look up interface by name.");
				//                return;
				//            }

				//				var addr = new SockAddrCan(ifr.IfIndex);
				//socket.Bind(addr);

				var iface = CanNetworkInterface.GetInterfaceByName(socket.SafeHandle, CanIfToSniff);
				socket.Bind(iface);

				//int bindResult = LibcNativeMethods.Bind(socketHandle, addr, Marshal.SizeOf(typeof(SockAddrCan)));
				//            if (bindResult == -1)
				//            {
				//                Console.WriteLine("Failed to bind to address.");
				//                return;
				//            }

				//int frameSize = Marshal.SizeOf(typeof(CanFrame));
                Console.WriteLine($"Sniffing {CanIfToSniff}...");
                Task.Run(() => PrintLoop());
                while (true)
                {
                    var readFrame = new CanFrame();
					int nReadBytes = socket.Read(out readFrame);
                    //int nReadBytes = LibcNativeMethods.Read(socketHandle, ref readFrame, frameSize); 
                    if (nReadBytes > 0)
                    {
                        concurrentQueue.Add(readFrame);
                    }
                }
            }
        }

        private static void PrintLoop()
        {
            while (true)
            {
                CanFrame readFrame;
				if (concurrentQueue.TryTake(out readFrame, 500))
				{
					if ((readFrame.CanId & (uint)CanIdFlags.CAN_RTR_FLAG) != 0)
					{
						Console.WriteLine($"{SocketCanConstants.CAN_ERR_MASK & readFrame.CanId,8:X}   [{readFrame.Length:D2}]  RTR");
					}
					else
					{
						Console.WriteLine($"{SocketCanConstants.CAN_ERR_MASK & readFrame.CanId,8:X}   [{readFrame.Length:D2}]  {BitConverter.ToString(readFrame.Data.Take(readFrame.Length).ToArray()).Replace("-", " ")}");
					}
				}

				// Some extra stuff to allow testing sending frames
				// if you use candump or otherwise have some means of monitoring
				// the bus this is useful to prove that you can send.
				// This could also be easily adapted to make a test program for talking
				// to simple CAN devices
				if (Console.KeyAvailable)
				{
					var character = Console.Read();

					Console.WriteLine("Sending Some Data");
					byte[] data = new byte[] { 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f };
					CanFrame frame = new CanFrame(1,[1,2,3,4,5,6,7,8]);
					int writeResult = socket.Write(frame);
					//int writeResult = LibcNativeMethods.Write(socketHandle, data, data.Length);

					if (writeResult == -1)
					{
						Console.WriteLine($"Write returned {LibcNativeMethods.Errno}");
					}
					else
					{
						Console.WriteLine($"Wrote {writeResult} bytes");
					}
				}
			}
        }
    }
}
