using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using MainProject.Models;
using System.Linq;

namespace MainProject.Pages
{
    public class EventViewModel : PageModel
    {
        public class EventInfo
        {
           public  string name{ get; set; }
            public string EventType { get; set; }
          
            public DateTime EventStart { get; set; }
           
            public DateTime EventEnd { get; set; }
        }
        public List<Event> eventList { get; set; }
        public int eventViewId{ get; set; }

        public int idEvent;

       public  List<Event> q { get; set; }

        private readonly Context db;
        public EventViewModel(Context db)
        {
            this.db = db;
            eventList = new();
        }

        
        public void OnGet( string id )
        {
            idEvent = Convert.ToInt32(id);
            try
            {
                //var q ;
                // List<ustomer> q;
              //  List< Event> q;
                if (idEvent == 1)
                {
                     q = db.Events
              .Where(c => c.EventType == "Conference").ToList();
              
                 //   eventList.Add(q);


             //       var w = db.Events
             //.Where(c => c.EventType == "Conference")
             //.AsEnumerable()
             //.Select(c => c.EventStart).ToString();
                   
             //       eventList.Add(w);


             //       var v = db.Events
             //.Where(c => c.EventType == "Conference")
             //.AsEnumerable()
             //.Select(c => c.EventEnd).ToString();
                    
             //       eventList.Add(v);
             //
             }
                else if (idEvent == 2)
                {
                     q = db.Events
                                 .Where(c => c.EventType == "Wedding").ToList();

                }


                else if (idEvent == 3) 
                {

                     q = db.Events
                .Where(c => c.EventType == "Concert").ToList();

                }
                 else
                    {
                         q = db.Events
                .Where(c => c.EventType == "Concert"|| c.EventType == "Wedding" || c.EventType == "Conference").ToList();

                    }



            }
                //string connectionString ="Data Source=C:\\Users\\Jessica\\HarmoniStay\\HarmoniStay.db";
                //using (SqlConnection connection=new SqlConnection(connectionString))
                //{
                //    string sql;
                //    connection.Open();
                //    if (idEvent == 1)
                //    {
                //         sql= "SELECT EventID,EventName,EventStart,EventEnd WHERE EventType=\"Conference\"";
                //    }
                //    else if(idEvent == 2)
                //    {
                //        sql = "SELECT EventID,EventName,EventStart,EventEnd WHERE EventType=\"Wedding\"";
                //    }


            //      else  {
            //         sql  = "SELECT EventID,EventName,EventStart,EventEnd WHERE EventType=\"Concert\"";
            //        }



            //    using (SqlCommand command = new SqlCommand(sql, connection)) {

            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                Event eventInfo = new Event();
            //                eventInfo.EventID = reader.GetInt32(0);
            //                eventInfo.EventName = reader.GetString(1);
            //                eventInfo.EventStart=  reader.GetDateTime(2);
            //                eventInfo.EventEnd = reader.GetDateTime(3);

            //                listEvent.Add(eventInfo);
            //            }
            //        }
            //    }
            //}
            catch (Exception ex) { 
             
            }
        }
    }
    
}
