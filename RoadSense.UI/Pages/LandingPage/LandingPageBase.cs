using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace RoadSense.UI.Pages.LandingPage;

public class LandingPageBase : ComponentBase
{
   

    protected List<Service> Services = new()
        {
            new Service
            {
                Title = "Real-Time Monitoring",
                Details = "Dashboard displaying live data on overspeeding and honking violations.",
                
            },
            new Service
            {
                Title = "Violation Tracking",
                Details = "Records of detected violations, including speed, honking, date, time, and location.",
                
            },
            new Service
            {
                Title = "Reports & Analytics",
                Details = "Daily, weekly, and monthly violation reports. Graphs and statistics for trend analysis.",
                
            },
        };


    public class Service
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public string ImageSrc { get; set; }
    }
}
