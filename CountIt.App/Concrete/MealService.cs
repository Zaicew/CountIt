using CountIt.App.Common;
using CountIt.Domain.Entity;
using CountIt.App.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountIt.App.Concrete
{
    public class MealService : BaseService<Meal>, IMealService<Meal>
    {       
    }
}
