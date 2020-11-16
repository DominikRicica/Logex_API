using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public interface ILocationService
    {
        double GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude);
    }
}
