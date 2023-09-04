using System;
using UnityEngine;
using UnityEngine.Serialization;

public class VAKController : MonoBehaviour{
   [FormerlySerializedAs("vakModel")]
   [SerializeField] VAKModel playerVAKModel;

   void OnEnable(){
      Broker.Subscribe<VAKMessage>(OnVAKMessageReceived);
   }

   void OnDisable(){
      Broker.Unsubscribe<VAKMessage>(OnVAKMessageReceived);
   }
   
   void OnVAKMessageReceived(VAKMessage obj){
      playerVAKModel.VValue += obj.VAKModel.VValue;
      playerVAKModel.AValue += obj.VAKModel.AValue;
      playerVAKModel.KValue += obj.VAKModel.KValue;
   }
}
