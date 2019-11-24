using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using news.Models;

namespace news.counter
{
    public class hitcounter
    {
        public void AddCount(hitcounter hc)
        {
            using (NewsWebAppEntities3 dc = new NewsWebAppEntities3())
            {
                DateTime today = DateTime.Now.Date;
                var v = dc.counter.Where( a => a.ipaddress.Equals()
            }
        }
    }
}