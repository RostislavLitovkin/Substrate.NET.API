//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Ajuna.NetApi.Model.SpCore;
using Ajuna.NetApi.Model.Types.Base;
using System;
using System.Collections.Generic;


namespace Ajuna.NetApi.Model.SpRuntime
{
    
    
    public enum MultiSignature
    {
        
        Ed25519,
        
        Sr25519,
        
        Ecdsa,
    }
    
    /// <summary>
    /// >> 215 - Variant[sp_runtime.MultiSignature]
    /// </summary>
    public sealed class EnumMultiSignature : BaseEnumExt<MultiSignature, Ajuna.NetApi.Model.SpCore.Signature, Ajuna.NetApi.Model.SpCore.Signature, Ajuna.NetApi.Model.SpCore.Signature>
    {
    }
}
