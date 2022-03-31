//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Ajuna.NetApi.Model.SpFinalityGrandpa;
using Ajuna.NetApi.Model.Types.Base;
using Ajuna.NetApi.Model.Types.Primitive;
using System;
using System.Collections.Generic;


namespace Ajuna.NetApi.Model.SpFinalityGrandpa
{
    
    
    /// <summary>
    /// >> 90 - Composite[sp_finality_grandpa.EquivocationProof]
    /// </summary>
    public sealed class EquivocationProof : BaseType
    {
        
        /// <summary>
        /// >> set_id
        /// </summary>
        private Ajuna.NetApi.Model.Types.Primitive.U64 _setId;
        
        /// <summary>
        /// >> equivocation
        /// </summary>
        private Ajuna.NetApi.Model.SpFinalityGrandpa.EnumEquivocation _equivocation;
        
        public Ajuna.NetApi.Model.Types.Primitive.U64 SetId
        {
            get
            {
                return this._setId;
            }
            set
            {
                this._setId = value;
            }
        }
        
        public Ajuna.NetApi.Model.SpFinalityGrandpa.EnumEquivocation Equivocation
        {
            get
            {
                return this._equivocation;
            }
            set
            {
                this._equivocation = value;
            }
        }
        
        public override string TypeName()
        {
            return "EquivocationProof";
        }
        
        public override byte[] Encode()
        {
            var result = new List<byte>();
            result.AddRange(SetId.Encode());
            result.AddRange(Equivocation.Encode());
            return result.ToArray();
        }
        
        public override void Decode(byte[] byteArray, ref int p)
        {
            var start = p;
            SetId = new Ajuna.NetApi.Model.Types.Primitive.U64();
            SetId.Decode(byteArray, ref p);
            Equivocation = new Ajuna.NetApi.Model.SpFinalityGrandpa.EnumEquivocation();
            Equivocation.Decode(byteArray, ref p);
            TypeSize = p - start;
        }
    }
}
