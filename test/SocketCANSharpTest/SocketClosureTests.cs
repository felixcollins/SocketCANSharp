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

using NUnit.Framework;
using SocketCANSharp;
using System.Net.Sockets;

namespace SocketCANSharpTest
{
    public class SocketClosureTests
    {
        SafeSocketHandle socketHandle;

        [TearDown]
        public void Cleanup()
        {
            socketHandle.Close();
        }

        [Test]
        public void CloseSocket_CAN_RAW_Test()
        {
            socketHandle = LibcNativeMethods.Socket(SocketCanConstants.PF_CAN, SocketType.Raw, SocketCanProtocolType.CAN_RAW);
            Assert.IsFalse(socketHandle.IsInvalid);
            Assert.IsFalse(socketHandle.IsClosed);
            socketHandle.Close();
            Assert.IsTrue(socketHandle.IsInvalid);
            Assert.IsTrue(socketHandle.IsClosed);
        }

        [Test]
        public void CloseSocket_CAN_ISOTP_Test()
        {
            socketHandle = LibcNativeMethods.Socket(SocketCanConstants.PF_CAN, SocketType.Dgram, SocketCanProtocolType.CAN_ISOTP);
            Assert.IsFalse(socketHandle.IsInvalid);
            Assert.IsFalse(socketHandle.IsClosed);
            socketHandle.Close();
            Assert.IsTrue(socketHandle.IsInvalid);
            Assert.IsTrue(socketHandle.IsClosed);
        }

        [Test]
        public void CloseSocket_CAN_J1939_Test()
        {
            socketHandle = LibcNativeMethods.Socket(SocketCanConstants.PF_CAN, SocketType.Dgram, SocketCanProtocolType.CAN_J1939);
            Assert.IsFalse(socketHandle.IsInvalid);
            Assert.IsFalse(socketHandle.IsClosed);
            socketHandle.Close();
            Assert.IsTrue(socketHandle.IsInvalid);
            Assert.IsTrue(socketHandle.IsClosed);
        }

        [Test]
        public void CloseSocket_CAN_BCM_Test()
        {
            socketHandle = LibcNativeMethods.Socket(SocketCanConstants.PF_CAN, SocketType.Dgram, SocketCanProtocolType.CAN_BCM);
            Assert.IsFalse(socketHandle.IsInvalid);
            Assert.IsFalse(socketHandle.IsClosed);
            socketHandle.Close();
            Assert.IsTrue(socketHandle.IsInvalid);
            Assert.IsTrue(socketHandle.IsClosed);
        }
    }
}