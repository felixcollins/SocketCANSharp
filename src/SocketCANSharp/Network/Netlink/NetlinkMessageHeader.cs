#region License
/* 
BSD 3-Clause License

Copyright (c) 2022, Derek Will
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

using System.Runtime.InteropServices;

namespace SocketCANSharp.Network.Netlink
{
    /// <summary>
    /// Represents a Netlink Message Header.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NetlinkMessageHeader
    {
        /// <summary>
        /// Length of message including the header.
        /// </summary>
        public uint MessageLength { get; set; }
        /// <summary>
        /// Message Type.
        /// </summary>
        public NetlinkMessageType MessageType { get; set; }
        /// <summary>
        /// Additional Flags.
        /// </summary>
        public NetlinkMessageFlags Flags { get; set; }
        /// <summary>
        /// Sequence number.
        /// </summary>
        public uint SequenceNumber { get; set; }
        /// <summary>
        /// Sender port ID.
        /// </summary>
        public uint SenderPortId { get; set; }

        /// <summary>
        /// Converts raw byte array into NetlinkMessageHeader object.
        /// </summary>
        /// <param name="data">Raw Byte Array</param>
        /// <returns>NetlinkMessageHeader object</returns>
        public static NetlinkMessageHeader FromBytes(byte[] data)
        {
            return NetlinkUtils.FromBytes<NetlinkMessageHeader>(data);
        }
    }
}