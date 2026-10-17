using UnityEngine;

public class TicketsManager
{
   private int _countOfTickets;

   private const string COUNT_OF_TICKETS = "Count of tickets";

   
   public int GetTickets()
   {
      _countOfTickets = PlayerPrefs.GetInt(COUNT_OF_TICKETS);

      return _countOfTickets;
   }

   public void AddTickets(int tickets)
   {
      GetTickets();
      _countOfTickets += tickets;
      
      PlayerPrefs.SetInt(COUNT_OF_TICKETS, _countOfTickets);
   }

   public void RemoveTicket()
   {
      GetTickets();
      _countOfTickets--;
      
      if(_countOfTickets <= 0)
         PlayerPrefs.SetInt(COUNT_OF_TICKETS, 0);
      else
         PlayerPrefs.SetInt(COUNT_OF_TICKETS, _countOfTickets);
   }
}
