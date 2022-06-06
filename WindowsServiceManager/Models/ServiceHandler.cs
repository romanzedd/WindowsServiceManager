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
            ServiceConverter(services, serviceControllers);
        }

        public static ServiceHandler GetInstance()
        {
            return instance;
        }

        private static void ServiceConverter(List<Service> services, List<ServiceController> serviceControllers)
        {
            foreach (var serviceController in serviceControllers)
            {
                services.Add(new Service(serviceController.ServiceName,
                                         serviceController.DisplayName,
                                         serviceController.Status.ToString(),
                                         serviceController.MachineName));
            }
        }

        public void UpdateServiceList()
        {
            serviceControllers = ServiceController.GetServices().ToList();
            services.RemoveAll(x => true);
            ServiceConverter(services, serviceControllers);
        }
        public async Task UpdateServiceListAsync()
        {
            await Task.Run(() => serviceControllers = ServiceController.GetServices().ToList());
            services.RemoveAll(x => true);
            await Task.Run(() => ServiceConverter(services, serviceControllers));
        }

        private static ServiceController GetServiceController(Service? service)
        {
            var handler = ServiceHandler.GetInstance();
            var _service = handler.serviceControllers.Where(x => x.DisplayName == service?.displayName &&
                                                                 x.ServiceName == service?.name &&
                                                                 x.MachineName == service?.account).FirstOrDefault();
            return _service;
        }

        public static async Task<string> StartService(Service? _service)
        {
            var service = GetServiceController(_service);
            if (service is null) return "Error: Service not found";
            try
            {
                await Task.Run(() => service.Start());
                await Task.Run(() => service.WaitForStatus(ServiceControllerStatus.Running));
            }
            catch (InvalidOperationException ex)
            {
                return ex.Message;
            }

            await ServiceHandler.GetInstance().UpdateServiceListAsync();
            return String.Concat("OK\n", service.Status.ToString());
        }
        public static async Task<string> StopService(Service? _service)
        {
            var service = GetServiceController(_service);
            if (service is null) return "Error: Service not found";
            try
            {
                await Task.Run(() => service.Stop());
                await Task.Run(() => service.WaitForStatus(ServiceControllerStatus.Stopped));
            }
            catch (InvalidOperationException ex)
            {
                return ex.Message;
            }

            await ServiceHandler.GetInstance().UpdateServiceListAsync();
            return String.Concat("OK\n", service.Status.ToString());
        }
    }
}
