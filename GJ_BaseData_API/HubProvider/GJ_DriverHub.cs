using GJ_BaseData_API.Filter;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace GJ_BaseData_API.HubProvider
{
    [IPAuthorHub]
    [HubName("GJ_DriverHub")]
    public class GJ_DriverHub : Hub
    {
    }
}