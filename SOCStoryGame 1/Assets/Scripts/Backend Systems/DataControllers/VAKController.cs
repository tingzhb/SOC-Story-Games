using UnityEngine;

public class VAKController : MonoBehaviour{
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
   }
}
