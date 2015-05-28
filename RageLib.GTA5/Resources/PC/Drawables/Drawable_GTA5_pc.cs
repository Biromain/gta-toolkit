/*
    Copyright(c) 2015 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using RageLib.Resources.Common;
using RageLib.Resources.GTA5.PC.Bounds;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    public class Drawable_GTA5_pc : DrawableBase_GTA5_pc
    {
        public override long Length
        {
            get { return 208; }
        }

        // structure data
        public ulong LightAttributesPointer;
        public ushort LightAttributesCount1;
        public ushort LightAttributesCount2;
        public uint Unknown_BCh;
        public uint Unknown_C0h;
        public uint Unknown_C4h;
        public ulong BoundsPointer;

        // reference data
        public ResourceSimpleArray<LightAttributes_GTA5_pc> LightAttributes;
        public Bound_GTA5_pc Bounds;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.LightAttributesPointer = reader.ReadUInt64();
            this.LightAttributesCount1 = reader.ReadUInt16();
            this.LightAttributesCount2 = reader.ReadUInt16();
            this.Unknown_BCh = reader.ReadUInt32();
            this.Unknown_C0h = reader.ReadUInt32();
            this.Unknown_C4h = reader.ReadUInt32();
            this.BoundsPointer = reader.ReadUInt64();

            // read reference data
            this.LightAttributes = reader.ReadBlockAt<ResourceSimpleArray<LightAttributes_GTA5_pc>>(
                this.LightAttributesPointer, // offset
                this.LightAttributesCount1
            );
            this.Bounds = reader.ReadBlockAt<Bound_GTA5_pc>(
                this.BoundsPointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.LightAttributesPointer = (ulong)(this.LightAttributes != null ? this.LightAttributes.Position : 0);
            this.BoundsPointer = (ulong)(this.Bounds != null ? this.Bounds.Position : 0);

            // write structure data
            writer.Write(this.LightAttributesPointer);
            writer.Write(this.LightAttributesCount1);
            writer.Write(this.LightAttributesCount2);
            writer.Write(this.Unknown_BCh);
            writer.Write(this.Unknown_C0h);
            writer.Write(this.Unknown_C4h);
            writer.Write(this.BoundsPointer);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (LightAttributes != null) list.Add(LightAttributes);
            if (Bounds != null) list.Add(Bounds);
            return list.ToArray();
        }

    }
}