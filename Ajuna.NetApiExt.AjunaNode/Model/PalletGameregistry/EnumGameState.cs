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


namespace Ajuna.NetApi.Model.PalletGameregistry
{
    
    
    public enum GameState
    {
        
        Waiting,
        
        Accepted,
        
        Running,
        
        Finished,
    }
    
    /// <summary>
    /// >> 207 - Variant[pallet_ajuna_gameregistry.game.GameState]
    /// </summary>
    public sealed class EnumGameState : BaseEnumExt<GameState, BaseVoid, BaseVoid, BaseVoid, Ajuna.NetApi.Model.SpCore.AccountId32>
    {
    }
}
