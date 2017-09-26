using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NerdDinner.Models
{
    public class DinnerRepository
    {
        private NerdDinnerDataContext db = new NerdDinnerDataContext();


        //Query Methods

        public IQueryable<Dinner> FindAllDinners()
        {
            return db.Dinners;
        }


        // Retrieves all dinners after the the current date & time
        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return db.Dinners
                .Where(dinner => dinner.EventDate > DateTime.Now)
                .OrderBy(dinner => dinner.EventDate);
        }


        //retrieves a specific dinner based on its DinnerID
        public Dinner GetDinner(int id)
        {
            return db.Dinners.SingleOrDefault(d => d.DinnerID == id);
        }

        // insert and delete

        //Creates a dinner
        public void Add(Dinner dinner)
        {
            db.Dinners.InsertOnSubmit(dinner);
        }

        //Deletes a dinner and all associated RSVPs
        public void Delete(Dinner dinner)
        {
            db.Dinners.DeleteOnSubmit(dinner);
            db.Rsvps.DeleteAllOnSubmit(dinner.Rsvps);
        }

        //Persistance

        public void Save()
        {
            db.SubmitChanges();
        }
    }
}