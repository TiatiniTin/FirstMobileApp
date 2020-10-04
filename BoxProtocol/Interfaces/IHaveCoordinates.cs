using Nest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoxProtocol.Models;
using Xamarin.Essentials;
using MagicOnion;

namespace BoxProtocol.Interfaces
{
    public interface IHaveCoordinates
    {
        Location Place_location { get; set; }
    }
}
