//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Ajuna.NetApi.Model.Base;
using Ajuna.NetApi.Model.SpCore;
using Ajuna.NetApi.Model.Types.Base;
using Ajuna.NetApi.Model.Types.Primitive;
using System;
using System.Collections.Generic;


namespace Ajuna.NetApi.Model.SpRuntime
{
    
    
    public enum MultiAddress
    {
        
        Id,
        
        Index,
        
        Raw,
        
        Address32,
        
        Address20,
    }
    
    /// <summary>
    /// >> 112 - Variant[sp_runtime.multiaddress.MultiAddress]
    /// </summary>
    public sealed class EnumMultiAddress : BaseEnumExt<MultiAddress, Ajuna.NetApi.Model.SpCore.AccountId32, BaseCom<BaseTuple>, BaseVec<Ajuna.NetApi.Model.Types.Primitive.U8>, Ajuna.NetApi.Model.Base.Arr32U8, Ajuna.NetApi.Model.Base.Arr20U8>
    {
    }
}
