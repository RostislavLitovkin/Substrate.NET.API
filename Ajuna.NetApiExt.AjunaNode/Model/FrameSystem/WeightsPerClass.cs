//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Ajuna.NetApi.Model.Types.Base;
using Ajuna.NetApi.Model.Types.Primitive;
using System;
using System.Collections.Generic;


namespace Ajuna.NetApi.Model.FrameSystem
{
    
    
    /// <summary>
    /// >> 66 - Composite[frame_system.limits.WeightsPerClass]
    /// </summary>
    public sealed class WeightsPerClass : BaseType
    {
        
        /// <summary>
        /// >> base_extrinsic
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.U64 _baseExtrinsic;
        
        /// <summary>
        /// >> max_extrinsic
        /// </summary>
        private BaseOpt<Ajuna.NetApi.Model.Types.Primitive.U64> _maxExtrinsic;
        
        /// <summary>
        /// >> max_total
        /// </summary>
        private BaseOpt<Ajuna.NetApi.Model.Types.Primitive.U64> _maxTotal;
        
        /// <summary>
        /// >> reserved
        /// </summary>
        private BaseOpt<Ajuna.NetApi.Model.Types.Primitive.U64> _reserved;
        
        public Ajuna.NetApi.Model.Types.Primitive.U64 BaseExtrinsic
        {
            get
            {
                return this._baseExtrinsic;
            }
            set
            {
                this._baseExtrinsic = value;
            }
        }
        
        public BaseOpt<Ajuna.NetApi.Model.Types.Primitive.U64> MaxExtrinsic
        {
            get
            {
                return this._maxExtrinsic;
            }
            set
            {
                this._maxExtrinsic = value;
            }
        }
        
        public BaseOpt<Ajuna.NetApi.Model.Types.Primitive.U64> MaxTotal
        {
            get
            {
                return this._maxTotal;
            }
            set
            {
                this._maxTotal = value;
            }
        }
        
        public BaseOpt<Ajuna.NetApi.Model.Types.Primitive.U64> Reserved
        {
            get
            {
                return this._reserved;
            }
            set
            {
                this._reserved = value;
            }
        }
        
        public override string TypeName()
        {
            return "WeightsPerClass";
        }
        
        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(BaseExtrinsic.Encode());
            result.AddRange(MaxExtrinsic.Encode());
            result.AddRange(MaxTotal.Encode());
            result.AddRange(Reserved.Encode());
            return result.ToArray();
        }
        
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            BaseExtrinsic = new Ajuna.NetApi.Model.Types.Primitive.U64();
            BaseExtrinsic.Decode(byteArray, ref p);
            MaxExtrinsic = new BaseOpt<Ajuna.NetApi.Model.Types.Primitive.U64>();
            MaxExtrinsic.Decode(byteArray, ref p);
            MaxTotal = new BaseOpt<Ajuna.NetApi.Model.Types.Primitive.U64>();
            MaxTotal.Decode(byteArray, ref p);
            Reserved = new BaseOpt<Ajuna.NetApi.Model.Types.Primitive.U64>();
            Reserved.Decode(byteArray, ref p);
            TypeSize = p - start;
        }
    }
}
