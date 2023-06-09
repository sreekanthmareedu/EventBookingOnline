using BusinessEvents.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.WebApplication.ViewComponents
{
    public class EventListingViewComponent : ViewComponent
    {

        private readonly IUnitofWork _unitofWork;
        public string lblDateDiff;

        public EventListingViewComponent(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            DateTime dt = new DateTime(2018, 08, 10); // your date from the db.
            TimeSpan dt2 = dt.Subtract(DateTime.Now);
            // add the time remaining to a label.
           lblDateDiff = "Time's up in "
                + dt2.Days.ToString() + " days, "
                + dt2.Hours.ToString() + " hours, "
                + dt2.Minutes.ToString() + " minutes "
                + dt2.Seconds.ToString() + " seconds.";
            return View();
        }

    }
}
