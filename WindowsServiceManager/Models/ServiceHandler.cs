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
        public ServiceControllerStatus StopService(ServiceController service)
        {
            service.Stop();
            return service.Status;
        }
        public ServiceControllerStatus StartService(ServiceController service)
        {
            service.Start();
            return service.Status;
        }
    }
}
