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
      playerVAKModel.VValue += obj.V;
      playerVAKModel.AValue += obj.A;
      playerVAKModel.KValue += obj.K;
      
      Debug.Log("V Delta: " + obj.V);
      Debug.Log("V: " + playerVAKModel.VValue);
      
      Debug.Log("A Delta: " + obj.A);
      Debug.Log("A: " + playerVAKModel.AValue);

      Debug.Log("K Delta: " + obj.K);
      Debug.Log("K: " + playerVAKModel.KValue);
   }
}
