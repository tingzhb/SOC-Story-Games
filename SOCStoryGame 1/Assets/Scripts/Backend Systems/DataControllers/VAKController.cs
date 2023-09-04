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

      var vValue = playerVAKModel.VValue;
      var aValue = playerVAKModel.AValue;
      var kValue = playerVAKModel.KValue;
      
      playerVAKModel.VAKTotal = playerVAKModel.VValue + playerVAKModel.AValue + playerVAKModel.KValue;
      
      var total = playerVAKModel.VAKTotal;

      playerVAKModel.VPercentage = vValue / total * 100;
      playerVAKModel.APercentage = aValue / total * 100;
      playerVAKModel.KPercentage = aValue / total * 100;
   }
}
