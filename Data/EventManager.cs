using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MunicipalServicesApp.Data;
using MunicipalServicesApp.Model;

namespace MunicipalServicesApp.Data
{
    public class EventManager
    {
        //Stores events by date for easy sorting
        private SortedDictionary<DateTime, List<Event>> eventsByDate = new SortedDictionary<DateTime, List<Event>>();

        //Quick lookup using eventId
        private Dictionary<int, Event> eventsById = new Dictionary<int, Event>();

        //Keeps trackof unique categories
        private HashSet<string> categories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //Last viewed event (like a stack of whatthe userclicked)
        private Stack<Event> lastViewedStack = new Stack<Event>();

        //Priortiy queue for upcoming events
        private PriorityQueue<Event, DateTime> upcomingQueue = new PriorityQueue<Event, DateTime>();

        //Track what users searched for
        private Dictionary<string, int> searchCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        //Id generator
        private int nextId = 1;

        public Event AddEvent(string name, string description, string category, DateTime date, string location) //Add a new event
        {
            var newEvent = new Event(nextId++, name, description, category, date, location);

            //Add to date dictionary
            var key = date.Date;
            if (!eventsByDate.TryGetValue(key, out var list))
            {
                list = new List<Event>();
                eventsByDate[key] = list;
            }
            list.Add(newEvent);

            //Add to ID Dictionary
            eventsById[newEvent.eventId] = newEvent;
            //Track categories
            categories.Add(newEvent.eventCategory);
            //Add to upcoming queue
            upcomingQueue.Enqueue(newEvent, newEvent.eventDate);

            return newEvent;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public bool RemoveEvent(int id)  //Remove an evennt by ID
        {
            if (!eventsById.TryGetValue(id, out var ev)) return false;
            eventsById.Remove(id);

            //Remove from date dictionary
            var key = ev.eventDate.Date;
            if (eventsByDate.TryGetValue(key, out var list))
            {
                list.RemoveAll(e => e.eventId == id);
                if (list.Count == 0) eventsByDate.Remove(key);
            }

            //Rebuild Upcoming queue
            RebuildUpcomingQueue();


            if (!eventsById.Values.Any(e => string.Equals(e.eventCategory, ev.eventCategory, StringComparison.OrdinalIgnoreCase)))
                categories.Remove(ev.eventCategory);

            return true;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        private void RebuildUpcomingQueue() //Private helper to rebuild the upcoming events queue
        {
            upcomingQueue = new PriorityQueue<Event, DateTime>();
            foreach (var e in eventsById.Values)
            {
                if (e.eventDate >= DateTime.Now) upcomingQueue.Enqueue(e, e.eventDate);
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public IEnumerable<Event> GetAllEvents() //Sort all events by date (oldest first)
        {
            foreach (var kv in eventsByDate)
                foreach (var e in kv.Value.OrderBy(x => x.eventDate))
                    yield return e;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public IEnumerable<Event> Search(string category = null, DateTime? from = null, DateTime? to = null, string nameOrDesc = null) //Search for an event
        {
            var query = eventsById.Values.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(e => string.Equals(e.eventCategory, category, StringComparison.OrdinalIgnoreCase));
                TrackSearch(category);
            }
            if (from.HasValue)
            {
                var f = from.Value.Date;
                query = query.Where(e => e.eventDate.Date >= f);
                TrackSearch(from.Value.ToString("yyyy-MM-dd"));
            }
            if (to.HasValue)
            {
                var t = to.Value.Date;
                query = query.Where(e => e.eventDate.Date <= t);
                TrackSearch(to.Value.ToString("yyyy-MM-dd"));
            }
            if (!string.IsNullOrWhiteSpace(nameOrDesc))
            {
                var q = nameOrDesc.Trim();
                query = query.Where(e => (e.eventName?.IndexOf(q, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0
                                       || (e.eventDescription?.IndexOf(q, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0);
                TrackSearch(nameOrDesc);
            }

            return query.OrderBy(e => e.eventDate).ThenBy(e => e.eventName);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public void MarkViewed(Event ev) //Mark an event as viewed (push onto stack)
        {
            if (ev == null) return;
            lastViewedStack.Push(ev);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public IEnumerable<Event> GetLastViewed(int count = 5) //Get last viewed events to show in recs panel
        {
            return lastViewedStack.Take(count);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public IEnumerable<Event> GetUpcoming(int count = 5) //get upcoming events
        {
            return GetAllEvents().Where(e => e.eventDate >= DateTime.Now).Take(count);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public IEnumerable<string> GetCategories() //sort categories alphabetically
        {
            return categories.OrderBy(c => c);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public Event GetById(int id) //get event by ID
        {
            eventsById.TryGetValue(id, out var ev);
            return ev;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public IEnumerable<Event> Recommend(int count = 5) //Recommend events based on user searches
        {
            // pick top categories by searchCounts
            var topKeys = searchCounts.OrderByDescending(kv => kv.Value)
                                      .Select(kv => kv.Key)
                                      .Where(k => !string.IsNullOrWhiteSpace(k))
                                      .ToList();

            var recommended = new List<Event>();
            var viewedIds = new HashSet<int>(lastViewedStack.Select(e => e.eventId));

            foreach (var key in topKeys)
            {
                
                var byCategory = eventsById.Values.Where(e => string.Equals(e.eventCategory, key, StringComparison.OrdinalIgnoreCase));
                foreach (var e in byCategory.OrderBy(e => e.eventDate))
                {
                    if (recommended.Count >= count) break;
                    if (!viewedIds.Contains(e.eventId)) recommended.Add(e);
                }
                if (recommended.Count >= count) break;

               
                if (DateTime.TryParse(key, out var dt))
                {
                    var byDate = eventsById.Values.Where(e => e.eventDate.Date == dt.Date);
                    foreach (var e in byDate.OrderBy(e => e.eventDate))
                    {
                        if (recommended.Count >= count) break;
                        if (!viewedIds.Contains(e.eventId)) recommended.Add(e);
                    }
                }
                if (recommended.Count >= count) break;
            }

           
            if (recommended.Count < count)
            {
                foreach (var e in GetUpcoming(count * 2))
                {
                    if (recommended.Count >= count) break;
                    if (!viewedIds.Contains(e.eventId) && !recommended.Contains(e))
                        recommended.Add(e);
                }
            }

            return recommended.Take(count);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        public void TrackEventClick(Event ev)
        {
            if (ev == null) return;

            // Mark as viewed
            MarkViewed(ev);

            // Track category
            if (!string.IsNullOrWhiteSpace(ev.eventCategory))
            {
                if (searchCounts.ContainsKey(ev.eventCategory))
                    searchCounts[ev.eventCategory]++;
                else
                    searchCounts[ev.eventCategory] = 1;
            }

            // Track event date
            string dateKey = ev.eventDate.ToString("yyyy-MM-dd");
            searchCounts[dateKey] = searchCounts.GetValueOrDefault(dateKey, 0) + 1;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        private void TrackSearch(string key) //Track searches to improve recommendations
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            if (searchCounts.ContainsKey(key)) searchCounts[key]++;
            else searchCounts[key] = 1;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//



    }
}
