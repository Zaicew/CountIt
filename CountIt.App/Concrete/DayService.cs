using CountIt.App.Common;
using CountIt.App.Managers;
using CountIt.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Schema;

namespace CountIt.App.Concrete
{
    public class DayService : BaseService<Day>
    {
        public List<MealInDay> MealsListInDay { get; set; }
        public DayService() : base()
        {
            MealsListInDay = new List<MealInDay>();
        }
    }
}
