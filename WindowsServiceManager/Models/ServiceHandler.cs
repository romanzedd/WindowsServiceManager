using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace WindowsServiceManager.Models
{
    public class ServiceHandler
    {
        private static readonly ServiceHandler instance = new ServiceHandler();

        public List<ServiceController> serviceControllers { get; private set; }
        public List<Service> services { get; private set; }

        private ServiceHandler()
        {
            this.serviceControllers = ServiceController.GetServices().ToList();
            this.services = new List<Service>();
            foreach (var serviceController in this.serviceControllers)
            {
                services.Add(new Service(serviceController.ServiceName,
                                         serviceController.DisplayName, 
                                         serviceController.Status.ToString(), 
                                         serviceController.MachineName));
            }
        }

        public static ServiceHandler GetInstance()
        {
            return instance;
        }

        public void UpdateServiceList()
        {
            serviceControllers = ServiceController.GetServices().ToList();
        }
        public async Task UpdateServiceListAsync()
        {
            await Task.Run(() => serviceControllers = ServiceController.GetServices().ToList());
        }

        //TODO: start and stop services _service LINQ to be populated with more search criteria
        public static async Task<ServiceControllerStatus> StartService(Service? service)
        {
            var handler = ServiceHandler.GetInstance();
            var _service = handler.serviceControllers.Where(x => x.DisplayName == service.displayName).FirstOrDefault();
            await Task.Delay(5000);
            await Task.Run(() => _service.Start());
            return _service.Status;
        }
        public static async Task<ServiceControllerStatus> StopService(Service? service)
        {
            var handler = ServiceHandler.GetInstance();
            var _service = handler.serviceControllers.Where(x => x.DisplayName == service.displayName).FirstOrDefault();
            await Task.Run(() => _service.Stop());
            return _service.Status;
        }
    }
}
