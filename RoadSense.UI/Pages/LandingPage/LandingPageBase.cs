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
                ImageSrc = "/images/Live_monitoring.png"

            },
            new Service
            {
                Title = "Violation Tracking",
                Details = "Records of detected violations, including speed, honking, date, time, and location.",
                ImageSrc = "/images/Violation_track.png"

            },
            new Service
            {
                Title = "Reports & Analytics",
                Details = "Daily, weekly, and monthly violation reports. Graphs and statistics for trend analysis.",
                ImageSrc = "/images/reportv.png"

            },
        };


    public class Service
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public string ImageSrc { get; set; }
    }
}
