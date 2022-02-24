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
using System.Runtime.InteropServices;

namespace SocketCANSharp
{
    /// <summary>
    /// A shorthand trivial (single CAN FD frame) Broadcast Manager Message to use when writing to a CAN_BCM socket with a TX_SEND opcode message.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class BcmCanFdSingleMessage
    {
        /// <summary>
        /// Broadcast Manager Message Header.
        /// </summary>
        public BcmMessageHeader Header { get; set; }

        /// <summary>
        /// CAN FD frame to send.
        /// </summary>
        public CanFdFrame Frame { get; set; }

        /// <summary>
        /// Instantiates a new Broadcast Manager Message using the supplied header and CAN FD frame.
        /// </summary>
        /// <param name="header">Message Header</param>
        /// <param name="frame">CAN FD Frame</param>
        public BcmCanFdSingleMessage(BcmMessageHeader header, CanFdFrame frame)
        {
            if (header == null)
                throw new ArgumentNullException(nameof(header), "Header cannot be null");

            Header = header;
            header.Flags |= BcmFlags.CAN_FD_FRAME;
            Frame = frame;
        }
    }
}