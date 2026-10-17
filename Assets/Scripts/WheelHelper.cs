using UnityEngine;

public class WheelHelper : MonoBehaviour
{
   public void GetNewTicket()
   {
      var whe = new TicketsManager();
      whe.AddTickets(1);
      //Debug.Log(whe.GetTickets());
   }
   
   public void DeleteAllTickets()
   {
      PlayerPrefs.SetInt("Count of tickets", 0);
      var whe = new TicketsManager();
      Debug.Log(whe.GetTickets());
   }
}
