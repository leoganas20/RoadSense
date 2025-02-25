using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace RoadSense.UI.Pages.LandingPage;

public class LandingPageBase : ComponentBase
{
   

    protected List<Service> Services = new()
        {
            new Service
            {
                Title = "Pediatricians",
                Details = "Medical specialists focused on the care and treatment of infants, children, and adolescents.",
                
            },
            new Service
            {
                Title = "Plastic Surgeons",
                Details = "Specialists in reconstructive and aesthetic surgery to improve or restore appearance or function.",
                
            },
            new Service
            {
                Title = "Ophthalmologists",
                Details = "Medical doctors who specialize in the diagnosis, treatment, and surgery of eye-related conditions.",
                
            },
            new Service
            {
                Title = "Orthopedic",
                Details = "Medical specialists in the diagnosis and treatment of musculoskeletal system disorders, including bones, joints, and muscles.",
                
            },
            new Service
            {
                Title = "Gastroenterologist",
                Details = "Specialists focused on diagnosing and treating conditions of the digestive system, including the stomach, intestines, and liver.",
                
            },
            new Service
            {
                Title = "Cardiology",
                Details = "Medical field specializing in diagnosing and treating heart conditions, including coronary artery disease, heart failure, and arrhythmias.",
               
            },
        };


    public class Service
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public string ImageSrc { get; set; }
    }
}
